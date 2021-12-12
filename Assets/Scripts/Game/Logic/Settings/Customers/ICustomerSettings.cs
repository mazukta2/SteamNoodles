using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using System.Collections.Generic;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface ICustomerSettings
    {
        int Money { get; }
        float OrderingTime { get; }
        float CookingTime { get; }
        float EatingTime { get; }
        float BaseTipMultiplayer { get; }
        IReadOnlyCollection<ICustomerFeatureSettings> Features { get; }
    }
}
