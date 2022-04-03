﻿using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public class PlacementFieldPresenter : BasePresenter
    {
        private PlacementField _model;
        private PlacementFieldView _view;
        private PlacementManagerPresenter _manager;
        private GhostManagerPresenter _ghostManager;
        private IAssets _assets;
        private List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(GhostManagerPresenter ghostManagerPresenter, PlacementField model, 
            PlacementFieldView view, PlacementManagerPresenter presenter, IAssets assets) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _manager = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _ghostManager = ghostManagerPresenter ?? throw new ArgumentNullException(nameof(ghostManagerPresenter));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));

            for (int x = _model.Rect.xMin; x <= _model.Rect.xMax; x++)
            {
                for (int y = _model.Rect.yMin; y <= _model.Rect.yMax; y++)
                {
                    _cells.Add(CreateCell(new IntPoint(x, y)));
                }
            }

            _ghostManager.OnGhostChanged += UpdateGhostCells;
            _ghostManager.OnGhostPostionChanged += UpdateGhostCells;

            _model.OnConstructionAdded += HandleOnConstructionAdded;

            UpdateGhostCells();
        }

        public FloatPoint GetLocalPosition(IntPoint position) => _model.GetLocalPosition(position);

        protected override void DisposeInner()
        {
            _ghostManager.OnGhostChanged -= UpdateGhostCells;
            _ghostManager.OnGhostPostionChanged -= UpdateGhostCells;
            _model.OnConstructionAdded -= HandleOnConstructionAdded;
        }

        private PlacementCellPresenter CreateCell(IntPoint position)
        {
            return _view.Manager.CellsContainer.Spawn<CellView>(_view.Manager.Cell).Init(this, position);
        }

        public void UpdateGhostCells()
        {
            var ghost = _ghostManager.GetGhost();
            var ocuppiedCells = ghost != null ? ghost.Definition.GetOccupiedSpace(ghost.GetLocalPosition(_model), ghost.Rotation) : null;

            foreach (var cell in _cells)
            {
                var state = CellPlacementStatus.Normal;
                if (ghost != null)
                {
                    if (_model.IsFreeCell(ghost.Definition, cell.Position, ghost.Rotation))
                        state = CellPlacementStatus.IsReadyToPlace;

                    if (ocuppiedCells.Any(x => x == cell.Position))
                    {
                        if (state == CellPlacementStatus.IsReadyToPlace)
                            state = CellPlacementStatus.IsAvailableGhostPlace;
                        else
                            state = CellPlacementStatus.IsNotAvailableGhostPlace;
                    }
                }

                cell.SetState(state);
            }
        }

        private void HandleOnConstructionAdded(Construction construction)
        {
            var view = _view.Manager.ConstrcutionContainer.Spawn<ConstructionView>(_view.Manager.ConstrcutionPrototype);
            view.Init(construction);
        }
    }
}
