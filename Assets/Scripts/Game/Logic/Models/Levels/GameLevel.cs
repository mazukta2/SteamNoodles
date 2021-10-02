using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Workers;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.States;
using System;

namespace Assets.Scripts.Logic.Models.Levels
{
    public class GameLevel
    {
        private GameState _state;

        public GameLevel(ILevelPrototype prototype, SessionRandom random)
        {
            if (prototype == null) throw new ArgumentNullException(nameof(prototype));
            if (random == null) throw new ArgumentNullException(nameof(random));

            _state = new GameState();
            _state.Prototype = prototype;
            _state.Time = new GameTime();
            _state.Hand = new PlayerHand(prototype.StartingHand);
            _state.Placement = new Placement(prototype);
            _state.Orders = new OrderManager(prototype, Placement, random);
            _state.Work = new WorkManager(Orders, Placement, Time);
        }

        public Placement Placement => _state.Placement;
        public OrderManager Orders => _state.Orders;
        public WorkManager Work => _state.Work;
        public PlayerHand Hand => _state.Hand;
        public GameTime Time => _state.Time;

        public class GameState : IStateEntity
        {
            public ILevelPrototype Prototype { get; set; }
            public Placement Placement { get; set; }
            public OrderManager Orders { get; set; }
            public WorkManager Work { get; set; }
            public PlayerHand Hand { get; set; }
            public GameTime Time { get; set; }
        }
    }
}
