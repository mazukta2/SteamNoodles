using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions
{
    public class ConstructionPointsTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsPointsForBuildingModelWorking()
        {
            var constructionsRepository = new Database<Construction>();
            var constructionsCardsRepository = new Database<ConstructionCard>();
            var constructionsSchemeRepository = new Database<ConstructionScheme>();

            var pointsService = new BuildingPointsService(0, 0, new GameTime(), 2, 2);
            var handService = new HandService(constructionsCardsRepository);
            var field = new Field(1, new IntPoint(11, 11));
            // var buildingService = new BuildingService(constructionsRepository);
            var pointsOnBuilding = new PointsOnBuildingService(constructionsRepository, pointsService);

            var scheme = ConstructionSetups.GetDefaultScheme();
            constructionsSchemeRepository.Add(scheme);
            var card = handService.Add(scheme);

            Assert.AreEqual(1, scheme.Points.AsInt());
            Assert.AreEqual(0, scheme.AdjacencyPoints.Count);
            Assert.AreEqual(0, pointsService.GetValue());

            // buildingService.Build(card, new FieldPosition(field, 0, 0), new FieldRotation(FieldRotation.Rotation.Top));

            Assert.AreEqual(1, pointsService.GetValue());

            pointsService.Dispose();
            pointsOnBuilding.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsAdjacencyPointsForBuildingModelWorking()
        {
            var constructionsRepository = new Database<Construction>();
            var constructionsCardsRepository = new Database<ConstructionCard>();
            var constructionsSchemeRepository = new Database<ConstructionScheme>();
            var ghost = new SingletonDatabase<ConstructionGhost>();

            var pointsService = new BuildingPointsService(0, 0, new GameTime(), 2, 2);
            var handService = new HandService(constructionsCardsRepository);
            var field = new Field(1, new IntPoint(11, 11));
            // var buildingService = new BuildingService(constructionsRepository);
            var pointsOnBuilding = new PointsOnBuildingService(constructionsRepository, pointsService);
            // var building = new BuildingAggregatorService(new SingletonRepository<Field>(), ghost, constructionsRepository);
            // var data = new DataProvider<GhostData>();
            
            var scheme = new ConstructionScheme(
                defId:new DefId("Construction"),
                placement:new ContructionPlacement(new[,] { { 1 } }),
                points:new BuildingPoints(1));
            scheme.SetAdjecity(new AdjacencyBonuses(new Dictionary<ConstructionScheme, BuildingPoints>() 
                { { scheme, new BuildingPoints(2) } }));

            constructionsSchemeRepository.Add(scheme);
            var card = handService.Add(scheme, new CardAmount(2));

            Assert.AreEqual(1, scheme.Points.AsInt());
            Assert.AreEqual(1, scheme.AdjacencyPoints.Count);
            Assert.AreEqual(0, pointsService.GetValue());

            // buildingService.Build(card, new FieldPosition(field, 0, 0), new FieldRotation(FieldRotation.Rotation.Top));

            ghost.Add(new ConstructionGhost(card, new FieldPosition(field, 1, 0)));
            
            // Assert.AreEqual(3, data.Get().Points.AsInt());

            // buildingService.Build(card, new FieldPosition(field, 1, 0), new FieldRotation(FieldRotation.Rotation.Top));

            Assert.AreEqual(4, pointsService.GetValue());

            pointsService.Dispose();
            pointsOnBuilding.Dispose();
            // building.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsNotGetPointsForBuildingOutsideField()
        {
            var constructionsRepository = new Database<Construction>();
            var field = new Field(10, new IntPoint(3, 3));
            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonDatabase<ConstructionGhost>();
            // var moving = new GhostMovingControlsService(ghost, new SingletonDatabase<Field>(), controls);
            // var building = new BuildingAggregatorService(new SingletonRepository<Field>(), ghost, constructionsRepository);

            var scheme = new ConstructionScheme(
                points:new BuildingPoints(5));

            var viewCollection = new ViewsCollection();
            var card = new ConstructionCard(scheme);

            var view = new GhostPointsView(viewCollection);
            // new GhostPointPresenter(view, new DataProvider<GhostData>(), new DataCollectionProvider<ConstructionPresenterData>(), field);

            ghost.Add(new ConstructionGhost(card, field));
            
            Assert.AreEqual("+5", view.Points.Value);

            // moving.SetTargetPosition(new FieldPosition(field, 999, 999));

            Assert.AreEqual("0", view.Points.Value);

            viewCollection.Dispose();
            controls.Dispose();
            // moving.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsPointsChangedByAdjacency()
        {
            var constructionsRepository = new Database<Construction>();
            var field = new SingletonDatabase<Field>(new Field(1, new IntPoint(5, 5)));
            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonDatabase<ConstructionGhost>();
            // var moving = new GhostMovingControlsService(ghost, field, controls);
            // var building = new BuildingAggregatorService(field, ghost, constructionsRepository);

            var placement = new ContructionPlacement(new [,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                });
            var scheme = new ConstructionScheme(
                placement: placement,
                points:new BuildingPoints(5));
            scheme.SetAdjecity(new AdjacencyBonuses(new Dictionary<ConstructionScheme, BuildingPoints>() { { scheme, new BuildingPoints(2) } }));

            var viewCollection = new ViewsCollection();
            var card = new ConstructionCard(scheme);

            var view = new GhostPointsView(viewCollection);
            // new GhostPointPresenter(view, new DataProvider<GhostData>(), new DataCollectionProvider<ConstructionPresenterData>(), field.Get());
           
            Assert.AreEqual("", view.Points.Value);
            
            ghost.Add(new ConstructionGhost(card, field.Get()));

            Assert.AreEqual("+5", view.Points.Value);

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field.Get(), 0, 0), new FieldRotation()));

            Assert.AreEqual("0", view.Points.Value);

            // moving.SetTargetPosition(new FieldPosition(field.Get(), -2, 0));

            Assert.AreEqual("+7", view.Points.Value);

            viewCollection.Dispose();
            controls.Dispose();
            // moving.Dispose();
            // building.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsAdjacencyPointsBoundariesCorrect()
        {
            var constructionsRepository = new Database<Construction>();
            var field = new Field(1, new IntPoint(15, 15));
            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonDatabase<ConstructionGhost>();
            // var moving = new GhostMovingControlsService(ghost, new SingletonDatabase<Field>(), controls);
            // var building = new BuildingAggregatorService(new SingletonRepository<Field>(), ghost, constructionsRepository);

            var placement = new ContructionPlacement(new [,] {
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                });
            var scheme = new ConstructionScheme(
                placement: placement,
                points: new BuildingPoints(5));
            scheme.SetAdjecity(new AdjacencyBonuses(new Dictionary<ConstructionScheme, BuildingPoints>() { { scheme, new BuildingPoints(2) } }));

            var viewCollection = new ViewsCollection();
            var card = new ConstructionCard(scheme);

            var view = new GhostPointsView(viewCollection);
            // new GhostPointPresenter(view, new DataProvider<GhostData>(), new DataCollectionProvider<ConstructionPresenterData>(), field);

            ghost.Add(new ConstructionGhost(card, field));

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field, 0, 0), new FieldRotation()));

            // moving.SetTargetPosition(new FieldPosition(field, 0, 0));
            Assert.AreEqual("0", view.Points.Value);

            // moving.SetTargetPosition(new FieldPosition(field, 1, 0));
            Assert.AreEqual("0", view.Points.Value);

            // moving.SetTargetPosition(new FieldPosition(field, 2, 0));
            Assert.AreEqual("0", view.Points.Value);

            // moving.SetTargetPosition(new FieldPosition(field, 3, 0));
            Assert.AreEqual("+7", view.Points.Value);

            // moving.SetTargetPosition(new FieldPosition(field, -1, 0));
            Assert.AreEqual("0", view.Points.Value);

            // moving.SetTargetPosition(new FieldPosition(field, -2, 0));
            Assert.AreEqual("0", view.Points.Value);

            // moving.SetTargetPosition(new FieldPosition(field, -3, 0));
            Assert.AreEqual("+7", view.Points.Value);


            // moving.SetTargetPosition(new FieldPosition(field, 0, -1));
            Assert.AreEqual("+7", view.Points.Value);

            // moving.SetTargetPosition(new FieldPosition(field, 0, 1));
            Assert.AreEqual("+7", view.Points.Value);

            viewCollection.Dispose();
            // moving.Dispose();
            controls.Dispose();
            // building.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
