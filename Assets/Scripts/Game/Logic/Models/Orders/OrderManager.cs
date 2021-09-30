using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.States.Game.Level;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Models.Session;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class OrderManager
    {
        private StateLink<OrdersState> _state;
        private SessionRandom _random;

        public OrderManager(StateLink<OrdersState> state, SessionRandom random)
        {
            if (random == null) throw new Exception(nameof(random));
            if (state == null) throw new Exception(nameof(state));

            _state = state;
            _random = random;
            UpdateCurrentOrder();
        }

        public CurrentOrder CurrentOrder => _state.Get().GetCurrentOrder();

        public List<AvailableOrder> GetAvailableOrders()
        {
            var list = new List<AvailableOrder>();
            foreach (var item in _state.Get().GetLevelOrders())
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
                    _state.Change(x => x.SetCurrentOrder(order));
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

        //private void OnOrderClosed(CurrentOrderClosedEvent evnt)
        //{
        //    if (CurrentOrder.IsOpen())
        //        return;

        //    CurrentOrder = null;
        //    UpdateCurrentOrder();
        //}

    }
}
