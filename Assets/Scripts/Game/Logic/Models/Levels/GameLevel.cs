using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Workers;
using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.States.Game.Level;
using System;
using System.Linq;
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

        private ILevelPrototype _prototype;

        public State State { get; }

        public GameLevel(ILevelPrototype prototype, SessionRandom random)
        {
            if (prototype == null) throw new ArgumentNullException(nameof(prototype));
            if (random == null) throw new ArgumentNullException(nameof(random));

            _prototype = prototype;
            State = new State();
            FillNewState();

            Time = new TimeManager();
            Hand = new PlayerHand(prototype.StartingHand);
            Placement = new Placement(State.Get<ConstructionsState>(), prototype.Size);
            Orders = new OrderManager(State.Get<OrdersState>(), random);
            WorkManager = new WorkManager(State.Get<WorkState>());
        }

        public void FillNewState()
        {
            var level = State.Add((state, id) => new LevelState(state, id, _prototype));
            var orders = level.Get().CreateOrders();
            var work = level.Get().CreateWorks();
            var constructions = level.Get().CreateConstructions();
        }
    }
}
