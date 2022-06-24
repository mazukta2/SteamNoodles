using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Ui;
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
            var view = new ScreenManagerView(viewCollection);

            var (card, screenManager) = Setup(view);

            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonRepository<ConstructionGhost>();
            var ghostService = new GhostService(ghost, new Field());

            screenManager.Open<IBuildScreenView>(x => 
                new BuildScreenPresenter(x, card, ghost.AsQuery(), ghostService, screenManager, controls));

            Assert.IsNotNull(view.Screen.FindView<BuildScreenView>());
            Assert.IsNull(view.Screen.FindView<MainScreenView>());

            controls.TapKey(GameKeys.Exit);

            Assert.IsNull(view.Screen.FindView<BuildScreenView>());
            Assert.IsNotNull(view.Screen.FindView<MainScreenView>());

            viewCollection.Dispose();
            controls.Dispose();
        }

        private (ConstructionCard, ScreenService) Setup(ScreenManagerView view)
        {
            var assets = new AssetsMock();
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);
            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            assets.AddPrefab("Screens/BuildScreen", new DefaultViewPrefab(x => new BuildScreenView(x)));
            assets.AddPrefab("Screens/MainScreen", new DefaultViewPrefab(x => new MainScreenView(x)));

            var screenService = new ScreenService(new GameAssetsService(assets));
            screenService.Bind(view);

            return (link, screenService);
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
