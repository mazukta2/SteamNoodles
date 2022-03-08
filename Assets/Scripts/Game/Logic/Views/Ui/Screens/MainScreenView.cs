using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class MainScreenView : ScreenView
    {
        public ButtonView BuildButton;

        private MainScreenPresenter _presenter;
        public override void SetManager(ScreenManagerPresenter manager)
        {
            _presenter = new MainScreenPresenter(this, manager);
        }

        public void OnValidate()
        {
            if (BuildButton == null) throw new ArgumentNullException(nameof(BuildButton));
        }
    }
}
