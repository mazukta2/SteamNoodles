using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class CustomersQueue : Disposable
    {
        public event Action<ServingCustomerProcess> OnNewCustomerIsMovingToQueue = delegate { };
        public event Action<ServingCustomerProcess> OnNewCustomerInQueue = delegate { };

        public int TargetCount { get; private set; }
        public int ActualCount => _customers.Count;

        private readonly IClashesSettings _clashesSettings;
        private readonly LevelUnits _units;
        private readonly SessionRandom _random;
        private readonly GameTime _time;
        private readonly UnitPlacement _placement;
        private readonly GameLevel _level;
        private readonly List<ServingCustomerProcess> _customers = new List<ServingCustomerProcess>();
        private readonly Deck<ICustomerSettings> _pool;

        public CustomersQueue(IClashesSettings clashesSettings, IUnitsSettings unitSettings, GameLevel level, UnitPlacement placement, LevelUnits units, GameTime time, SessionRandom random)
        {
            _clashesSettings = clashesSettings;
            _units = units;
            _random = random;
            _time = time;
            _placement = placement;
            _level = level;

            _pool = new Deck<ICustomerSettings>(random);
            foreach (var item in unitSettings.Deck)
                _pool.Add(item.Key, item.Value);

            RefreshTargetCount();
        }

        protected override void DisposeInner()
        {
            foreach (var item in _customers)
                item.OnJoinQueue -= Process_OnJoinQueue;
            _customers.Clear();
        }

        private void RefreshTargetCount()
        {
            TargetCount = _random.GetRandom(_clashesSettings.MinQueue, _clashesSettings.MaxQueue);
        }

        public void AddPotentialCustumer(ICustomerSettings customer)
        {
            _pool.Add(customer);
        }

        public void RemovePotentialCustomer(ICustomerSettings customer)
        {
            _pool.Remove(customer);
        }

        public IReadOnlyCollection<ICustomerSettings> GetCustomersPool()
        {
            return _pool.GetItemsList();
        }

        public void CancelEverithing()
        {
            foreach (var item in _customers)
                item.OnJoinQueue -= Process_OnJoinQueue;
            _customers.Clear();
        }
        public ServingCustomerProcess Take()
        {
            var queue = _customers.Where(x => x.CurrentPhase == ServingCustomerProcess.Phase.InQueue);

            if (queue.Count() == 0)
                return null;

            var item = queue.First();
            item.OnJoinQueue -= Process_OnJoinQueue;
            _customers.Remove(item);
            return item;
        }

        public void Add()
        {
            var unit = FindNextCustomer();
            if (unit == null)
                throw new Exception("Cant find customer");

            var process = new ServingCustomerProcess(_placement, _time, _level, _units, unit);
            process.OnJoinQueue += Process_OnJoinQueue;
            _customers.Add(process);
            OnNewCustomerIsMovingToQueue(process);
        }

        private void Process_OnJoinQueue(ServingCustomerProcess obj)
        {
            OnNewCustomerInQueue(obj);
        }

        private Unit FindNextCustomer()
        {
            var unitSetting = _pool.Take();
            return _units.GetUnit(unitSetting);
        }

    }
}
