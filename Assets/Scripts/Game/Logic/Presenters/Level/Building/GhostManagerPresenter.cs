using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level
{
    public class GhostManagerPresenter : BasePresenter
    {
        public event Action OnGhostChanged = delegate { };

        private GhostManagerView _view;
        private ScreenManagerPresenter _screenManager;
        private ConstructionsSettingsDefinition _settings;
        private GhostPresenter _ghost;

        public GhostManagerPresenter(ScreenManagerPresenter screenManager, ConstructionsSettingsDefinition settings, GhostManagerView view) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));

            _screenManager.OnScreenOpened += OnScreenOpen;
        }

        protected override void DisposeInner()
        {
            _screenManager.OnScreenOpened -= OnScreenOpen;
            RemoveGhost();
        }

        public GhostPresenter GetGhost()
        {
            return _ghost;
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
            _ghost = _view.GhostPrototype.Create<GhostView>(_view.Container).Init(_settings, buildScreen.CurrentCard);
            if (_ghost == null) throw new Exception("Empty presenter");

            OnGhostChanged();
        }

        private void RemoveGhost()
        {
            _view.Container.Clear();
            _ghost = null;
            OnGhostChanged();
        }

    }
}
