﻿using Assets.Scripts.Logic.Prototypes.Levels;
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
        public event Action OnCustomerServed = delegate { };
        public CustomersQueue Queue { get; }
        public UnitPlacement UnitPlacement { get; }

        private readonly IUnitsSettings _unitsSettings;
        private readonly Placement _placement;
        private readonly SessionRandom _random;
        private readonly LevelUnits _units;
        private readonly GameTime _time;
        private readonly GameLevel _level;
        private readonly List<ServingCustomerProcess> _customers = new List<ServingCustomerProcess>();

        public CustomerManager(GameLevel level, IClashesSettings clashesSettings, IUnitsSettings unitsSettings, Placement placement, LevelUnits units, GameTime time, SessionRandom random)
        {
            _unitsSettings = unitsSettings ?? throw new Exception(nameof(unitsSettings));
            _placement = placement ?? throw new Exception(nameof(placement));
            _units = units ?? throw new Exception(nameof(units));
            _random = random ?? throw new Exception(nameof(random));
            _time = time ?? throw new Exception(nameof(time));
            _level = level ?? throw new Exception(nameof(level));

            UnitPlacement = new UnitPlacement(placement);
            Queue = new CustomersQueue(clashesSettings, _unitsSettings, _level, UnitPlacement, units, time, _random);
            Queue.OnNewCustomerInQueue += Queue_OnNewCustomerInQueue;
            Queue.OnNewCustomerIsMovingToQueue += Queue_OnNewCustomerIsMovingToQueue;
            
            _units.OnPotentialUnitAdded += AddPotentialCustumer;
            _units.OnPotentialUnitRemoved += RemovePotentialCustomer;

            foreach (var item in _units.GetFullPool())
                for (int i = 0; i < item.Value; i++)
                    AddPotentialCustumer(item.Key);

            TryToMoveAQueue();
        }

        protected override void DisposeInner()
        {
            _units.OnPotentialUnitAdded -= AddPotentialCustumer;
            _units.OnPotentialUnitRemoved -= RemovePotentialCustomer;
            Queue.OnNewCustomerInQueue -= Queue_OnNewCustomerInQueue;
            Queue.OnNewCustomerIsMovingToQueue -= Queue_OnNewCustomerIsMovingToQueue;
            Queue.Dispose();
            UnitPlacement.Dispose();

            foreach (var item in _customers.ToList())
                RemoveCustomer(item);
        }

        public void AbortClash()
        {
            foreach (var item in _customers.ToList())
            {
                if (item.CurrentPhase == ServingCustomerProcess.Phase.WaitCooking || item.CurrentPhase == ServingCustomerProcess.Phase.Eating)
                {
                    item.CancelWithReturns();
                }

                RemoveCustomer(item);
            }
        }

        public ReadOnlyCollection<ServingCustomerProcess> GetCustomers()
        {
            return _customers.AsReadOnly();
        }

        public void AddPotentialCustumer(ICustomerSettings customer)
        {
            Queue.AddPotentialCustumer(customer);
        }

        public void RemovePotentialCustomer(ICustomerSettings customer)
        {
            Queue.RemovePotentialCustomer(customer);
        }

        public IReadOnlyCollection<ICustomerSettings> GetCustomersPool()
        {
            return Queue.GetCustomersPool();
        }

        private void TryToMoveAQueue()
        {
            if (UnitPlacement.IsAnybodyPlacedTo(UnitPlacement.GetOrderingPlace()))
                return;

            var unit = Queue.Take();
            if (unit == null)
                return;

            if (!_customers.Contains(unit))
                throw new Exception("NotExisting unit");
            if (unit.CurrentPhase != ServingCustomerProcess.Phase.InQueue)
                throw new Exception("Wrong state unit");

            unit.MoveToOdering();
        }

        private void ServingCustomer_OnStartEating(ServingCustomerProcess servingCustomerProcess)
        {
            TryToMoveAQueue();
        }

        private void Item_OnWaitCooking(ServingCustomerProcess obj)
        {
            TryToMoveAQueue();
        }

        private void Queue_OnNewCustomerInQueue(ServingCustomerProcess obj)
        {
            TryToMoveAQueue();
        }

        private void Queue_OnNewCustomerIsMovingToQueue(ServingCustomerProcess unit)
        {
            AddCustomer(unit);
        }


        private void Customer_OnFinished(ServingCustomerProcess unit)
        {
            RemoveCustomer(unit);
            TryToMoveAQueue();
        }

        private void AddCustomer(ServingCustomerProcess unit)
        {
            unit.OnWaitCooking += Item_OnWaitCooking;
            unit.OnStartEating += ServingCustomer_OnStartEating;
            unit.OnFinished += Customer_OnFinished;
            _customers.Add(unit);
        }

        private void RemoveCustomer(ServingCustomerProcess unit)
        {
            unit.OnWaitCooking -= Item_OnWaitCooking;
            unit.OnStartEating -= ServingCustomer_OnStartEating;
            unit.OnFinished -= Customer_OnFinished;
            unit.Dispose();
            _customers.Remove(unit);
        }
    }
}
