using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Session;

namespace Tests.Assets.Scripts.Game.Logic.Models.Orders
{
    public class OrderManager
    {
        public CurrentOrder Order { get; private set; }
        private List<AvailableOrder> _levelOrders = new List<AvailableOrder>();
        private SessionRandom _random;

        public OrderManager(Session.SessionRandom random, AvailableOrder[] orders)
        {
            if (random == null) throw new Exception(nameof(random));
            if (orders == null) throw new Exception(nameof(orders));

            _random = random;
            _levelOrders = orders.ToList();
        }

        public void UpdateOrders(Construction[] constructions)
        {
            if (Order == null)
            {
                var order = FindCurrentOrder(constructions);
                if (order != null)
                    Order = order.ToCurrentOrder();
            }
        }

        private AvailableOrder FindCurrentOrder(Construction[] constructions)
        {
            var list = new List<AvailableOrder>();
            foreach (var item in _levelOrders)
            {
                if (item.CanBeOrder(constructions))
                    list.Add(item);
            }

            if (list.Count == 0)
                return null;

            return list[_random.GetRandom(0, list.Count)];
        }
    }
}
