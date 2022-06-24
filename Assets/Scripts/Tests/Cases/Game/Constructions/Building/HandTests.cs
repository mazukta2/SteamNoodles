using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
{
    public class HandTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void CardsSpawnedOnStart()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var deck = new DeckService<ConstructionScheme>();
            var hand = new HandService(cardsRepository);
            var schemes = new SchemesService(schemesRepository, deck);
            var points = new BuildingPointsService();

            Assert.AreEqual(0, cardsRepository.Count);

            var stageLevel = new StageLevel(new[] { scheme });

            var flow = new RewardsService(stageLevel, hand, schemes, points);
            flow.Start();

            Assert.AreEqual(1, cardsRepository.Count);

            flow.Dispose();
            points.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void SameCardsCollapse()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var scheme1 = new ConstructionScheme();
            var scheme2 = new ConstructionScheme();
            schemesRepository.Add(scheme1);
            schemesRepository.Add(scheme2);

            var cardsRepository = new Repository<ConstructionCard>();
            var hand = new HandService(cardsRepository);

            Assert.AreEqual(0, cardsRepository.Count);

            var card1 = hand.Add(scheme1);
            Assert.AreEqual(1, cardsRepository.Count);
            Assert.AreEqual(1, cardsRepository.Get(card1.Id).Amount.Value);

            hand.Add(scheme1);

            Assert.AreEqual(1, cardsRepository.Count);
            Assert.AreEqual(2, cardsRepository.Get(card1.Id).Amount.Value);

            var card2 = hand.Add(scheme2);

            Assert.AreEqual(2, cardsRepository.Count);
            Assert.AreEqual(2, cardsRepository.Get(card1.Id).Amount.Value);
            Assert.AreEqual(1, cardsRepository.Get(card2.Id).Amount.Value);
        }

        [Test, Order(TestCore.ModelOrder)]
        public void SameCardsRemoved()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var scheme1 = new ConstructionScheme();
            var scheme2 = new ConstructionScheme();
            schemesRepository.Add(scheme1);
            schemesRepository.Add(scheme2);

            var cardsRepository = new Repository<ConstructionCard>();
            var hand = new HandService(cardsRepository);

            Assert.AreEqual(0, cardsRepository.Count);

            var card1 = hand.Add(scheme1, new CardAmount(2));
            var card2 = hand.Add(scheme2);

            Assert.AreEqual(2, cardsRepository.Count);
            Assert.AreEqual(2, cardsRepository.Get(card1.Id).Amount.Value);
            Assert.AreEqual(1, cardsRepository.Get(card2.Id).Amount.Value);

            hand.Remove(card1);

            Assert.AreEqual(2, cardsRepository.Count);
            Assert.AreEqual(1, cardsRepository.Get(card1.Id).Amount.Value);
            Assert.AreEqual(1, cardsRepository.Get(card2.Id).Amount.Value);

            hand.Remove(card1);
            Assert.AreEqual(1, cardsRepository.Count);
            Assert.AreEqual(1, cardsRepository.Get(card2.Id).Amount.Value);

            hand.Remove(card2);
            Assert.AreEqual(0, cardsRepository.Count);
        }


        [Test, Order(TestCore.ModelOrder)]
        public void YouGetNewCardsAfterLevelUp()
        {
            var cardsRepository = new Repository<ConstructionCard>();
            var schemesRepository = new Repository<ConstructionScheme>();

            var time = new GameTime();
            var stageLevel = new StageLevel();
            var pointsService = new BuildingPointsService(0,0, time, 2, 2);

            var deck = new DeckService<ConstructionScheme>();
            deck.Add(new ConstructionScheme());
            var hand = new HandService(cardsRepository);
            var schemes = new SchemesService(schemesRepository, deck);

            var flow = new RewardsService(stageLevel, hand, schemes, pointsService, 1);
            Assert.AreEqual(new BuildingLevel(0), pointsService.GetCurrentLevel());

            Assert.AreEqual(0, cardsRepository.Count);

            pointsService.Change(new BuildingPoints(3));
            Assert.AreEqual(new BuildingLevel(1), pointsService.GetCurrentLevel());

            Assert.AreEqual(1, cardsRepository.Count);

            pointsService.Dispose();
            flow.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void CardsSpawnedAndRemovedInPresenter()
        {
            var screenManager = new ScreenService(new GameAssetsService(new AssetsMock()));

            var schemesRepository = new Repository<ConstructionScheme>();
            var scheme1 = new ConstructionScheme();
            var scheme2 = new ConstructionScheme();
            schemesRepository.Add(scheme1);
            schemesRepository.Add(scheme2);

            var cardsRepository = new Repository<ConstructionCard>();
            var ghostService = new GhostService(new Field());

            var viewCollection = new ViewsCollection();
            var handView = new HandView(viewCollection);
            new HandPresenter(handView, cardsRepository, ghostService, screenManager);

            Assert.AreEqual(0, handView.Collection.FindViews<IHandConstructionView>().Count);
            cardsRepository.Add(new ConstructionCard(scheme1));

            Assert.AreEqual(1, handView.Collection.FindViews<IHandConstructionView>().Count);

            cardsRepository.Add(new ConstructionCard(scheme2));
            Assert.AreEqual(2, handView.Collection.FindViews<IHandConstructionView>().Count);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void AmountShownCorrectly()
        {
            var screenManager = new ScreenService(new GameAssetsService(new AssetsMock()));
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            new HandConstructionPresenter(view, link, cardsRepository, screenManager);

            Assert.AreEqual("1", view.Amount.Value);
            card.Add(new CardAmount(1));
            cardsRepository.Save(card);
            Assert.AreEqual("2", view.Amount.Value);
            card.Remove(new CardAmount(1));
            cardsRepository.Save(card);
            Assert.AreEqual("1", view.Amount.Value);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void ConstructionIconSet()
        {
            var screenManager = new ScreenService(new GameAssetsService(new AssetsMock()));
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();
            
            var scheme = new ConstructionScheme(image: "image");
            schemesRepository.Add(scheme);

            var link = cardsRepository.Add(new ConstructionCard(scheme));

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            new HandConstructionPresenter(view, link, cardsRepository, screenManager);

            Assert.AreEqual("image", view.Image.Path);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void TooltipViewSpawning()
        {
            var screenManager = new ScreenService(new GameAssetsService(new AssetsMock()));
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            new HandConstructionPresenter(view, link, cardsRepository, screenManager);

            Assert.IsNull(view.TooltipContainer.FindView<IHandConstructionTooltipView>());
            view.SetHighlight(true);
            Assert.IsNotNull(view.TooltipContainer.FindView<IHandConstructionTooltipView>());
            view.SetHighlight(false);
            Assert.IsNull(view.TooltipContainer.FindView<IHandConstructionTooltipView>());

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void ClickingOpensBuildingScreen()
        {
            var assets = new AssetsMock();
            assets.AddPrefab("Screens/BuildScreen", new DefaultViewPrefab(x => new BuildScreenView(x)));
            var screenManager = new ScreenService(new GameAssetsService(assets));
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            var viewCollection = new ViewsCollection();
            screenManager.Bind(new ScreenManagerView(viewCollection));
            var view = new HandConstructionView(viewCollection);
            new HandConstructionPresenter(view, link, cardsRepository, screenManager);

            view.Button.Click();

            Assert.IsNotNull(viewCollection.FindView<IBuildScreenView>());

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void HandAnimationsPlayedInBuildingMode()
        {
            var screenManager = new ScreenService(new GameAssetsService(new AssetsMock()));
            var cardsRepository = new Repository<ConstructionCard>();

            var ghostService = new GhostService(new Field());
            var viewCollection = new ViewsCollection();
            var handView = new HandView(viewCollection);
            new HandPresenter(handView, cardsRepository, ghostService, screenManager);

            Assert.AreEqual("Choose", handView.Animator.Animation);
            ghostService.Show(new ConstructionCard(new ConstructionScheme()));
            Assert.AreEqual("Build", handView.Animator.Animation);
            ghostService.Hide();
            Assert.AreEqual("Choose", handView.Animator.Animation);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void CancelWorks()
        {
            var assets = new AssetsMock();
            assets.AddPrefab("Screens/MainScreen", new DefaultViewPrefab(x => new MainScreenView(x)));
            var screenManager = new ScreenService(new GameAssetsService(assets));
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var ghostService = new GhostService(new Field());
            var viewCollection = new ViewsCollection();
            screenManager.Bind(new ScreenManagerView(viewCollection));
            var handView = new HandView(viewCollection);
            new HandPresenter(handView, cardsRepository, ghostService, screenManager);

            handView.CancelButton.Click();

            Assert.IsNotNull(viewCollection.FindView<IMainScreenView>());

            viewCollection.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
