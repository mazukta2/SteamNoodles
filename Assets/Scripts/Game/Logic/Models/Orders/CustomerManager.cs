using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Clashes;
using Game.Assets.Scripts.Game.Logic.Models.Effects.Systems;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
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

        private readonly IUnitsSettings _unitsSettings;
        private readonly Placement _placement;
        private readonly SessionRandom _random;
        private readonly LevelUnits _units;
        private readonly GameTime _time;
        private readonly GameClashes _clashes;
        private readonly GameLevel _level;
        private readonly UnitServingMoneyCalculator _moneyCalculator;
        private readonly Deck<ICustomerSettings> _pool;

        public CustomerManager(GameLevel level, IUnitsSettings unitsSettings, UnitServingMoneyCalculator moneyCalculator, Placement placement, GameClashes clashes, LevelUnits units, GameTime time, SessionRandom random)
        {
            _unitsSettings = unitsSettings ?? throw new Exception(nameof(unitsSettings));
            _placement = placement ?? throw new Exception(nameof(placement));
            _units = units ?? throw new Exception(nameof(units));
            _random = random ?? throw new Exception(nameof(random));
            _time = time ?? throw new Exception(nameof(time));
            _clashes = clashes ?? throw new Exception(nameof(clashes));
            _level = level ?? throw new Exception(nameof(level));
            _moneyCalculator = moneyCalculator ?? throw new Exception(nameof(level));

            _pool = new Deck<ICustomerSettings>(random);
            foreach (var item in _unitsSettings.Deck)
                _pool.Add(item.Key, item.Value);

            _clashes.OnClashStarted += AddOrder;
            _clashes.OnClashEnded += CancelOrder;

            if (_clashes.IsInClash)
                AddOrder();
        }

        protected override void DisposeInner()
        {
            _clashes.OnClashStarted -= AddOrder;
            _clashes.OnClashEnded -= CancelOrder;

            if (CurrentCustomer != null)
            {
                CurrentCustomer.OnDispose -= CurrentCustomer_OnFinished;
                CurrentCustomer.Dispose();
            }
        }

        public void AddCustumer(ICustomerSettings customer)
        {
            _pool.Add(customer);
        }

        public void RemoveCustomer(ICustomerSettings customer)
        {
            _pool.Remove(customer);
        }

        public Deck<ICustomerSettings> GetCustomersPool()
        {
            return _pool;
        }

        private void AddOrder()
        {
            if (CurrentCustomer != null)
                throw new Exception("Remove previous cutomer before settting");

            var unit = FindNextCustomer();
            if (unit == null)
                throw new Exception("Cant find customer");

            _units.TakeFromCrowd(unit);
            CurrentCustomer = new ServingCustomerProcess(_time, _moneyCalculator, _placement, _level, unit);
            CurrentCustomer.OnFinished += CurrentCustomer_OnFinished;
            OnCurrentCustomerChanged();
        }

        private void CancelOrder()
        {
            CurrentCustomer.OnFinished -= CurrentCustomer_OnFinished;
            _units.ReturnToCrowd(CurrentCustomer.Unit);
            CurrentCustomer.Cancel();
            CurrentCustomer.Dispose();
            CurrentCustomer = null;
            OnCurrentCustomerChanged();
        }

        private Unit FindNextCustomer()
        {
            var unitSetting = _pool.Take();
            return _units.GetUnit(unitSetting);
        }

        private void CurrentCustomer_OnFinished()
        {
            CurrentCustomer.OnFinished -= CurrentCustomer_OnFinished;
            _units.ReturnToCrowd(CurrentCustomer.Unit);
            CurrentCustomer.Dispose();
            CurrentCustomer = null;
            OnCurrentCustomerChanged();
            AddOrder();
        }


    }
}
