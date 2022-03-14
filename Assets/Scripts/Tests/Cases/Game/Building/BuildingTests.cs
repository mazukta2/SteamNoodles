using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment.Definitions.List;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game.Tests.Cases.Constructions
{
    public class BuildingTests
    {
        [Test]
        public void IsClickingActivatingBuildingScreen()
        {
            var game = Construct().Build();

            Assert.IsNotNull(game.CurrentLevel.FindView<MainScreenView>());
            Assert.IsNull(game.CurrentLevel.FindView<BuildScreenView>());

            var hand = game.CurrentLevel.FindView<HandView>();
            var view = hand.Cards.Get<HandConstructionView>().First();
            view.Button.Click();

            Assert.IsNull(game.CurrentLevel.FindView<MainScreenView>());
            Assert.IsNotNull(game.CurrentLevel.FindView<BuildScreenView>());

            var buildScreen = game.CurrentLevel.FindView<BuildScreenView>();
            buildScreen.CancelButton.Click();

            Assert.IsNotNull(game.CurrentLevel.FindView<MainScreenView>());
            Assert.IsNull(game.CurrentLevel.FindView<BuildScreenView>());

            game.Dispose();
        }

        [Test]
        public void IsCreatingAGhost()
        {
            var game = Construct().Build();

            var manager = game.CurrentLevel.FindView<GhostManagerView>();
            Assert.IsNotNull(manager);
            Assert.AreEqual(0, manager.Container.Get<GhostView>().Count);

            var hand = game.CurrentLevel.FindView<HandView>();
            var view = hand.Cards.Get<HandConstructionView>().First();
            view.Button.Click();

            Assert.AreEqual(1, manager.Container.Get<GhostView>().Count);

            var buildScreen = game.CurrentLevel.FindView<BuildScreenView>();
            buildScreen.CancelButton.Click();

            Assert.AreEqual(0, manager.Container.Get<GhostView>().Count);

            game.Dispose();
        }

        [Test]
        public void IsPlacementExits()
        {
            var game = Construct().Build();

            var hand = game.CurrentLevel.FindView<HandView>();

            game.Dispose();
        }


        [Test]
        public void IsAvailableCellsIsHighlightedInGhostMode()
        {
            var game = Construct().Build();

            //var game = new GameController();
            //var (models, presenters, views) = game.LoadLevel();
            //var construction = views.Screen.Hand.Cards.List.First();

            //var cells = presenters.Placement.GetCells();
            //Assert.IsTrue(cells.All(x => x.State == CellPresenter.CellState.Normal
            //    && x.View.GetState() == CellPresenter.CellState.Normal));

            //construction.Button.Click();

            //Assert.IsTrue(cells.All(x => (x.State == CellPresenter.CellState.IsReadyToPlace || x.State == CellPresenter.CellState.IsAvailableGhostPlace)
            //    && (x.View.GetState() == CellPresenter.CellState.IsReadyToPlace || x.View.GetState() == CellPresenter.CellState.IsAvailableGhostPlace)));
            //game.Exit();

            game.Dispose();
        }


        //[Test]
        //public void IsBuildingPlacingIsExitGhostMode()
        //{
        //    var game = new GameController();
        //    var (models, presenters, views) = game.LoadLevel();
        //    var construction = views.Screen.Hand.Cards.List.First();

        //    construction.Button.Click();

        //    var view = views.Placement.Ghost.Value;
        //    Assert.IsNotNull(view);
        //    views.Placement.Click(new FloatPoint(0f, 0f));
        //    Assert.IsNull(views.Placement.Ghost.Value);
        //    Assert.IsTrue(view.IsDisposed);
        //    game.Exit();
        //}

        //[Test]
        //public void IsCellsBeneathGhostIsHighlighted()
        //{
        //    var game = new GameController();
        //    var (models, presenters, views) = game.LoadLevel();
        //    var construction = views.Screen.Hand.Cards.List.First();
        //    construction.Button.Click();

        //    var ghost = views.Placement.Ghost.Value;
        //    ghost.GetMoveAction()(new FloatPoint(0, 0));

        //    var cells = presenters.Placement.GetCells();
        //    var highlighedCells = cells.Where(x => x.State == CellPresenter.CellState.IsAvailableGhostPlace && x.View.GetState() == CellPresenter.CellState.IsAvailableGhostPlace);
        //    Assert.AreEqual(2, highlighedCells.Count());

        //    Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(0, 0)));
        //    Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(1, 0)));

        //    ghost.GetMoveAction()(new FloatPoint(presenters.Placement.CellSize + presenters.Placement.CellSize / 4, 0));

        //    highlighedCells = cells.Where(x => x.State == CellPresenter.CellState.IsAvailableGhostPlace && x.View.GetState() == CellPresenter.CellState.IsAvailableGhostPlace);
        //    Assert.AreEqual(2, highlighedCells.Count());

        //    Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(1, 0)));
        //    Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(2, 0)));
        //    game.Exit();
        //}

        //[Test]
        //public void IsGhostViewChangeVisualIfItAvailable()
        //{
        //    var game = new GameController();
        //    var (models, presenters, views) = game.LoadLevel();
        //    Assert.AreEqual(1, views.Screen.Hand.Cards.List.Length);
        //    var proto = new ConstructionSettings
        //    {
        //        Requirements = new Requirements() { DownEdge = true }
        //    };
        //    models.Hand.Add(proto);

        //    Assert.AreEqual(2, views.Screen.Hand.Cards.List.Length);
        //    var construction = views.Screen.Hand.Cards.List.Last();
        //    construction.Button.Click();

        //    var ghost = views.Placement.Ghost.Value;

        //    Assert.IsFalse(ghost.GetCanBePlacedState());
        //    ghost.GetMoveAction()(new FloatPoint(0, -presenters.Placement.CellSize * 2 - presenters.Placement.CellSize / 4));

        //    Assert.IsFalse(presenters.Placement.Ghost.Position == new Point(0, 2));

        //    Assert.IsTrue(ghost.GetCanBePlacedState());
        //    game.Exit();
        //}

        //[Test]
        //public void IsCellsBeneathGhostIsHighlightedRed()
        //{
        //    var game = new GameController();
        //    var (models, presenters, views) = game.LoadLevel();
        //    var building = new ConstructionSettings
        //    {
        //        Size = new Point(2, 2),
        //        Requirements = new Requirements()
        //        {
        //            DownEdge = true,
        //        }
        //    };
        //    models.Hand.Add(building);

        //    var construction = views.Screen.Hand.Cards.List.Last();
        //    construction.Button.Click();
        //    var worldPos = new FloatPoint(0f, -presenters.Placement.CellSize / 2);
        //    views.Placement.Ghost.Value.GetMoveAction()(worldPos);

        //    var cells = presenters.Placement.GetCells();

        //    Assert.AreEqual(CellPresenter.CellState.Normal,
        //        cells.First(x => x.Position == new Point(-1, 0)).View.GetState());
        //    Assert.AreEqual(CellPresenter.CellState.IsNotAvailableGhostPlace,
        //        cells.First(x => x.Position == new Point(0, 0)).View.GetState());
        //    Assert.AreEqual(CellPresenter.CellState.IsNotAvailableGhostPlace,
        //        cells.First(x => x.Position == new Point(1, 0)).View.GetState());
        //    Assert.AreEqual(CellPresenter.CellState.Normal,
        //        cells.First(x => x.Position == new Point(-1, 0)).View.GetState());

        //    Assert.AreEqual(CellPresenter.CellState.IsReadyToPlace,
        //        cells.First(x => x.Position == new Point(-1, -1)).View.GetState());
        //    Assert.AreEqual(CellPresenter.CellState.IsAvailableGhostPlace,
        //        cells.First(x => x.Position == new Point(0, -1)).View.GetState());
        //    Assert.AreEqual(CellPresenter.CellState.IsAvailableGhostPlace,
        //        cells.First(x => x.Position == new Point(1, -1)).View.GetState());
        //    Assert.AreEqual(CellPresenter.CellState.IsReadyToPlace,
        //        cells.First(x => x.Position == new Point(-1, -1)).View.GetState());
        //    game.Exit();
        //}
        //#endregion

        //#region Build
        //[Test]
        //public void IsConstructionPlaced()
        //{
        //    var game = new GameController();
        //    var (models, presenters, views) = game.LoadLevel();
        //    var construction = views.Screen.Hand.Cards.List.First();

        //    construction.Button.Click();

        //    Assert.IsTrue(presenters.Placement.Ghost.CanPlaceGhost());

        //    Assert.AreEqual(0, views.Placement.Constructions.List.Length);

        //    views.Placement.Click(new FloatPoint(0f, 0f));

        //    Assert.AreEqual(1, views.Placement.Constructions.List.Length);
        //    game.Exit();
        //}

        //[Test]
        //public void IsConstructionPlacedInRightPosition()
        //{
        //    var game = new GameController();
        //    var (models, presenters, views) = game.LoadLevel();
        //    var construction = views.Screen.Hand.Cards.List.First();
        //    construction.Button.Click();

        //    Assert.IsTrue(views.Placement.Constructions.List.Length == 0);

        //    var ghost = views.Placement.Ghost.Value;
        //    var cellPos = new Point(0, -2);
        //    var worldPos = new FloatPoint(0, -presenters.Placement.CellSize * 2 - presenters.Placement.CellSize / 4);
        //    ghost.GetMoveAction()(worldPos);

        //    Assert.AreEqual(cellPos, presenters.Placement.Ghost.Position);
        //    views.Placement.Click(worldPos);

        //    Assert.AreEqual(presenters.Placement.GetWorldPosition(cellPos),
        //        views.Placement.Constructions.List.First().GetPosition());
        //    game.Exit();
        //}

        //[Test]
        //public void IsConstructionPlacedHaveRightImage()
        //{
        //    var game = new GameController();
        //    var (models, presenters, views) = game.LoadLevel();
        //    var construction = views.Screen.Hand.Cards.List.First();
        //    var model = models.Hand.Cards.First();
        //    construction.Button.Click();
        //    views.Placement.Click(new FloatPoint(0, 0));

        //    Assert.IsTrue(model.BuildingView != null);
        //    Assert.AreEqual(model.BuildingView,
        //        views.Placement.Constructions.List.First().GetImage());
        //    game.Exit();
        //}

        //[Test]
        //public void IsConstructionButtomRestrictionIsWorking()
        //{
        //    var game = new GameController();
        //    var (models, presenters, views) = game.LoadLevel();

        //    var building = new ConstructionSettings
        //    {
        //        Requirements = new Requirements()
        //        {
        //            DownEdge = true,
        //        }
        //    };
        //    models.Hand.Add(building);

        //    var construction = views.Screen.Hand.Cards.List.Last();
        //    construction.Button.Click();

        //    Assert.AreEqual(0, views.Placement.Constructions.List.Length);

        //    var worldPos = new FloatPoint(0f, presenters.Placement.CellSize * 1 + presenters.Placement.CellSize / 4);
        //    views.Placement.Ghost.Value.GetMoveAction()(worldPos);
        //    var position = presenters.Placement.Ghost.Position;

        //    Assert.AreEqual(new Point(0, 1), presenters.Placement.Ghost.Position);
        //    var space = presenters.Placement.Ghost.Card.GetOccupiedSpace(new Point(0, 1));
        //    Assert.AreEqual(2, space.Length);
        //    Assert.IsTrue(space.Any(x => x == new Point(0, 1)));
        //    Assert.IsTrue(space.Any(x => x == new Point(1, 1)));

        //    Assert.IsFalse(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(0, 1)));
        //    Assert.IsFalse(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(1, 1)));

        //    Assert.IsFalse(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(0, -1)));
        //    Assert.IsFalse(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(1, -1)));

        //    Assert.IsTrue(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(0, -2)));
        //    Assert.IsTrue(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(1, -2)));

        //    worldPos = new FloatPoint(0f, -presenters.Placement.CellSize * 2 - presenters.Placement.CellSize / 4);
        //    views.Placement.Click(worldPos);
        //    Assert.AreEqual(1, presenters.Placement.GetConstructions().Length);

        //    game.Exit();
        //}



        //[Test]
        //public void IsRemovedFromHand()
        //{
        //    var game = new GameController();
        //    var (models, presenters, views) = game.LoadLevel();

        //    Assert.AreEqual(1, views.Screen.Hand.Cards.List.Length);
        //    var construction = views.Screen.Hand.Cards.List.First();

        //    construction.Button.Click();
        //    views.Placement.Click(new FloatPoint(0f, 0f));

        //    Assert.AreEqual(0, views.Screen.Hand.Cards.List.Length);
        //    game.Exit();
        //}
        //#endregion



        private GameTestConstructor Construct()
        {
            var construction = new ConstructionDefinition();
            return new GameTestConstructor()
                .LoadDefinitions(new DefaultDefinitions())
                .AddAndLoadLevel(new LevelDefinitionMock(new BasicSellingLevel())
                {
                    StartingHand = new List<ConstructionDefinition>() { construction }
                });
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
