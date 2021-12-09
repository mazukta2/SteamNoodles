using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Tests.Mocks.Prototypes.Levels
{
    public class CustomerSettings : ICustomerSettings
    {
        public int Money { get; set; } = 1;
        public int ServingTime { get; set; } = 3;

        public float TipMultiplayer { get; set; } = 2;

        public IReadOnlyCollection<ICustomerFeatureSettings> Features => _features.AsReadOnly();

        public List<ICustomerFeatureSettings> _features = new List<ICustomerFeatureSettings>();

        
        public void AddFeature(ICustomerFeatureSettings customerFeature)
        {
            _features.Add(customerFeature);
        }
    }
}
