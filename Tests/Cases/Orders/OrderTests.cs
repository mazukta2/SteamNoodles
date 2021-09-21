using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using NUnit.Framework;
using Tests.Mocks.Prototypes.Levels;
using Tests.Tests.Shortcuts;

namespace Tests.Buildings
{
    public class OrderTests
    {
        #region Existance
        [Test]
        public void IsCurrentOrderSetted()
        {
            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel();

            Assert.IsNull(levelViewModel.Screen.Order?.View);
            Assert.IsNull(level.Placement.Orders.Order);

            var ingridient = new BasicIngredientPrototype();
            var proto = new BasicBuildingPrototype();
            proto.ProvideIngredient = ingridient;
            var scheme = new ConstructionScheme(proto);
            var construction = level.Placement.Place(scheme, new Point(0, 0));
            Assert.IsTrue(construction.IsProvide(ingridient));

            Assert.IsNotNull(level.Placement.Orders.Order);
            Assert.IsNotNull(levelViewModel.Screen.Order?.View);
        }
        #endregion
    }
}
