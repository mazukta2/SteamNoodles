using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Presenters.Requests.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class PlacementCellPresenter : BasePresenter<ICellView>
    {
        public IntPoint Position => _position;

        private ICellView _cellView;
        private IntPoint _position;

        public PlacementCellPresenter(ICellView view, IntPoint position,
            FieldModel field) : base(view)
        {
            _cellView = view ?? throw new ArgumentNullException(nameof(view));
            _position = position;

            view.LocalPosition.Value = field.GetCellWorldPosition(position);
        }

        public void SetState(CellPlacementStatus state)
        {
            _cellView.State.Value = state;
            if (state == CellPlacementStatus.IsAvailableGhostPlace)
                _cellView.Animator.Play(CellAnimations.Highlighed.ToString());
            else
                _cellView.Animator.Play(CellAnimations.Idle.ToString());
        }

        public enum CellAnimations
        {
            Idle,
            Highlighed
        }
    }
}
