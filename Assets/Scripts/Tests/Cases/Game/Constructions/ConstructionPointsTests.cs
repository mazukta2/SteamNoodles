using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;
using Game.Assets.Scripts.Game.Logic.Functions.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Models.Services.Fields;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Repositories;
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
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();

            var pointsService = new BuildingPointsService(0, 0, new GameTime(), 2, 2);
            var handService = new HandService(constructionsCardsRepository);
            var field = new Field(1, new IntPoint(11, 11));
            var buildingService = new BuildingService(constructionsRepository);
            var pointsOnBuilding = new PointsOnBuildingService(constructionsRepository, pointsService);

            var scheme = ConstructionSetups.GetDefaultScheme();
            constructionsSchemeRepository.Add(scheme);
            var card = handService.Add(scheme);

            Assert.AreEqual(1, scheme.Points.AsInt());
            Assert.AreEqual(0, scheme.AdjacencyPoints.Count);
            Assert.AreEqual(0, pointsService.GetValue());

            buildingService.Build(card, new FieldPosition(field, 0, 0), new FieldRotation(FieldRotation.Rotation.Top));

            Assert.AreEqual(1, pointsService.GetValue());

            pointsService.Dispose();
            pointsOnBuilding.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsAdjacencyPointsForBuildingModelWorking()
        {
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();

            var pointsService = new BuildingPointsService(0, 0, new GameTime(), 2, 2);
            var handService = new HandService(constructionsCardsRepository);
            var field = new Field(1, new IntPoint(11, 11));
            var buildingService = new BuildingService(constructionsRepository);
            var pointsOnBuilding = new PointsOnBuildingService(constructionsRepository, pointsService);
            
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

            buildingService.Build(card, new FieldPosition(field, 0, 0), new FieldRotation(FieldRotation.Rotation.Top));
            
            Assert.AreEqual(3, constructionsRepository.GetPoints(card.Scheme, new FieldPosition(field,1, 0), 
                new FieldRotation(FieldRotation.Rotation.Top)).AsInt());

            buildingService.Build(card, new FieldPosition(field, 1, 0), new FieldRotation(FieldRotation.Rotation.Top));

            Assert.AreEqual(4, pointsService.GetValue());

            pointsService.Dispose();
            pointsOnBuilding.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsNotGetPointsForBuildingOutsideField()
        {
            var constructionsRepository = new Repository<Construction>();
            var field = new Field(10, new IntPoint(3, 3));
            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonRepository<ConstructionGhost>();
            var buildingMode = new GhostMovingService(ghost, field.AsQuery(), controls);

            var scheme = new ConstructionScheme(
                points:new BuildingPoints(5));

            var viewCollection = new ViewsCollection();
            var card = new ConstructionCard(scheme);

            var view = new GhostPointsView(viewCollection);
            new GhostPointPresenter(view, ghost.AsQuery(), constructionsRepository.AsQuery(), field);

            ghost.Add(new ConstructionGhost(card, field));
            
            Assert.AreEqual("+5", view.Points.Value);

            buildingMode.SetTargetPosition(new FieldPosition(field, 999, 999));

            Assert.AreEqual("0", view.Points.Value);

            viewCollection.Dispose();
            controls.Dispose();
            buildingMode.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsPointsChangedByAdjacency()
        {
            var constructionsRepository = new Repository<Construction>();
            var field = new SingletonRepository<Field>(new Field(1, new IntPoint(5, 5)));
            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonRepository<ConstructionGhost>();
            var buildingMode = new GhostMovingService(ghost, field.AsQuery(), controls);
            var points = new GhostPointsService(ghost, constructionsRepository.AsQuery());
            var cells = new FieldCellsService(field.AsQuery(), ghost.AsQuery(), constructionsRepository.AsQuery());

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
            new GhostPointPresenter(view, ghost.AsQuery(), constructionsRepository.AsQuery(), field.Get());
           
            Assert.AreEqual("", view.Points.Value);
            
            ghost.Add(new ConstructionGhost(card, field.Get()));

            Assert.AreEqual("+5", view.Points.Value);

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field.Get(), 0, 0), new FieldRotation()));

            Assert.AreEqual("0", view.Points.Value);

            buildingMode.SetTargetPosition(new FieldPosition(field.Get(), -2, 0));

            Assert.AreEqual("+7", view.Points.Value);

            viewCollection.Dispose();
            controls.Dispose();
            buildingMode.Dispose();
            points.Dispose();
            cells.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsAdjacencyPointsBoundariesCorrect()
        {
            var constructionsRepository = new Repository<Construction>();
            var field = new Field(1, new IntPoint(15, 15));
            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonRepository<ConstructionGhost>();
            var buildingMode = new GhostMovingService(ghost, field.AsQuery(), controls);

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
            new GhostPointPresenter(view, ghost.AsQuery(), constructionsRepository.AsQuery(), field);

            ghost.Add(new ConstructionGhost(card, field));

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field, 0, 0), new FieldRotation()));

            buildingMode.SetTargetPosition(new FieldPosition(field, 0, 0));
            Assert.AreEqual("0", view.Points.Value);

            buildingMode.SetTargetPosition(new FieldPosition(field, 1, 0));
            Assert.AreEqual("0", view.Points.Value);

            buildingMode.SetTargetPosition(new FieldPosition(field, 2, 0));
            Assert.AreEqual("0", view.Points.Value);

            buildingMode.SetTargetPosition(new FieldPosition(field, 3, 0));
            Assert.AreEqual("+7", view.Points.Value);

            buildingMode.SetTargetPosition(new FieldPosition(field, -1, 0));
            Assert.AreEqual("0", view.Points.Value);

            buildingMode.SetTargetPosition(new FieldPosition(field, -2, 0));
            Assert.AreEqual("0", view.Points.Value);

            buildingMode.SetTargetPosition(new FieldPosition(field, -3, 0));
            Assert.AreEqual("+7", view.Points.Value);


            buildingMode.SetTargetPosition(new FieldPosition(field, 0, -1));
            Assert.AreEqual("+7", view.Points.Value);

            buildingMode.SetTargetPosition(new FieldPosition(field, 0, 1));
            Assert.AreEqual("+7", view.Points.Value);

            viewCollection.Dispose();
            buildingMode.Dispose();
            controls.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
