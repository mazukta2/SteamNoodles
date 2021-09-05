using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Core.Helpers;
using Assets.Scripts.Data.Buildings;
using Assets.Scripts.Models.Buildings;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Assets.Scripts.Tests
{
    public class BuildingsTests
    {
        [Test]
        public void Rect()
        {
            var size = new Vector2Int(4, 4);
            var buildinGrid = new Placement(size);
            var rect = buildinGrid.Rect;
            Assert.AreEqual(-2, rect.xMin);
            Assert.AreEqual(2, rect.xMax);
            Assert.AreEqual(-2, rect.yMin);
            Assert.AreEqual(2, rect.yMax);

            Assert.IsTrue(rect.IsInside(new Vector2Int(0, 0)));
            Assert.IsTrue(rect.IsInside(new Vector2Int(-2, -2)));
            Assert.IsTrue(rect.IsInside(new Vector2Int(2, 2)));
            Assert.IsFalse(rect.IsInside(new Vector2Int(-3, -3)));
            Assert.IsFalse(rect.IsInside(new Vector2Int(3, 3)));
        }

        [Test]
        public void Placing()
        {
            var size = new Vector2Int(4, 4);
            var buildinGrid = new Placement(size);

            var buildingScheme = new BuildingScheme(new SchemeDataMock(new Vector2Int(3, 3)));

            var space = buildingScheme.GetOccupiedSpace(Vector2Int.zero);
            Assert.IsTrue(space.Contains(new Vector2Int(0, 0)));
            Assert.IsTrue(space.Contains(new Vector2Int(1, 1)));
            Assert.IsTrue(space.Contains(new Vector2Int(2, 2)));
            Assert.IsFalse(space.Contains(new Vector2Int(3, 3)));
            Assert.IsTrue(buildinGrid.CanPlace(buildingScheme, new Vector2Int(0, 0)));

            space = buildingScheme.GetOccupiedSpace(new Vector2Int(1, 0));
            Assert.IsTrue(space.Contains(new Vector2Int(1, 0)));
            Assert.IsTrue(space.Contains(new Vector2Int(2, 1)));
            Assert.IsTrue(space.Contains(new Vector2Int(3, 2)));
            Assert.IsFalse(space.Contains(new Vector2Int(4, 3)));

            Assert.IsFalse(buildinGrid.IsFreeCell(buildingScheme, new Vector2Int(3, 2)));

            Assert.IsFalse(buildinGrid.CanPlace(buildingScheme, new Vector2Int(1, 0)));
        }

        public class SchemeDataMock : IBuildingSchemeModelData
        {
            public SchemeDataMock(Vector2Int size)
            {
                Size = size;
            }

            public Vector2Int Size { get; }
            public Requirements Requirements => new Requirements();
        }
    }
}