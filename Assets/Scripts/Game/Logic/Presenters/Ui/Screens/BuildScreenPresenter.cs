using Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Screens;
using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BaseGameScreenPresenter
    {
        private BuildScreenViewPresenter _view;
        private ScreenManagerPresenter _screenManager;

        public BuildScreenPresenter(BuildScreenViewPresenter view, ScreenManagerPresenter screenManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
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
