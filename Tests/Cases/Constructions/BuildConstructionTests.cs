using NUnit.Framework;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
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

            Assert.IsTrue(cells.All(x => x.State == CellViewModel.CellState.IsReadyToPlace
                && x.View.GetState() == CellViewModel.CellState.IsReadyToPlace));
        }

        [Test]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            throw new System.Exception();
        }

        [Test]
        public void IsGhostViewChangeVisualIfCanBePlaced()
        {
            throw new System.Exception();
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
