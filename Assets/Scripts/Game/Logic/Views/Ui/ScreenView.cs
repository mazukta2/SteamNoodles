using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui
{
    public abstract class ScreenView<T> : View<T> where T : ScreenViewPresenter
    {
    }

    public abstract class ScreenViewPresenter : ViewPresenter
    {
        private ScreenManagerPresenter _manager;

        protected ScreenViewPresenter(ILevel level) : base(level)
        {
        }

        public virtual void SetManager(ScreenManagerPresenter manager)
        {
            _manager = manager;
        }

        public ScreenManagerPresenter GetManager() => _manager;
    }
}
