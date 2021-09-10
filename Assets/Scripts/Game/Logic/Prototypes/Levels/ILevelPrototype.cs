using Assets.Scripts.Game.Logic.Common.Math;
using System;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface ILevelPrototype
    {
        Point Size { get; }
        IBuildingPrototype[] StartingHand { get; }

        void Load(Action<ILevelPrototype> onFinished);
    }
}
