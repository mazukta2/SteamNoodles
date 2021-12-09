namespace Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features
{
    public interface IMoneyForConstructionCustomerFeatureSettings : ICustomerFeatureSettings
    {
        int Money { get; }
        IConstructionSettings Construction { get; }
    }

}
