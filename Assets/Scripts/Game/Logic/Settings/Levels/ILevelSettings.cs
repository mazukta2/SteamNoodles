using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Prototypes.Levels
{
    public interface ILevelSettings : IPlacementSettings, IUnitsSettings, IClashesSettings
    {
        IConstructionSettings[] StartingHand { get; }
    }

    public interface IQueueSettings
    {
        int QueueSize { get; }
    }

    public interface IPlacementSettings
    {
        Point Size { get; }
    }

    public interface IUnitsSettings
    {
        Rect UnitsSpawnRect { get; }
    }

    public interface IClashesSettings
    {
        float ClashTime { get; }
    }
}
