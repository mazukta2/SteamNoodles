using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class PlacementCellPresenter : BasePresenter<ICellView>
    {
        public IntPoint Position => _position;

        private ICellView _cellView;
        private PlacementFieldPresenter _field;
        private IntPoint _position;
        private ConstructionsSettingsDefinition _constructionsSettingsDefinition;
        private FieldPositionsCalculator _fieldPositions;

        public PlacementCellPresenter(ICellView view, PlacementFieldPresenter field, 
            ConstructionsSettingsDefinition constructionsSettingsDefinition, IntPoint position) : base(view)
        {
            _cellView = view ?? throw new ArgumentNullException(nameof(view));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _position = position;
            _constructionsSettingsDefinition = constructionsSettingsDefinition;
            _fieldPositions = new FieldPositionsCalculator(constructionsSettingsDefinition.CellSize);

            view.LocalPosition.Value = GetPosition();
        }

        private GameVector3 GetPosition() => _fieldPositions.GetMapPositionByGridPosition(_position, new IntRect(0, 0, 1, 1));

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
