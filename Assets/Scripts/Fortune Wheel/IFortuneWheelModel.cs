using System.Collections.Generic;

public interface IFortuneWheelModel
{
    public int GetRandomRewardIndex(int rewardsCount);

    public List<int> GenerateRewards(FortuneWheelConfig config);
}
