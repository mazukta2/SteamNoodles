using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Models.Session;
using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logic.Models.Events.GameEvents
{
    public class CurrentOrderCreatedEvent : IGameEvent
    {
        public CurrentOrderCreatedEvent(ActiveOrder order) => (Order) = (order);
        public ActiveOrder Order { get; }
    };
}
