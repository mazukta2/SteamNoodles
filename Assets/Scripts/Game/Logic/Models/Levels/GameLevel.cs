﻿using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Clashes;
using Game.Assets.Scripts.Game.Logic.Models.Effects;
using Game.Assets.Scripts.Game.Logic.Models.Effects.Systems;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class GameLevel : Disposable
    {
        public event Action OnMoneyChanged = delegate { };

        public int Money { get; private set; }
        public Placement Placement { get; private set; }
        public CustomerManager Customers { get; private set; }

        public PlayerHand Hand { get; private set; }
        public LevelUnits Units { get; private set; }
        public GameClashes Clashes { get; private set; }

        private List<IEffectSystem> _effectSystems = new List<IEffectSystem>();

        private UnitServingMoneyCalculator _servingMoney;
        private ILevelSettings _settings;

        public GameLevel(ILevelSettings settings, SessionRandom random, GameTime time)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            if (random == null) throw new ArgumentNullException(nameof(random));
            if (time == null) throw new ArgumentNullException(nameof(time));

            Hand = new PlayerHand(settings.StartingHand);
            Placement = new Placement(settings, Hand);
            Units = new LevelUnits(settings, Placement, time, random, settings);
            Clashes = new GameClashes(settings, time);
            _servingMoney = new UnitServingMoneyCalculator(Placement);
            Customers = new CustomerManager(this, settings, _servingMoney, Placement, Clashes, Units, time, random);

            AddEffectSystems();
        }

        protected override void DisposeInner()
        {
            RemoveEffectSystems();

            Hand.Dispose();
            Placement.Dispose();
            Units.Dispose();
            Clashes.Dispose();
            Customers.Dispose();
            _servingMoney.Dispose();
        }

        private void AddEffectSystems()
        {
            _effectSystems.Add(new UnitPoolEffectSystem(this, Placement, Units, Customers));
        }

        private void RemoveEffectSystems()
        {
            foreach (var effect in _effectSystems)
                effect.Dispose();
        }

        public void AddCustumer(ICustomerSettings customer)
        {
            Units.AddCustumer(customer);
            Customers.AddCustumer(customer);
        }

        public void RemoveCustomer(ICustomerSettings customer)
        {
            Units.RemoveCustomer(customer);
            Customers.RemoveCustomer(customer);
        }

        public void ChangeMoney(int value)
        {
            Money += value;
            OnMoneyChanged();
        }
    }
}
