using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class PlacementPresenter : IPresenter
    {
        public bool IsDestoyed { get; private set; }

        private Placement _model;
        private Dictionary<Construction, ConstructionPresenter> _constructions = new Dictionary<Construction, ConstructionPresenter>();
        private List<CellPresenter> _cells = new List<CellPresenter>();

        public PlacementPresenter(Placement model, IPlacementView view)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            View = view;
            View.SetClick(OnClick);

            for (int x = _model.Rect.xMin; x <= _model.Rect.xMax; x++)
            {
                for (int y = _model.Rect.yMin; y <= _model.Rect.yMax; y++)
                {
                    _cells.Add(new CellPresenter(this, new Point(x, y), View.CreateCell()));
                }
            }

            UpdateConstructions();
            model.OnConstructionAdded += ConstructionAdded;
            model.OnConstructionRemoved += ConstructionRemoved;
        }

        public void Destroy()
        {
            _model.OnConstructionAdded -= ConstructionAdded;
            _model.OnConstructionRemoved += ConstructionRemoved;
            View.Destroy();
            Ghost?.Destroy();

            IsDestoyed = true;
        }

        public bool CanPlace(ConstructionScheme scheme, Point position)
        {
            return _model.CanPlace(scheme, position);
        }

        public Point GetSize() => _model.Size;

        public ConstructionGhostPresenter Ghost { get; private set; }
        public IPlacementView View { get; private set; }
        public float CellSize => _model.CellSize;

        public void SetGhost(ConstructionScheme obj)
        {
            if (Ghost != null) throw new Exception("Ghost already existing");

            Ghost = new ConstructionGhostPresenter(this, obj, View.CreateGhost());
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

        public ConstructionPresenter[] GetConstructions()
        {
            return _constructions.Values.ToArray();
        }

        public CellPresenter[] GetCells()
        {
            return _cells.ToArray();
        }

        public void UpdateGhostCells()
        {
            foreach (var cell in _cells)
            {
                var state = CellPresenter.CellState.Normal;
                if (Ghost != null)
                {
                    var ocuppiedCells = Ghost.Scheme.GetOccupiedSpace(Ghost.Position);
                    if (_model.IsFreeCell(Ghost.Scheme, cell.Position))
                        state = CellPresenter.CellState.IsReadyToPlace;

                    if (ocuppiedCells.Any(x => x == cell.Position))
                    {
                        if (state == CellPresenter.CellState.IsReadyToPlace)
                            state = CellPresenter.CellState.IsAvailableGhostPlace;
                        else
                            state = CellPresenter.CellState.IsNotAvailableGhostPlace;
                    }
                }

                cell.SetState(state);
            }
        }

        public Vector2 GetWorldPosition(Point point)
        {
            return new Vector2(point.X * _model.CellSize, point.Y * _model.CellSize);
        }

        private void ConstructionRemoved(Construction obj) => UpdateConstructions();
        private void ConstructionAdded(Construction obj) => UpdateConstructions();

        private void UpdateConstructions()
        {
            var toDelete = _constructions.Where(x => !_model.Contain(x.Key));
            foreach (var viewModel in toDelete)
            {
                viewModel.Value.Destroy();
                _constructions.Remove(viewModel.Key);
            }

            var toAdd = _model.Constructions.Where(x => !_constructions.ContainsKey(x));
            foreach (var model in toAdd)
                _constructions.Add(model, new ConstructionPresenter(this, model, View.CreateConstrcution()));
        }
    }
}
