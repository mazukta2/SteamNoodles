using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class Resources : Disposable
    {
        public BuildingPointsManager Points { get; } 
        public Coins Coins { get; }

        public Resources(ConstructionsSettingsDefinition constructionsSettingsDefinition, IGameTime time)
        {
            Points = new BuildingPointsManager(constructionsSettingsDefinition, time,
                constructionsSettingsDefinition.LevelUpPower, constructionsSettingsDefinition.LevelUpOffset);
            Coins = new Coins();
        }

        protected override void DisposeInner()
        {
            Points.Dispose();
        }
    }
}
