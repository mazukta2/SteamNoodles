using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class Resources : Disposable
    {
        public BuildingPoints Points { get; } 
        public Coins Coins { get; }

        public Resources(ConstructionsSettingsDefinition constructionsSettingsDefinition)
        {
            Points = new BuildingPoints(constructionsSettingsDefinition.LevelUpPower, constructionsSettingsDefinition.LevelUpOffset);
            Coins = new Coins();
        }
    }
}
