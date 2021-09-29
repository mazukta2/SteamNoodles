using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Workers;
using System;
using System.Linq;
using Tests.Assets.Scripts.Game.Logic.Models.Orders;
using Tests.Assets.Scripts.Game.Logic.Models.Session;

namespace Assets.Scripts.Logic.Models.Levels
{
    public class GameLevel
    {
        public Placement Placement { get; }
        public OrderManager Orders { get; }
        public WorkManager WorkManager { get; }
        public PlayerHand Hand { get; }
        public TimeManager Time { get; }

        public GameLevel(ILevelPrototype prototype, SessionRandom random)
        {
            if (prototype == null) throw new ArgumentNullException(nameof(prototype));
            if (random == null) throw new ArgumentNullException(nameof(random));

            Time = new TimeManager();
            Hand = new PlayerHand(prototype.StartingHand);
            var orders = prototype.Orders.Select(x => new AvailableOrder(this, x));
            Placement = new Placement(this, prototype.Size);
            Orders = new OrderManager(this, random, orders.ToArray());
            WorkManager = new WorkManager(this);
        }
    }
}
