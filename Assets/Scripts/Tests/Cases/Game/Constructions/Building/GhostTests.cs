using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Presenters.Commands;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Tests.Cases;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
{
    public class GhostTests
    {
        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostCreatedInBuildingMode()
        {
            var buildingMode = new BuildingModeService();
            var commands = new CommandsMock();

            var scheme = new ConstructionScheme();

            var viewCollection = new ViewsCollection();
            var view = new GhostManagerView(viewCollection);
            new GhostManagerPresenter(view, buildingMode, commands);

            Assert.IsTrue(commands.IsEmpty());

            buildingMode.Show(new ConstructionCard(scheme));

            Assert.IsTrue(commands.Last<AddGhostCommand>());

            buildingMode.Hide();

            Assert.IsTrue(commands.Last<RemoveGhostCommand>());

            viewCollection.Dispose();
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void IsAvailableCellsIsHighlightedInGhostMode()
        {
            var constructionsRepository = new Repository<Construction>();

            var buildinMode = new BuildingModeService();
            var commands = new CommandsMock();

            var scheme = new ConstructionScheme();

            var fieldService = new FieldService(10, new IntPoint(3, 3));
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, buildinMode, fieldService, constructionService, constructionsRepository, commands);

            var cells = view.CellsContainer.FindViews<CellView>();
            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            buildinMode.Show(new ConstructionCard(scheme));

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.IsReadyToPlace || x.State.Value == CellPlacementStatus.IsAvailableGhostPlace));

            buildinMode.Hide();

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            var constructionsRepository = new Repository<Construction>();

            var buildinMode = new BuildingModeService();
            var commands = new CommandsMock();

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
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, buildinMode, fieldService, constructionService, constructionsRepository, commands);

            buildinMode.Show(new ConstructionCard(scheme));

            var cells = view.CellsContainer.FindViews<CellView>();
            var highlighedCells = cells.Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace).OrderBy(x => x.LocalPosition.Value.X);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.AreEqual(new GameVector3(0, 0, 0), highlighedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlighedCells.Last().LocalPosition.Value);

            buildinMode.SetGhostPosition(new FieldPosition(1, 0), new FieldRotation());

            highlighedCells = cells.Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace).OrderBy(x => x.LocalPosition.Value.X);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlighedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(1f, 0, 0), highlighedCells.Last().LocalPosition.Value);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostChangesPositions()
        {
            var mockControls = new ControlsMock();
            var controls = new GameControls(mockControls);

            var buildinMode = new BuildingModeService();
            var commands = new CommandsMock();

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
            var viewCollection = new ViewsCollection();

            buildinMode.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, buildinMode, fieldService, controls, commands);

            Assert.AreEqual(new FieldPosition(0, 0), buildinMode.GetPosition());

            mockControls.MovePointer(new GameVector3(0.75f, 0, 0)); // move left

            Assert.AreEqual(new FieldPosition(1, 0), buildinMode.GetPosition());

            //// -0.5 to 0.5
            //mockControls.MovePointer(new GameVector3(0, 0, 0f));
            ////Assert.AreEqual(new FieldPosition(1,  0), buildinMode.GetPosition());

            //// -0.5 to -1.5
            //mockControls.MovePointer(new GameVector3(-0.9f, 0, -1f));
            //Assert.AreEqual(new FieldPosition(-1,  -1), buildinMode.GetPosition());

            //// -1.5 to -2.5
            //mockControls.MovePointer(new GameVector3(-1.9f, 0, -2f));
            //Assert.AreEqual(new FieldPosition(-2, -2), buildinMode.GetPosition());

            viewCollection.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostReactsToChangingPosition()
        {
            var mockControls = new ControlsMock();
            var controls = new GameControls(mockControls);

            var buildinMode = new BuildingModeService();
            var commands = new CommandsMock();

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
            var viewCollection = new ViewsCollection();

            buildinMode.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, buildinMode, fieldService, controls, commands);

            Assert.AreEqual(new GameVector3(0.25f, 0f, 0), view.LocalPosition.Value);

            buildinMode.SetGhostPosition(new FieldPosition(1, 0), new FieldRotation());

            Assert.AreEqual(new GameVector3(0.75f, 0f, 0), view.LocalPosition.Value);

            viewCollection.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostViewChangeVisualIfItAvailable()
        {
            var constructionsRepository = new Repository<Construction>();

            var buildinMode = new BuildingModeService();

            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                ContructionPlacement.One,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "", new Requirements() { DownEdge = true });

            var card = new ConstructionCard(scheme);

            var fieldService = new FieldService(1, new IntPoint(5, 5));
            var viewCollection = new ViewsCollection();

            buildinMode.Show(card);

            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);

            var view = new ConstructionModelView(viewCollection);
            new GhostModelPresenter(view, buildinMode, constructionsService);

            // disallowed because DownEdge enabled.
            Assert.IsFalse(constructionsService.CanPlace(card, new FieldPosition(0, 0), new FieldRotation()));

            Assert.AreEqual("Dragging", ((AnimatorMock)view.Animator).Animation);
            Assert.AreEqual("Disallowed", ((AnimatorMock)view.BorderAnimator).Animation);

            buildinMode.SetGhostPosition(new FieldPosition(0, -2), new FieldRotation());
            Assert.IsTrue(constructionsService.CanPlace(card, new FieldPosition(0, -2), new FieldRotation()));

            Assert.AreEqual("Dragging", ((AnimatorMock)view.Animator).Animation);
            Assert.AreEqual("Idle", ((AnimatorMock)view.BorderAnimator).Animation);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsCellsIsColoredRightWithGhost()
        {
            var constructionsRepository = new Repository<Construction>();

            var buildinMode = new BuildingModeService();
            var commands = new CommandsMock();

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
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            var viewCollection = new ViewsCollection();

            buildinMode.Show(new ConstructionCard(scheme));

            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, buildinMode, fieldService, constructionService, constructionsRepository, commands);

            buildinMode.SetGhostPosition(new FieldPosition(0, -1), new FieldRotation());

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

        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostSpawnsRightModel()
        {
            var mockControls = new ControlsMock();
            var controls = new GameControls(mockControls);

            var fieldService = new FieldService(1, new IntPoint(3, 3));
            var buildinMode = new BuildingModeService();
            var commands = new CommandsMock();

            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                ContructionPlacement.One,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "view", new Requirements());

            var viewCollection = new ViewsCollection();
            buildinMode.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, buildinMode, fieldService, controls, commands);

            Assert.IsTrue(commands.Last<AddGhostModelCommand>());
            Assert.AreEqual(scheme.Id, commands.Commands.OfType<AddGhostModelCommand>().Last().Scheme.Id);

            viewCollection.Dispose();
            controls.Dispose();
        }

        [Test]
        public void IsRotationWorking()
        {
            var constructionsRepository = new Repository<Construction>();

            var buildinMode = new BuildingModeService();
            var commands = new CommandsMock();

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
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            var viewCollection = new ViewsCollection();

            buildinMode.Show(new ConstructionCard(scheme));

            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, buildinMode, fieldService, constructionService, constructionsRepository, commands);

            buildinMode.SetGhostPosition(new FieldPosition(0, 0), new FieldRotation());

            var cells = view.CellsContainer.FindViews<CellView>();
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,2,1,1 },
                { 1,1,1,1,1 }
            });

            buildinMode.SetGhostPosition(new FieldPosition(0, 0), new FieldRotation(FieldRotation.Rotation.Right));
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,2,1 },
                { 1,1,1,1,1 }
            });

            buildinMode.SetGhostPosition(new FieldPosition(0, 0), new FieldRotation(FieldRotation.Rotation.Bottom));
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,1,2,1 },
                { 1,1,2,2,1 },
                { 1,1,1,1,1 }
            });

            buildinMode.SetGhostPosition(new FieldPosition(0, 0), new FieldRotation(FieldRotation.Rotation.Left));
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,1,1 }
            });

            viewCollection.Dispose();

            void CheckPosition(int[,] expected)
            {
                var actual = ToArray(5, cells);
                Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");
            }
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
