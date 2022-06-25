using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Localization;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Entities.Constructions
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

        public ConstructionCard() : this(new ConstructionScheme())
        {
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
            FireEvent(new HandConstructionAmountChangedEvent());
        }

        public void Remove(CardAmount amount)
        {
            Amount = new CardAmount(Amount.Value - amount.Value);
            FireEvent(new HandConstructionAmountChangedEvent());
        }
    }
}
