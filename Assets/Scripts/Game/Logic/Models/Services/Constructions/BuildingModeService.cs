using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class BuildingModeService : IService
    {
        private FieldPosition _fieldPosition;
        private FieldRotation _fieldRotation;
        private GameVector3 _targetPosition;

        public event Action<bool> OnChanged = delegate { };
        public event Action OnPositionChanged = delegate { };
        public event Action OnHighligtingChanged = delegate { };

        public ConstructionCard Card { get; private set; }
        public bool IsEnabled => Card != null;
        public IReadOnlyCollection<Construction> ConstructionsHighlights { get; private set; } = new List<Construction>();

        public BuildingModeService()
        {
        }

        public void Show(ConstructionCard constructionCard)
        {
            Card = constructionCard;
            _fieldPosition = new FieldPosition(0, 0);
            _fieldRotation = new FieldRotation(FieldRotation.Rotation.Top);
            OnChanged(IsEnabled);
        }

        public void Hide()
        {
            Card = null;
            OnChanged(IsEnabled);
        }

        public void SetHighlight(IReadOnlyCollection<Construction> constructions)
        {
            ConstructionsHighlights = constructions;
            OnHighligtingChanged();
        }

        public void SetTargetPosition(GameVector3 pointerPosition)
        {
            _targetPosition = pointerPosition;
            //var size = _buildingModeService.Card.Scheme.Placement.GetRect(_buildingModeService.GetRotation());
            //var fieldPosition = _fieldService.GetWorldConstructionToField(_pointerPosition, size);
            //_buildingModeService.SetTargetPosition(_pointerPosition);
            //_buildingModeService.SetGhostPosition(fieldPosition, _buildingModeService.GetRotation());

            //SetPosition(fieldPosition, fieldRotation);
            OnPositionChanged();
        }

        public void SetGhostPosition(FieldPosition fieldPosition, FieldRotation fieldRotation)
        {
            SetPosition(fieldPosition, fieldRotation);
        }

        public FieldPosition GetPosition()
        {
            return _fieldPosition;
        }

        public GameVector3 GetTargetPosition()
        {
            return _targetPosition;
        }

        public FieldRotation GetRotation()
        {
            return _fieldRotation;
        }

        private void SetPosition(FieldPosition fieldPosition, FieldRotation fieldRotation)
        {
            _fieldPosition = fieldPosition;
            _fieldRotation = fieldRotation;
            OnPositionChanged();
        }

    }
}
