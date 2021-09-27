using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Logic.Models.Session;
using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.GameEvents
{
    public class CurrentOrderClosedEvent : IGameEvent
    {
        public CurrentOrderClosedEvent(CurrentOrder order) => Order = order;
        public CurrentOrder Order { get; }
    };
}
