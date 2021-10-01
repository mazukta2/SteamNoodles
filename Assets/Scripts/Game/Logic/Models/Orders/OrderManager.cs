﻿using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
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

        public OrderManager(State state, IOrdersPrototype orders, SessionRandom random)
        {
            if (random == null) throw new Exception(nameof(random));
            if (state == null) throw new Exception(nameof(state));
            _state = state;
            _random = random;

            _state.Add(new GameState(orders));

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
                    _state.Change<GameState>(_id, x => x.CurrentOrder = newOrder.Id);
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
            return Get().Orders.Orders.Select(x => new AvailableOrder(x)).ToArray();
        }

        //private void OnOrderClosed(CurrentOrderClosedEvent evnt)
        //{
        //    if (CurrentOrder.IsOpen())
        //        return;

        //    CurrentOrder = null;
        //    UpdateCurrentOrder();
        //}

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
