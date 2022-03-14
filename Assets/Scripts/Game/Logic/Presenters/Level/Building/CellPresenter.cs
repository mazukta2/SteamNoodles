using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Views.Level;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class CellPresenter : BasePresenter
    {
        private CellView _cellView;
        private PlacementFieldPresenter _field;

        public CellPresenter(CellView view, PlacementFieldPresenter field) : base(view)
        {
            _cellView = view;
            _field = field;
        }
    }
    //{
    //    private PlacementPresenter _placementModel;
    //    private PlacementPresenter _placementPresenter;
    //    private Point _point;
    //    private DisposableViewListKeeper<ICellView> _cells;

    //    public CellPresenter(PlacementPresenter placement, Point position, DisposableViewListKeeper<ICellView> view)
    //    {
    //        _placementModel = placement;
    //        View = view.Create();
    //        Position = position;
    //        View.SetPosition(placement.GetWorldPosition(position));
    //        View.SetState(State);
    //    }

    //    protected override void DisposeInner()
    //    {
    //        View.Dispose();
    //    }

    //    public ICellView View { get; private set; }
    //    public CellState State { get; private set; }
    //    public Point Position { get; private set; }

    //    public void SetState(CellState state)
    //    {
    //        State = state;
    //        View.SetState(state);
    //    }

    //    public enum CellState
    //    {
    //        Normal,
    //        IsReadyToPlace,
    //        IsAvailableGhostPlace,
    //        IsNotAvailableGhostPlace,
    //    }
}
