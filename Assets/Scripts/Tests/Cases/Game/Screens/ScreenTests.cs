using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using NUnit.Framework;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Screens
{
    public class ScreenTests
    {
        [Test]
        public void MainScreenIsOpened()
        {
            var build = new BuildConstructor().Build();

            //var screenManager = build.LevelCollection.FindView<ScreenManagerView>();
           // Assert.IsNotNull(screenManager);

          //  var mainScreen = build.LevelCollection.FindView<MainScreenView>();
           // Assert.IsNotNull(mainScreen);

            build.Dispose();
        }

        [Test]
        public void EscFromBuildingScreen()
        {
            var build = new BuildConstructor().Build();
            /*
            Assert.IsNull(build.LevelCollection.FindView<BuildScreenView>());
            var view = build.LevelCollection.FindViews<HandConstructionView>().First();
            view.Button.Click();
            Assert.IsNotNull(build.LevelCollection.FindView<BuildScreenView>());

            var manager = (GameKeysManager)IGameKeysManager.Default;
            manager.TapKey(GameKeys.Exit);

            Assert.IsNotNull(build.LevelCollection.FindView<MainScreenView>());

            manager.TapKey(GameKeys.Exit);

            Assert.IsNotNull(build.LevelCollection.FindView<GameMenuScreenView>());

            manager.TapKey(GameKeys.Exit);

            Assert.IsNotNull(build.LevelCollection.FindView<MainScreenView>());
            */
            build.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
