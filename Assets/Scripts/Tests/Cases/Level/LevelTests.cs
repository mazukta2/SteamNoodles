using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Models.Session;
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
            build.Core.Levels.Load(levelDefinition);
            var levelLoading = build.Core.Levels;
            ILevel loadedLevel = null;

            levelLoading.OnLoaded += HandleOnLoaded;
            build.Levels.FinishLoading();
            levelLoading.OnLoaded -= HandleOnLoaded;

            Assert.IsTrue(levelLoading.State == LevelLoading.LevelsState.IsLoaded);
            Assert.IsNotNull(loadedLevel);

            loadedLevel.Dispose();
            build.Dispose();

            void HandleOnLoaded(ILevel level, IViewsCollection collection)
            {
                loadedLevel = level;
            }
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

            var level = IBattleLevel.Default;

            Assert.AreEqual(level, IBattleLevel.Default);
            build.Core.Levels.Load(level.Definition);
            build.Levels.FinishLoading();

            Assert.IsFalse(build.Core.IsDisposed);
            Assert.IsFalse(build.Core.Levels.IsDisposed);
            Assert.AreEqual(LevelLoading.LevelsState.IsLoaded, build.Core.Levels.State);
            Assert.AreNotEqual(level, IBattleLevel.Default);
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
