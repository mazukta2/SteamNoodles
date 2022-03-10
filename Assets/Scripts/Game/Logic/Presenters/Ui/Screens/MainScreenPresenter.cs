using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class MainScreenPresenter : BaseGameScreenPresenter
    {
        private MainScreenView _view;
        private ScreenManagerPresenter _screenManager;

        public MainScreenPresenter(MainScreenView view, ScreenManagerPresenter screenManager) : base(screenManager, view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            view.HandView.SetScreen(this);
        }

        protected override void DisposeInner()
        {
        }

    }
}
