﻿using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements
{
    public class PlacementViewModel : IViewModel
    {
        public readonly float CellSize = 0.25f;

        public bool IsDestoyed { get; private set; }

        private Placement _model;
        private Dictionary<uint, ConstructionViewModel> _constructions = new Dictionary<uint, ConstructionViewModel>();
        private List<CellViewModel> _cells = new List<CellViewModel>();

        public PlacementViewModel(Placement model, IPlacementView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            View = view;
            View.SetClick(OnClick);

            for (int x = _model.Rect.xMin; x <= _model.Rect.xMax; x++)
            {
                for (int y = _model.Rect.yMin; y <= _model.Rect.yMax; y++)
                {
                    _cells.Add(new CellViewModel(this, new Point(x, y), View.CreateCell()));
                }
            }

            UpdateConstructions();
            model.OnConstructionAdded += UpdateConstructions;
        }

        public void Destroy()
        {
            _model.OnConstructionAdded -= UpdateConstructions;
            View.Destroy();
            Ghost?.Destroy();

            IsDestoyed = true;
        }

        public bool CanPlace(ConstructionScheme scheme, Point position)
        {
            return _model.CanPlace(scheme, position);
        }

        public Point GetSize() => _model.Size;

        public ConstructionGhostViewModel Ghost { get; private set; }
        public IPlacementView View { get; private set; }
        public void SetGhost(ConstructionScheme obj)
        {
            if (Ghost != null) throw new Exception("Ghost already existing");

            Ghost = new ConstructionGhostViewModel(this, obj, View.CreateGhost());
            UpdateGhostCells();
        }

        public void ClearGhost()
        {
            Ghost.Destroy();
            Ghost = null;
            UpdateGhostCells();
        }

        public void OnClick(Vector2 worldPosition)
        {
            if (Ghost != null)
            {
                Ghost.MoveTo(worldPosition);
                if (Ghost.CanPlaceGhost())
                {
                    _model.Place(Ghost.Scheme, Ghost.GetCellPosition(worldPosition));
                    ClearGhost();
                }
            }
        }

        public ConstructionViewModel[] GetConstructions()
        {
            return _constructions.Values.ToArray();
        }

        public CellViewModel[] GetCells()
        {
            return _cells.ToArray();
        }

        public void UpdateGhostCells()
        {
            foreach (var cell in _cells)
            {
                var state = CellViewModel.CellState.Normal;
                if (Ghost != null)
                {
                    var ocuppiedCells = Ghost.Scheme.GetOccupiedSpace(Ghost.Position);
                    if (_model.IsFreeCell(Ghost.Scheme, cell.Position))
                        state = CellViewModel.CellState.IsReadyToPlace;

                    if (ocuppiedCells.Any(x => x == cell.Position))
                    {
                        if (state == CellViewModel.CellState.IsReadyToPlace)
                            state = CellViewModel.CellState.IsAvailableGhostPlace;
                        else
                            state = CellViewModel.CellState.IsNotAvailableGhostPlace;
                    }
                }

                cell.SetState(state);
            }
        }

        public Vector2 GetWorldPosition(Point point)
        {
            return new Vector2(point.X * CellSize, point.Y * CellSize);
        }

        private void UpdateConstructions()
        {
            var toDelete = _constructions.Where(x => !_model.Contain(x.Key));
            foreach (var viewModel in toDelete)
            {
                viewModel.Value.Destroy();
                _constructions.Remove(viewModel.Key);
            }

            var toAdd = _model.Constructions.Where(x => !_constructions.ContainsKey(x.Id));
            foreach (var model in toAdd)
                _constructions.Add(model.Id, new ConstructionViewModel(this, model, View.CreateConstrcution()));
        }
    }
}
