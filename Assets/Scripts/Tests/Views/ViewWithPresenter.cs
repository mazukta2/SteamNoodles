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
    public abstract class ViewWithPresenter<TPresenter> : View,
        IViewWithGenericPresenter<TPresenter>
        where TPresenter : IPresenter
    {
        private List<IPresenter> _presenters = new List<IPresenter>();

        protected ViewWithPresenter(IViewsCollection level) : base(level)
        {
        }

        public void DefaultInit()
        {
            if (this is IViewWithDefaultPresenter defaultInit)
                defaultInit.InitDefaultPresenter();
        }

        public void AddPresenter(IPresenter presenter)
        {
            _presenters.Add(presenter);
        }
    }
}
