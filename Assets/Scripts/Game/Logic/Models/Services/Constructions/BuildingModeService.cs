using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class BuildingModeService : IService
    {
        private FieldPosition _fieldPosition;
        private FieldRotation _fieldRotation;

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

        public void SetGhostPosition(FieldPosition fieldPosition, FieldRotation fieldRotation)
        {
            _fieldPosition = fieldPosition;
            _fieldRotation = fieldRotation;
            OnPositionChanged();
        }

        public FieldPosition GetPosition()
        {
            return _fieldPosition;
        }

        public FieldRotation GetRotation()
        {
            return _fieldRotation;
        }

    }
}
