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
