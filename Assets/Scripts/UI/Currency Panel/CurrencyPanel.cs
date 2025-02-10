using Game.Scripts.Utils.Extensions;
using TMPro;
using UnityEngine;

public class CurrencyPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currencyText;

    public void Init(PlayerData playerData)
    {
        playerData.CoinsAmount.ValueChanged += UpdateUI;
        UpdateUI(playerData.CoinsAmount);
    }

    private void UpdateUI(int value)
    {
        _currencyText.text = value.ToShortNumber();
    }
}
