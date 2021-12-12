using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Tests.Mocks.Settings.Customers.Features
{
    public class TipsForConstructionTagCustomerFeatureSettings : ITipsForConstructionTagCustomerFeatureSettings
    {
        public PercentModificator TipModificator { get; set; } = new PercentModificator(PercentModificator.ActionType.Add, 25f);
        public ConstructionTag Tag { get; set; } = ConstructionTag.Machine;
    }
}
