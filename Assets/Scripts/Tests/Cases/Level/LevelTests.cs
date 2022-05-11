using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels.Levels;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
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
            var session = build.GameModel.CreateSession();
            session.Dispose();
            build.Dispose();
        }

        [Test]
        public void LevelIsLoading()
        {
            var levelDefinition = LevelDefinitionSetups.GetEmpty("lvl");
            var build = new GameConstructor().DisableAutoLoad().AddLevel(levelDefinition).Build();
            var session = build.GameModel.CreateSession();
            var levelLoading = session.LoadLevel(levelDefinition);
            GameLevel loadedLevel = null;

            levelLoading.OnLoaded += HandleOnLoaded;
            build.Levels.FinishLoading();
            levelLoading.OnLoaded -= HandleOnLoaded;
            Assert.IsTrue(levelLoading.IsDisposed);
            Assert.IsNotNull(loadedLevel);

            loadedLevel.Dispose();
            session.Dispose();
            build.Dispose();

            void HandleOnLoaded(GameLevel level)
            {
                loadedLevel = level;
            }
        }

        [Test]
        public void LevelShortcutIsWorking()
        {
            var build = new GameConstructor().Build();
            Assert.IsNotNull(build.Levels.Controller.Collection);
            build.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
