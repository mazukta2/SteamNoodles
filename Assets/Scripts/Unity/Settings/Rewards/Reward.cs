using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Settings.Rewards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUnity.Assets.Scripts.Unity.Settings.Rewards
{
    public class Reward : IReward
    {
        public int MinToHand { get; set; }
        public int MaxToHand { get; set; }
        public IReadOnlyDictionary<IConstructionSettings, int> ToHand { get; set; }
    }
}
