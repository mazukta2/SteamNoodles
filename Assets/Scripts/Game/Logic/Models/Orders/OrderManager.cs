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
        //private readonly IOrdersPrototype _prototype;
        private readonly Placement _placement;
        private readonly SessionRandom _random;
        private readonly LevelUnits _units;
        private readonly GameTime _time;
        private readonly GameClashes _clashes;

        public OrderManager(IOrdersSettings orders, Placement placement, GameClashes clashes, LevelUnits units, GameTime time, SessionRandom random)
        {
            //if (orders == null) throw new Exception(nameof(orders));

            //_prototype = orders;
            _placement = placement ?? throw new Exception(nameof(placement));
            _units = units ?? throw new Exception(nameof(units));
            _random = random ?? throw new Exception(nameof(random));
            _time = time;
            _clashes = clashes;

            _clashes.OnClashStarted += UpdateCurrentOrder;
            _time.OnTimeChanged += Time_OnTimeChanged;

            UpdateCurrentOrder();
        }

        protected override void DisposeInner()
        {
            _clashes.OnClashStarted -= UpdateCurrentOrder;
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
                if (!_clashes.IsInClash)
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

            if (CurrentOrder == null && _clashes.IsInClash)
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
