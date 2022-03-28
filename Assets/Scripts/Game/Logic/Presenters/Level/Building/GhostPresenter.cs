﻿using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using static Game.Assets.Scripts.Game.Logic.Presenters.Ui.ScreenManagerPresenter;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class GhostPresenter : BasePresenter
    {
        public Action OnGhostPostionChanged = delegate { };
        public ConstructionDefinition Definition { get; private set; }
        
        private readonly GhostView _view;
        private readonly ConstructionsSettingsDefinition _constructionsSettings;
        private readonly IControls _controls;
        private readonly BuildScreenPresenter _buildScreen;
        private readonly ConstructionsManager _constructionsManager;
        private readonly ScreenManagerPresenter _screenManager;
        private readonly IAssets _assets;
        private FloatPoint _worldPosition;

        public GhostPresenter(ConstructionsSettingsDefinition constructionsSettings, 
            ScreenManagerPresenter screenManager,
            ConstructionsManager constructionsManager,
            BuildScreenPresenter buildScreen, IControls controls, IAssets assets, GhostView view) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _constructionsSettings = constructionsSettings ?? throw new ArgumentNullException(nameof(constructionsSettings));
            _controls = controls ?? throw new ArgumentNullException(nameof(controls));
            _buildScreen = buildScreen ?? throw new ArgumentNullException(nameof(buildScreen));
            _constructionsManager = constructionsManager ?? throw new ArgumentNullException(nameof(constructionsManager));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
            Definition = _buildScreen.CurrentCard.Definition ?? throw new ArgumentNullException(nameof(_buildScreen.CurrentCard.Definition));

            _controls.OnLevelClick += HandleOnLevelClick;
            _controls.OnLevelPointerMoved += HandleOnPointerMoved;

            _view.Container.Clear();
            _view.Container.Create<ConstructionModelView>(_assets.GetConstruction(_buildScreen.CurrentCard.Definition.LevelViewPath));

            UpdatePoints();
        }

        protected override void DisposeInner()
        {
            _controls.OnLevelClick -= HandleOnLevelClick;
            _controls.OnLevelPointerMoved -= HandleOnPointerMoved;
        }

        public IntPoint GetLocalPosition(PlacementField placementField)
        {
            return GetWorldCellPosition() + placementField.Offset;
        }

        public IntPoint GetWorldCellPosition()
        {
            var halfCell = _constructionsSettings.CellSize / 2;

            var offset = new FloatPoint(_buildScreen.CurrentCard.Definition.GetRect().Width * halfCell - halfCell,
                _buildScreen.CurrentCard.Definition.GetRect().Height * halfCell - halfCell);

            var pos = _worldPosition - offset;

            var mousePosX = Math.Round(pos.X / _constructionsSettings.CellSize);
            var mousePosY = Math.Round(pos.Y / _constructionsSettings.CellSize);

            return new IntPoint((int)Math.Ceiling(mousePosX), (int)Math.Ceiling(mousePosY));
        }

        private void HandleOnLevelClick()
        {
            foreach (var field in _constructionsManager.Placements)
            {
                if (field.CanPlace(_buildScreen.CurrentCard, GetLocalPosition(field)))
                {
                    field.Build(_buildScreen.CurrentCard, GetLocalPosition(field));
                    break;
                }
            }
            _screenManager.GetCollection<CommonScreens>().Open<MainScreenView>();
        }

        private void HandleOnPointerMoved(FloatPoint worldPosition)
        {
            _worldPosition = worldPosition;
            _view.CanPlace = CanPlace();
            _view.LocalPosition.Value = GetLocalPosition();
            UpdatePoints();
            OnGhostPostionChanged();
        }

        public FloatPoint GetLocalPosition()
        {
            var worldCell = GetWorldCellPosition();
            return new FloatPoint(worldCell.X * _constructionsSettings.CellSize, worldCell.Y * _constructionsSettings.CellSize);
        }

        private bool CanPlace()
        {
            foreach (var field in _constructionsManager.Placements)
            {
                if (field.CanPlace(_buildScreen.CurrentCard, GetLocalPosition(field)))
                {
                    return true;
                }
            }
            return false;
        }

        private void UpdatePoints()
        {
            var points = 0;
            foreach (var field in _constructionsManager.Placements)
            {
                points += field.GetPoints(_buildScreen.CurrentCard.Definition, GetLocalPosition(field));
            }

            _buildScreen.UpdatePoints(points);
        }

    }
}
