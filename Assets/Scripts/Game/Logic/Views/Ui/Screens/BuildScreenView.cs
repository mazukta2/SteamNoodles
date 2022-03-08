using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class BuildScreenView : ScreenView
    {
        public ButtonView CloseButton;

        private BuildScreenPresenter _presenter;
        public override void SetManager(ScreenManagerPresenter manager)
        {
            _presenter = new BuildScreenPresenter(this, manager);
        }

        public void OnValidate()
        {
            if (CloseButton == null) throw new ArgumentNullException(nameof(CloseButton));
        }
    }
}
