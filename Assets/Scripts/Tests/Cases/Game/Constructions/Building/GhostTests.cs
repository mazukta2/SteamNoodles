using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Localization;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
{
    public class GhostTests
    {
        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostCreatedInBuildingMode()
        {
            var buildingMode = new BuildingModeService(new FieldService(0, IntPoint.Zero));

            var scheme = new ConstructionScheme();

            var viewCollection = new ViewsCollection();
            var view = new GhostManagerView(viewCollection);
            new GhostManagerPresenter(view, buildingMode);

            //Assert.IsTrue(commands.IsEmpty());

            buildingMode.Show(new ConstructionCard(scheme));

            //Assert.IsTrue(commands.Last<AddGhostCommand>());

            buildingMode.Hide();

            //Assert.IsTrue(commands.Last<RemoveGhostCommand>());

            viewCollection.Dispose();
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void IsAvailableCellsIsHighlightedInGhostMode()
        {
            var constructionsRepository = new Repository<Construction>();


            var scheme = new ConstructionScheme();

            var fieldService = new FieldService(10, new IntPoint(3, 3));
            var buildingMode = new BuildingModeService(fieldService);
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, buildingMode, fieldService, constructionService);

            var cells = view.CellsContainer.FindViews<CellView>();
            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            buildingMode.Show(new ConstructionCard(scheme));

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.IsReadyToPlace || x.State.Value == CellPlacementStatus.IsAvailableGhostPlace));

            buildingMode.Hide();

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            viewCollection.Dispose();
            constructionService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            var constructionsRepository = new Repository<Construction>();


            var placement = new ContructionPlacement(new int[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                });

            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "", new Requirements());

            var fieldService = new FieldService(0.5f, new IntPoint(7, 7));
            var buildingMode = new BuildingModeService(fieldService);
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, buildingMode, fieldService, constructionService);

            buildingMode.Show(new ConstructionCard(scheme));

            var cells = view.CellsContainer.FindViews<CellView>();
            var highlighedCells = cells.Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace).OrderBy(x => x.LocalPosition.Value.X);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.AreEqual(new GameVector3(0, 0, 0), highlighedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlighedCells.Last().LocalPosition.Value);

            buildingMode.SetTargetPosition(new FieldPosition(1, 0));

            highlighedCells = cells.Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace).OrderBy(x => x.LocalPosition.Value.X);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlighedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(1f, 0, 0), highlighedCells.Last().LocalPosition.Value);

            viewCollection.Dispose();
            constructionService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostChangesPositions()
        {
            var mockControls = new ControlsMock();
            var controls = new GameControlsService(mockControls);

            var placement = new ContructionPlacement(new int[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "", new Requirements());

            var fieldService = new FieldService(0.5f, new IntPoint(7, 7));
            var buildingMode = new BuildingModeService(fieldService);

            var viewCollection = new ViewsCollection();

            buildingMode.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, buildingMode, fieldService, controls);

            Assert.AreEqual(new FieldPosition(0, 0), buildingMode.GetPosition());

            mockControls.MovePointer(new GameVector3(0.75f, 0, 0)); // move left

            Assert.AreEqual(new FieldPosition(1, 0), buildingMode.GetPosition());

            //// -0.5 to 0.5
            //mockControls.MovePointer(new GameVector3(0, 0, 0f));
            ////Assert.AreEqual(new FieldPosition(1,  0), buildingMode.GetPosition());

            //// -0.5 to -1.5
            //mockControls.MovePointer(new GameVector3(-0.9f, 0, -1f));
            //Assert.AreEqual(new FieldPosition(-1,  -1), buildingMode.GetPosition());

            //// -1.5 to -2.5
            //mockControls.MovePointer(new GameVector3(-1.9f, 0, -2f));
            //Assert.AreEqual(new FieldPosition(-2, -2), buildingMode.GetPosition());

            viewCollection.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostReactsToChangingPosition()
        {
            var mockControls = new ControlsMock();
            var controls = new GameControlsService(mockControls);


            var placement = new ContructionPlacement(new int[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "", new Requirements());

            var fieldService = new FieldService(0.5f, new IntPoint(7, 7));
            var buildingMode = new BuildingModeService(fieldService);
            var viewCollection = new ViewsCollection();

            buildingMode.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, buildingMode, fieldService, controls);

            Assert.AreEqual(new GameVector3(0.25f, 0f, 0), view.LocalPosition.Value);

            buildingMode.SetTargetPosition(new FieldPosition(1, 0));

            Assert.AreEqual(new GameVector3(0.75f, 0f, 0), view.LocalPosition.Value);

            viewCollection.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostViewChangeVisualIfItAvailable()
        {
            var constructionsRepository = new Repository<Construction>();


            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                ContructionPlacement.One,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "", new Requirements() { DownEdge = true });

            var card = new ConstructionCard(scheme);

            var fieldService = new FieldService(1, new IntPoint(5, 5));
            var buildingMode = new BuildingModeService(fieldService);
            var viewCollection = new ViewsCollection();

            buildingMode.Show(card);

            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);

            var view = new ConstructionModelView(viewCollection);
            new GhostModelPresenter(view, buildingMode, constructionsService);

            // disallowed because DownEdge enabled.
            Assert.IsFalse(constructionsService.CanPlace(card, new FieldPosition(0, 0), new FieldRotation()));

            Assert.AreEqual("Dragging", ((AnimatorMock)view.Animator).Animation);
            Assert.AreEqual("Disallowed", ((AnimatorMock)view.BorderAnimator).Animation);

            buildingMode.SetTargetPosition(new FieldPosition(0, -2));
            Assert.IsTrue(constructionsService.CanPlace(card, new FieldPosition(0, -2), new FieldRotation()));

            Assert.AreEqual("Dragging", ((AnimatorMock)view.Animator).Animation);
            Assert.AreEqual("Idle", ((AnimatorMock)view.BorderAnimator).Animation);

            viewCollection.Dispose();
            constructionsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void CellsStatusCorrect()
        {
            var constructionsRepository = new Repository<Construction>();


            var placement = new ContructionPlacement(new int[,] {
                    { 1, 1 },
                    { 1, 1 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "", new Requirements() { DownEdge = true });

            var fieldService = new FieldService(1, new IntPoint(5, 5));
            var buildingMode = new BuildingModeService(fieldService);
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);

            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, buildingMode, fieldService, constructionService);

            buildingMode.Show(new ConstructionCard(scheme));

            buildingMode.SetTargetPosition(new FieldPosition(0, -1));

            var cells = view.CellsContainer.FindViews<CellView>();
            var actual = ToArray(5, cells);
            var expected = new int[5, 5]
            {
                { 1,1,0,0,0 },
                { 1,1,0,0,0 },
                { 1,2,3,0,0 },
                { 1,2,3,0,0 },
                { 1,1,0,0,0 }
            };

            Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");

            constructionService.Dispose();
            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsCellsIsColoredRightWithGhost()
        {
            var constructionsRepository = new Repository<Construction>();


            var placement = new ContructionPlacement(new int[,] {
                    { 1, 1 },
                    { 1, 1 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "", new Requirements() { DownEdge = true });

            var fieldService = new FieldService(1, new IntPoint(5, 5));
            var buildingMode = new BuildingModeService(fieldService);
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            var viewCollection = new ViewsCollection();

            buildingMode.Show(new ConstructionCard(scheme));

            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, buildingMode, fieldService, constructionService);

            buildingMode.SetTargetPosition(new FieldPosition(0, -1));

            var cells = view.CellsContainer.FindViews<CellView>();
            var actual = ToArray(5, cells);
            var expected = new int[5, 5]
            {
                { 1,1,0,0,0 },
                { 1,1,0,0,0 },
                { 1,2,3,0,0 },
                { 1,2,3,0,0 },
                { 1,1,0,0,0 }
            };

            Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");

            viewCollection.Dispose();
            constructionService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostSpawnsRightModel()
        {
            var mockControls = new ControlsMock();
            var controls = new GameControlsService(mockControls);

            var fieldService = new FieldService(1, new IntPoint(3, 3));
            var buildingMode = new BuildingModeService(fieldService);

            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                ContructionPlacement.One,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "view", new Requirements());

            var viewCollection = new ViewsCollection();
            buildingMode.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, buildingMode, fieldService, controls);

            //Assert.IsTrue(commands.Last<AddGhostModelCommand>());
            //Assert.AreEqual(scheme.Id, commands.Commands.OfType<AddGhostModelCommand>().Last().Scheme.Id);

            viewCollection.Dispose();
            controls.Dispose();
        }

        [Test]
        public void IsRotationWorking()
        {
            var constructionsRepository = new Repository<Construction>();

            var placement = new ContructionPlacement(new int[,] {
                    { 1, 1 },
                    { 1, 0 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "", new Requirements());

            var fieldService = new FieldService(1, new IntPoint(5, 5));
            var buildingMode = new BuildingModeService(fieldService);

            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            var viewCollection = new ViewsCollection();

            buildingMode.Show(new ConstructionCard(scheme));

            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, buildingMode, fieldService, constructionService);

            buildingMode.SetTargetPosition(new FieldPosition(0, 0));

            var cells = view.CellsContainer.FindViews<CellView>();
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,2,1,1 },
                { 1,1,1,1,1 }
            });

            buildingMode.SetRotation(new FieldRotation(FieldRotation.Rotation.Right));
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,2,1 },
                { 1,1,1,1,1 }
            });

            buildingMode.SetRotation(new FieldRotation(FieldRotation.Rotation.Bottom));
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,1,2,1 },
                { 1,1,2,2,1 },
                { 1,1,1,1,1 }
            });

            buildingMode.SetRotation(new FieldRotation(FieldRotation.Rotation.Left));
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,1,1 }
            });

            viewCollection.Dispose();
            constructionService.Dispose();

            void CheckPosition(int[,] expected)
            {
                var actual = ToArray(5, cells);
                Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");
            }
        }
        
        int[,] ToArray(int size, Dictionary<IntPoint, CellPlacementStatus> cells)
        {
            var actual = new int[size, size];
            var chkd = 0;
            for (int x = -size / 2; x <= size / 2; x++)
                for (int y = -size / 2; y <= size / 2; y++)
                {
                    actual[x + size / 2, y + size / 2] = (int)cells[new IntPoint(x, y)];
                    chkd++;
                }

            Assert.AreEqual(chkd, cells.Count());
            return actual;
        }

        int[,] ToArray(int size, IReadOnlyCollection<CellView> cells)
        {
            var actual = new int[size, size];
            var chkd = 0;
            for (int x = -size / 2; x <= size / 2; x++)
                for (int y = -size / 2; y <= size / 2; y++)
                {
                    actual[x + size / 2, y + size / 2] = (int)cells.First(c => c.LocalPosition.Value == new GameVector3(x, 0, y)).State.Value;
                    chkd++;
                }

            Assert.AreEqual(chkd, cells.Count());
            return actual;
        }

        string PrintPosition(int[,] matrix)
        {
            var result = "";
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    result += matrix[x, y].ToString();
                }
                result += "\r\n";
            }
            return result;
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
