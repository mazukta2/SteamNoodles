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
    public class UnitServingMoneyCalculator : Disposable
    {
        private SessionRandom _random;
        private Placement _placement;
        private GameLevel _level;

        public UnitServingMoneyCalculator(SessionRandom random, GameLevel level, Placement placement)
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
                return (int)(money * unit.Settings.TipMultiplayer);
            }
            return 0;
        }
    }
}
