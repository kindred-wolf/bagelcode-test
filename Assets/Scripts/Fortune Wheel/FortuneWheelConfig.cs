using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fortune Wheel Config", menuName = "Configs/Fortune Wheel Config")]
public class FortuneWheelConfig : ScriptableObject
{
    public int LowestValue;
    public int BiggestValue;
    public int StepValue;
    public int RewardsCount;
    public int SpinsAmount;
    public float AnimationTime;
    public AnimationCurve SpinningCurve;
}
