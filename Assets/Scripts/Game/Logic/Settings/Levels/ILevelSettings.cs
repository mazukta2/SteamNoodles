using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Prototypes.Levels
{
    public interface ILevelSettings : IOrdersPrototype, IPlacementPrototype, IUnitsPrototype
    {
        IConstructionSettings[] StartingHand { get; }
    }

    public interface IOrdersPrototype
    {
        IOrderSettings[] Orders { get; }
    }

    public interface IQueuePrototype
    {
        int QueueSize { get; }
    }

    public interface IPlacementPrototype
    {
        Point Size { get; }
    }

    public interface IUnitsPrototype
    {
        Rect UnitsSpawnRect { get; }
    }
}
