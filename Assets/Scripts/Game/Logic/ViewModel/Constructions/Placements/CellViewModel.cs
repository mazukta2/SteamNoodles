using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Models.Buildings;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements
{
    public class CellViewModel
    {
        private PlacementViewModel _placementModel;
        public CellViewModel(PlacementViewModel placement, Point position, ICellView view)
        {
            _placementModel = placement;
            View = view;
            Position = position;
            View.SetPosition(placement.GetWorldPosition(position));
            View.SetState(State);
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
