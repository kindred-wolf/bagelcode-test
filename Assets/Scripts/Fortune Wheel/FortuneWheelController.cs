using Game.Scripts.Utils;
using Game.Scripts.Utils.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private List<int> _rewardsList = new();
    private List<FortuneWheelReward> _rewardsObjects = new();

    public void Init(PlayerData playerData, FortuneWheelConfig config)
    {
        _playerData = playerData;
        _wheelConfig = config;

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
        int rewardIndex = _random.Next(0, _rewardsList.Count);
        int reward = _rewardsList[rewardIndex];
        Action callback = () =>
        {
            GiveRewards(reward);
            //RestartWheel();
        };

        StartCoroutine(SpinAnimation(rewardIndex, callback));
    }

    private IEnumerator SpinAnimation(int rewardIndex, Action onComplete)
    {
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
        HashSet<int> rewards = new();
        int rangeSize = (_wheelConfig.BiggestValue - _wheelConfig.LowestValue) / _wheelConfig.StepValue + 1;

        if (_wheelConfig.RewardsCount > rangeSize)
        {
            throw new ArgumentException("Not enough unique values to generate the required rewards.");
        }

        while (rewards.Count < _wheelConfig.RewardsCount)
        {
            int randomValue = _wheelConfig.LowestValue + _random.Next(rangeSize) * _wheelConfig.StepValue;
            rewards.Add(randomValue);
        }

        return new List<int>(rewards);
    }

    private void GiveRewards(int reward)
    {
        _playerData.CoinsAmount.Value += reward;
    }

    private void RestartWheel()
    {
        _rewardsObjects.ForEach(reward => Destroy(reward.gameObject));
        _rewardsObjects.Clear();

        _wheelTransform.rotation = Quaternion.identity;

        SetupWheel();
    }
}
