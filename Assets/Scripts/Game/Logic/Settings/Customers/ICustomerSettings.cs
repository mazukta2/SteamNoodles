using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using System.Collections.Generic;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface ICustomerSettings
    {
        int Money { get; }
        int ServingTime { get; }
        float BaseTipMultiplayer { get; }
        IReadOnlyCollection<ICustomerFeatureSettings> Features { get; }
    }
}
