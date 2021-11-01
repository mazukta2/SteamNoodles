using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Models.Orders;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.GameEvents
{
    public class CurrentOrderClosedEvent : IGameEvent
    {
        public CurrentOrderClosedEvent(ServingOrderProcess order) => Order = order;
        public ServingOrderProcess Order { get; }
    };
}
