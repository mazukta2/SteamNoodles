using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Effects.Systems
{
    public class UnitServicing : Disposable
    {
        private SessionRandom _random;
        private Placement _placement;
        private GameLevel _level;

        public UnitServicing(SessionRandom random, GameLevel level, Placement placement)
        {
            _random = random;
            _placement = placement;
            _level = level;
        }

        protected override void DisposeInner()
        {
        }

        public int GetServingMoney(Unit unit)
        {
            var settings = unit.Settings;
            var money = settings.Money;

            foreach (var item in settings.Features.OfType<IMoneyForConstructionCustomerFeatureSettings>())
            {
                var count = _placement.Constructions.Where(x => x.GetSettings() == item.Construction).Count();
                money += count * item.Money;
            }

            return money;
        }

        public int GetTips(Unit unit)
        {
            var settings = unit.Settings;
            var money = settings.Money;

            var chance = _random.GetRandom(0, 100);
            if (chance < _level.Service)
            {
                var calculator = new PercentCalculator();
                foreach (var item in settings.Features.OfType<ITipsForConstructionTagCustomerFeatureSettings>())
                {
                    var count = _placement.Constructions.Sum(x => x.GetTagsCount(item.Tag));
                    if (count > 0)
                        calculator.Add(item.TipModificator, count);
                }
                var tipModificator = calculator.GetFor(unit.Settings.BaseTipMultiplayer, 0);
                return (int)(money * tipModificator);
            }
            return 0;
        }

        public float GetEatingTime(Unit unit)
        {
            var calculator = new PercentCalculator();

            foreach (var item in unit.Settings.Features.OfType<IEatingSpeedFeatureSettings>())
            {
                calculator.Add(item.TimeModificator);
            }

            return calculator.GetFor(unit.Settings.EatingTime, 1f);
        }
    }
}
