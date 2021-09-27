using Assets.Scripts.Logic.Models.Levels;
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

            Assert.IsNull(level.Orders.CurrentOrder);
            Assert.IsNull(levelViewModel.Screen.Order?.View);

            var scheme = new ConstructionScheme(proto);
            var construction = level.Placement.Place(scheme, new Point(0, 0));
            Assert.IsTrue(construction.IsProvide(ingridient));

            Assert.IsNotNull(level.Orders.CurrentOrder);
            Assert.IsNotNull(levelViewModel.Screen.Order?.View);
            Assert.IsNotNull(levelViewModel.Screen.Order.Recipies.First().View);
        }

        #endregion

        #region Orders

        [Test]
        public void OrdersAreSelectedOnlyIfWeCanProcessThem()
        {
            var levelProto = new TestLevelPrototype();

            var weHaveThisIngridient = new TestIngredientPrototype();
            var weDontHaveThisIngridient = new TestIngredientPrototype();
            CreateOrder(levelProto, weHaveThisIngridient);
            CreateOrder(levelProto, weDontHaveThisIngridient);

            var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel(levelProto);

            Assert.AreEqual(0, level.Orders.GetAvailableOrders().Count);

            CreateBuilding(level, weHaveThisIngridient);

            Assert.AreEqual(1, level.Orders.GetAvailableOrders().Count);
            Assert.IsTrue(level.Orders.GetAvailableOrders().First().Have(weHaveThisIngridient));
            Assert.IsTrue(level.Orders.CurrentOrder.Have(weHaveThisIngridient));

            static void CreateBuilding(GameLevel level, TestIngredientPrototype ingredientPrototype)
            {
                var buildingProto = new TestBuildingPrototype() { ProvideIngredient = ingredientPrototype };
                var scheme = new ConstructionScheme(buildingProto);
                level.Placement.Place(scheme, new Point(0, 0));
            }

            static void CreateOrder(TestLevelPrototype levelPrototype, TestIngredientPrototype ingredientPrototype)
            {
                var order = new TestOrderPrototype();
                order.Add(new TestRecipePrototype(ingredientPrototype));
                levelPrototype.Add(order);
            }
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
