using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BaseGameScreenPresenter
    {
        private BuildScreenView _view;
        private ScreenManagerPresenter _screenManager;

        public BuildScreenPresenter(BuildScreenView view, ScreenManagerPresenter screenManager) : base(screenManager, view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _view.CancelButton.SetAction(CancelClick);
        }

        protected override void DisposeInner()
        {
        }

        private void CancelClick()
        {
            _screenManager.GetScreen<MainScreenView>().Open();
        }
    }
}
