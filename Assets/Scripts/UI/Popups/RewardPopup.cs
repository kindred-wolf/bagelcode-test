using Game.Scripts.Utils.Extensions;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardPopup : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private Button _claimButton;
    private Action _onCloseAction;
    private string _rewardTextTemplate;

    public void Init(int reward, Action onCloseCallback)
    {
        if (string.IsNullOrEmpty(_rewardTextTemplate))
        {
            _rewardTextTemplate = _rewardText.text;
        }

        _rewardText.text = string.Format(_rewardTextTemplate, reward.ToShortNumber());
        _onCloseAction += onCloseCallback;

        gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        _claimButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _onCloseAction = null;
        _claimButton.onClick.RemoveListener(Close);
    }

    private void Close()
    {
        _onCloseAction?.Invoke();
        gameObject.SetActive(false);
    }
}
