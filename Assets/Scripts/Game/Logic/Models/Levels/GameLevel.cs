using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Workers;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.States;
using System;
using Tests.Assets.Scripts.Game.Logic.Models.Session;

namespace Assets.Scripts.Logic.Models.Levels
{
    public class GameLevel
    {
        public Placement Placement { get; }
        public OrderManager Orders { get; }
        public WorkManager Work { get; }
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

            Time = new TimeManager();
            Hand = new PlayerHand(prototype.StartingHand);

            Orders = new OrderManager(State, _prototype, random);
            Work = new WorkManager(State);
            Placement = new Placement(State, prototype.Size);
        }

        //public struct LevelState : IStateEntity
        //{
        //    public ILevelPrototype Prototype { get; }

        //    public LevelState(ILevelPrototype proto)
        //    {
        //        Prototype = proto;
        //    }

        //}
    }
}
