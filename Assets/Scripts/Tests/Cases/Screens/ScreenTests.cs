using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Level
{
    public class ScreenTests
    {
        /*
        [Test]
        public void MainScreenIsOpened()
        {
            var build = new TestGameConstructor().LoadDefinitions(new DefaultDefinitions()).LoadLevel(new HouseLevel()).Build();

            var screenManager = build.Engine.Levels.GetCurrentLevel().FindObject<ScreenManagerView>();
            Assert.IsNotNull(screenManager);

            var mainScreen = build.Engine.Levels.GetCurrentLevel().FindObject<MainScreenView>();
            Assert.IsNotNull(mainScreen);

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
