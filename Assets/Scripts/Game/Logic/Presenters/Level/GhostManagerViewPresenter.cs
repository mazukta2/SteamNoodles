using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level
{
    public class GhostManagerPresenter : BasePresenter
    {
        private GhostManagerView _view;
        private ScreenManagerPresenter _screenManager;

        public GhostManagerPresenter(ScreenManagerPresenter screenManager, GhostManagerView view) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));

            _screenManager.OnScreenOpened += OnScreenOpen;
        }

        protected override void DisposeInner()
        {
            _screenManager.OnScreenOpened -= OnScreenOpen;
            RemoveGhost();
        }

        private void OnScreenOpen(BaseGameScreenPresenter screen)
        {
            if (screen is BuildScreenPresenter buildScreen)
                CreateGhost(buildScreen);
            else
                RemoveGhost();
        }

        private void CreateGhost(BuildScreenPresenter buildScreen)
        {
            _view.GhostPrototype.Create<GhostView>(_view.Container);
        }

        private void RemoveGhost()
        {
            _view.Container.Clear();
        }

    }
}
