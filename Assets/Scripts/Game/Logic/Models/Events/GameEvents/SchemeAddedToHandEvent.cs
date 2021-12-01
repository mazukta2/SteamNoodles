using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.GameEvents
{
    public class SchemeAddedToHandEvent : IGameEvent
    {
        public SchemeAddedToHandEvent(ConstructionCard building) => Construction = building;
        public ConstructionCard Construction { get; }
    };
}
