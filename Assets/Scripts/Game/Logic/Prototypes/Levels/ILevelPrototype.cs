using Assets.Scripts.Game.Logic.Common.Math;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface ILevelPrototype
    {
        Point Size { get; }
        IBuildingPrototype[] StartingHand { get; }

        void Load(Action<ILevelPrototype, ILevelView> onFinished);
    }
}
