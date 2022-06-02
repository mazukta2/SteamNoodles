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
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
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
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
{
    public class GhostTests
    {
        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostCreatedInBuildingMode()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var buildingMode = new BuildingModeService();
            var commands = new PresenterCommandsMock();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);
            var link = cardsRepository.Add(new ConstructionCard(scheme));

            var viewCollection = new ViewsCollection();
            var view = new GhostManagerView(viewCollection);
            new GhostManagerPresenter(view, buildingMode, commands);

            Assert.IsTrue(commands.IsEmpty());

            buildingMode.Show(link);

            Assert.IsTrue(commands.Last<AddGhostCommand>());

            buildingMode.Hide();

            Assert.IsTrue(commands.Last<RemoveGhostCommand>());

            viewCollection.Dispose();
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void IsAvailableCellsIsHighlightedInGhostMode()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();
            var constructionsRepository = new Repository<Construction>();

            var buildinMode = new BuildingModeService();
            var commands = new PresenterCommandsMock();

            var scheme = new ConstructionScheme();
            schemesRepository.Add(scheme);
            var link = cardsRepository.Add(new ConstructionCard(scheme));

            var fieldService = new FieldService(10, new IntPoint(3, 3));
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, buildinMode, fieldService, constructionService);

            var cells = view.CellsContainer.FindViews<CellView>();
            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            buildinMode.Show(link);

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.IsReadyToPlace || x.State.Value == CellPlacementStatus.IsAvailableGhostPlace));

            buildinMode.Hide();

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();
            var constructionsRepository = new Repository<Construction>();

            var buildinMode = new BuildingModeService();
            var commands = new PresenterCommandsMock();

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

            schemesRepository.Add(scheme);
            var link = cardsRepository.Add(new ConstructionCard(scheme));

            var fieldService = new FieldService(0.5f, new IntPoint(7, 7));
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, buildinMode, fieldService, constructionService);

            buildinMode.Show(link);

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
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var mockControls = new ControlsMock();
            var controls = new GameControls(mockControls);

            var buildinMode = new BuildingModeService();
            var commands = new PresenterCommandsMock();

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

            schemesRepository.Add(scheme);
            var link = cardsRepository.Add(new ConstructionCard(scheme));

            var fieldService = new FieldService(0.5f, new IntPoint(7, 7));
            var viewCollection = new ViewsCollection();

            buildinMode.Show(link);

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, link, buildinMode, fieldService, controls, commands);

            Assert.AreEqual(new FieldPosition(0, 0), buildinMode.GetPosition());

            mockControls.MovePointer(new GameVector3(0.75f, 0, 0)); // move left

            Assert.AreEqual(new FieldPosition(1, 0), buildinMode.GetPosition());

            viewCollection.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostReactsToChangingPosition()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var mockControls = new ControlsMock();
            var controls = new GameControls(mockControls);

            var buildinMode = new BuildingModeService();
            var commands = new PresenterCommandsMock();

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

            schemesRepository.Add(scheme);
            var link = cardsRepository.Add(new ConstructionCard(scheme));

            var fieldService = new FieldService(0.5f, new IntPoint(7, 7));
            var viewCollection = new ViewsCollection();

            buildinMode.Show(link);

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, link, buildinMode, fieldService, controls, commands);

            Assert.AreEqual(new GameVector3(0.25f, 0f, 0), view.LocalPosition.Value);

            buildinMode.SetGhostPosition(new FieldPosition(1, 0), new FieldRotation());

            Assert.AreEqual(new GameVector3(0.75f, 0f, 0), view.LocalPosition.Value);

            viewCollection.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostViewChangeVisualIfItAvailable()
        {
            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var mockControls = new ControlsMock();
            var controls = new GameControls(mockControls);

            var buildinMode = new BuildingModeService();
            var commands = new PresenterCommandsMock();

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
                "", "", new Requirements() { DownEdge = true });

            schemesRepository.Add(scheme);
            var link = cardsRepository.Add(new ConstructionCard(scheme));

            var fieldService = new FieldService(0.5f, new IntPoint(7, 7));
            var viewCollection = new ViewsCollection();

            buildinMode.Show(link);

            var view = new ConstructionModelView(viewCollection);
            var presenter = new GhostModelPresenter(view);

            Assert.AreEqual("Dragging", ((AnimatorMock)view.Animator).Animation);
            Assert.AreEqual("Disallowed", ((AnimatorMock)view.BorderAnimator).Animation);

            var newPos = new GameVector3(0.25f, 0, -4f * cellSize);
            game.Controls.MovePointer(newPos); // move down

            Assert.AreEqual("Dragging", ((AnimatorMock)view.Animator).Animation);
            Assert.AreEqual("Idle", ((AnimatorMock)view.BorderAnimator).Animation);

            viewCollection.Dispose();
            controls.Dispose();
        }

        //[Test]
        //public void IsCellsBeneathGhostIsHighlightedRed()
        //{
        //    var size = 5;
        //    var game = new GameConstructor()
        //        .UpdateDefinition<ConstructionsSettingsDefinition>((d) => d.CellSize = 1)
        //        .UpdateDefinition<LevelDefinitionMock>((d) =>
        //        {
        //            d.PlacementField.Size = new IntPoint(size, size);
        //            d.StartingHand.First().Placement = new int[,]
        //            {
        //                { 1, 1 },
        //                { 1, 1 }
        //            };
        //            d.StartingHand.First().Requirements = new Requirements() { DownEdge = true };
        //        })
        //        .Build();

        //    game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();

        //    var worldPos = new GameVector3(0f, 0, -0.75f);
        //    game.Controls.MovePointer(worldPos);

        //    var cells = game.LevelCollection.FindViews<CellView>();
        //    var actual = ToArray(size, cells);
        //    var expected = new int[5, 5]
        //    {
        //        { 1,1,0,0,0 },
        //        { 1,1,0,0,0 },
        //        { 1,2,3,0,0 },
        //        { 1,2,3,0,0 },
        //        { 1,1,0,0,0 }
        //    };

        //    Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");
        //    game.Dispose();
        //}

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostSpawnsRightModel()
        {
            var mockControls = new ControlsMock();
            var controls = new GameControls(mockControls);

            var schemesRepository = new Repository<ConstructionScheme>();
            var cardsRepository = new Repository<ConstructionCard>();

            var fieldService = new FieldService(1, new IntPoint(3, 3));
            var buildinMode = new BuildingModeService();
            var commands = new PresenterCommandsMock();

            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                ContructionPlacement.One,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "view", new Requirements());

            schemesRepository.Add(scheme);
            var link = cardsRepository.Add(new ConstructionCard(scheme));

            var viewCollection = new ViewsCollection();

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, link, buildinMode, fieldService, controls, commands);

            Assert.IsTrue(commands.Last<AddGhostModelCommand>());
            Assert.AreEqual(scheme.Id, commands.Commands.OfType<AddGhostModelCommand>().Last().Scheme.Id);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsBuildingClickWorking()
        {

            //game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();

            //Assert.IsNotNull(game.LevelCollection.FindView<GhostView>());

            //game.Controls.Click();

            //Assert.IsNull(game.LevelCollection.FindView<GhostView>());

        }

        [Test]
        public void IsBuildingPlacingIsExitGhostMode()
        {
            //var game = new GameConstructor().Build();

            //game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();

            //Assert.IsNotNull(game.LevelCollection.FindView<GhostView>());

            //game.Controls.Click();

            //Assert.IsNull(game.LevelCollection.FindView<GhostView>());

            //game.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
