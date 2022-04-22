﻿using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public interface IHandConstructionView : IPresenterView
    {
        IButton Button { get; }
        IImage Image { get; }
        IViewContainer TooltipContainer { get; }
        IViewPrefab TooltipPrefab { get; }
    }
}