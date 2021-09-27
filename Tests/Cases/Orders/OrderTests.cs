using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Tests.Mocks.Prototypes.Levels;
using NUnit.Framework;
using System;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;
using Tests.Tests.Mocks.Views.Levels;
using Tests.Tests.Shortcuts;

namespace Tests.Buildings
{
    public class OrderTests
    {
        #region Existance
        [Test]
        public void IsCurrentOrderSetted()
        {
            var ingridient = new TestIngredientPrototype();
            var proto = new TestBuildingPrototype();
            proto.ProvideIngredient = ingridient;
            var levelProto = new TestLevelPrototype();
            var order = new TestOrderPrototype();
            order.Add(new TestRecipePrototype(ingridient));
            levelProto.Add(order);

            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel(levelProto);

            Assert.IsNull(level.Placement.Orders.Order);
            Assert.IsNull(levelViewModel.Screen.Order?.View);

            var scheme = new ConstructionScheme(proto);
            var construction = level.Placement.Place(scheme, new Point(0, 0));
            Assert.IsTrue(construction.IsProvide(ingridient));

            Assert.IsNotNull(level.Placement.Orders.Order);
            Assert.IsNotNull(levelViewModel.Screen.Order?.View);
            Assert.IsNotNull(levelViewModel.Screen.Order.Recipies.First().View);
        }

        #endregion

        #region Orders

        [Test]
        public void OrdersAreSelectedOnlyIfWeCanProcessThem()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CloseOrderIfAllRecipiesAreComplited()
        {
            throw new NotImplementedException();
        }

        #endregion


        #region Recepies

        [Test]
        public void RecipeProcessing()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void CompliteRecipesOneByOne()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void RecipeTimingIsRight()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
