using System;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement
{
    public class PlacementCellPresenter : BasePresenter<ICellView>
    {
        public FieldPosition Position => _position;
        
        private ICellView _cellView;
        private FieldPosition _position;

        public PlacementCellPresenter(ICellView view, FieldPosition position) : base(view)
        {
            _cellView = view ?? throw new ArgumentNullException(nameof(view));
            _position = position;

            view.LocalPosition.Value = position.GetWorldPosition();
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
