using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Models.Session;
using Assets.Scripts.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Assets.Scripts.Game.Logic.Models.Orders;

namespace Assets.Scripts.Logic.Models.Events.GameEvents
{
    public class CurrentOrderCreatedEvent : IGameEvent
    {
        public CurrentOrderCreatedEvent(CurrentOrder order) => (Order) = (order);
        public CurrentOrder Order { get; }
    };
}
