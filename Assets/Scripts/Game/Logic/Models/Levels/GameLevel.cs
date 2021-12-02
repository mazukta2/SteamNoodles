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
        public LevelUnits Units { get; private set; }
        public GameClashes Clashes { get; private set; }

        public GameLevel(ILevelSettings settings, SessionRandom random, GameTime time)
        {
            if (random == null) throw new ArgumentNullException(nameof(random));
            if (time == null) throw new ArgumentNullException(nameof(time));

            Prototype = settings ?? throw new ArgumentNullException(nameof(settings));

            Hand = new PlayerHand(settings.StartingHand);
            Placement = new Placement(settings, Hand);
            Units = new LevelUnits(Placement, time, random, settings);
            Clashes = new GameClashes(settings, time);
            Orders = new OrderManager(settings, Placement, Clashes, Units, time, random);
        }

        protected override void DisposeInner()
        {
            Hand.Dispose();
            Placement.Dispose();
            Units.Dispose();
            Clashes.Dispose();
            Orders.Dispose();
        }


    }
}
