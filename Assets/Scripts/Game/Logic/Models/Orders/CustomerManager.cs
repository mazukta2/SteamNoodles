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
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class CustomerManager : Disposable
    {
        public event Action OnCurrentCustomerChanged = delegate { };

        public ServingCustomerProcess ServingCustomer { get; private set; }

        private readonly IUnitsSettings _unitsSettings;
        private readonly Placement _placement;
        private readonly SessionRandom _random;
        private readonly LevelUnits _units;
        private readonly GameTime _time;
        private readonly GameClashes _clashes;
        private readonly GameLevel _level;
        private readonly Deck<ICustomerSettings> _pool;
        private readonly Dictionary<ServingCustomerProcess, Construction> _tables = new Dictionary<ServingCustomerProcess, Construction>();
        private readonly List<ServingCustomerProcess> _customers = new List<ServingCustomerProcess>();

        public CustomerManager(GameLevel level, IUnitsSettings unitsSettings, Placement placement, GameClashes clashes, LevelUnits units, GameTime time, SessionRandom random)
        {
            _unitsSettings = unitsSettings ?? throw new Exception(nameof(unitsSettings));
            _placement = placement ?? throw new Exception(nameof(placement));
            _units = units ?? throw new Exception(nameof(units));
            _random = random ?? throw new Exception(nameof(random));
            _time = time ?? throw new Exception(nameof(time));
            _clashes = clashes ?? throw new Exception(nameof(clashes));
            _level = level ?? throw new Exception(nameof(level));

            _pool = new Deck<ICustomerSettings>(random);
            foreach (var item in _unitsSettings.Deck)
                _pool.Add(item.Key, item.Value);

            _clashes.OnClashStarted += TryAddOrder;
            _clashes.OnClashEnded += CancelOrder;

            if (_clashes.IsInClash)
                TryAddOrder();
        }

        protected override void DisposeInner()
        {
            _clashes.OnClashStarted -= TryAddOrder;
            _clashes.OnClashEnded -= CancelOrder;

            foreach (var item in _customers)
            {
                item.OnFinished -= Customer_OnFinished;
                item.OnStartEating -= ServingCustomer_OnStartEating;
                item.Dispose();
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

        private void TryAddOrder()
        {
            if (IsOccupied(GetOrderingPlace()))
                return;

            if (ServingCustomer != null)
                return;

            var unit = FindNextCustomer();
            if (unit == null)
                throw new Exception("Cant find customer");

            _units.TakeFromCrowd(unit);
            ServingCustomer = new ServingCustomerProcess(this, _time, _placement, _level, unit);
            ServingCustomer.OnFinished += Customer_OnFinished;
            ServingCustomer.OnStartEating += ServingCustomer_OnStartEating;
            _customers.Add(ServingCustomer);
            OnCurrentCustomerChanged();
        }

        public void ClearPlacing(ServingCustomerProcess servingCustomerProcess)
        {
            _tables.Remove(servingCustomerProcess);
            TryAddOrder();
        }

        public void Occupy(ServingCustomerProcess servingCustomerProcess, Construction construction)
        {
            _tables.Add(servingCustomerProcess, construction);
        }

        public bool IsOccupied(Construction construction)
        {
            var ordering = construction.GetFeatures().OfType<IOrderingPlaceConstructionFeatureSettings>().FirstOrDefault();
            if (ordering != null && ServingCustomer != null && ServingCustomer.CurrentPhase == ServingCustomerProcess.Phase.Ordering)
                return true;

            if (_tables.Values.Contains(construction))
            {
                return true;
            }

            return false;
        }

        private void CancelOrder()
        {
            if (ServingCustomer != null)
                ServingCustomer.Cancel();
            foreach (var item in _customers)
            {
                item.OnFinished -= Customer_OnFinished;
                item.OnStartEating -= ServingCustomer_OnStartEating;
                item.Dispose();
                _units.ReturnToCrowd(item.Unit);
            }
            ServingCustomer = null;
            OnCurrentCustomerChanged();
        }

        public ReadOnlyCollection<Construction> GetFreePlacesToEat()
        {
           return _placement.Constructions.Where(x => x.GetFeatures().OfType<IPlaceToEatConstructionFeatureSettings>().Any() && !IsOccupied(x)).ToList().AsReadOnly();
        }

        public Construction GetOrderingPlace()
        {
            return _placement.Constructions.First(x => x.GetFeatures().OfType<IOrderingPlaceConstructionFeatureSettings>().Any());
        }

        private Unit FindNextCustomer()
        {
            var unitSetting = _pool.Take();
            return _units.GetUnit(unitSetting);
        }

        private void Customer_OnFinished(ServingCustomerProcess servingCustomerProcess)
        {
            servingCustomerProcess.OnStartEating -= ServingCustomer_OnStartEating;
            servingCustomerProcess.OnFinished -= Customer_OnFinished;
            _units.ReturnToCrowd(servingCustomerProcess.Unit);
            servingCustomerProcess.Dispose();
            _customers.Remove(servingCustomerProcess);

            TryAddOrder();
        }

        private void ServingCustomer_OnStartEating(ServingCustomerProcess servingCustomerProcess)
        {
            ServingCustomer = null;
            OnCurrentCustomerChanged();
            TryAddOrder();
        }

    }
}
