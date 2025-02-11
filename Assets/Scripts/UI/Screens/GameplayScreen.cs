using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayScreen : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private CurrencyPanel _currencyPanel;

    public void Init(PlayerData playerData)
    {
        _currencyPanel.Init(playerData);
        _backButton.onClick.AddListener(LoadMenu);
    }

    private void LoadMenu()
    {
        SceneController.LoadScene(Scenes.Menu);
    }
}
