using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.States.Game.Level
{
    public struct OrdersState : IStateEntity
    {
        private State _state;
        public uint Id { get; }
        public ILevelPrototype Prototype { get; }
        public uint CurrentOrder { get; set; }

        public OrdersState(State state, uint id, ILevelPrototype proto)
        {
            _state = state;
            Id = id;
            Prototype = proto;
            CurrentOrder = 0;
        }

        public CurrentOrder GetCurrentOrder()
        {
            if (CurrentOrder == 0)
                return null;

            return new CurrentOrder(_state.Get<OrderState>(CurrentOrder));
        }

        public AvailableOrder[] GetLevelOrders()
        {
            return Prototype.Orders.Select(x => new AvailableOrder(x)).ToArray();
        }

        public CurrentOrder SetCurrentOrder(AvailableOrder order)
        {
            var state = _state.Add((state, id) => new OrderState(state, id, order.GetPrototype()));
            return new CurrentOrder(state);
        }
    }
}
