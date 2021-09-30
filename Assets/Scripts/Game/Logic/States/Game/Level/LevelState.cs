using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.States.Game.Level
{
    public struct LevelState : IStateEntity
    {
        private State _state;
        public uint Id { get; }
        public ILevelPrototype Prototype { get; }

        public LevelState(State state, uint id, ILevelPrototype proto)
        {
            _state = state;
            Id = id;
            Prototype = proto;
        }

        public StateLink<OrdersState> CreateOrders()
        {
            var prototype = Prototype;
            var state = _state;
            return _state.Add(Add);

            OrdersState Add(State st, uint id)
            {
                return new OrdersState(state, id, prototype);
            }
        }

        public StateLink<WorkState> CreateWorks()
        {
            var prototype = Prototype;
            var state = _state;
            return _state.Add(Add);

            WorkState Add(State st, uint id)
            {
                return new WorkState(state, id);
            }
        }

        public StateLink<ConstructionsState> CreateConstructions()
        {
            var prototype = Prototype;
            var state = _state;
            return _state.Add(Add);

            ConstructionsState Add(State st, uint id)
            {
                return new ConstructionsState(state, id);
            }
        }
    }
}
