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
        private readonly FieldService _fieldService;
        private FieldPosition _fieldPosition;
        private FieldRotation _fieldRotation;
        private GameVector3 _targetPosition;
        private ConstructionCard _card;
        private IReadOnlyCollection<Construction> _constructionsHighlights = new List<Construction>();

        public BuildingModeService(FieldService fieldService)
        {
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
        }
        
        public event Action<bool> OnChanged = delegate { };
        public event Action OnPositionChanged = delegate { };
        public event Action OnHighlightingChanged = delegate { };
        
        public void Show(ConstructionCard constructionCard)
        {
            _card = constructionCard;
            _fieldPosition = new FieldPosition(0, 0);
            _fieldRotation = new FieldRotation(FieldRotation.Rotation.Top);
            OnChanged(IsEnabled());
        }

        public void Hide()
        {
            _card = null;
            OnChanged(IsEnabled());
        }

        public bool IsEnabled() => GetCard() != null;

        public IReadOnlyCollection<Construction> GetConstructionsHighlights()
        {
            return _constructionsHighlights;
        }

        public void SetHighlight(IReadOnlyCollection<Construction> constructions)
        {
            _constructionsHighlights = constructions;
            OnHighlightingChanged();
        }

        public void SetTargetPosition(GameVector3 pointerPosition)
        {
            _targetPosition = pointerPosition;
            
            var size = _card.Scheme.Placement.GetRect(_fieldRotation);
            var fieldPosition = _fieldService.GetFieldPosition(pointerPosition, size);
            _fieldPosition = fieldPosition;
            OnPositionChanged();
        }

        public void SetTargetPosition(FieldPosition fieldPosition)
        {
            var size = _card.Scheme.Placement.GetRect(_fieldRotation);
            _targetPosition = _fieldService.GetWorldPosition(_fieldPosition, size);
            _fieldPosition = fieldPosition;
            
            OnPositionChanged();
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

        public ConstructionCard GetCard()
        {
            return _card;
        }

        public void SetRotation(FieldRotation fieldRotation)
        {
            _fieldRotation = fieldRotation;
            OnPositionChanged();
        }
    }
}
