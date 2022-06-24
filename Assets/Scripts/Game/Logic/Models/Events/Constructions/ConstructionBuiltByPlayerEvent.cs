using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.Constructions
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
