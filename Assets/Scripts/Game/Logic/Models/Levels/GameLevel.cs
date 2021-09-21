using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Linq;
using Tests.Assets.Scripts.Game.Logic.Models.Orders;
using Tests.Assets.Scripts.Game.Logic.Models.Session;

namespace Assets.Scripts.Logic.Models.Levels
{
    public class GameLevel
    {
        public Placement Placement { get; }

        public PlayerHand Hand { get; }

        public GameLevel(ILevelPrototype prototype, SessionRandom random)
        {
            if (prototype == null) throw new ArgumentNullException(nameof(prototype));
            if (random == null) throw new ArgumentNullException(nameof(random));

            Hand = new PlayerHand(prototype.StartingHand);

            var orders = prototype.Orders.Select(x => new AvailableOrder(x));
            Placement = new Placement(prototype.Size, new OrderManager(random, orders.ToArray()));
        }
    }
}
