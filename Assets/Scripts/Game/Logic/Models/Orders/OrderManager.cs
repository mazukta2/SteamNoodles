using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Clashes;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class OrderManager : Disposable
    {
        public event Action OnCurrentOrderChanged = delegate { };

        public ServingOrderProcess CurrentOrder { get; private set; }
        private IOrdersPrototype _orders;
        private IOrdersPrototype _prototype;
        private Placement _placement;
        private SessionRandom _random;
        private LevelUnits _units;
        private GameTime _time;
        private GameClashes _clashes;

        public OrderManager(IOrdersPrototype orders, Placement placement, GameClashes clashes, LevelUnits units, GameTime time, SessionRandom random)
        {
            if (random == null) throw new Exception(nameof(random));
            if (orders == null) throw new Exception(nameof(orders));
            if (placement == null) throw new Exception(nameof(placement));
            if (units == null) throw new Exception(nameof(units));

            _prototype = orders;
            _placement = placement;
            _units = units;
            _random = random;
            _time = time;
            _clashes = clashes;

            _time.OnTimeChanged += Time_OnTimeChanged;

            UpdateCurrentOrder();
        }

        protected override void DisposeInner()
        {
            _time.OnTimeChanged -= Time_OnTimeChanged;
            CurrentOrder?.Dispose();
        }

        public List<Unit> GetPotentialCustumers()
        {
            var listOfUnits = _units.Units.OrderBy(u => Math.Abs(u.Position.X));
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
                if (!_clashes.IsClashStarted)
                {
                    CurrentOrder.Break();
                }
                
                if (!CurrentOrder.IsOpen)
                {
                    _units.ReturnToCrowd(CurrentOrder.Unit);
                    CurrentOrder.Dispose();
                    CurrentOrder = null;
                    OnCurrentOrderChanged();
                }
            }

            if (CurrentOrder == null && _clashes.IsClashStarted)
            {
                var unit = FindNextCustumer();
                if (unit != null)
                {
                    _units.TakeFromCrowd(unit);

                    CurrentOrder = new ServingOrderProcess(_time, _placement, unit);
                    OnCurrentOrderChanged();
                }
            }
        }

        private Unit FindNextCustumer()
        {
            var orders = GetPotentialCustumers();
            if (orders.Count == 0)
                return null;

            return orders[_random.GetRandom(0, orders.Count)];
        }

    }
}
