using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Models.Orders;

namespace Assets.Scripts.Logic.Models.Events.GameEvents
{
    public class CurrentOrderCreatedEvent : IGameEvent
    {
        public CurrentOrderCreatedEvent(ActiveOrder order) => (Order) = (order);
        public ActiveOrder Order { get; }
    };
}
