﻿using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface ICellView : IViewWithPresenter
    {
        ILevelPosition LocalPosition { get; }
        ISwitcher<CellPlacementStatus> State { get; }
        IAnimator Animator { get; }
    }
}