using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Assets.Scripts.Game.Logic.Models.Session;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class OrderManager
    {
        private SessionRandom _random;
        private State _state;
        private Placement _placement;
        private uint _id;
        private GameState Get() => _state.Get<GameState>(_id);
        public OrderManager(State state, uint id, SessionRandom random)
        {
            if (random == null) throw new Exception(nameof(random));
            if (state == null) throw new Exception(nameof(state));

            _state = state;
            _id = id;
            _random = random;
            UpdateCurrentOrder();
        }

        public OrderManager(State state, IOrdersPrototype orders, Placement placement, SessionRandom random)
        {
            if (random == null) throw new Exception(nameof(random));
            if (state == null) throw new Exception(nameof(state));
            _state = state;
            _placement = placement;
            _random = random;

            (_id, _) = _state.Add(new GameState(orders));

            _state.Subscribe<Construction.GameState>(UpdateConstruction, States.Events.StateEventType.Add);
            _state.Subscribe<Construction.GameState>(UpdateConstruction, States.Events.StateEventType.Remove);
            UpdateCurrentOrder();
        }
        public CurrentOrder CurrentOrder => GetCurrentOrder();

        public List<AvailableOrder> GetAvailableOrders()
        {
            var list = new List<AvailableOrder>();
            foreach (var item in GetLevelOrders())
            {
                if (item.CanBeOrder())
                    list.Add(item);
            }
            return list;
        }


        private void UpdateCurrentOrder()
        {
            if (CurrentOrder == null)
            {
                var order = FindNextOrder();
                if (order != null)
                {
                    var newOrder = new CurrentOrder(_state, order);
                    var state = Get();
                    state.CurrentOrder = newOrder.Id;
                    _state.Change(_id, state);
                }
            }
        }

        private AvailableOrder FindNextOrder()
        {
            var orders = GetAvailableOrders();
            if (orders.Count == 0)
                return null;

            return orders[_random.GetRandom(0, orders.Count)];
        }

        private CurrentOrder GetCurrentOrder()
        {
            var state = Get();
            if (state.CurrentOrder == 0)
                return null;

            return new CurrentOrder(_state, state.CurrentOrder);
        }

        private AvailableOrder[] GetLevelOrders()
        {
            return Get().Orders.Orders.Select(x => new AvailableOrder(_placement, x)).ToArray();
        }


        private void UpdateConstruction(uint id, Construction.GameState state)
        {
            UpdateCurrentOrder();
        }

        public struct GameState : IStateEntity
        {
            public uint CurrentOrder { get; set; }
            public IOrdersPrototype Orders { get; set; }

            public GameState(IOrdersPrototype orders)
            {
                CurrentOrder = 0;
                Orders = orders;
            }

        }
    }
}
