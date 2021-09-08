using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.Logic;
using Tests.Mocks.Prototypes.Levels;
using Tests.Mocks.Common.Math;
using Assets.Scripts.Models.Buildings;

namespace Tests.Buildings
{
    public class BuildingsTests
    {
        [Test]
        public void Test()
        {
            var game = new GameLogic();
            var session = game.CreateSession();
            session.LoadLevel(new BasicLevelPrototype());
        }

        [Test]
        public void Rect()
        {
            var size = new Point(4, 4);
            var buildinGrid = new Placement(size);
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
        }

        //[Test]
        //public void Placing()
        //{
        //    var size = new Vector2Int(4, 4);
        //    var buildinGrid = new Placement(size);

        //    var buildingScheme = new BuildingScheme(new SchemeDataMock(new Vector2Int(3, 3)));

        //    var space = buildingScheme.GetOccupiedSpace(Vector2Int.zero);
        //    Assert.IsTrue(space.Contains(new Vector2Int(0, 0)));
        //    Assert.IsTrue(space.Contains(new Vector2Int(1, 1)));
        //    Assert.IsTrue(space.Contains(new Vector2Int(2, 2)));
        //    Assert.IsFalse(space.Contains(new Vector2Int(3, 3)));
        //    Assert.IsTrue(buildinGrid.CanPlace(buildingScheme, new Vector2Int(0, 0)));

        //    space = buildingScheme.GetOccupiedSpace(new Vector2Int(1, 0));
        //    Assert.IsTrue(space.Contains(new Vector2Int(1, 0)));
        //    Assert.IsTrue(space.Contains(new Vector2Int(2, 1)));
        //    Assert.IsTrue(space.Contains(new Vector2Int(3, 2)));
        //    Assert.IsFalse(space.Contains(new Vector2Int(4, 3)));

        //    Assert.IsFalse(buildinGrid.IsFreeCell(buildingScheme, new Vector2Int(3, 2)));

        //    Assert.IsFalse(buildinGrid.CanPlace(buildingScheme, new Vector2Int(1, 0)));
        //}

        //public class SchemeDataMock : IBuildingSchemeModelData
        //{
        //    public SchemeDataMock(Vector2Int size)
        //    {
        //        Size = size;
        //    }

        //    public Vector2Int Size { get; }
        //    public Requirements Requirements => new Requirements();
        //}
    }
}
