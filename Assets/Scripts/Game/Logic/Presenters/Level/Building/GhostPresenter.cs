﻿using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Environment.Engine.Controls;
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
        public FieldRotation Rotation { get; private set; }
        
        private readonly GhostView _view;
        private readonly ConstructionsSettingsDefinition _constructionsSettings;
        private readonly IControls _controls;
        private readonly BuildScreenPresenter _buildScreen;
        private readonly ConstructionsManager _constructionsManager;
        private readonly ScreenManagerPresenter _screenManager;
        private readonly IAssets _assets;
        private FloatPoint _worldPosition;
        private KeyCommand _rotateLeft;
        private KeyCommand _rotateRight;

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

            _rotateLeft = _controls.Keys.GetKey(GameKeys.RotateLeft);
            _rotateRight = _controls.Keys.GetKey(GameKeys.RotateRight);

            _view.Container.Clear();
            _view.Container.Spawn<ConstructionModelView>(_assets.GetConstruction(_buildScreen.CurrentCard.Definition.LevelViewPath));

            _rotateLeft.OnTap += HandleRotateLeftTap;
            _rotateRight.OnTap += HandleRotateRightTap;

            _controls.OnLevelClick += HandleOnLevelClick;
            _controls.OnLevelPointerMoved += HandleOnPointerMoved;

            UpdatePoints();
        }

        protected override void DisposeInner()
        {
            _controls.OnLevelClick -= HandleOnLevelClick;
            _controls.OnLevelPointerMoved -= HandleOnPointerMoved;

            _rotateLeft.OnTap -= HandleRotateLeftTap;
            _rotateRight.OnTap -= HandleRotateRightTap;
        }

        public IntPoint GetLocalPosition(PlacementField placementField)
        {
            return GetWorldCellPosition() + placementField.Offset;
        }

        public IntPoint GetWorldCellPosition()
        {
            var halfCell = _constructionsSettings.CellSize / 2;

            var offset = new FloatPoint(_buildScreen.CurrentCard.Definition.GetRect(Rotation).Width * halfCell - halfCell,
                _buildScreen.CurrentCard.Definition.GetRect(Rotation).Height * halfCell - halfCell);

            var pos = _worldPosition - offset;

            var mousePosX = Math.Round(pos.X / _constructionsSettings.CellSize);
            var mousePosY = Math.Round(pos.Y / _constructionsSettings.CellSize);

            return new IntPoint((int)Math.Ceiling(mousePosX), (int)Math.Ceiling(mousePosY));
        }

        private void HandleOnLevelClick()
        {
            foreach (var field in _constructionsManager.Placements)
            {
                if (field.CanPlace(_buildScreen.CurrentCard, GetLocalPosition(field), Rotation))
                {
                    field.Build(_buildScreen.CurrentCard, GetLocalPosition(field), Rotation);
                    break;
                }
            }
            _screenManager.GetCollection<CommonScreens>().Open<MainScreenView>();
        }

        private void HandleOnPointerMoved(FloatPoint worldPosition)
        {
            _worldPosition = worldPosition;
            UpdatePosition();
        }

        private bool CanPlace()
        {
            foreach (var field in _constructionsManager.Placements)
            {
                if (field.CanPlace(_buildScreen.CurrentCard, GetLocalPosition(field), Rotation))
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
                points += field.GetPoints(_buildScreen.CurrentCard.Definition, GetLocalPosition(field), Rotation);
            }

            _buildScreen.UpdatePoints(points);
        }

        private void HandleRotateLeftTap()
        {
            Rotation = ConstructionRotation.RotateLeft(Rotation);
            UpdatePosition();
        }

        private void HandleRotateRightTap()
        {
            Rotation = ConstructionRotation.RotateRight(Rotation);
            UpdatePosition();
        }

        private void UpdatePosition()
        {
            _view.CanPlace = CanPlace();
            _view.Rotator.Look(ConstructionRotation.ToDirection(Rotation));
            _view.LocalPosition.Value = GetViewPosition();
            UpdatePoints();
            OnGhostPostionChanged();
        }

        private FloatPoint GetViewPosition()
        {
            var worldCell = GetWorldCellPosition();
            var objectRect = Definition.GetRect(Rotation);
            var rect = new FloatPoint(objectRect.Width * _constructionsSettings.CellSize, objectRect.Height * _constructionsSettings.CellSize);
            var offset = new FloatPoint(rect.X /2 , rect.Y / 2);

            return new FloatPoint(worldCell.X * _constructionsSettings.CellSize, worldCell.Y * _constructionsSettings.CellSize) + offset;
        }

    }
}
