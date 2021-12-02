using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Clashes;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class GameLevel : Disposable
    {
        public ILevelSettings Prototype { get; private set; }
        public Placement Placement { get; private set; }
        public OrderManager Orders { get; private set; }
        public PlayerHand Hand { get; private set; }
        public GameTime Time { get; private set; }
        public LevelUnits Units { get; private set; }
        public GameClashes Clashes { get; private set; }

        public GameLevel(ILevelSettings prototype, SessionRandom random)
        {
            if (random == null) throw new ArgumentNullException(nameof(random));

            Prototype = prototype ?? throw new ArgumentNullException(nameof(prototype));
            Time = new GameTime();
            Hand = new PlayerHand(prototype.StartingHand);
            Placement = new Placement(prototype, Hand);
            Units = new LevelUnits(Placement, Time, random, prototype);
            Clashes = new GameClashes();
            Orders = new OrderManager(prototype, Placement, Clashes, Units, Time, random);
        }

        protected override void DisposeInner()
        {
            Time.Dispose();
            Hand.Dispose();
            Placement.Dispose();
            Units.Dispose();
            Clashes.Dispose();
            Orders.Dispose();
        }


    }
}
