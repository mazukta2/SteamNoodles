using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Level
{
    public class LevelTests
    {
        /*
        [Test]
        public void GameIsStarting()
        {
            var build = new TestGameConstructor().Build();
            Assert.IsNotNull(build.Game);
            build.Dispose();
            Assert.IsNull(build.Game);
        }


        [Test]
        public void SessionIsStarting()
        {
            var build = new TestGameConstructor().Build();
            var session = build.Game.CreateSession();
            session.Dispose();
            build.Dispose();
        }

        [Test]
        public void LevelIsLoading()
        {
            var levelDefinition = new TestLevelDefinition(new EmptyLevel());
            var build = new TestGameConstructor().LoadDefinitions(new DefaultDefinitions()).AddLevel(levelDefinition).Build();
            var session = build.Game.CreateSession();
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
            var build = new TestGameConstructor().LoadLevel(new EmptyLevel()).Build();
            Assert.IsNotNull(build.Engine.Levels.GetCurrentLevel());
            build.Dispose();
        }
        */

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
