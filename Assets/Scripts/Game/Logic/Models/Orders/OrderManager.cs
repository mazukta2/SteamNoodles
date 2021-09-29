using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;
using Game.Assets.Scripts.Game.Logic.Models.Events.GameEvents;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.Models.Session;

namespace Tests.Assets.Scripts.Game.Logic.Models.Orders
{
    public class OrderManager
    {
        private List<AvailableOrder> _levelOrders = new List<AvailableOrder>();
        private GameLevel _level;
        private SessionRandom _random;

        public History History { get; } = new History();

        public OrderManager(GameLevel level, Session.SessionRandom random, AvailableOrder[] orders)
        {
            if (random == null) throw new Exception(nameof(random));
            if (orders == null) throw new Exception(nameof(orders));

            _level = level;
            _random = random;
            _levelOrders = orders.ToList();
        }

        public CurrentOrder CurrentOrder { get; private set; }

        private HistoryReader _orderReader;

        public void TryGetOrder()
        {
            if (CurrentOrder == null)
            {
                var order = FindCurrentOrder();
                if (order != null)
                {
                    CurrentOrder = order.ToCurrentOrder();
                    _orderReader?.Dispose();
                    _orderReader = new HistoryReader(CurrentOrder.History);
                    _orderReader.Subscribe<CurrentOrderClosedEvent>(OnOrderClosed);
                    _level.WorkManager.HandleOrder();

                    History.Add(new CurrentOrderCreatedEvent(CurrentOrder));
                }
            }
        }

        public List<AvailableOrder> GetAvailableOrders()
        {
            var list = new List<AvailableOrder>();
            foreach (var item in _levelOrders)
            {
                if (item.CanBeOrder(_level.Placement.Constructions.ToArray()))
                    list.Add(item);
            }
            return list;
        }

        private AvailableOrder FindCurrentOrder()
        {
            var orders = GetAvailableOrders();
            if (orders.Count == 0)
                return null;

            return orders[_random.GetRandom(0, orders.Count)];
        }

        private void OnOrderClosed(CurrentOrderClosedEvent evnt)
        {
            if (CurrentOrder.IsOpen())
                return;

            CurrentOrder = null;
            TryGetOrder();
        }

    }
}
