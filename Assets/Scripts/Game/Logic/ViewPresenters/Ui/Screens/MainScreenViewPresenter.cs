using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using System;

namespace Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Screens
{
    public class MainScreenViewPresenter : ScreenViewPresenter
    {
        private MainScreenPresenter _presenter;
        public MainScreenViewPresenter(ILevel level) : base(level)
        {
        }

        public override void SetManager(ScreenManagerPresenter manager)
        {
            base.SetManager(manager);
            _presenter = new MainScreenPresenter(this, manager);
        }
    }
}
