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

        public float BaseTipMultiplayer { get; set; } = 1;

        public IReadOnlyCollection<ICustomerFeatureSettings> Features => _features.AsReadOnly();

        public float OrderingTime { get; set; } = 1;

        public float CookingTime { get; set; } = 1;

        public float EatingTime { get; set; } = 1;

        public List<ICustomerFeatureSettings> _features = new List<ICustomerFeatureSettings>();

        
        public void AddFeature(ICustomerFeatureSettings customerFeature)
        {
            _features.Add(customerFeature);
        }
    }
}
