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
    public class CustomerManager : Disposable
    {
        public event Action OnCurrentCustomerChanged = delegate { };

        public ServingCustomerProcess CurrentCustomer { get; private set; }

        private readonly Placement _placement;
        private readonly SessionRandom _random;
        private readonly LevelUnits _units;
        private readonly GameTime _time;
        private readonly GameClashes _clashes;

        public CustomerManager(Placement placement, GameClashes clashes, LevelUnits units, GameTime time, SessionRandom random)
        {
            _placement = placement ?? throw new Exception(nameof(placement));
            _units = units ?? throw new Exception(nameof(units));
            _random = random ?? throw new Exception(nameof(random));
            _time = time ?? throw new Exception(nameof(time));
            _clashes = clashes ?? throw new Exception(nameof(clashes));

            _clashes.OnClashStarted += UpdateCurrentOrder;
            _clashes.OnClashEnded += UpdateCurrentOrder;
            _time.OnTimeChanged += Time_OnTimeChanged;

            UpdateCurrentOrder();
        }

        protected override void DisposeInner()
        {
            _clashes.OnClashStarted -= UpdateCurrentOrder;
            _time.OnTimeChanged -= Time_OnTimeChanged;
            _clashes.OnClashEnded -= UpdateCurrentOrder;
            CurrentCustomer?.Dispose();
        }

        public List<Unit> GetPotentialCustomers()
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
            if (CurrentCustomer != null)
            {
                if (!_clashes.IsInClash)
                {
                    CurrentCustomer.Break();
                }
                
                if (!CurrentCustomer.IsOpen)
                {
                    _units.ReturnToCrowd(CurrentCustomer.Unit);
                    CurrentCustomer.Dispose();
                    CurrentCustomer = null;
                    OnCurrentCustomerChanged();
                }
            }

            if (CurrentCustomer == null && _clashes.IsInClash)
            {
                var unit = FindNextCustumer();
                if (unit != null)
                {
                    _units.TakeFromCrowd(unit);

                    CurrentCustomer = new ServingCustomerProcess(_time, _placement, unit);
                    OnCurrentCustomerChanged();
                }
            }
        }

        private Unit FindNextCustumer()
        {
            var customers = GetPotentialCustomers();
            if (customers.Count == 0)
                return null;

            return customers[_random.GetRandom(0, customers.Count)];
        }

    }
}
