using Game.Assets.Scripts.Game.Logic.Settings.Rewards;

namespace Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features
{
    public interface IGiveRewardOnBuildConstructionFeatureSettings : IConstructionFeatureSettings
    {
        IReward Reward { get; }
    }

}
