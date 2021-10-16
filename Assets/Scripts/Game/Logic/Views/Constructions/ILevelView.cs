﻿using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Assets.Scripts.Game.Logic.Views
{
    public interface ILevelView : IView
    {
        IHandView CreateHand();
        IPlacementView CreatePlacement();
        ICurrentOrderView CreateCurrentOrder();
        void SetTimeMover(Action<float> moveTime);
    }
}
