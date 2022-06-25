using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Services.Definitions;
using Game.Assets.Scripts.Game.Logic.Services.Game;
using Game.Assets.Scripts.Game.Logic.Services.Session;
using Game.Assets.Scripts.Tests.Definitions;
using Game.Assets.Scripts.Tests.Presenters.Localization;

namespace Game.Assets.Scripts.Tests.Cases.Levels
{
    public class LevelTests
    {
        [Test, Order(TestCore.EssentialOrder)]
        public void IsCoreDefaultsFilled()
        {
            var core = CreateDefaultCore();
            var services = core.Services;

            Assert.IsNotNull(IGameTime.Default);
            Assert.IsNotNull(IGameRandom.Default);
            Assert.IsNotNull(ILocalizationManager.Default);
            Assert.IsNotNull(IPresenterServices.Default);
            Assert.IsNotNull(IModelServices.Default);
            Assert.IsNotNull(core.Services);
            Assert.IsTrue(services.Has<GameService>());
            Assert.IsTrue(services.Has<LevelsService>());
            Assert.IsTrue(services.Has<DefinitionsService>());
            Assert.IsTrue(services.Has<GameControlsService>());
            Assert.IsTrue(services.Has<GameAssetsService>());

            core.Dispose();

            Assert.IsNull(IGameTime.Default);
            Assert.IsNull(IGameRandom.Default);
            Assert.IsNull(ILocalizationManager.Default);
            Assert.IsNull(IPresenterServices.Default);
            Assert.IsNull(IModelServices.Default);
            Assert.IsNull(core.Services);
            Assert.IsFalse(services.Has<GameService>());
            Assert.IsFalse(services.Has<LevelsService>());
            Assert.IsFalse(services.Has<DefinitionsService>());
            Assert.IsFalse(services.Has<GameControlsService>());
            Assert.IsFalse(services.Has<GameAssetsService>());
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
        public void LevelReloadingWorked()
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

        [Test, Order(TestCore.EssentialOrder)]
        public void StartingCodeWorking()
        {
            var level = new LevelDefinitionMock("leveldef", new EmptyLevel());
            var starter = new LevelStarter();
            level.Starter = starter;
            level.SceneName = "leveldef";

            var services = new ServiceManager();

            var definitions = new List<LevelDefinition>() { level };

            var levelManager = new LevelsManagerMock();
            levelManager.Add(level);
            var levels = new Repository<Level>();

            var levelService = new LevelsService(services, levelManager, levels, definitions, level);
            levelService.StartFirstLevel();
            levelManager.FinishLoading();

            Assert.AreEqual("leveldef", levelService.GetCurrentLevel().SceneName);
            Assert.IsTrue(levelService.GetCurrentLevel() is SpecialLevel);
            Assert.IsTrue(starter.StartedServices is SpecialLevel);
            Assert.AreEqual("leveldef", starter.StartedServices.SceneName);
            Assert.IsTrue(starter.StartedLevel is SpecialLevel);
            Assert.AreEqual("leveldef", starter.StartedLevel.SceneName);

            levelService.Dispose();
            services.Dispose();
        }

        private Core CreateDefaultCore()
        {
            var definitions = new DefinitionsMock();
            var level = new LevelDefinitionMock("leveldef", new EmptyLevel());
            level.SceneName = "leveldef";
            definitions.Add("leveldef", level);
            definitions.Add("maindef", new MainDefinition() { StartLevel = level });

            var levelManager = new LevelsManagerMock();
            levelManager.Add(new Level(level));

            return new Core(new EngineMock(),
                levelManager,
                new AssetsMock(),
                definitions,
                new ControlsMock(),
                new LocalizationManagerMock(),
                new GameTime(), false);
        }

        public class LevelStarter : LevelSpecifics
        {
            public Level StartedLevel;
            public Level StartedServices;

            public override Level CreateEntity(LevelDefinition definition, ServiceManager services)
            {
                return new SpecialLevel(definition);
            }

            public override void StartServices(Level level)
            {
                StartedServices = level;
            }

            public override void StartLevel(Level level)
            {
                StartedLevel = level;
            }

        }
        public record SpecialLevel : Level
        {
            public SpecialLevel(LevelDefinition def) : base(def)
            {
            }
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
