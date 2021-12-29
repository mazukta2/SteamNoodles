using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using System.Collections.Generic;

namespace Assets.Scripts.Data.Buildings
{
    public class CustomerSettings : ICustomerSettings
    {
        public int Money => throw new System.NotImplementedException();

        public float OrderingTime => throw new System.NotImplementedException();

        public float CookingTime => throw new System.NotImplementedException();

        public float EatingTime => throw new System.NotImplementedException();

        public float BaseTipMultiplayer => throw new System.NotImplementedException();

        public IReadOnlyCollection<ICustomerFeatureSettings> Features => throw new System.NotImplementedException();

        public float Speed => throw new System.NotImplementedException();
    }
}

