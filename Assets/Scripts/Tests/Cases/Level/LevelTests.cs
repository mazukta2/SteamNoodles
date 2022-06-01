using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Level
{
    public class LevelTests
    {
        [Test]
        public void GameIsStarting()
        {
            var build = new GameConstructor().Build();
            Assert.IsNotNull(build.GameModel);
            build.Dispose();
            Assert.IsNull(build.GameModel);
        }

        [Test]
        public void SessionIsStarting()
        {
            var build = new GameConstructor().Build();

            Assert.IsNotNull(IGameSession.Default);
            build.Dispose();

            Assert.IsNull(IGameSession.Default);
        }

        [Test]
        public void LevelIsLoading()
        {
            var levelDefinition = LevelDefinitionSetups.GetEmpty("lvl");
            var build = new GameConstructor().DisableAutoLoad().AddLevel(levelDefinition).Build();
            build.LoadLevel(levelDefinition);
            build.Levels.FinishLoading();

            Assert.IsTrue(build.Core.Levels.State == LevelLoading.LevelsState.IsLoaded);
            Assert.IsNotNull(((GameModel)IGame.Default).CurrentLevel);

            build.Dispose();
        }

        [Test]
        public void LevelShortcutIsWorking()
        {
            var build = new GameConstructor().Build();
            Assert.IsNotNull(build.LevelCollection);
            build.Dispose();
        }

        [Test]
        public void IsLevelReloadingWorked()
        {
            var build = new GameConstructor().Build();
            Assert.AreEqual(LevelLoading.LevelsState.IsLoaded, build.Core.Levels.State);

            var level = IStageLevelService.Default;

            Assert.AreEqual(level, IStageLevelService.Default);
            build.LoadLevel(level.Definition);
            build.Levels.FinishLoading();

            Assert.IsFalse(build.Core.IsDisposed);
            Assert.IsFalse(build.Core.Levels.IsDisposed);
            Assert.AreEqual(LevelLoading.LevelsState.IsLoaded, build.Core.Levels.State);
            Assert.AreNotEqual(level, IStageLevelService.Default);
            Assert.IsTrue(level.IsDisposed);

            build.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
