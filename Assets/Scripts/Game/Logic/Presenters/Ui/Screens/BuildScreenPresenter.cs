using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BaseGameScreenPresenter
    {
        private BuildScreenView _view;
        private ScreenManagerPresenter _manager;

        public BuildScreenPresenter(BuildScreenView view, ScreenManagerPresenter manager) :base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            _view.OnValidate();
            _view.CloseButton.SetAction(CloseClick);
        }

        protected override void DisposeInner()
        {
        }

        private void CloseClick()
        {
            _manager.GetScreen<MainScreenView>().Open();
        }

    }
}
