using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class PlacementCellPresenter : BasePresenter<ICellView>
    {
        public IntPoint Position => _position;

        private ICellView _cellView;
        private IntPoint _position;
        private readonly FieldService _fieldService;

        public PlacementCellPresenter(ICellView view, IntPoint position, FieldService fieldService) : base(view)
        {
            _cellView = view ?? throw new ArgumentNullException(nameof(view));
            _position = position;
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));

            view.LocalPosition.Value = GetPosition();
        }

        private GameVector3 GetPosition() => _fieldService.GetWorldPosition(new FieldPosition(_position), new IntRect(0, 0, 1, 1));

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
