using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Services.Common;
using Game.Assets.Scripts.Game.Logic.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;
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
            var schemesRepository = new Database<ConstructionSchemeEntity>();
            var cardsRepository = new Database<ConstructionCardEntity>();

            var scheme = new ConstructionSchemeEntity();
            schemesRepository.Add(scheme);

            var deck = new DeckService<ConstructionSchemeEntity>();
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
            var schemesRepository = new Database<ConstructionSchemeEntity>();
            var scheme1 = new ConstructionSchemeEntity();
            var scheme2 = new ConstructionSchemeEntity();
            schemesRepository.Add(scheme1);
            schemesRepository.Add(scheme2);

            var cardsRepository = new Database<ConstructionCardEntity>();
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
            var schemesRepository = new Database<ConstructionSchemeEntity>();
            var scheme1 = new ConstructionSchemeEntity();
            var scheme2 = new ConstructionSchemeEntity();
            schemesRepository.Add(scheme1);
            schemesRepository.Add(scheme2);

            var cardsRepository = new Database<ConstructionCardEntity>();
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
            var cardsRepository = new Database<ConstructionCardEntity>();
            var schemesRepository = new Database<ConstructionSchemeEntity>();

            var time = new GameTime();
            var stageLevel = new StageLevel();
            var pointsService = new BuildingPointsService(0,0, time, 2, 2);

            var deck = new DeckService<ConstructionSchemeEntity>();
            deck.Add(new ConstructionSchemeEntity());
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

            var schemesRepository = new Database<ConstructionSchemeEntity>();
            var scheme1 = new ConstructionSchemeEntity();
            var scheme2 = new ConstructionSchemeEntity();
            schemesRepository.Add(scheme1);
            schemesRepository.Add(scheme2);

            var cardsRepository = new Database<ConstructionCardEntity>();
            var ghost = new SingletonDatabase<GhostEntity>();

            var viewCollection = new ViewsCollection();
            var handView = new HandView(viewCollection);
            // new HandPresenter(handView, new DataCollectionProvider<ConstructionCardData>(), new DataProvider<GhostData>(), screenManager);

            Assert.AreEqual(0, handView.Collection.FindViews<IHandConstructionView>().Count);
            cardsRepository.Add(new ConstructionCardEntity(scheme1));

            Assert.AreEqual(1, handView.Collection.FindViews<IHandConstructionView>().Count);

            cardsRepository.Add(new ConstructionCardEntity(scheme2));
            Assert.AreEqual(2, handView.Collection.FindViews<IHandConstructionView>().Count);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void AmountShownCorrectly()
        {
            var screenManager = new ScreenService(new GameAssetsService(new AssetsMock()));
            var schemesRepository = new Database<ConstructionSchemeEntity>();
            var cardsRepository = new Database<ConstructionCardEntity>();

            var scheme = new ConstructionSchemeEntity();
            schemesRepository.Add(scheme);

            var card = new ConstructionCardEntity(scheme);
            var link = cardsRepository.Add(card);

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            // new HandConstructionPresenter(view, new DataProvider<ConstructionCardPresentation>(), screenManager);

            Assert.AreEqual("1", view.Amount.Value);
            card.Add(new CardAmount(1));
            Assert.AreEqual("2", view.Amount.Value);
            card.Remove(new CardAmount(1));
            Assert.AreEqual("1", view.Amount.Value);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void ConstructionIconSet()
        {
            var screenManager = new ScreenService(new GameAssetsService(new AssetsMock()));
            var schemesRepository = new Database<ConstructionSchemeEntity>();
            var cardsRepository = new Database<ConstructionCardEntity>();
            
            var scheme = new ConstructionSchemeEntity(image: "image");
            schemesRepository.Add(scheme);

            var link = cardsRepository.Add(new ConstructionCardEntity(scheme));

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            // new HandConstructionPresenter(view, new DataProvider<ConstructionCardPresentation>(), screenManager);

            Assert.AreEqual("image", view.Image.Path);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void TooltipViewSpawning()
        {
            var screenManager = new ScreenService(new GameAssetsService(new AssetsMock()));
            var schemesRepository = new Database<ConstructionSchemeEntity>();
            var cardsRepository = new Database<ConstructionCardEntity>();

            var scheme = new ConstructionSchemeEntity();
            schemesRepository.Add(scheme);

            var card = new ConstructionCardEntity(scheme);
            var link = cardsRepository.Add(card);

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            // new HandConstructionPresenter(view, new DataProvider<ConstructionCardPresentation>(), screenManager);

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
            var schemesRepository = new Database<ConstructionSchemeEntity>();
            var cardsRepository = new Database<ConstructionCardEntity>();

            var scheme = new ConstructionSchemeEntity();
            schemesRepository.Add(scheme);

            var card = new ConstructionCardEntity(scheme);
            var link = cardsRepository.Add(card);

            var viewCollection = new ViewsCollection();
            screenManager.Bind(new ScreenManagerView(viewCollection));
            var view = new HandConstructionView(viewCollection);
            // new HandConstructionPresenter(view, new DataProvider<ConstructionCardPresentation>(), screenManager);

            view.Button.Click();

            Assert.IsNotNull(viewCollection.FindView<IBuildScreenView>());

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void HandAnimationsPlayedInBuildingMode()
        {
            var screenManager = new ScreenService(new GameAssetsService(new AssetsMock()));
            var cardsRepository = new Database<ConstructionCardEntity>();

            var ghost = new SingletonDatabase<GhostEntity>();
            var viewCollection = new ViewsCollection();
            var handView = new HandView(viewCollection);
            // new HandPresenter(handView, new DataCollectionProvider<ConstructionCardData>(), new DataProvider<GhostData>(), screenManager);

            Assert.AreEqual("Choose", handView.Animator.Animation);
            ghost.Add(new GhostEntity(new ConstructionCardEntity(new ConstructionSchemeEntity())));
            Assert.AreEqual("Build", handView.Animator.Animation);
            ghost.Remove();
            Assert.AreEqual("Choose", handView.Animator.Animation);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void CancelWorks()
        {
            var assets = new AssetsMock();
            assets.AddPrefab("Screens/MainScreen", new DefaultViewPrefab(x => new MainScreenView(x)));
            var screenManager = new ScreenService(new GameAssetsService(assets));
            var schemesRepository = new Database<ConstructionSchemeEntity>();
            var cardsRepository = new Database<ConstructionCardEntity>();

            var scheme = new ConstructionSchemeEntity();
            schemesRepository.Add(scheme);

            var ghost = new SingletonDatabase<GhostEntity>();
            var viewCollection = new ViewsCollection();
            screenManager.Bind(new ScreenManagerView(viewCollection));
            var handView = new HandView(viewCollection);
            // new HandPresenter(handView, new DataCollectionProvider<ConstructionCardData>(), new DataProvider<GhostData>(), screenManager);

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
