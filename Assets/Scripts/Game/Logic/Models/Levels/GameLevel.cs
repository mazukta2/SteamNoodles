using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Clashes;
using Game.Assets.Scripts.Game.Logic.Models.Effects;
using Game.Assets.Scripts.Game.Logic.Models.Effects.Systems;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Rewards;
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

        private RewardCalculator _rewardCalculator;

        public PlayerHand Hand { get; private set; }
        public LevelUnits Units { get; private set; }
        public GameClashes Clashes { get; private set; }
        public int Service => Placement.GetConstructionsWithTag(ConstructionTag.Service);

        private List<IEffectSystem> _effectSystems = new List<IEffectSystem>();

        private UnitServicing _servingMoney;
        private ILevelSettings _settings;

        public GameLevel(ILevelSettings settings, SessionRandom random, GameTime time)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            if (random == null) throw new ArgumentNullException(nameof(random));
            if (time == null) throw new ArgumentNullException(nameof(time));

            _rewardCalculator = new RewardCalculator(this, random);
            Hand = new PlayerHand(settings);
            Placement = new Placement(settings, Hand, _rewardCalculator);
            _servingMoney = new UnitServicing(random, this, Placement);
            Units = new LevelUnits(settings, Placement, time, random, settings, _servingMoney);
            Clashes = new GameClashes(settings, time, _rewardCalculator);
            Customers = new CustomerManager(this, settings, Placement, Clashes, Units, time, random);

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
            _rewardCalculator.Dispose();
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
