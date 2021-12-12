using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Tests.Mocks.Settings.Customers.Features
{
    public class EatingSpeedFeatureSettings : IEatingSpeedFeatureSettings
    {
        public PercentModificator TimeModificator { get; set; } = new PercentModificator(PercentModificator.ActionType.Add, 100);
    }
}
