using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;

namespace Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features
{
    public interface IEatingSpeedFeatureSettings : ICustomerFeatureSettings
    {
        PercentModificator TimeModificator { get; }
    }

}
