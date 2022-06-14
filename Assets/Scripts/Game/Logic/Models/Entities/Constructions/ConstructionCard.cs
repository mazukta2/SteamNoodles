using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Localization;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions
{
    public record ConstructionCard : Entity
    {
        public ConstructionScheme Scheme { get; }
        public CardAmount Amount { get; private set; }

        public LocalizationTag Name => Scheme.Name;
        public BuildingPoints Points => Scheme.Points;
        public string HandImagePath => Scheme.HandImagePath;
        public AdjacencyBonuses AdjacencyPoints => Scheme.AdjacencyPoints;

        public ConstructionCard(Uid id, ConstructionScheme scheme) : base(id)
        {
            Amount = new CardAmount(1);
            Scheme = scheme;
        }

        public ConstructionCard(ConstructionScheme scheme)
        {
            Amount = new CardAmount(1);
            Scheme = scheme;
        }

        public ConstructionCard(ConstructionScheme scheme, CardAmount amount)
        {
            Amount = amount;
            Scheme = scheme;
        }

        public void Add(CardAmount amount)
        {
            Amount = new CardAmount(Amount.Value + amount.Value);
        }

        public void Remove(CardAmount amount)
        {
            Amount = new CardAmount(Amount.Value - amount.Value);
        }
    }
}
