using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.Constructions
{
    public record ConstructionBuiltByPlayerEvent : IModelEvent
    {
        public BuildingPoints Points { get; set; }

        public ConstructionBuiltByPlayerEvent(BuildingPoints points)
        {
            Points = points;
        }
    }
}
