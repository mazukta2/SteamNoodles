﻿using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Linq;

namespace Game.Tests.Cases.Constructions
{
    public class PlacementGridTests
    {
        [Test]
        public void PlacementGridRectHasRightSize()
        {
            var game = new GameController();
            var (level, _, _) = game.LoadLevel();
            var buildinGrid = level.Placement;
            var rect = buildinGrid.Rect;
            Assert.AreEqual(-2, rect.xMin);
            Assert.AreEqual(2, rect.xMax);
            Assert.AreEqual(-2, rect.yMin);
            Assert.AreEqual(2, rect.yMax);

            Assert.IsTrue(rect.IsInside(new Point(0, 0)));
            Assert.IsTrue(rect.IsInside(new Point(-2, -2)));
            Assert.IsTrue(rect.IsInside(new Point(2, 2)));
            Assert.IsFalse(rect.IsInside(new Point(-3, -3)));
            Assert.IsFalse(rect.IsInside(new Point(3, 3)));
            game.Exit();
        }

        [Test]
        public void IsCellsCreated()
        {
            var game = new GameController();
            var (level, levelViewModel, levelView) = game.LoadLevel();
            var cells = levelViewModel.Placement.GetCells();
            Assert.IsTrue(cells.Length > 0);
            Assert.AreEqual((levelViewModel.Placement.GetSize().X + 1) * (levelViewModel.Placement.GetSize().Y + 1), cells.Length);

            Assert.IsTrue(cells.All(x => x.View != null));
            game.Exit();
        }

        [Test]
        public void IsOffsetWorking()
        {
            var game = new GameController();
            var levelSettings = new LevelSettings();
            levelSettings.Offset = new FloatPoint(0, 0.5f);

            var (level, levelViewModel, levelView) = game.LoadLevel(levelSettings);
            var cells = levelView.Placement.Cells;
            ;
            //Assert.IsTrue(4.5*levelViewModel., cells.List.Max(x => x.GetPosition().Y));
            //Assert.IsTrue(cells.Length > 0);
            //Assert.AreEqual((levelViewModel.Placement.GetSize().X + 1) * (levelViewModel.Placement.GetSize().Y + 1), cells.Length);

            //Assert.IsTrue(cells.All(x => x.View != null));
            //game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
