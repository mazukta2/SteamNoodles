using Assets.Scripts.Logic.Prototypes.Levels;

namespace Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features
{
    public interface INewCustomerConstructionFeatureSettings : IConstructionFeatureSettings
    {
        ICustomerSettings Customer { get; }
    }

}
