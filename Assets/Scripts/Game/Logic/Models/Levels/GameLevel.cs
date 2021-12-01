using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Clashes;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.States;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class GameLevel
    {
        private GameState _state;

        public GameLevel(ILevelSettings prototype, SessionRandom random)
        {
            if (prototype == null) throw new ArgumentNullException(nameof(prototype));
            if (random == null) throw new ArgumentNullException(nameof(random));

            _state = new GameState();
            _state.Prototype = prototype;
            _state.Time = new GameTime();
            _state.Hand = new PlayerHand(prototype.StartingHand);
            _state.Placement = new Placement(prototype, _state.Hand);
            _state.Units = new LevelUnits(_state.Placement, _state.Time, random, prototype);
            _state.Clashes = new GameClashes();
            _state.Orders = new OrderManager(prototype, Placement, Clashes, Units, Time, random);
        }

        public void Destroy()
        {
            _state.Units.Destroy();
            _state.Orders.Destroy();
        }

        public Placement Placement => _state.Placement;
        public OrderManager Orders => _state.Orders;
        public PlayerHand Hand => _state.Hand;
        public GameTime Time => _state.Time;
        public LevelUnits Units => _state.Units;
        public GameClashes Clashes => _state.Clashes;

        public class GameState : IStateEntity
        {
            public ILevelSettings Prototype { get; set; }
            public Placement Placement { get; set; }
            public OrderManager Orders { get; set; }
            public PlayerHand Hand { get; set; }
            public GameTime Time { get; set; }
            public LevelUnits Units { get; set; }
            public GameClashes Clashes { get; internal set; }
        }
    }
}
