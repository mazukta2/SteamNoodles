using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class PlacementCellPresenter : BasePresenter
    {
        private CellView _cellView;
        private PlacementFieldPresenter _field;
        private IntPoint _position;
        private ConstructionsSettingsDefinition _constructionsSettingsDefinition;
        private FloatPoint _offset;

        public PlacementCellPresenter(CellView view, PlacementFieldPresenter field, 
            ConstructionsSettingsDefinition constructionsSettingsDefinition, IntPoint position, FloatPoint offset) : base(view)
        {
            _cellView = view ?? throw new ArgumentNullException(nameof(view));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _position = position;
            _constructionsSettingsDefinition = constructionsSettingsDefinition;
            _offset = offset;

            view.LocalPosition = GetPosition();
        }

        private FloatPoint GetPosition()
        {
            return new FloatPoint(_position.X * _constructionsSettingsDefinition.CellSize,
                   _position.Y * _constructionsSettingsDefinition.CellSize) + _offset;
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
