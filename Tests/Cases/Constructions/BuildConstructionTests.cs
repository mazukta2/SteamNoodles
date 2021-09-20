using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using NUnit.Framework;
using System.Linq;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Tests.Mocks.Prototypes.Levels;
using Tests.Tests.Shortcuts;

namespace Tests.Tests.Cases.Constructions
{
    public class BuildConstructionTests
    {
        #region Hand
        [Test]
        public void IsHandExistingOnLevelLoaded()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            Assert.IsNotNull(level.Hand);
            Assert.IsNotNull(levelViewModel.Screen.Hand);
            Assert.IsNotNull(levelViewModel.Screen.Hand.View);
        }

        [Test]
        public void IsHandHaveItems()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            var items = levelViewModel.Screen.Hand.GetConstructions();
            Assert.IsTrue(items.Length > 0);
            Assert.IsTrue(items.All(x => x.View != null));
        }

        [Test]
        public void IsIconSettedInHand()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            var item = levelViewModel.Screen.Hand.GetConstructions().First();
            var icon = item.View.GetIcon();
            Assert.IsTrue(level.Hand.CurrentSchemes.First().HandIcon.Equals(icon));
        }
        #endregion

        #region Ghost
        [Test]
        public void IsClickToSchemeEnterAndExitGhostMode()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();

            Assert.IsNull(levelViewModel.Placement.Ghost);

            var construction = levelViewModel.Screen.Hand.GetConstructions().First();
            construction.View.Click();

            var view = levelViewModel.Placement.Ghost.View;
            Assert.IsNotNull(view);

            construction.View.Click();

            Assert.IsNull(levelViewModel.Placement.Ghost);
            Assert.IsTrue(view.IsDestoyed);
        }

        [Test]
        public void IsBuildingPlacingIsExitGhostMode()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetConstructions().First();

            construction.View.Click();

            var view = levelViewModel.Placement.Ghost.View;
            Assert.IsNotNull(view);
            levelViewModel.Placement.View.Click(new Vector2(0f, 0f));
            Assert.IsNull(levelViewModel.Placement.Ghost);
            Assert.IsTrue(view.IsDestoyed);
        }

        [Test]
        public void IsAvailableCellsIsHighlightedInGhostMode()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetConstructions().First();

            var cells = levelViewModel.Placement.GetCells();
            Assert.IsTrue(cells.All(x => x.State == CellViewModel.CellState.Normal
                && x.View.GetState() == CellViewModel.CellState.Normal));

            construction.View.Click();

            Assert.IsTrue(cells.All(x => (x.State == CellViewModel.CellState.IsReadyToPlace || x.State == CellViewModel.CellState.IsAvailableGhostPlace)
                && (x.View.GetState() == CellViewModel.CellState.IsReadyToPlace || x.View.GetState() == CellViewModel.CellState.IsAvailableGhostPlace)));
        }

        [Test]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetConstructions().First();
            construction.View.Click();

            var ghost = levelViewModel.Placement.Ghost;
            ghost.View.GetMoveAction()(new Vector2(0, 0));

            var cells = levelViewModel.Placement.GetCells();
            var highlighedCells = cells.Where(x => x.State == CellViewModel.CellState.IsAvailableGhostPlace && x.View.GetState() == CellViewModel.CellState.IsAvailableGhostPlace);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(0, 0)));
            Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(1, 0)));

            ghost.View.GetMoveAction()(new Vector2(levelViewModel.Placement.CellSize + levelViewModel.Placement.CellSize/4, 0));

            highlighedCells = cells.Where(x => x.State == CellViewModel.CellState.IsAvailableGhostPlace && x.View.GetState() == CellViewModel.CellState.IsAvailableGhostPlace);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(1, 0)));
            Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(2, 0)));
        }

        [Test]
        public void IsGhostViewChangeVisualIfItAvailable()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            var proto = new BasicBuildingPrototype();
            proto.Requirements = new Requirements() { DownEdge = true };
            level.Hand.Add(proto);

            Assert.AreEqual(2, levelViewModel.Screen.Hand.GetConstructions().Length);
            var construction = levelViewModel.Screen.Hand.GetConstructions().Last();
            construction.View.Click();

            var ghost = levelViewModel.Placement.Ghost.View;

            Assert.IsFalse(ghost.GetCanBePlacedState());
            ghost.GetMoveAction()(new Vector2(0, - levelViewModel.Placement.CellSize * 2 - levelViewModel.Placement.CellSize / 4));

            Assert.IsFalse(levelViewModel.Placement.Ghost.Position == new Point(0, 2));

            Assert.IsTrue(ghost.GetCanBePlacedState());
        }

        [Test]
        public void IsCellsBeneathGhostIsHighlightedRed()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            var building = new BasicBuildingPrototype();
            building.Size = new Point(2, 2);
            building.Requirements = new Requirements()
            {
                DownEdge = true,
            };
            levelViewModel.Screen.Hand.Add(building);

            var construction = levelViewModel.Screen.Hand.GetConstructions().Last();
            construction.View.Click();
            var worldPos = new Vector2(0f, -levelViewModel.Placement.CellSize /2 );
            levelViewModel.Placement.Ghost.View.GetMoveAction()(worldPos);

            var cells = levelViewModel.Placement.GetCells();

            Assert.AreEqual(CellViewModel.CellState.Normal,
                cells.First(x => x.Position == new Point(-1, 0)).View.GetState());
            Assert.AreEqual(CellViewModel.CellState.IsNotAvailableGhostPlace, 
                cells.First(x => x.Position == new Point(0, 0)).View.GetState());
            Assert.AreEqual(CellViewModel.CellState.IsNotAvailableGhostPlace,
                cells.First(x => x.Position == new Point(1, 0)).View.GetState());
            Assert.AreEqual(CellViewModel.CellState.Normal,
                cells.First(x => x.Position == new Point(-1, 0)).View.GetState());

            Assert.AreEqual(CellViewModel.CellState.IsReadyToPlace,
                cells.First(x => x.Position == new Point(-1, -1)).View.GetState());
            Assert.AreEqual(CellViewModel.CellState.IsAvailableGhostPlace,
                cells.First(x => x.Position == new Point(0, -1)).View.GetState());
            Assert.AreEqual(CellViewModel.CellState.IsAvailableGhostPlace,
                cells.First(x => x.Position == new Point(1, -1)).View.GetState());
            Assert.AreEqual(CellViewModel.CellState.IsReadyToPlace,
                cells.First(x => x.Position == new Point(-1, -1)).View.GetState());
        }
        #endregion

        #region Build
        [Test]
        public void IsConstructionPlaced()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetConstructions().First();

            construction.View.Click();

            Assert.IsTrue(levelViewModel.Placement.Ghost.CanPlaceGhost());

            Assert.AreEqual(0, levelViewModel.Placement.GetConstructions().Length);

            levelViewModel.Placement.View.Click(new Vector2(0f, 0f));

            Assert.AreEqual(1, levelViewModel.Placement.GetConstructions().Length);
            Assert.IsNotNull(levelViewModel.Placement.GetConstructions().First().View);
        }

        [Test]
        public void IsConstructionPlacedInRightPosition()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetConstructions().First();
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
        }

        [Test]
        public void IsConstructionPlacedHaveRightImage()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetConstructions().First();
            var model = level.Hand.CurrentSchemes.First();
            construction.View.Click();
            levelViewModel.Placement.View.Click(new Vector2(0, 0));

            Assert.IsTrue(model.BuildingView != null);
            Assert.AreEqual(model.BuildingView, 
                levelViewModel.Placement.GetConstructions().First().View.GetImage());
        }

        [Test]
        public void IsConstructionButtomRestrictionIsWorking()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();

            var building = new BasicBuildingPrototype();
            building.Requirements = new Requirements()
            {
                DownEdge = true,
            };
            levelViewModel.Screen.Hand.Add(building);

            var construction = levelViewModel.Screen.Hand.GetConstructions().Last();
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
            
            worldPos = new Vector2(0f, - levelViewModel.Placement.CellSize * 2 - levelViewModel.Placement.CellSize / 4);
            levelViewModel.Placement.View.Click(worldPos);
            Assert.AreEqual(1, levelViewModel.Placement.GetConstructions().Length);

        }
        #endregion


    }
}
