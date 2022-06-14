﻿using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Units
{
    public interface IUnitModelView : IViewWithPresenter, IViewWithDefaultPresenter
    {
        IAnimator Animator { get; }
        IUnitDresser UnitDresser { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new UnitModelPresenter(this);
        }

    }
}