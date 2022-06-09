using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Presenters.Commands;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Screens
{
    public class MenuScreenTests
    {
        [Test, Order(TestCore.PresenterOrder)]
        public void EscFromMainScreen()
        {
            var events = new EventManager();
            var assets = new AssetsMock();
            var commands = new CommandsMock();
            var schemesRepository = new Repository<ConstructionScheme>(events);
            var cardsRepository = new Repository<ConstructionCard>(events);

            var buildingModeService = new BuildingModeService(events);

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);
            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            assets.AddPrefab("Screens/MainScreen", new DefaultViewCollectionPrefabMock(x => new MainScreenView(x)));
            assets.AddPrefab("Screens/GameMenuScreen", new DefaultViewCollectionPrefabMock(x => new GameMenuScreenView(x)));

            var viewCollection = new ViewsCollection();
            var view = new ScreenManagerView(viewCollection);
            ScreenManagerPresenter.Default = new ScreenManagerPresenter(view, new GameAssets(assets));

            var keyManager = new GameKeysManager();
            IGameKeysManager.Default = keyManager;

            ScreenManagerPresenter.Default.Open<IMainScreenView>(x => new MainScreenPresenter(x, keyManager, commands));

            Assert.IsFalse(commands.Last<OpenGameMenuScreenCommand>());

            keyManager.TapKey(GameKeys.Exit);

            Assert.IsTrue(commands.Last<OpenGameMenuScreenCommand>());

            viewCollection.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
