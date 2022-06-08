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
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Presenters.Commands;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Tests.Cases;
using NUnit.Framework;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Hand
{
    public class HandTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void SameCardsCollapse()
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
        public void SameCardsRemoved()
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
        public void CardsSpawnedOnStart()
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

            var flow = new RewardsService(stageLevel, hand, schemes, points);
            flow.Start();

            Assert.AreEqual(1, cardsRepository.Count);

            flow.Dispose();
            points.Dispose();
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
            var hand = new HandService(cardsRepository, schemesRepository);
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
            var commands = new CommandsMock();
            new HandPresenter(handView, cardsRepository, mode, commands);

            Assert.AreEqual(0, commands.Commands.OfType<AddHandConstructionCommand>().Count());
            var card1 = hand.Add(scheme1);
            Assert.AreEqual(1, commands.Commands.OfType<AddHandConstructionCommand>().Count());
            var card2 = hand.Add(scheme2);
            Assert.AreEqual(2, commands.Commands.OfType<AddHandConstructionCommand>().Count());
            hand.Remove(card1);
            Assert.AreEqual(1, commands.Commands.OfType<RemoveHandConstructionCommand>().Count());
            hand.Remove(card2);
            Assert.AreEqual(2, commands.Commands.OfType<RemoveHandConstructionCommand>().Count());

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void AmountShownCorrectly()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);
            var commands = new CommandsMock();

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            new HandConstructionPresenter(link, view, commands);

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
        public void ConstructionIconSetted()
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
            var commands = new CommandsMock();

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            new HandConstructionPresenter(link, view, commands);

            Assert.AreEqual("image", view.Image.Path);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void TooltipViewSpawning()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            var viewCollection = new ViewsCollection();

            var view = new HandConstructionView(viewCollection);
            var commands = new CommandsMock();
            new HandConstructionPresenter(link, view, commands);

            Assert.IsTrue(commands.IsEmpty());
            view.SetHighlight(true);
            Assert.IsTrue(commands.Last<OpenConstructionTooltipCommand>());
            view.SetHighlight(false);
            Assert.IsTrue(commands.Last<CloseConstructionTooltipCommand>());

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void ClickingOpensBuildingScreen()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            var link = cardsRepository.Add(card);

            var viewCollection = new ViewsCollection();
            var view = new HandConstructionView(viewCollection);
            var commands = new CommandsMock();
            new HandConstructionPresenter(link, view, commands);

            view.Button.Click();

            Assert.IsTrue(commands.Only<OpenBuildingScreenCommand>());

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void HandAnimationsPlayedInBuildingMode()
        {
            var cardsRepository = new Repository<ConstructionCard>();

            var mode = new BuildingModeService();
            var viewCollection = new ViewsCollection();
            var handView = new HandView(viewCollection);
            var commands = new CommandsMock();
            new HandPresenter(handView, cardsRepository, mode, commands);

            Assert.AreEqual("Choose", handView.Animator.Animation);
            mode.Show(new ConstructionCard(new ConstructionScheme()));
            Assert.AreEqual("Build", handView.Animator.Animation);
            mode.Hide();
            Assert.AreEqual("Choose", handView.Animator.Animation);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void CancelWorks()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);

            var mode = new BuildingModeService();
            var viewCollection = new ViewsCollection();
            var handView = new HandView(viewCollection);
            var commands = new CommandsMock();
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
