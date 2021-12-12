using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Settings.Rewards
{
    public interface IReward
    {
        int MinToHand { get; }
        int MaxToHand { get; }
        IReadOnlyDictionary<IConstructionSettings, int> ToHand { get; } 

    }
}
