using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level
{
    public class GhostManagerPresenter : BasePresenter<IGhostManagerView>
    {
        public event Action OnGhostChanged = delegate { };
        public event Action OnGhostPostionChanged = delegate { };

        private IGhostManagerView _view;
        private readonly IGameTime _time;
        private ScreenManagerPresenter _screenManager;
        private PlacementField _constructionsManager;
        private ConstructionsSettingsDefinition _settings;
        private IControls _controls;
        private GhostPresenter _ghost;

        public GhostManagerPresenter(ScreenManagerPresenter screenManager, ConstructionsSettingsDefinition settings, IControls controls,
            PlacementField constructionsManager,
            IGhostManagerView view, IGameTime time) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _time = time;
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

        private void OnScreenOpen(IScreenView screen)
        {
            if (screen is IBuildScreenView buildScreen)
                CreateGhost(buildScreen.Presenter);
            else
                RemoveGhost();
        }

        private void CreateGhost(BuildScreenPresenter buildScreen)
        {
            var view = _view.Container.Spawn<IGhostView>(_view.GhostPrototype);
            _ghost = new GhostPresenter(_settings, _screenManager, _constructionsManager, buildScreen, 
                _controls, IGameKeysManager.Default, IGameAssets.Default, view, _time);
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
