using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Tests.Mocks.Settings.Buildings.Features
{
    public class MoneyForConstructionCustomerFeatureSettings : IMoneyForConstructionCustomerFeatureSettings
    {
        public int Money { get; set; }
        public IConstructionSettings Construction { get;  set; }

        public MoneyForConstructionCustomerFeatureSettings(IConstructionSettings construction)
        {
            Construction = construction;
        }
    }
}
