using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Models.Buildings;
using NUnit.Framework;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;
using Tests.Tests.Shortcuts;

namespace Tests.Buildings
{
    public class OrderTests
    {
        [Test]
        public void IsCurrentOrderSetted()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();

            Assert.IsNull(level.Placement.Orders.Order);

            var ingridient = new BasicIngredientPrototype();

            var proto = new BasicBuildingPrototype();
            proto.ProvideIngredient = ingridient;
            var scheme = new ConstructionScheme(proto);

            var construction = level.Placement.Place(scheme, new Point(0, 0));
            Assert.IsTrue(construction.IsProvide(ingridient));

            Assert.IsNotNull(level.Placement.Orders.Order);
        }

    }
}
