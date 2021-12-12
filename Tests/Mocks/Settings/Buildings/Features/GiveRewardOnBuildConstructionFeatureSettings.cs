using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using Game.Assets.Scripts.Game.Logic.Settings.Rewards;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Tests.Mocks.Settings.Buildings.Features
{
    public class GiveRewardOnBuildConstructionFeatureSettings : IGiveRewardOnBuildConstructionFeatureSettings
    {
        public IReward Reward { get; set; }
        public GiveRewardOnBuildConstructionFeatureSettings(IReward reward)
        {
            Reward = reward;
        }
    }
}
