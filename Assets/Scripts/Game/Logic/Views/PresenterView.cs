using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views
{
    public abstract class PresenterView<TPresenter> : View, 
        IViewWith<TPresenter>, IInitPresenter 
        where TPresenter : IPresenter
    {
        public TPresenter Presenter { get; private set; }
        protected PresenterView(ILevel level) : base(level)
        {
        }

        void IInitPresenter.SetPresenter(IPresenter presenter)
        {
            Presenter = (TPresenter)presenter;
        }
    }
}
