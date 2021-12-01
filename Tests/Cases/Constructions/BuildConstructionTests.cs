using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Prototypes.Levels;
using NUnit.Framework;
using System.Linq;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;

namespace Game.Tests.Cases.Constructions
{
    public class BuildConstructionTests
    {
        #region Hand
        [Test]
        public void IsHandExistingOnLevelLoaded()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            Assert.IsNotNull(level.Hand);
            Assert.IsNotNull(levelViewModel.Screen.Hand);
            Assert.IsNotNull(levelViewModel.Screen.Hand.View);
            game.Exit();
        }

        [Test]
        public void IsHandHaveItems()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            var items = levelViewModel.Screen.Hand.GetSchemes();
            Assert.IsTrue(items.Length > 0);
            Assert.IsTrue(items.All(x => x.View != null));
            game.Exit();
        }

        [Test]
        public void IsIconSettedInHand()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            var item = levelViewModel.Screen.Hand.GetSchemes().First();
            var icon = item.View.GetIcon();
            Assert.IsTrue(level.Hand.Cards.First().HandIcon.Equals(icon));
            game.Exit();
        }
        #endregion

        #region Ghost
        [Test]
        public void IsClickToSchemeEnterAndExitGhostMode()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();

            Assert.IsNull(levelViewModel.Placement.Ghost);

            var construction = levelViewModel.Screen.Hand.GetSchemes().First();
            construction.View.Click();

            var view = levelViewModel.Placement.Ghost.View;
            Assert.IsNotNull(view);

            construction.View.Click();

            Assert.IsNull(levelViewModel.Placement.Ghost);
            Assert.IsTrue(view.IsDisposed);
            game.Exit();
        }

        [Test]
        public void IsBuildingPlacingIsExitGhostMode()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetSchemes().First();

            construction.View.Click();

            var view = levelViewModel.Placement.Ghost.View;
            Assert.IsNotNull(view);
            levelViewModel.Placement.View.Click(new Vector2(0f, 0f));
            Assert.IsNull(levelViewModel.Placement.Ghost);
            Assert.IsTrue(view.IsDisposed);
            game.Exit();
        }

        [Test]
        public void IsAvailableCellsIsHighlightedInGhostMode()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetSchemes().First();

            var cells = levelViewModel.Placement.GetCells();
            Assert.IsTrue(cells.All(x => x.State == CellPresenter.CellState.Normal
                && x.View.GetState() == CellPresenter.CellState.Normal));

            construction.View.Click();

            Assert.IsTrue(cells.All(x => (x.State == CellPresenter.CellState.IsReadyToPlace || x.State == CellPresenter.CellState.IsAvailableGhostPlace)
                && (x.View.GetState() == CellPresenter.CellState.IsReadyToPlace || x.View.GetState() == CellPresenter.CellState.IsAvailableGhostPlace)));
            game.Exit();
        }

        [Test]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetSchemes().First();
            construction.View.Click();

            var ghost = levelViewModel.Placement.Ghost;
            ghost.View.GetMoveAction()(new Vector2(0, 0));

            var cells = levelViewModel.Placement.GetCells();
            var highlighedCells = cells.Where(x => x.State == CellPresenter.CellState.IsAvailableGhostPlace && x.View.GetState() == CellPresenter.CellState.IsAvailableGhostPlace);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(0, 0)));
            Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(1, 0)));

            ghost.View.GetMoveAction()(new Vector2(levelViewModel.Placement.CellSize + levelViewModel.Placement.CellSize / 4, 0));

            highlighedCells = cells.Where(x => x.State == CellPresenter.CellState.IsAvailableGhostPlace && x.View.GetState() == CellPresenter.CellState.IsAvailableGhostPlace);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(1, 0)));
            Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(2, 0)));
            game.Exit();
        }

        [Test]
        public void IsGhostViewChangeVisualIfItAvailable()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            Assert.AreEqual(1, levelViewModel.Screen.Hand.GetSchemes().Length);
            var proto = new TestBuildingPrototype();
            proto.Requirements = new Requirements() { DownEdge = true };
            level.Hand.Add(proto);

            Assert.AreEqual(2, levelViewModel.Screen.Hand.GetSchemes().Length);
            var construction = levelViewModel.Screen.Hand.GetSchemes().Last();
            construction.View.Click();

            var ghost = levelViewModel.Placement.Ghost.View;

            Assert.IsFalse(ghost.GetCanBePlacedState());
            ghost.GetMoveAction()(new Vector2(0, -levelViewModel.Placement.CellSize * 2 - levelViewModel.Placement.CellSize / 4));

            Assert.IsFalse(levelViewModel.Placement.Ghost.Position == new Point(0, 2));

            Assert.IsTrue(ghost.GetCanBePlacedState());
            game.Exit();
        }

        [Test]
        public void IsCellsBeneathGhostIsHighlightedRed()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            var building = new TestBuildingPrototype();
            building.Size = new Point(2, 2);
            building.Requirements = new Requirements()
            {
                DownEdge = true,
            };
            levelViewModel.Screen.Hand.Add(building);

            var construction = levelViewModel.Screen.Hand.GetSchemes().Last();
            construction.View.Click();
            var worldPos = new Vector2(0f, -levelViewModel.Placement.CellSize / 2);
            levelViewModel.Placement.Ghost.View.GetMoveAction()(worldPos);

            var cells = levelViewModel.Placement.GetCells();

            Assert.AreEqual(CellPresenter.CellState.Normal,
                cells.First(x => x.Position == new Point(-1, 0)).View.GetState());
            Assert.AreEqual(CellPresenter.CellState.IsNotAvailableGhostPlace,
                cells.First(x => x.Position == new Point(0, 0)).View.GetState());
            Assert.AreEqual(CellPresenter.CellState.IsNotAvailableGhostPlace,
                cells.First(x => x.Position == new Point(1, 0)).View.GetState());
            Assert.AreEqual(CellPresenter.CellState.Normal,
                cells.First(x => x.Position == new Point(-1, 0)).View.GetState());

            Assert.AreEqual(CellPresenter.CellState.IsReadyToPlace,
                cells.First(x => x.Position == new Point(-1, -1)).View.GetState());
            Assert.AreEqual(CellPresenter.CellState.IsAvailableGhostPlace,
                cells.First(x => x.Position == new Point(0, -1)).View.GetState());
            Assert.AreEqual(CellPresenter.CellState.IsAvailableGhostPlace,
                cells.First(x => x.Position == new Point(1, -1)).View.GetState());
            Assert.AreEqual(CellPresenter.CellState.IsReadyToPlace,
                cells.First(x => x.Position == new Point(-1, -1)).View.GetState());
            game.Exit();
        }
        #endregion

        #region Build
        [Test]
        public void IsConstructionPlaced()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetSchemes().First();

            construction.View.Click();

            Assert.IsTrue(levelViewModel.Placement.Ghost.CanPlaceGhost());

            Assert.AreEqual(0, levelViewModel.Placement.GetConstructions().Length);

            levelViewModel.Placement.View.Click(new Vector2(0f, 0f));

            Assert.AreEqual(1, levelViewModel.Placement.GetConstructions().Length);
            Assert.IsNotNull(levelViewModel.Placement.GetConstructions().First().View);
            game.Exit();
        }

        [Test]
        public void IsConstructionPlacedInRightPosition()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetSchemes().First();
            construction.View.Click();

            Assert.IsTrue(levelViewModel.Placement.GetConstructions().Length == 0);

            var ghost = levelViewModel.Placement.Ghost.View;
            var cellPos = new Point(0, -2);
            var worldPos = new Vector2(0, -levelViewModel.Placement.CellSize * 2 - levelViewModel.Placement.CellSize / 4);
            ghost.GetMoveAction()(worldPos);

            Assert.AreEqual(cellPos, levelViewModel.Placement.Ghost.Position);
            levelViewModel.Placement.View.Click(worldPos);

            Assert.AreEqual(levelViewModel.Placement.GetWorldPosition(cellPos),
                levelViewModel.Placement.GetConstructions().First().View.GetPosition());
            game.Exit();
        }

        [Test]
        public void IsConstructionPlacedHaveRightImage()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetSchemes().First();
            var model = level.Hand.Cards.First();
            construction.View.Click();
            levelViewModel.Placement.View.Click(new Vector2(0, 0));

            Assert.IsTrue(model.BuildingView != null);
            Assert.AreEqual(model.BuildingView,
                levelViewModel.Placement.GetConstructions().First().View.GetImage());
            game.Exit();
        }

        [Test]
        public void IsConstructionButtomRestrictionIsWorking()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();

            var building = new TestBuildingPrototype();
            building.Requirements = new Requirements()
            {
                DownEdge = true,
            };
            levelViewModel.Screen.Hand.Add(building);

            var construction = levelViewModel.Screen.Hand.GetSchemes().Last();
            construction.View.Click();

            Assert.AreEqual(0, levelViewModel.Placement.GetConstructions().Length);

            var worldPos = new Vector2(0f, levelViewModel.Placement.CellSize * 1 + levelViewModel.Placement.CellSize / 4);
            levelViewModel.Placement.Ghost.View.GetMoveAction()(worldPos);
            var position = levelViewModel.Placement.Ghost.Position;

            Assert.AreEqual(new Point(0, 1), levelViewModel.Placement.Ghost.Position);
            var space = levelViewModel.Placement.Ghost.Scheme.GetOccupiedSpace(new Point(0, 1));
            Assert.AreEqual(2, space.Length);
            Assert.IsTrue(space.Any(x => x == new Point(0, 1)));
            Assert.IsTrue(space.Any(x => x == new Point(1, 1)));

            Assert.IsFalse(level.Placement.IsFreeCell(levelViewModel.Placement.Ghost.Scheme, new Point(0, 1)));
            Assert.IsFalse(level.Placement.IsFreeCell(levelViewModel.Placement.Ghost.Scheme, new Point(1, 1)));

            Assert.IsFalse(level.Placement.IsFreeCell(levelViewModel.Placement.Ghost.Scheme, new Point(0, -1)));
            Assert.IsFalse(level.Placement.IsFreeCell(levelViewModel.Placement.Ghost.Scheme, new Point(1, -1)));

            Assert.IsTrue(level.Placement.IsFreeCell(levelViewModel.Placement.Ghost.Scheme, new Point(0, -2)));
            Assert.IsTrue(level.Placement.IsFreeCell(levelViewModel.Placement.Ghost.Scheme, new Point(1, -2)));

            worldPos = new Vector2(0f, -levelViewModel.Placement.CellSize * 2 - levelViewModel.Placement.CellSize / 4);
            levelViewModel.Placement.View.Click(worldPos);
            Assert.AreEqual(1, levelViewModel.Placement.GetConstructions().Length);

            game.Exit();
        }



        [Test]
        public void IsRemovedFromHand()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetSchemes().First();

            Assert.AreEqual(1, levelViewModel.Screen.Hand.GetSchemes().Length);

            construction.View.Click();
            levelViewModel.Placement.View.Click(new Vector2(0f, 0f));

            Assert.AreEqual(0, levelViewModel.Screen.Hand.GetSchemes().Length);
            game.Exit();
        }
        #endregion


    }
}
