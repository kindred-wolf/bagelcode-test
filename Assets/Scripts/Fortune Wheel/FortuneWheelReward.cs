using Game.Scripts.Utils.Extensions;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FortuneWheelReward : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _rewardText;

    public void UpdateRewardText(int text)
    {
        _rewardText.text = text.ToString();
    }
}
