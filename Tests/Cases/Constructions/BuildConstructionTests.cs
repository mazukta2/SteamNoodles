using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Buildings;
using NUnit.Framework;
using System.Linq;
using System.Numerics;

namespace Game.Tests.Cases.Constructions
{
    public class BuildConstructionTests
    {
        #region Hand
        [Test]
        public void IsHandExistingOnLevelLoaded()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();
            Assert.IsNotNull(models.Hand);
            Assert.IsNotNull(presenters.Screen.Hand);
            Assert.IsNotNull(views.Screen.Hand);
            game.Exit();
        }

        [Test]
        public void IsHandHaveItems()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();
            Assert.IsTrue(views.Screen.Hand.Cards.List.Length > 0);
            game.Exit();
        }

        [Test]
        public void IsIconSettedInHand()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();
            var item = views.Screen.Hand.Cards.List.First();
            var icon = item.GetIcon();
            Assert.IsTrue(models.Hand.Cards.First().HandIcon.Equals(icon));
            game.Exit();
        }
        #endregion

        #region Ghost
        [Test]
        public void IsClickToSchemeEnterAndExitGhostMode()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();

            Assert.IsNull(presenters.Placement.Ghost);

            var construction = views.Screen.Hand.Cards.List.First();
            construction.Button.Click();

            var view = views.Placement.Ghost.Value;
            Assert.IsNotNull(view);

            construction.Button.Click();

            Assert.IsNull(views.Placement.Ghost.Value);
            Assert.IsTrue(view.IsDisposed);
            game.Exit();
        }

        [Test]
        public void IsBuildingPlacingIsExitGhostMode()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();
            var construction = views.Screen.Hand.Cards.List.First();

            construction.Button.Click();

            var view = views.Placement.Ghost.Value;
            Assert.IsNotNull(view);
            views.Placement.Click(new Vector2(0f, 0f));
            Assert.IsNull(views.Placement.Ghost.Value);
            Assert.IsTrue(view.IsDisposed);
            game.Exit();
        }

        [Test]
        public void IsAvailableCellsIsHighlightedInGhostMode()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();
            var construction = views.Screen.Hand.Cards.List.First();

            var cells = presenters.Placement.GetCells();
            Assert.IsTrue(cells.All(x => x.State == CellPresenter.CellState.Normal
                && x.View.GetState() == CellPresenter.CellState.Normal));

            construction.Button.Click();

            Assert.IsTrue(cells.All(x => (x.State == CellPresenter.CellState.IsReadyToPlace || x.State == CellPresenter.CellState.IsAvailableGhostPlace)
                && (x.View.GetState() == CellPresenter.CellState.IsReadyToPlace || x.View.GetState() == CellPresenter.CellState.IsAvailableGhostPlace)));
            game.Exit();
        }

        [Test]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();
            var construction = views.Screen.Hand.Cards.List.First();
            construction.Button.Click();

            var ghost = views.Placement.Ghost.Value;
            ghost.GetMoveAction()(new Vector2(0, 0));

            var cells = presenters.Placement.GetCells();
            var highlighedCells = cells.Where(x => x.State == CellPresenter.CellState.IsAvailableGhostPlace && x.View.GetState() == CellPresenter.CellState.IsAvailableGhostPlace);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(0, 0)));
            Assert.IsTrue(highlighedCells.Any(x => x.Position == new Point(1, 0)));

            ghost.GetMoveAction()(new Vector2(presenters.Placement.CellSize + presenters.Placement.CellSize / 4, 0));

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
            var (models, presenters, views) = game.LoadLevel();
            Assert.AreEqual(1, views.Screen.Hand.Cards.List.Length);
            var proto = new ConstructionSettings
            {
                Requirements = new Requirements() { DownEdge = true }
            };
            models.Hand.Add(proto);

            Assert.AreEqual(2, views.Screen.Hand.Cards.List.Length);
            var construction = views.Screen.Hand.Cards.List.Last();
            construction.Button.Click();

            var ghost = views.Placement.Ghost.Value;

            Assert.IsFalse(ghost.GetCanBePlacedState());
            ghost.GetMoveAction()(new Vector2(0, -presenters.Placement.CellSize * 2 - presenters.Placement.CellSize / 4));

            Assert.IsFalse(presenters.Placement.Ghost.Position == new Point(0, 2));

            Assert.IsTrue(ghost.GetCanBePlacedState());
            game.Exit();
        }

        [Test]
        public void IsCellsBeneathGhostIsHighlightedRed()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();
            var building = new ConstructionSettings
            {
                Size = new Point(2, 2),
                Requirements = new Requirements()
                {
                    DownEdge = true,
                }
            };
            models.Hand.Add(building);

            var construction = views.Screen.Hand.Cards.List.Last();
            construction.Button.Click();
            var worldPos = new Vector2(0f, -presenters.Placement.CellSize / 2);
            views.Placement.Ghost.Value.GetMoveAction()(worldPos);

            var cells = presenters.Placement.GetCells();

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
            var (models, presenters, views) = game.LoadLevel();
            var construction = views.Screen.Hand.Cards.List.First();

            construction.Button.Click();

            Assert.IsTrue(presenters.Placement.Ghost.CanPlaceGhost());

            Assert.AreEqual(0, views.Placement.Constructions.List.Length);

            views.Placement.Click(new Vector2(0f, 0f));

            Assert.AreEqual(1, views.Placement.Constructions.List.Length);
            game.Exit();
        }

        [Test]
        public void IsConstructionPlacedInRightPosition()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();
            var construction = views.Screen.Hand.Cards.List.First();
            construction.Button.Click();

            Assert.IsTrue(views.Placement.Constructions.List.Length == 0);

            var ghost = views.Placement.Ghost.Value;
            var cellPos = new Point(0, -2);
            var worldPos = new Vector2(0, -presenters.Placement.CellSize * 2 - presenters.Placement.CellSize / 4);
            ghost.GetMoveAction()(worldPos);

            Assert.AreEqual(cellPos, presenters.Placement.Ghost.Position);
            views.Placement.Click(worldPos);

            Assert.AreEqual(presenters.Placement.GetWorldPosition(cellPos),
                views.Placement.Constructions.List.First().GetPosition());
            game.Exit();
        }

        [Test]
        public void IsConstructionPlacedHaveRightImage()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();
            var construction = views.Screen.Hand.Cards.List.First();
            var model = models.Hand.Cards.First();
            construction.Button.Click();
            views.Placement.Click(new Vector2(0, 0));

            Assert.IsTrue(model.BuildingView != null);
            Assert.AreEqual(model.BuildingView,
                views.Placement.Constructions.List.First().GetImage());
            game.Exit();
        }

        [Test]
        public void IsConstructionButtomRestrictionIsWorking()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();

            var building = new ConstructionSettings
            {
                Requirements = new Requirements()
                {
                    DownEdge = true,
                }
            };
            models.Hand.Add(building);

            var construction = views.Screen.Hand.Cards.List.Last();
            construction.Button.Click();

            Assert.AreEqual(0, views.Placement.Constructions.List.Length);

            var worldPos = new Vector2(0f, presenters.Placement.CellSize * 1 + presenters.Placement.CellSize / 4);
            views.Placement.Ghost.Value.GetMoveAction()(worldPos);
            var position = presenters.Placement.Ghost.Position;

            Assert.AreEqual(new Point(0, 1), presenters.Placement.Ghost.Position);
            var space = presenters.Placement.Ghost.Card.GetOccupiedSpace(new Point(0, 1));
            Assert.AreEqual(2, space.Length);
            Assert.IsTrue(space.Any(x => x == new Point(0, 1)));
            Assert.IsTrue(space.Any(x => x == new Point(1, 1)));

            Assert.IsFalse(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(0, 1)));
            Assert.IsFalse(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(1, 1)));

            Assert.IsFalse(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(0, -1)));
            Assert.IsFalse(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(1, -1)));

            Assert.IsTrue(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(0, -2)));
            Assert.IsTrue(models.Placement.IsFreeCell(presenters.Placement.Ghost.Card, new Point(1, -2)));

            worldPos = new Vector2(0f, -presenters.Placement.CellSize * 2 - presenters.Placement.CellSize / 4);
            views.Placement.Click(worldPos);
            Assert.AreEqual(1, presenters.Placement.GetConstructions().Length);

            game.Exit();
        }



        [Test]
        public void IsRemovedFromHand()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();

            Assert.AreEqual(1, views.Screen.Hand.Cards.List.Length);
            var construction = views.Screen.Hand.Cards.List.First();

            construction.Button.Click();
            views.Placement.Click(new Vector2(0f, 0f));

            Assert.AreEqual(0, views.Screen.Hand.Cards.List.Length);
            game.Exit();
        }
        #endregion


        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
