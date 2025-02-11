using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheelController : MonoBehaviour
{
    [SerializeField] private Transform _wheelTransform;
    [SerializeField] private Transform _rewardsContent;
    [SerializeField] private Button _spinButton;

    [Space]
    [SerializeField] private FortuneWheelReward _rewardPrefab;

    private System.Random _random = new();
    private PlayerData _playerData;
    private FortuneWheelConfig _wheelConfig;
    private RewardPopup _rewardPopup;

    private List<int> _rewardsList = new();
    private List<FortuneWheelReward> _rewardsObjects = new();

    public void Init(PlayerData playerData, FortuneWheelConfig config, RewardPopup rewardPopup)
    {
        _playerData = playerData;
        _wheelConfig = config;
        _rewardPopup = rewardPopup;

        SetupWheel();
    }

    private void OnEnable()
    {
        _spinButton.onClick.AddListener(Spin);
    }

    private void OnDisable()
    {
        _spinButton.onClick.RemoveListener(Spin);
    }

    private void SetupWheel()
    {
        _rewardsList = GenerateRewards();
        SetupUI();
    }

    private void SetupUI()
    {
        int rewardsCount = _rewardsList.Count;

        float rotationPerItem = 360f / rewardsCount;

        for (int i = 0; i < rewardsCount; i++)
        {
            FortuneWheelReward reward = Instantiate(_rewardPrefab, _rewardsContent);
            float offset = rotationPerItem / 2f;
            float newRotation = rotationPerItem * i + offset;

            reward.transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
            reward.UpdateRewardText(_rewardsList[i]);

            _rewardsObjects.Add(reward);
        }
    }

    private void Spin()
    {
        // Here we can request spin from backend and save

        int rewardIndex = _random.Next(0, _rewardsList.Count);
        int reward = _rewardsList[rewardIndex];
        Action callback = () =>
        {
            GiveRewards(reward);
            _rewardPopup.Init(reward, RestartWheel);
        };

        StartCoroutine(SpinAnimation(rewardIndex, callback));
    }

    private IEnumerator SpinAnimation(int rewardIndex, Action onComplete)
    {
        _spinButton.interactable = false;

        AudioController.Play(AudioType.WheelSpin);

        int totalSegments = _rewardsList.Count;
        float elapsedTime = 0f;
        float totalRotation = 360f * _wheelConfig.SpinsAmount;
        float startRotation = _wheelTransform.eulerAngles.z;
        float segmentAngle = 360f / totalSegments;
        float targetRotation = totalRotation + rewardIndex * segmentAngle;

        while (elapsedTime < _wheelConfig.AnimationTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _wheelConfig.AnimationTime;
            float curveValue = _wheelConfig.SpinningCurve.Evaluate(t);
            float newRotation = Mathf.Lerp(0, targetRotation, curveValue);
            _wheelTransform.eulerAngles = new Vector3(0, 0, startRotation - newRotation);
            yield return null;
        }

        onComplete?.Invoke();
    }

    public List<int> GenerateRewards()
    {
        // We can change that to get list of possible rewards from backend, so it can't be cheated

        HashSet<int> rewards = new HashSet<int>();
        List<int> possibleValues = new List<int>();

        for (int value = _wheelConfig.LowestValue; value <= _wheelConfig.BiggestValue; value += 100)
        {
            possibleValues.Add(value);
        }

        while (rewards.Count < _wheelConfig.RewardsCount && possibleValues.Count > 0)
        {
            int index = _random.Next(possibleValues.Count);
            int selectedValue = possibleValues[index];

            if (rewards.Count == 0 || IsValidReward(rewards, selectedValue))
            {
                rewards.Add(selectedValue);
                possibleValues.RemoveAt(index);
            }
        }

        return new List<int>(rewards);
    }

    private bool IsValidReward(HashSet<int> rewards, int value)
    {
        foreach (int reward in rewards)
        {
            if (Math.Abs(reward - value) < _wheelConfig.StepValue)
            {
                return false;
            }
        }
        return true;
    }

    private void GiveRewards(int reward)
    {
        _playerData.CoinsAmount.Value += reward;
        AudioController.Play(AudioType.Win);
    }

    private void RestartWheel()
    {
        _rewardsObjects.ForEach(reward => Destroy(reward.gameObject));
        _rewardsObjects.Clear();

        _wheelTransform.rotation = Quaternion.identity;

        SetupWheel();
        _spinButton.interactable = true;
    }
}
