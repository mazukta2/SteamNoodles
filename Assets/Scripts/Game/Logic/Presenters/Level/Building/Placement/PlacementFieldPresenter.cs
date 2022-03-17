using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public class PlacementFieldPresenter : BasePresenter
    {
        private PlacementField _model;
        private PlacementFieldView _view;
        private PlacementManagerPresenter _manager;
        private GhostManagerPresenter _ghostManager;
        private List<PlacementCellPresenter> _cells = new List<PlacementCellPresenter>();

        public PlacementFieldPresenter(GhostManagerPresenter ghostManagerPresenter, PlacementField model, PlacementFieldView view, PlacementManagerPresenter presenter) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _manager = presenter ?? throw new ArgumentNullException(nameof(presenter));
            _ghostManager = ghostManagerPresenter ?? throw new ArgumentNullException(nameof(ghostManagerPresenter));

            for (int x = _model.Rect.xMin; x <= _model.Rect.xMax; x++)
            {
                for (int y = _model.Rect.yMin; y <= _model.Rect.yMax; y++)
                {
                    _cells.Add(CreateCell(new IntPoint(x, y)));
                }
            }

            _ghostManager.OnGhostChanged += UpdateGhostCells;

            UpdateGhostCells();
        }

        protected override void DisposeInner()
        {
            _ghostManager.OnGhostChanged -= UpdateGhostCells;
        }

        private PlacementCellPresenter CreateCell(IntPoint position)
        {
            return _view.Manager.Cell.Create<CellView>(_view.Manager.CellsContainer).Init(this, position, GetOffset());
        }

        private FloatPoint GetOffset()
        {
            var offset = FloatPoint.Zero;
            if (_model.Size.X % 2 == 0)
                offset += new FloatPoint(0.5f, 0);
            if (_model.Size.Y % 2 == 0)
                offset += new FloatPoint(0, 0.5f);

            return offset * _model.ConstructionsSettings.CellSize;
        }

        public void UpdateGhostCells()
        {
            var ghost = _ghostManager.GetGhost();
            var ocuppiedCells = ghost != null ? ghost.Definition.GetOccupiedSpace(ghost.GetPosition(this)) : null;

            foreach (var cell in _cells)
            {
                var state = CellPlacementStatus.Normal;
                if (ghost != null)
                {
                    if (_model.IsFreeCell(ghost.Definition, cell.Position))
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

        //public bool CanPlace(ConstructionCard scheme, Point position)
        //{
        //    return _model.CanPlace(scheme, position);
        //}

        //public Point GetSize() => _model.Size;

        //public ConstructionGhostPresenter Ghost { get; private set; }
        //public IPlacementView View { get; private set; }
        //public float CellSize => _model.CellSize;

        //public void SetGhost(ConstructionCard card)
        //{
        //    if (Ghost != null) throw new Exception("Ghost already existing");

        //    Ghost = new ConstructionGhostPresenter(this, card, View.Ghost.Create());
        //    UpdateGhostCells();
        //}

        //public void ClearGhost()
        //{
        //    Ghost?.Dispose();
        //    Ghost = null;
        //    UpdateGhostCells();
        //}

        //public void OnClick(FloatPoint worldPosition)
        //{
        //    if (Ghost != null)
        //    {
        //        Ghost.MoveTo(worldPosition);
        //        if (Ghost.CanPlaceGhost())
        //        {
        //            _model.Build(Ghost.Card, Ghost.GetCellPosition(worldPosition));
        //            ClearGhost();
        //        }
        //    }
        //}

        //public ConstructionPresenter[] GetConstructions()
        //{
        //    return _constructions.Values.ToArray();
        //}

        //public CellPresenter[] GetCells()
        //{
        //    return _cells.ToArray();
        //}

        //public FloatPoint GetWorldPosition(Point point)
        //{
        //    return new FloatPoint(point.X * _model.CellSize, point.Y * _model.CellSize);
        //}

        //private void ConstructionRemoved(Construction obj) => UpdateConstructions();
        //private void ConstructionAdded(Construction obj) => UpdateConstructions();

        //private void UpdateConstructions()
        //{
        //    var toDelete = _constructions.Where(x => !_model.Contain(x.Key));
        //    foreach (var viewModel in toDelete)
        //    {
        //        _constructions.Remove(viewModel.Key);
        //        viewModel.Value.Dispose();
        //    }

        //    var toAdd = _model.Constructions.Where(x => !_constructions.ContainsKey(x));
        //    foreach (var model in toAdd)
        //        _constructions.Add(model, new ConstructionPresenter(this, model, View.Constructions.Create()));
        //}
    }
}
