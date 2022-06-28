using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Screens
{
    public class MenuScreenTests
    {
        [Test, Order(TestCore.PresenterOrder)]
        public void EscFromMainScreen()
        {
            var assets = new AssetsMock();
            var schemesRepository = new Database<ConstructionScheme>();
            var cardsRepository = new Database<ConstructionCard>();

            // var buildingModeService = new GhostService(new Field(1, IntPoint.One));

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);
            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            assets.AddPrefab("Screens/MainScreen", new DefaultViewPrefab(x => new MainScreenView(x)));
            assets.AddPrefab("Screens/GameMenuScreen", new DefaultViewPrefab(x => new GameMenuScreenView(x)));

            var viewCollection = new ViewsCollection();
            var view = new ScreenManagerView(viewCollection);
            //ScreenManagerPresenter.Default = new ScreenManagerPresenter(view, new GameAssetsService(assets));

            //var keyManager = new GameKeysService();
            //IGameKeysManager.Default = keyManager;

            //ScreenManagerPresenter.Default.Open<IMainScreenView>(x => new MainScreenPresenter(x, keyManager));

            //Assert.IsFalse(commands.Last<OpenGameMenuScreenCommand>());

            //keyManager.TapKey(GameKeys.Exit);

            //Assert.IsTrue(commands.Last<OpenGameMenuScreenCommand>());

            viewCollection.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
