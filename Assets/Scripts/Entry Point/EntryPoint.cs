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
    [SerializeField] private FortuneWheelView _fortuneWheelView;

    void Start()
    {
        PlayerData playerData = new();
        playerData.Init(_playerDataConfig);

        _gameplayScreen.Init(playerData);

        FortuneWheelModel model = new();
        FortuneWheelController fortuneWheelController = new(model, playerData, _wheelConfig);
        _fortuneWheelView.Init(fortuneWheelController, _rewardPopup);
    }
}
