using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Tests.Environment.Definitions.List;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
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
            var build = new GameTestConstructor().Build();
            Assert.IsNotNull(build.GameModel);
            build.Dispose();
            Assert.IsNull(build.GameModel);
        }

        [Test]
        public void SessionIsStarting()
        {
            var build = new GameTestConstructor().Build();
            var session = build.GameModel.CreateSession();
            session.Dispose();
            build.Dispose();
        }

        [Test]
        public void LevelIsLoading()
        {
            var levelDefinition = new LevelDefinitionInTests(new EmptyLevel());
            var build = new GameTestConstructor().LoadDefinitions(new DefaultDefinitions()).AddLevel(levelDefinition).Build();
            var session = build.GameModel.CreateSession();
            var levelLoading = session.LoadLevel(levelDefinition);
            GameLevel loadedLevel = null;

            levelLoading.OnLoaded += HandleOnLoaded;
            build.Engine.Levels.FinishLoading();
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
            var build = new GameTestConstructor().AddAndLoadLevel(new EmptyLevel()).Build();
            Assert.IsNotNull(build.Engine.Levels.GetCurrentLevel());
            build.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
