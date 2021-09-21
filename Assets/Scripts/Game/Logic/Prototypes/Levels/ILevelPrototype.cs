﻿using Assets.Scripts.Game.Logic.Common.Math;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface ILevelPrototype
    {
        Point Size { get; }
        IConstructionPrototype[] StartingHand { get; }
        IOrderPrototype[] Orders { get; }

        void Load(Action<ILevelPrototype, ILevelView> onFinished);
    }
}
