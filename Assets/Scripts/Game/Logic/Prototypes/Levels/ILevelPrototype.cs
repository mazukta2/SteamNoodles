using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Game.Logic.Contexts;
using System;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface ILevelPrototype
    {
        Point Size { get; }
        IBuildingPrototype[] StartingHand { get; }

        void Load(Action<ILevelPrototype, ILevelContext> onFinished);
    }
}
