using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Common;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Customers
{
    public class ConstructionPointsTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsPointsForBuildingModelWorking()
        {
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();
            var deck = new DeckService<ConstructionScheme>();

            var pointsService = new BuildingPointsService(0, 0, new GameTime(), 2, 2);
            var schemesService = new SchemesService(constructionsSchemeRepository, deck);
            var handService = new HandService(constructionsCardsRepository, constructionsSchemeRepository);
            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var buildngService = new BuildingService(constructionsRepository, pointsService, handService, fieldService);

            var definition = ConstructionSetups.GetDefault();
            var scheme = schemesService.Add(definition);
            var card = handService.Add(scheme);

            Assert.AreEqual(1, scheme.Points.Value);
            Assert.AreEqual(0, scheme.AdjacencyPoints.Count);
            Assert.AreEqual(0, pointsService.Value);

            buildngService.Build(card, new FieldPosition(0, 0), new FieldRotation(FieldRotation.Rotation.Top));

            Assert.AreEqual(1, pointsService.Value);

            pointsService.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsAdjacencyPointsForBuildingModelWorking()
        {
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();
            var deck = new DeckService<ConstructionScheme>();

            var pointsService = new BuildingPointsService(0, 0, new GameTime(), 2, 2);
            var schemesService = new SchemesService(constructionsSchemeRepository, deck);
            var handService = new HandService(constructionsCardsRepository, constructionsSchemeRepository);
            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var buildngService = new BuildingService(constructionsRepository, pointsService, handService, fieldService);

            var definition = ConstructionSetups.GetDefault();
            definition.Points = 1;
            definition.AdjacencyPoints = new Dictionary<ConstructionDefinition, int>() { { definition, 2 } };
            definition.Placement = new int[,] { {1} };

            var scheme = schemesService.Add(definition);
            var card = handService.Add(scheme, new CardAmount(2));

            Assert.AreEqual(1, scheme.Points.Value);
            Assert.AreEqual(1, scheme.AdjacencyPoints.Count);
            Assert.AreEqual(0, pointsService.Value);

            buildngService.Build(card, new FieldPosition(0, 0), new FieldRotation(FieldRotation.Rotation.Top));
            
            Assert.AreEqual(3, buildngService.GetPoints(card, new FieldPosition(1, 0), new FieldRotation(FieldRotation.Rotation.Top)).Value);

            buildngService.Build(card, new FieldPosition(1, 0), new FieldRotation(FieldRotation.Rotation.Top));

            Assert.AreEqual(4, pointsService.Value);

            pointsService.Dispose();
        }

        [Test]
        public void IsPointsForBuildingWorking()
        {
            var game = new GameConstructor()
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 5)
                .Build();

            Assert.IsNotNull(game.LevelCollection.FindView<PointCounterWidgetView>());
            Assert.AreEqual("0/3", game.LevelCollection.FindView<PointCounterWidgetView>().Points.Value);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();

            Assert.AreEqual("+5", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Controls.Click();

            Assert.AreEqual("5/8", game.LevelCollection.FindView<PointCounterWidgetView>().Points.Value);

            game.Dispose();
        }

        [Test]
        public void IsNotGetPointsForBuildingOutsideField()
        {
            var game = new GameConstructor()
                .UpdateDefinition<ConstructionDefinition>(x => x.Points = 5)
                .Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();

            Assert.AreEqual("+5", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new Scripts.Game.Logic.Common.Math.GameVector3(999, 0, 999));

            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Dispose();
        }

        [Test]
        public void IsPointsChangedByAdjacency()
        {
            var construction1 = ConstructionSetups.GetDefault();
            {
                construction1.Points = 5;
                construction1.Placement = new int[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                };
                construction1.LevelViewPath = "DebugConstruction";
            };
            construction1.AdjacencyPoints = new Dictionary<ConstructionDefinition, int>() { { construction1, 2 } };

            var game = new GameConstructor()
                .AddDefinition("construction1", construction1)
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .UpdateDefinition<LevelDefinitionMock>(x => x.ConstructionsReward = new Dictionary<ConstructionDefinition, int>())
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { construction1, construction1 })
                .Build();

            Assert.AreEqual("0/3", game.LevelCollection.FindView<PointCounterWidgetView>().Points.Value);
            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            Assert.AreEqual("+5", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.Click();
            Assert.AreEqual(1, game.LevelCollection.FindViews<ConstructionView>().Count);
            Assert.AreEqual("5/8", game.LevelCollection.FindView<PointCounterWidgetView>().Points.Value);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            Assert.AreEqual("+7", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Dispose();
        }

        [Test]
        public void IsNotGetPointsForBuildingInWrongPlace()
        {
            var constructionDefinition = ConstructionSetups.GetDefault();
            {
                constructionDefinition.Points = 5;
                constructionDefinition.Placement = new int[,] {
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                };
                constructionDefinition.LevelViewPath = "DebugConstruction";
            };

            constructionDefinition.AdjacencyPoints = new Dictionary<ConstructionDefinition, int>() { { constructionDefinition, 2 } };
            var game = new GameConstructor()
                .AddDefinition("construction1", constructionDefinition)
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .UpdateDefinition<LevelDefinitionMock>(x => x.ConstructionsReward = new Dictionary<ConstructionDefinition, int>())
                .UpdateDefinition<LevelDefinitionMock>(x => x.
                    StartingHand = new List<ConstructionDefinition>() { constructionDefinition, constructionDefinition })
                .Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new GameVector3(1, 0, 0));
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new GameVector3(2, 0, 0));
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new GameVector3(3, 0, 0));
            Assert.AreEqual("+7", game.LevelCollection.FindView<BuildScreenView>().Points.Value);


            game.Controls.MovePointer(new GameVector3(-1, 0, 0));
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            Assert.AreEqual("0", game.LevelCollection.FindView<BuildScreenView>().Points.Value);
            game.Controls.MovePointer(new GameVector3(-3, 0, 0));
            Assert.AreEqual("+7", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new GameVector3(0, 0, -1));
            Assert.AreEqual("+7", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Controls.MovePointer(new GameVector3(0, 0, 1));
            Assert.AreEqual("+7", game.LevelCollection.FindView<BuildScreenView>().Points.Value);

            game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
