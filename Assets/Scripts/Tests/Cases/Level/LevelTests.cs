using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Infrastructure;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Tests.Environment.Game;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Level
{
    public class LevelTests
    {
        [Test]
        public void GameIsStarting()
        {
            var build = new BuildConstructor().Build();
            build.Dispose();
        }

        [Test]
        public void StartMenuMusicPlays()
        {
            var level = new LevelDefinition()
            {
                Variation = new StartMenuVariation()
                {
                    SceneName = "SceneName",
                    StartMusic = "MusicTrack"
                }
            };
            var build = new BuildConstructor()
                .UpdateDefinition<MainDefinition>(x => x.StartLevel = level)
                .AddDefinition("MainScene", level)
                .Build();
            
            Assert.IsTrue(IModels.Default.Has<StartMenu>());

            Assert.AreEqual("MusicTrack", IInfrastructure.Default.Application.Music.GetTarget().Name);

            build.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
