using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Prototypes.Levels
{
    public interface ILevelPrototype : IOrdersPrototype, IPlacementPrototype
    {
        IConstructionPrototype[] StartingHand { get; }

        void Load(Action<ILevelPrototype, ILevelView> onFinished);
    }

    public interface IOrdersPrototype
    {
        IOrderPrototype[] Orders { get; }
    }

    public interface IPlacementPrototype
    {
        Point Size { get; }
    }
}
