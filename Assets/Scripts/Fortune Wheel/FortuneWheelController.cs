using System.Collections.Generic;

public class FortuneWheelController
{
    private FortuneWheelModel _model;
    private PlayerData _playerData;

    private FortuneWheelConfig _wheelConfig;
    public FortuneWheelConfig Config => _wheelConfig;

    public FortuneWheelController(FortuneWheelModel model, PlayerData playerData, FortuneWheelConfig config)
    {
        _model = model;
        _playerData = playerData;
        _wheelConfig = config;
    }

    public int GetRandomRewardIndex(int rewardsCount)
    {
        return _model.GetRandomRewardIndex(rewardsCount);
    }

    public List<int> GenerateRewards()
    {
        return _model.GenerateRewards(_wheelConfig);
    }

    public void GiveRewards(int reward)
    {
        _playerData.CoinsAmount.Value += reward;
        AudioController.Play(AudioType.Win);
    }
}
