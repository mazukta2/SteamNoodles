using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Environment.Views
{
    public abstract class PresenterView<TPresenter> : View,
        IViewWith<TPresenter>, IInitPresenter
        where TPresenter : IPresenter
    {
        public TPresenter Presenter { get; private set; }
        protected PresenterView(LevelView level) : base(level)
        {
        }

        void IInitPresenter.SetPresenter(IPresenter presenter)
        {
            Presenter = (TPresenter)presenter;
        }
    }
}
