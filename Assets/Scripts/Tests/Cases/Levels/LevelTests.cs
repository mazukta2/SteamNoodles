using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Levels;
using Game.Tests.Cases;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Levels
{
    public class LevelTests
    {
        [Test, Order(TestCore.EssentialOrder)]
        public void IsCoreDefaultsFilled()
        {
            var core = CreateDefaultCore();
            var services = core.Services;

            Assert.IsNotNull(IGameKeysManager.Default);
            Assert.IsNotNull(IGameAssets.Default);
            Assert.IsNotNull(IGameDefinitions.Default);
            Assert.IsNotNull(IGameControls.Default);
            Assert.IsNotNull(IGameTime.Default);
            Assert.IsNotNull(IGameRandom.Default);
            Assert.IsNotNull(ILocalizationManager.Default);
            Assert.IsNotNull(IPresenterServices.Default);
            Assert.IsNotNull(IModelServices.Default);
            Assert.IsNotNull(core.Services);
            Assert.IsTrue(services.Has<GameService>());
            Assert.IsTrue(services.Has<LevelsService>());

            core.Dispose();

            Assert.IsNull(IGameKeysManager.Default);
            Assert.IsNull(IGameAssets.Default);
            Assert.IsNull(IGameDefinitions.Default);
            Assert.IsNull(IGameControls.Default);
            Assert.IsNull(IGameTime.Default);
            Assert.IsNull(IGameRandom.Default);
            Assert.IsNull(ILocalizationManager.Default);
            Assert.IsNull(IPresenterServices.Default);
            Assert.IsNull(IModelServices.Default);
            Assert.IsNull(core.Services);
            Assert.IsFalse(services.Has<GameService>());
            Assert.IsFalse(services.Has<LevelsService>());
        }

        [Test, Order(TestCore.EssentialOrder)]
        public void GameIsStarting()
        {
            var core = CreateDefaultCore();

            core.StartGame();

            Assert.IsNotNull(core.Services.Get<LevelsService>().GetCurrentLevel());
            Assert.AreEqual(LevelsService.LevelsState.IsLoading, core.Services.Get<LevelsService>().GetCurrentState());

            core.Dispose();
        }

        [Test, Order(TestCore.EssentialOrder)]
        public void LevelIsLoading()
        {
            var levelManager = new LevelsManagerMock();
            var levels = new Repository<Level>();
            var firstLevel = levelManager.Add(new Level());
            var levelService = new LevelsService(levelManager, levels, firstLevel);

            Assert.AreEqual(LevelsService.LevelsState.None, levelService.GetCurrentState());
            Assert.IsNull(levelService.GetCurrentLevel());

            levelService.StartFirstLevel();

            Assert.AreEqual(LevelsService.LevelsState.IsLoading, levelService.GetCurrentState());
            Assert.IsNotNull(levelService.GetCurrentLevel());
            Assert.AreEqual(firstLevel.Id, levelService.GetCurrentLevel().Id);
            
            levelManager.FinishLoading();

            Assert.AreEqual(LevelsService.LevelsState.IsLoaded, levelService.GetCurrentState());
            Assert.IsNotNull(levelService.GetCurrentLevel());
            Assert.AreEqual(firstLevel.Id, levelService.GetCurrentLevel().Id);

            levelService.Dispose();
        }

        [Test, Order(TestCore.EssentialOrder)]
        public void IsLevelReloadingWorked()
        {
            var levelManager = new LevelsManagerMock();
            var levels = new Repository<Level>();
            var firstLevel = levelManager.Add(new Level());
            var levelService = new LevelsService(levelManager, levels, firstLevel);
            levelService.StartFirstLevel();
            levelManager.FinishLoading();

            levelService.ReloadCurrentLevel();

            Assert.AreEqual(LevelsService.LevelsState.IsLoading, levelService.GetCurrentState());
            Assert.IsNotNull(levelService.GetCurrentLevel());
            Assert.AreEqual(firstLevel.Id, levelService.GetCurrentLevel().Id);

            levelManager.FinishLoading();

            Assert.AreEqual(LevelsService.LevelsState.IsLoaded, levelService.GetCurrentState());
            Assert.IsNotNull(levelService.GetCurrentLevel());
            Assert.AreEqual(firstLevel.Id, levelService.GetCurrentLevel().Id);

            levelService.Dispose();
        }

        private Core CreateDefaultCore()
        {
            var definitions = new DefinitionsMock();
            var level = new LevelDefinitionMock("leveldef", new EmptyLevel());
            definitions.Add("leveldef", level);
            definitions.Add("maindef", new MainDefinition() { StartLevel = level });

            var levelManager = new LevelsManagerMock();
            levelManager.Add(new Level(level));

            return new Core(new EngineMock(),
                levelManager,
                new GameAssets(new AssetsMock()),
                definitions,
                new GameControls(new ControlsMock()),
                new LocalizationManagerMock(),
                new GameTime(), false);
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
