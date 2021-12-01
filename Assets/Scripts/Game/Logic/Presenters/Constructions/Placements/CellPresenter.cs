using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class CellPresenter : Disposable
    {
        private PlacementPresenter _placementModel;
        public CellPresenter(PlacementPresenter placement, Point position, ICellView view)
        {
            _placementModel = placement;
            View = view;
            Position = position;
            View.SetPosition(placement.GetWorldPosition(position));
            View.SetState(State);
        }

        protected override void DisposeInner()
        {
            View.Dispose();
        }

        public ICellView View { get; private set; }
        public CellState State { get; private set; }
        public Point Position { get; private set; }

        public void SetState(CellState state)
        {
            State = state;
            View.SetState(state);
        }

        public enum CellState
        {
            Normal,
            IsReadyToPlace,
            IsAvailableGhostPlace,
            IsNotAvailableGhostPlace,
        }
    }
}
