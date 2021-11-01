using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Clashes;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
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
        public OrderManager(IOrdersPrototype orders, Placement placement, GameClashes clashes, LevelUnits units, GameTime time, SessionRandom random)
        {
            if (random == null) throw new Exception(nameof(random));
            if (orders == null) throw new Exception(nameof(orders));
            if (placement == null) throw new Exception(nameof(placement));
            if (units == null) throw new Exception(nameof(units));

            _state = new GameState();
            _state.Prototype = orders;
            _state.Placement = placement;
            _state.Units = units;
            _state.Random = random;
            _state.Time = time;
            _state.Clashes = clashes;

            _state.Time.OnTimeChanged += Time_OnTimeChanged;

            UpdateCurrentOrder();
        }

        public void Destroy()
        {
            _state.Time.OnTimeChanged -= Time_OnTimeChanged;
        }

        public ServingOrderProcess CurrentOrder => _state.CurrentOrder;

        public List<Unit> GetPotentialCustumers()
        {
            var listOfUnits = _state.Units.Units.OrderBy(u => Math.Abs(u.Position.X));
            var list = new List<Unit>();
            foreach (var item in listOfUnits)
            {
                if (item.CanOrder())
                    list.Add(item);

                if (list.Count > 5) // on
                    break;
            }
            return list;
        }

        private void Time_OnTimeChanged(float obj)
        {
            UpdateCurrentOrder();
        }

        private void UpdateCurrentOrder()
        {
            if (CurrentOrder != null)
            {
                if (!_state.Clashes.IsClashStarted)
                {
                    _state.CurrentOrder.Break();
                }
                
                if (!CurrentOrder.IsOpen)
                {
                    _state.Units.ReturnToCrowd(_state.CurrentOrder.Unit);
                    _state.CurrentOrder = null;
                    OnCurrentOrderChanged();
                }
            }

            if (CurrentOrder == null && _state.Clashes.IsClashStarted)
            {
                var unit = FindNextCustumer();
                if (unit != null)
                {
                    _state.Units.TakeFromCrowd(unit);

                    _state.CurrentOrder = new ServingOrderProcess(_state.Placement, unit);
                    OnCurrentOrderChanged();
                }
            }
        }

        private Unit FindNextCustumer()
        {
            var orders = GetPotentialCustumers();
            if (orders.Count == 0)
                return null;

            return orders[_state.Random.GetRandom(0, orders.Count)];
        }

        private struct GameState : IStateEntity
        {
            public ServingOrderProcess CurrentOrder { get; set; }
            public IOrdersPrototype Orders { get; set; }
            public IOrdersPrototype Prototype { get; internal set; }
            public Placement Placement { get; internal set; }
            public SessionRandom Random { get; internal set; }
            public LevelUnits Units { get; internal set; }
            public GameTime Time { get; internal set; }
            public GameClashes Clashes { get; internal set; }
        }
    }
}
