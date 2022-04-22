using Game.Assets.Scripts.Tests.Environment.Views.Ui;
using Game.Assets.Scripts.Tests.Environment.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Level
{
    public class ScreenTests
    {
        [Test]
        public void MainScreenIsOpened()
        {
            var build = new GameTestConstructor().Build();

            var screenManager = build.CurrentLevel.FindView<ScreenManagerView>();
            Assert.IsNotNull(screenManager);

            var mainScreen = build.CurrentLevel.FindView<MainScreenView>();
            Assert.IsNotNull(mainScreen);

            build.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
