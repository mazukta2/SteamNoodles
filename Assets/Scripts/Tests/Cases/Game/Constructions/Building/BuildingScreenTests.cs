using System.Linq;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
{
    public class BuildingScreenTests
    {
        [Test, Order(TestCore.PresenterOrder)]
        public void EscFromBuildingScreen()
        {
            var viewCollection = new ViewsCollection();
            var ghostSetup = new GhostConstructionSetup().FillDefault();
            var screensSetup = new ScreensSetup(viewCollection).FullDefault();
            
            var controls = new GameControlsService(new ControlsMock());

            screensSetup.ScreenService.Open<IBuildScreenView>(x => 
                new BuildScreenPresenter(x,
                    ghostSetup.ConstructionsCardsDatabase.Get().First().Id, 
                    ghostSetup.GhostRepository, screensSetup.ScreenService, controls));

            Assert.IsNotNull(screensSetup.View.Screen.FindView<BuildScreenView>());
            Assert.IsNull(screensSetup.View.Screen.FindView<MainScreenView>());

            controls.TapKey(GameKeys.Exit);

            Assert.IsNull(screensSetup.View.Screen.FindView<BuildScreenView>());
            Assert.IsNotNull(screensSetup.View.Screen.FindView<MainScreenView>());

            viewCollection.Dispose();
            controls.Dispose();
            
            ghostSetup.Dispose();
            screensSetup.Dispose();
            controls.Dispose();
        }


        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
