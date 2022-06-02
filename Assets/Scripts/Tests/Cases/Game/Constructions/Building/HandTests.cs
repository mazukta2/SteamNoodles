using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Presenters.Commands;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using Game.Tests.Cases;
using NUnit.Framework;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Hand
{
    public class HandTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsSameCardsCollapse()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var scheme1 = new ConstructionScheme();
            var scheme2 = new ConstructionScheme();
            schemesRepository.Add(scheme1);
            schemesRepository.Add(scheme2);

            var cardsRepository = new Repository<ConstructionCard>();
            var hand = new HandService(cardsRepository, schemesRepository);

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
        public void IsSameCardsRemoved()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var scheme1 = new ConstructionScheme();
            var scheme2 = new ConstructionScheme();
            schemesRepository.Add(scheme1);
            schemesRepository.Add(scheme2);

            var cardsRepository = new Repository<ConstructionCard>();
            var hand = new HandService(cardsRepository, schemesRepository);

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
        public void IsCardsSpawnedOnStart()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var deck = new DeckService<ConstructionScheme>();
            var hand = new HandService(cardsRepository, schemesRepository);
            var schemes = new SchemesService(schemesRepository, deck);
            var points = new BuildingPointsService();

            Assert.AreEqual(0, cardsRepository.Count);

            var stageLevel = new StageLevel(new[] { scheme });

            var flow = new StageFlowService(stageLevel, hand, schemes, points);
            flow.Start();

            Assert.AreEqual(1, cardsRepository.Count);

            flow.Dispose();
            points.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsYouGetNewCardsAfterLevelUp()
        {
            var cardsRepository = new Repository<ConstructionCard>();
            var schemesRepository = new Repository<ConstructionScheme>();

            var time = new GameTime();
            var stageLevel = new StageLevel();
            var pointsService = new BuildingPointsService(0,0, time, 2, 2);

            var deck = new DeckService<ConstructionScheme>();
            deck.Add(new ConstructionScheme());
            var hand = new HandService(cardsRepository, schemesRepository);
            var schemes = new SchemesService(schemesRepository, deck);

            var flow = new StageFlowService(stageLevel, hand, schemes, pointsService, 1);
            Assert.AreEqual(new BuildingLevel(0), pointsService.GetCurrentLevel());

            Assert.AreEqual(0, cardsRepository.Count);

            pointsService.ChangePoints(new BuildingPoints(3));
            Assert.AreEqual(new BuildingLevel(1), pointsService.GetCurrentLevel());

            Assert.AreEqual(1, cardsRepository.Count);

            pointsService.Dispose();
            flow.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsCardsSpawnedAndRemovedInPresenter()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var scheme1 = new ConstructionScheme();
            var scheme2 = new ConstructionScheme();
            schemesRepository.Add(scheme1);
            schemesRepository.Add(scheme2);

            var cardsRepository = new Repository<ConstructionCard>();
            var hand = new HandService(cardsRepository, schemesRepository);
            var mode = new BuildingModeService();

            var viewCollection = new ViewsCollection();
            var handView = new HandView(viewCollection);
            var commands = new PresenterCommandsMock();
            new HandPresenter(handView, cardsRepository, mode, commands);

            Assert.AreEqual(0, handView.Collection.FindViews<IHandConstructionView>().Count);
            var card1 = hand.Add(scheme1);
            Assert.AreEqual(1, handView.Collection.FindViews<IHandConstructionView>().Count);
            var card2 = hand.Add(scheme2);
            Assert.AreEqual(2, handView.Collection.FindViews<IHandConstructionView>().Count);
            hand.Remove(card1);
            Assert.AreEqual(1, handView.Collection.FindViews<IHandConstructionView>().Count);
            hand.Remove(card2);
            Assert.AreEqual(0, handView.Collection.FindViews<IHandConstructionView>().Count);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsAmountShownCorrectly()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            new HandConstructionPresenter(link, view);

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
        public void IsConstructionIconSetted()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();
            
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None, 
                ContructionPlacement.One, 
                LocalizationTag.None, 
                new BuildingPoints(0), 
                new AdjacencyBonuses(), 
                "image", "", new Requirements());
            schemesRepository.Add(scheme);

            var link = cardsRepository.Add(new ConstructionCard(scheme));

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            new HandConstructionPresenter(link, view);

            Assert.AreEqual("image", view.Image.Path);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsTooltipViewSpawning()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            var commands = new PresenterCommandsMock();
            new HandConstructionPresenter(link, view, commands);

            Assert.IsTrue(commands.IsEmpty());
            view.SetHighlight(true);
            Assert.IsTrue(commands.Last<OpenConstructionTooltipCommand>());
            view.SetHighlight(false);
            Assert.IsTrue(commands.Last<CloseConstructionTooltipCommand>());

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsClickingOpensBuildingScreen()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            var viewCollection = new ViewsCollection();
            var view = new HandConstructionView(viewCollection);
            var commands = new PresenterCommandsMock();
            new HandConstructionPresenter(link, view, commands);

            view.Button.Click();

            Assert.IsTrue(commands.Only<OpenBuildingScreenCommand>());

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsHandAnimationsPlayedInBuildingMode()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            var mode = new BuildingModeService();
            var viewCollection = new ViewsCollection();
            var handView = new HandView(viewCollection);
            var commands = new PresenterCommandsMock();
            new HandPresenter(handView, cardsRepository, mode, commands);

            Assert.AreEqual("Choose", handView.Animator.Animation);
            mode.Show(link);
            Assert.AreEqual("Build", handView.Animator.Animation);
            mode.Hide();
            Assert.AreEqual("Choose", handView.Animator.Animation);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsCancelWorks()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var mode = new BuildingModeService();
            var viewCollection = new ViewsCollection();
            var handView = new HandView(viewCollection);
            var commands = new PresenterCommandsMock();
            new HandPresenter(handView, cardsRepository, mode, commands);

            handView.CancelButton.Click();

            Assert.IsTrue(commands.Last<OpenMainScreenCommand>());

            viewCollection.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
