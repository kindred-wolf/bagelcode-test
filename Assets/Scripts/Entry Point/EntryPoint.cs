using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerDataConfig _playerDataConfig;
    [SerializeField] private FortuneWheelConfig _wheelConfig;

    [Space]
    [SerializeField] private RewardPopup _rewardPopup;
    [SerializeField] private GameplayScreen _gameplayScreen;
    [SerializeField] private FortuneWheelController _fortuneWheelController;

    void Start()
    {
        PlayerData playerData = new();
        playerData.Init(_playerDataConfig);

        _gameplayScreen.Init(playerData);
        _fortuneWheelController.Init(playerData, _wheelConfig, _rewardPopup);

    }
}
