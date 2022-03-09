using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class MainScreenView : ScreenView<MainScreenViewPresenter>
    {
        private MainScreenViewPresenter _viewPresenter;
        public override MainScreenViewPresenter GetViewPresenter() => _viewPresenter;
        protected override void CreatedInner()
        {
            _viewPresenter = new MainScreenViewPresenter(Level);
        }

        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }
    }

    public class MainScreenViewPresenter : ScreenViewPresenter
    {
        private MainScreenPresenter _presenter;
        public MainScreenViewPresenter(ILevel level) : base(level)
        {
        }

        public override void SetManager(ScreenManagerPresenter manager)
        {
            _presenter = new MainScreenPresenter(this, manager);
        }
    }
}
