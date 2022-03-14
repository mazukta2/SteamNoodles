using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views.Level;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class PlacementManagerPresenter : BasePresenter
    {
        //private Placement _model;
        //private Dictionary<Construction, ConstructionPresenter> _constructions = new Dictionary<Construction, ConstructionPresenter>();
        //private List<CellPresenter> _cells = new List<CellPresenter>();

        public PlacementManagerPresenter(PlacementManagerView view) : base(view)
        {
            //if (model == null) throw new ArgumentNullException(nameof(model));
            //_model = model;
            //View = view;
            //View.SetClick(OnClick);

            //for (int x = _model.Rect.xMin; x <= _model.Rect.xMax; x++)
            //{
            //    for (int y = _model.Rect.yMin; y <= _model.Rect.yMax; y++)
            //    {
            //        _cells.Add(new CellPresenter(this, new Point(x, y), View.Cells));
            //    }
            //}

            //UpdateConstructions();
            //model.OnConstructionAdded += ConstructionAdded;
            //model.OnConstructionRemoved += ConstructionRemoved;
        }


        //protected override void DisposeInner()
        //{
        //    _model.OnConstructionAdded -= ConstructionAdded;
        //    _model.OnConstructionRemoved += ConstructionRemoved;
        //    Ghost?.Dispose();

        //    foreach (var cell in _cells)
        //        cell.Dispose();

        //    foreach (var construction in _constructions)
        //        construction.Value.Dispose();
        //}

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

        //public void UpdateGhostCells()
        //{
        //    foreach (var cell in _cells)
        //    {
        //        var state = CellPresenter.CellState.Normal;
        //        if (Ghost != null)
        //        {
        //            var ocuppiedCells = Ghost.Card.GetOccupiedSpace(Ghost.Position);
        //            if (_model.IsFreeCell(Ghost.Card, cell.Position))
        //                state = CellPresenter.CellState.IsReadyToPlace;

        //            if (ocuppiedCells.Any(x => x == cell.Position))
        //            {
        //                if (state == CellPresenter.CellState.IsReadyToPlace)
        //                    state = CellPresenter.CellState.IsAvailableGhostPlace;
        //                else
        //                    state = CellPresenter.CellState.IsNotAvailableGhostPlace;
        //            }
        //        }

        //        cell.SetState(state);
        //    }
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
