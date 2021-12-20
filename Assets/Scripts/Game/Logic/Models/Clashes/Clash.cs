using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Rewards;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Clashes
{
    public class Clash : Disposable
    {
        public event Action OnClashStoped = delegate { };
        public int NeedToServe => _settings.NeedToServe;
        public int Served { get; private set; }
        public CustomerManager Customers { get; private set; }

        private readonly RewardCalculator _rewardCalculator;
        private readonly IClashesSettings _settings;

        public Clash(GameClashes clashes, GameLevel level, IClashesSettings settings, 
            IUnitsSettings unitsSettings, Placement placement, 
            RewardCalculator rewardCalculator, LevelUnits units, GameTime time, SessionRandom random)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _rewardCalculator = rewardCalculator ?? throw new ArgumentNullException(nameof(rewardCalculator));
            Customers = new CustomerManager(level, settings, unitsSettings, placement, clashes, units, time, random);
            Customers.OnCustomerServed += _customerManager_OnCustomerServed;
        }

        protected override void DisposeInner()
        {
            Customers.OnCustomerServed -= _customerManager_OnCustomerServed;
            Customers.Dispose();
        }

        public void Stop()
        {
            _rewardCalculator.Give(_settings.ClashReward);
            OnClashStoped();
        }

        private void _customerManager_OnCustomerServed()
        {
            Served++;
            if (Served >= NeedToServe)
                Stop();
        }
    }
}
