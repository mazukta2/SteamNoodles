using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views
{
    public abstract class PresenterView<TPresenter> : View,
        IViewWithGenericPresenter<TPresenter>
        where TPresenter : IPresenter
    {
        public TPresenter Presenter { get; set; }
        IPresenter IViewWithPresenter.Presenter { get => Presenter; set => Presenter = (TPresenter)value; }
        protected PresenterView(LevelView level) : base(level)
        {
        }
    }
}
