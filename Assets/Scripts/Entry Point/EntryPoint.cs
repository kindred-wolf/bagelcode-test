using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerDataConfig _playerDataConfig;

    [Space]
    [SerializeField] private CurrencyPanel _currencyPanel;

    void Start()
    {
        PlayerData playerData = new();
        playerData.Init(_playerDataConfig);

        _currencyPanel.Init(playerData);
    }
}
