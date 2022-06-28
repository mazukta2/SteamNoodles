using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Localization;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Entities.Constructions
{
    public record ConstructionCardEntity : Entity
    {
        public ConstructionSchemeEntity SchemeEntity { get; }
        public CardAmount Amount { get; private set; }

        public LocalizationTag Name => SchemeEntity.Name;
        public BuildingPoints Points => SchemeEntity.Points;
        public string HandImagePath => SchemeEntity.HandImagePath;
        public AdjacencyBonuses AdjacencyPoints => SchemeEntity.AdjacencyPoints;

        public ConstructionCardEntity(Uid id, ConstructionSchemeEntity schemeEntity) : base(id)
        {
            Amount = new CardAmount(1);
            SchemeEntity = schemeEntity;
        }

        public ConstructionCardEntity() : this(new ConstructionSchemeEntity())
        {
        }

        public ConstructionCardEntity(ConstructionSchemeEntity schemeEntity)
        {
            Amount = new CardAmount(1);
            SchemeEntity = schemeEntity;
        }

        public ConstructionCardEntity(ConstructionSchemeEntity schemeEntity, CardAmount amount)
        {
            Amount = amount;
            SchemeEntity = schemeEntity;
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
