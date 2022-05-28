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
        private PlacementFieldPresenter _field;
        private IntPoint _position;
        private readonly IFieldPresenterService _fieldService;
        private ConstructionsSettingsDefinition _constructionsSettingsDefinition;

        public PlacementCellPresenter(ICellView view, PlacementFieldPresenter field, 
            ConstructionsSettingsDefinition constructionsSettingsDefinition, IntPoint position, IFieldPresenterService fieldService) : base(view)
        {
            _cellView = view ?? throw new ArgumentNullException(nameof(view));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _position = position;
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _constructionsSettingsDefinition = constructionsSettingsDefinition;

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
