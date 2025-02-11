using System;
using System.Collections.Generic;

public class FortuneWheelModel : IFortuneWheelModel
{
    //
    // Model can be easily changed (for example, we can change all calculations for an API requests)
    //

    private Random _random = new();

    public int GetRandomRewardIndex(int rewardsCount)
    {
        return _random.Next(0, rewardsCount);
    }

    public List<int> GenerateRewards(FortuneWheelConfig config)
    {
        HashSet<int> rewards = new();
        List<int> possibleValues = new();

        for (int value = config.LowestValue; value <= config.BiggestValue; value += 100)
        {
            possibleValues.Add(value);
        }

        while (rewards.Count < config.RewardsCount && possibleValues.Count > 0)
        {
            int index = _random.Next(possibleValues.Count);
            int selectedValue = possibleValues[index];

            if (rewards.Count == 0 || IsValidReward(rewards, selectedValue, config.StepValue))
            {
                rewards.Add(selectedValue);
                possibleValues.RemoveAt(index);
            }
        }

        return new List<int>(rewards);
    }

    private bool IsValidReward(HashSet<int> rewards, int value, int stepValue)
    {
        foreach (int reward in rewards)
        {
            if (Math.Abs(reward - value) < stepValue)
            {
                return false;
            }
        }
        return true;
    }
}
