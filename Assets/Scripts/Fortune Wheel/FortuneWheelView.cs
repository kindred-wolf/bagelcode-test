using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FortuneWheelView : MonoBehaviour
{
    [SerializeField] private Button _spinButton;
    [SerializeField] private Transform _wheelTransform;
    [SerializeField] private Transform _rewardsContent;
    
    [Space]
    [SerializeField] private FortuneWheelReward _rewardPrefab;

    private FortuneWheelController _controller;
    private RewardPopup _rewardPopup;

    private List<FortuneWheelReward> _rewardsObjects = new();
    private List<int> _rewardsList = new();

    public void Init(FortuneWheelController controller, RewardPopup rewardPopup)
    {
        _controller = controller;
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

    private void Spin()
    {
        // Here we can request spin from backend and save
        int rewardIndex = _controller.GetRandomRewardIndex(_rewardsList.Count);
        int reward = _rewardsList[rewardIndex];
        Action callback = () =>
        {
            _controller.GiveRewards(reward);
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
        float totalRotation = 360f * _controller.Config.SpinsAmount;
        float startRotation = _wheelTransform.eulerAngles.z;
        float segmentAngle = 360f / totalSegments;
        float targetRotation = totalRotation + rewardIndex * segmentAngle;

        while (elapsedTime < _controller.Config.AnimationTime)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / _controller.Config.AnimationTime;
            float curveValue = _controller.Config.SpinningCurve.Evaluate(t);
            float newRotation = Mathf.Lerp(0, targetRotation, curveValue);
            _wheelTransform.eulerAngles = new Vector3(0, 0, startRotation - newRotation);
            yield return null;
        }

        onComplete?.Invoke();
    }

    private void SetupWheel()
    {
        _rewardsList = _controller.GenerateRewards();
        SetupUI(_rewardsList);
    }

    private void SetupUI(List<int> rewardsList)
    {
        int rewardsCount = rewardsList.Count;

        float rotationPerItem = 360f / rewardsCount;

        for (int i = 0; i < rewardsCount; i++)
        {
            FortuneWheelReward reward = Instantiate(_rewardPrefab, _rewardsContent);
            float offset = rotationPerItem / 2f;
            float newRotation = rotationPerItem * i + offset;

            reward.transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
            reward.UpdateRewardText(rewardsList[i]);

            _rewardsObjects.Add(reward);
        }
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
