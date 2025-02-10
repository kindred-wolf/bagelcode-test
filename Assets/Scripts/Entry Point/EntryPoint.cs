using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerDataConfig _playerDataConfig;
    [SerializeField] private FortuneWheelConfig _wheelConfig;

    [Space]
    [SerializeField] private CurrencyPanel _currencyPanel;
    [SerializeField] private RewardPopup _rewardPopup;
    [SerializeField] private FortuneWheelController _fortuneWheelController;

    void Start()
    {
        PlayerData playerData = new();
        playerData.Init(_playerDataConfig);

        _currencyPanel.Init(playerData);
        _fortuneWheelController.Init(playerData, _wheelConfig, _rewardPopup);
    }
}
