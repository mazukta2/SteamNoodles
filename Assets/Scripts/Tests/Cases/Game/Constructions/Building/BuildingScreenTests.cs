using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
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
            var events = new EventManager();
            var buildingModeService = new BuildingModeService(events);
            var viewCollection = new ViewsCollection();
            var (card, screenManager) = Setup(viewCollection);

            var keyManager = new GameKeysManager();
            IGameKeysManager.Default = keyManager;
            ScreenManagerPresenter.Default.Open<IBuildScreenView>(x => 
                new BuildScreenPresenter(x, card.Get(), buildingModeService, IGameKeysManager.Default));

            Assert.IsNotNull(screenManager.Screen.FindView<BuildScreenView>());
            Assert.IsNull(screenManager.Screen.FindView<MainScreenView>());

            keyManager.TapKey(GameKeys.Exit);

            Assert.IsNull(screenManager.Screen.FindView<BuildScreenView>());
            Assert.IsNotNull(screenManager.Screen.FindView<MainScreenView>());

            viewCollection.Dispose();
        }

        private (EntityLink<ConstructionCard>, ScreenManagerView) Setup(ViewsCollection viewCollection)
        {
            var events = new EventManager();
            var assets = new AssetsMock();
            var schemesRepository = new Repository<ConstructionScheme>(events);
            var cardsRepository = new Repository<ConstructionCard>(events);

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);
            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            assets.AddPrefab("Screens/BuildScreen", new DefaultViewPrefab(x => new BuildScreenView(x)));
            assets.AddPrefab("Screens/MainScreen", new DefaultViewPrefab(x => new MainScreenView(x)));

            var view = new ScreenManagerView(viewCollection);
            ScreenManagerPresenter.Default = new ScreenManagerPresenter(view, new GameAssets(assets));

            return (link, view);
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
