using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment.Definitions.List;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Level
{
    public class ScreenTests
    {
        [Test]
        public void MainScreenIsOpened()
        {
            var build = new GameTestConstructor().AddAndLoadLevel(new BasicSellingLevel()).Build();

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
