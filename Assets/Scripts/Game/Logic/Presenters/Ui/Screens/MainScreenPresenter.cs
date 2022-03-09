using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class MainScreenPresenter : BaseGameScreenPresenter
    {
        private MainScreenViewPresenter _view;
        private ScreenManagerPresenter _screenManager;

        public MainScreenPresenter(MainScreenViewPresenter view, ScreenManagerPresenter screenManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));

            //_view.BuildButton.SetAction(BuildClick);
        }

        protected override void DisposeInner()
        {
        }

        private void BuildClick()
        {
            //_screenManager.GetScreen<BuildScreenView>().Open();
        }

    }
}
