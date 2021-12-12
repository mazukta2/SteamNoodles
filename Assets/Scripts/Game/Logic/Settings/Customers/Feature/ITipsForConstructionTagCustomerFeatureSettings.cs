using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;

namespace Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features
{
    public interface ITipsForConstructionTagCustomerFeatureSettings : ICustomerFeatureSettings
    {
        PercentModificator TipModificator { get; }
        ConstructionTag Tag { get; }
    }

}
