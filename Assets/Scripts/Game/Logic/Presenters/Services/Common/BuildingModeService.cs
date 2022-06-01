using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Services.Common
{
    public class BuildingModeService
    {
        public event Action<bool> OnChanged = delegate { };
        public event Action OnHighligtingChanged = delegate { };

        public EntityLink<ConstructionCard> Card { get; private set; }
        public bool IsEnabled { get; private set; }
        public IReadOnlyCollection<Construction> ConstructionsHighlights { get; private set; } = new List<Construction>();

        public void Show(EntityLink<ConstructionCard> constructionCard)
        {
            IsEnabled = true;
            Card = constructionCard;
            OnChanged(IsEnabled);
        }

        public void Hide()
        {
            IsEnabled = false;
        }

        public void SetHighlight(IReadOnlyCollection<Construction> constructions)
        {
            ConstructionsHighlights = constructions;
            OnHighligtingChanged();
        }

    }
}
