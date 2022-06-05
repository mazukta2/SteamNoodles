using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Level
{
    public class LevelTests
    {
        [Test]
        public void GameIsStarting()
        {
            throw new System.Exception();
            //var build = new GameConstructor().Build();
            //Assert.IsNotNull(build.GameModel);
            //build.Dispose();
            //Assert.IsNull(build.GameModel);
        }

        [Test]
        public void SessionIsStarting()
        {
            throw new System.Exception();
            //var build = new GameConstructor().Build();

            //Assert.IsNotNull(IGameSession.Default);
            //build.Dispose();

            //Assert.IsNull(IGameSession.Default);
        }

        [Test]
        public void LevelIsLoading()
        {
            throw new System.Exception();
            //var levelDefinition = LevelDefinitionSetups.GetEmpty("lvl");
            //var build = new GameConstructor().DisableAutoLoad().AddLevel(levelDefinition).Build();
            //build.LoadLevel(levelDefinition);
            //build.Levels.FinishLoading();

            //Assert.IsTrue(build.Core.Levels.State == LevelLoading.LevelsState.IsLoaded);
            //Assert.IsNotNull(((GameModel)IGame.Default).CurrentLevel);

            //build.Dispose();
        }

        [Test]
        public void LevelShortcutIsWorking()
        {
            throw new System.Exception();
            //var build = new GameConstructor().Build();
            //Assert.IsNotNull(build.LevelCollection);
            //build.Dispose();
        }

        [Test]
        public void IsLevelReloadingWorked()
        {
            throw new System.Exception();
            //var build = new GameConstructor().Build();
            //Assert.AreEqual(LevelLoading.LevelsState.IsLoaded, build.Core.Levels.State);

            //var level = IStageLevelService.Default;

            //Assert.AreEqual(level, IStageLevelService.Default);
            //build.LoadLevel(level.Definition);
            //build.Levels.FinishLoading();

            //Assert.IsFalse(build.Core.IsDisposed);
            //Assert.IsFalse(build.Core.Levels.IsDisposed);
            //Assert.AreEqual(LevelLoading.LevelsState.IsLoaded, build.Core.Levels.State);
            //Assert.AreNotEqual(level, IStageLevelService.Default);
            //Assert.IsTrue(level.IsDisposed);

            //build.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
