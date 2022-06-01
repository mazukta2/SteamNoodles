using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Screens
{
    public class BuildingScreenTests
    {
        [Test, Order(TestCore.PresenterOrder)]
        public void EscFromBuildingScreen()
        {
            var assets = new AssetsMock();
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var buildingModeService = new BuildingModeService();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);
            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            assets.AddPrefab("Screens/BuildScreen", new DefaultViewCollectionPrefabMock(x => new BuildScreenView(x)));
            assets.AddPrefab("Screens/MainScreen", new DefaultViewCollectionPrefabMock(x => new MainScreenView(x)));

            var viewCollection = new ViewsCollection();
            var view = new ScreenManagerView(viewCollection);
            ScreenManagerPresenter.Default = new ScreenManagerPresenter(view, new GameAssets(assets));

            var keyManager = new GameKeysManager();
            IGameKeysManager.Default = keyManager;
            ScreenManagerPresenter.Default.Open<IBuildScreenView>(x => 
                new BuildScreenPresenter(x, link, buildingModeService, IGameKeysManager.Default));

            Assert.IsNotNull(view.Screen.FindView<BuildScreenView>());
            Assert.IsNull(view.Screen.FindView<MainScreenView>());

            keyManager.TapKey(GameKeys.Exit);

            Assert.IsNull(view.Screen.FindView<BuildScreenView>());
            Assert.IsNotNull(view.Screen.FindView<MainScreenView>());

            //manager.TapKey(GameKeys.Exit);

            //Assert.IsNotNull(build.LevelCollection.FindView<GameMenuScreenView>());

            //manager.TapKey(GameKeys.Exit);

            //Assert.IsNotNull(build.LevelCollection.FindView<MainScreenView>());

            viewCollection.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
