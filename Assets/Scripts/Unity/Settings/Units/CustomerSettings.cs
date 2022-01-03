using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using System.Collections.Generic;

namespace Assets.Scripts.Data.Buildings
{
    public class CustomerSettings : ICustomerSettings
    {
        public int Money { get; set; }
        public float OrderingTime { get; set; }
        public float CookingTime { get; set; }
        public float EatingTime { get; set; }
        public float BaseTipMultiplayer { get; set; }
        public IReadOnlyCollection<ICustomerFeatureSettings> Features { get; set; }
        public float Speed { get; set; }
    }
}

