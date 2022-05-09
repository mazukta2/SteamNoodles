﻿using Game.Assets.Scripts.Game.Environment.Creation;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public interface IHandView : IViewWithPresenter
    {
        IViewContainer Cards { get;  }
        IViewPrefab CardPrototype { get; }
    }
}
