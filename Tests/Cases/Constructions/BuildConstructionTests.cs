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
        #endregion

        #region Build
        [Test]
        public void IsConstructionPlaced()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();
            var construction = levelViewModel.Screen.Hand.GetConstructions().First();

            construction.View.Click();

            Assert.IsTrue(levelViewModel.Placement.CanPlaceGhost(new Vector2(0f, 0f)));

            levelViewModel.Placement.View.Click(new Vector2(0f, 0f));

            Assert.IsTrue(levelViewModel.Placement.GetConstructions().Length > 0);
            Assert.IsNotNull(levelViewModel.Placement.GetConstructions().First().View);
        }

        [Test]
        public void IsConstructionPlacedInRightPosition()
        {
            throw new System.Exception();
        }

        [Test]
        public void IsConstructionPlacedHaveRightImage()
        {
            throw new System.Exception();
        }

        [Test]
        public void IsConstructionButtomRestrictionIsWorking()
        {
            throw new System.Exception();
        }
        #endregion


    }
}
