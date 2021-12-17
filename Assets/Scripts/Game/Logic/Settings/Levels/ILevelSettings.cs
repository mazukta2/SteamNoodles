using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Settings.Rewards;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Prototypes.Levels
{
    public interface ILevelSettings : IPlacementSettings, IUnitsSettings, IClashesSettings, IHandSettings
    {
    }

    public interface IHandSettings
    {
        IConstructionSettings[] StartingHand { get; }
        int HandSize { get; }
    }

    public interface IPlacementSettings
    {
        Point Size { get; }
    }

    public interface IUnitsSettings
    {
        Rect UnitsSpawnRect { get; }
        Dictionary<ICustomerSettings, int> Deck { get; }
    }

    public interface IClashesSettings
    {
        float ClashTime { get; }
        IReward ClashReward { get; }
        int MinQueue { get; }
        int MaxQueue { get; }
    }
}
