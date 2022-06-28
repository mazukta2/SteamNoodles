using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Events.Constructions
{
    public record ConstructionBuiltByPlayerEvent : IModelEvent
    {
        public BuildingPoints Points { get; set; }
        public ConstructionCardEntity CardEntity { get; set; }

        public ConstructionBuiltByPlayerEvent(BuildingPoints points, ConstructionCardEntity cardEntity)
        {
            Points = points;
            CardEntity = cardEntity;
        }
    }
}
