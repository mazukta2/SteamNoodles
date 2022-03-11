using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;

namespace Game.Assets.Scripts.Tests.Environment.Definitions.List
{
    public class DefaultDefinitions : DefinitionsMockCreator
    {
        public override void Create(GameEngineInTests engine)
        {
            engine.Assets.Screens.AddPrototype<MainScreenView>(new MainScreenPrefab());
            engine.Assets.Screens.AddPrototype<BuildScreenView>(new BuildScreenPrefab());
            engine.Settings.Add(nameof(LevelDefinitionMock), new LevelDefinitionMock(new EmptyLevel()));
        }
    }
}
