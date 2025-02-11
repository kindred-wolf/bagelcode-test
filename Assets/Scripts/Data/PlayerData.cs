using Game.Scripts.Utils;
using UnityEngine;

public class PlayerData
{
    private const string COINS_KEY = "coins_amount";
    public ReactiveProperty<int> CoinsAmount;

    public void Init(PlayerDataConfig config)
    {
        // Possible to change Prefs for web request
        int savedCoins = PlayerPrefs.GetInt(COINS_KEY, config.DefaultCoins);
        CoinsAmount = new(savedCoins);

        CoinsAmount.ValueChanged += SaveCurrencyValue;
    }

    private void SaveCurrencyValue(int value)
    {
        if(value < 0)
        {
            value = 0;
        }

        PlayerPrefs.SetInt(COINS_KEY, value);
    }
}
