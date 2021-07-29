using Assets.Scripts.Models.Buildings;

namespace Assets.Scripts.Models.Events
{
    public class BuildingPlacedEvent
    {
        public Building Building { get; private set; }

        public BuildingPlacedEvent(Building building)
        {
            Building = building;
        }
    }
}
