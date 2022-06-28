using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Events.Constructions
{
    public record ConstructionBuiltByPlayerEvent : IModelEvent
    {
        public BuildingPoints Points { get; set; }
        public ConstructionCard Card { get; set; }

        public ConstructionBuiltByPlayerEvent(BuildingPoints points, ConstructionCard card)
        {
            Points = points;
            Card = card;
        }
    }
}
