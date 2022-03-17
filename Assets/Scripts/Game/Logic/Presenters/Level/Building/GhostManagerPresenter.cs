﻿using Game.Assets.Scripts.Game.Environment.Engine;
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
            _ghost = _view.GhostPrototype.Create<GhostView>(_view.Container).Init(_settings, _controls, _screenManager, _constructionsManager, buildScreen.CurrentCard);
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
