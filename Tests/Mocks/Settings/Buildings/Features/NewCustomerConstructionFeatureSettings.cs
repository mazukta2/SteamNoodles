using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Tests.Mocks.Settings.Buildings.Features
{
    public class NewCustomerConstructionFeatureSettings : INewCustomerConstructionFeatureSettings
    {
        public ICustomerSettings Customer { get; private set; }

        public NewCustomerConstructionFeatureSettings(ICustomerSettings customer)
        {
            Customer = customer;
        }
    }
}
