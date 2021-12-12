using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Settings.Rewards;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Game.Tests.Mocks.Settings.Rewards
{
    public class Reward : IReward
    {
        public IReadOnlyDictionary<IConstructionSettings, int> ToHand => ToHandSource.AsReadOnly();
        public Dictionary<IConstructionSettings, int> ToHandSource { get; set; }
        public int MinToHand { get; set; } = 1;
        public int MaxToHand { get; set; } = 1;
    }
}
