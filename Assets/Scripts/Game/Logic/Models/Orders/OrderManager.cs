using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class OrderManager
    {
        public event Action OnCurrentOrderChanged = delegate { };
        private GameState _state;
        public OrderManager(IOrdersPrototype orders, Placement placement, SessionRandom random)
        {
            if (random == null) throw new Exception(nameof(random));
            if (orders == null) throw new Exception(nameof(orders));
            if (placement == null) throw new Exception(nameof(placement));

            _state = new GameState();
            _state.Prototype = orders;
            _state.Placement = placement;
            _state.Random = random;

            _state.Placement.OnConstructionAdded += (construction) => UpdateCurrentOrder();
            _state.Placement.OnConstructionRemoved += (construction) => UpdateCurrentOrder();
            
            UpdateCurrentOrder();
        }

        public ActiveOrder CurrentOrder => _state.CurrentOrder;

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
            if (CurrentOrder != null && !CurrentOrder.IsOpen())
            {
                _state.CurrentOrder = null;

                OnCurrentOrderChanged();
            }

            if (CurrentOrder == null)
            {
                var order = FindNextOrder();
                if (order != null)
                {
                    _state.CurrentOrder = new ActiveOrder(order);
                    _state.CurrentOrder.OnComplited += UpdateCurrentOrder;
                    OnCurrentOrderChanged();
                }
            }
        }

        private AvailableOrder FindNextOrder()
        {
            var orders = GetAvailableOrders();
            if (orders.Count == 0)
                return null;

            return orders[_state.Random.GetRandom(0, orders.Count)];
        }

        private AvailableOrder[] GetLevelOrders()
        {
            return _state.Prototype.Orders.Select(x => new AvailableOrder(_state.Placement, x)).ToArray();
        }

        private struct GameState : IStateEntity
        {
            public ActiveOrder CurrentOrder { get; set; }
            public IOrdersPrototype Orders { get; set; }
            public IOrdersPrototype Prototype { get; internal set; }
            public Placement Placement { get; internal set; }
            public SessionRandom Random { get; internal set; }
        }
    }
}
