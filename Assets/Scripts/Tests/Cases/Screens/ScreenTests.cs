using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;
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
            var build = new GameTestConstructor().LoadDefinitions(new DefaultDefinitions()).AddAndLoadLevel(new BasicSellingLevel()).Build();

            var screenManager = build.CurrentLevel.FindViewPresenter<ScreenManagerViewPresenter>();
            Assert.IsNotNull(screenManager);

            var mainScreen = build.CurrentLevel.FindViewPresenter<MainScreenViewPresenter>();
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
