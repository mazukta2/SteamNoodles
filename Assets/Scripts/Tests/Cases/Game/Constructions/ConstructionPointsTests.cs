using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using Game.Tests.Cases;
using NUnit.Framework;
using System.Collections.Generic;

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
            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);
            var buildngService = new BuildingService(constructionsRepository, constructionsService, pointsService, handService, fieldService);

            var definition = ConstructionSetups.GetDefault();
            var scheme = schemesService.Add(definition);
            var card = handService.Add(scheme);

            Assert.AreEqual(1, scheme.Points.Value);
            Assert.AreEqual(0, scheme.AdjacencyPoints.Count);
            Assert.AreEqual(0, pointsService.GetValue());

            buildngService.Build(card, new FieldPosition(0, 0), new FieldRotation(FieldRotation.Rotation.Top));

            Assert.AreEqual(1, pointsService.GetValue());

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
            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);
            var buildngService = new BuildingService(constructionsRepository, constructionsService, pointsService, handService, fieldService);

            var definition = ConstructionSetups.GetDefault();
            definition.Points = 1;
            definition.AdjacencyPoints = new Dictionary<ConstructionDefinition, int>() { { definition, 2 } };
            definition.Placement = new int[,] { {1} };

            var scheme = schemesService.Add(definition);
            var card = handService.Add(scheme, new CardAmount(2));

            Assert.AreEqual(1, scheme.Points.Value);
            Assert.AreEqual(1, scheme.AdjacencyPoints.Count);
            Assert.AreEqual(0, pointsService.GetValue());

            buildngService.Build(card, new FieldPosition(0, 0), new FieldRotation(FieldRotation.Rotation.Top));
            
            Assert.AreEqual(3, constructionsService.GetPoints(card, new FieldPosition(1, 0), new FieldRotation(FieldRotation.Rotation.Top)).Value);

            buildngService.Build(card, new FieldPosition(1, 0), new FieldRotation(FieldRotation.Rotation.Top));

            Assert.AreEqual(4, pointsService.GetValue());

            pointsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsNotGetPointsForBuildingOutsideField()
        {
            var constructionsRepository = new Repository<Construction>();
            var buildinMode = new BuildingModeService();
            var fieldService = new FieldService(10, new IntPoint(3, 3));
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);

            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                ContructionPlacement.One,
                LocalizationTag.None,
                new BuildingPoints(5),
                new AdjacencyBonuses(),
                "", "", new Requirements());

            var viewCollection = new ViewsCollection();
            var card = new ConstructionCard(scheme);

            var view = new GhostPointsView(viewCollection);
            new GhostPointPresenter(view, buildinMode, constructionService, fieldService);

            buildinMode.Show(card);
            
            Assert.AreEqual("+5", view.Points.Value);

            buildinMode.SetGhostPosition(new FieldPosition(999, 999), new FieldRotation());

            Assert.AreEqual("0", view.Points.Value);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsPointsChangedByAdjacency()
        {
            var constructionsRepository = new Repository<Construction>();
            var buildinMode = new BuildingModeService();
            var fieldService = new FieldService(1, new IntPoint(5, 5));
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);

            var placement = new ContructionPlacement(new int[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement,
                LocalizationTag.None,
                new BuildingPoints(5),
                new AdjacencyBonuses(),
                "", "", new Requirements());
            scheme.SetAdjecity(new AdjacencyBonuses(new Dictionary<ConstructionScheme, BuildingPoints>() { { scheme, new BuildingPoints(2) } }));

            var viewCollection = new ViewsCollection();
            var card = new ConstructionCard(scheme);

            var view = new GhostPointsView(viewCollection);
            new GhostPointPresenter(view, buildinMode, constructionService, fieldService);

            buildinMode.Show(card);

            Assert.AreEqual("+5", view.Points.Value);

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(0, 0), new FieldRotation()));

            buildinMode.SetGhostPosition(new FieldPosition(0, 0), new FieldRotation());
            Assert.AreEqual("0", view.Points.Value);

            buildinMode.SetGhostPosition(new FieldPosition(-2, 0), new FieldRotation());

            Assert.AreEqual("+7", view.Points.Value);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsAdjecencyPointsBoundariesCorrect()
        {
            var constructionsRepository = new Repository<Construction>();
            var buildinMode = new BuildingModeService();
            var fieldService = new FieldService(1, new IntPoint(15, 15));
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);

            var placement = new ContructionPlacement(new int[,] {
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement,
                LocalizationTag.None,
                new BuildingPoints(5),
                new AdjacencyBonuses(),
                "", "", new Requirements());
            scheme.SetAdjecity(new AdjacencyBonuses(new Dictionary<ConstructionScheme, BuildingPoints>() { { scheme, new BuildingPoints(2) } }));

            var viewCollection = new ViewsCollection();
            var card = new ConstructionCard(scheme);

            var view = new GhostPointsView(viewCollection);
            new GhostPointPresenter(view, buildinMode, constructionService, fieldService);

            buildinMode.Show(card);

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(0, 0), new FieldRotation()));

            buildinMode.SetGhostPosition(new FieldPosition(0, 0), new FieldRotation());
            Assert.AreEqual("0", view.Points.Value);

            buildinMode.SetGhostPosition(new FieldPosition(1, 0), new FieldRotation());
            Assert.AreEqual("0", view.Points.Value);

            buildinMode.SetGhostPosition(new FieldPosition(2, 0), new FieldRotation());
            Assert.AreEqual("0", view.Points.Value);

            buildinMode.SetGhostPosition(new FieldPosition(3, 0), new FieldRotation());
            Assert.AreEqual("+7", view.Points.Value);

            buildinMode.SetGhostPosition(new FieldPosition(-1, 0), new FieldRotation());
            Assert.AreEqual("0", view.Points.Value);

            buildinMode.SetGhostPosition(new FieldPosition(-2, 0), new FieldRotation());
            Assert.AreEqual("0", view.Points.Value);

            buildinMode.SetGhostPosition(new FieldPosition(-3, 0), new FieldRotation());
            Assert.AreEqual("+7", view.Points.Value);


            buildinMode.SetGhostPosition(new FieldPosition(0, -1), new FieldRotation());
            Assert.AreEqual("+7", view.Points.Value);

            buildinMode.SetGhostPosition(new FieldPosition(0, 1), new FieldRotation());
            Assert.AreEqual("+7", view.Points.Value);

            viewCollection.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
