using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Environment.Definitions.List
{
    public class DefaultDefinitions : DefinitionsMockCreator
    {
        public override void Create(GameEngineInTests engine)
        {
            engine.Assets.Screens.AddPrototype<MainScreenView>(new MainScreenPrefab());
            engine.Assets.Screens.AddPrototype<BuildScreenView>(new BuildScreenPrefab());
            engine.Settings.Add(nameof(ConstructionsSettingsDefinition), new ConstructionsSettingsDefinition() { 
                CellSize = 20,
            });

            var construciton = new ConstructionDefinition();
            var level = new LevelDefinitionMock("DebugLevel", new BasicSellingLevel())
            {
                StartingHand = new List<ConstructionDefinition>() { construciton }
            };
            engine.Settings.Add(level.Name, level);
        }
    }
}
