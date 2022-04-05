using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
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
        public event Action OnGhostPostionChanged = delegate { };

        private GhostManagerView _view;
        private ScreenManagerPresenter _screenManager;
        private ConstructionsManager _constructionsManager;
        private ConstructionsSettingsDefinition _settings;
        private IControls _controls;
        private GhostPresenter _ghost;

        public GhostManagerPresenter(ScreenManagerPresenter screenManager, ConstructionsSettingsDefinition settings, IControls controls, 
            ConstructionsManager constructionsManager,
            GhostManagerView view) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _constructionsManager = constructionsManager ?? throw new ArgumentNullException(nameof(constructionsManager));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _controls = controls ?? throw new ArgumentNullException(nameof(controls));

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
            var view = _view.Container.Spawn<GhostView>(_view.GhostPrototype);
            _ghost = new GhostPresenter(_settings, _screenManager, _constructionsManager, buildScreen, _controls, _view.Level.Engine.Assets, view);
            view.Presenter = _ghost;
            _ghost.OnGhostPostionChanged += UpdateGhostPosition;

            OnGhostChanged();
        }

        private void RemoveGhost()
        {
            _view.Container.Clear();
            if (_ghost != null)
            {
                _ghost.OnGhostPostionChanged -= UpdateGhostPosition;
                _ghost = null;
                OnGhostChanged();
            }
        }

        private void UpdateGhostPosition()
        {
            OnGhostPostionChanged();
        }

    }
}
