using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Tests.Mocks.Prototypes.Levels;
using Game.Tests.Shortcuts;
using NUnit.Framework;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Orders
{
    public class OrderTests
    {
        //#region Existance
        //[Test]
        //public void IsCurrentOrderSetted()
        //{
        //    var ingridient = new TestIngredientPrototype();
        //    var proto = new TestBuildingPrototype();
        //    proto.ProvideIngredient = ingridient;
        //    var levelProto = new TestLevelPrototype();
        //    var order = new TestOrderPrototype();
        //    var recipe = new TestRecipePrototype(ingridient);
        //    order.Add(recipe);
        //    levelProto.Add(order);

        //    var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel(levelProto);

        //    Assert.IsNull(level.Orders.CurrentOrder);
        //    Assert.IsNull(levelViewModel.Screen.Order?.View);

        //    var scheme = new ConstructionScheme(proto);
        //    var construction = level.Placement.Place(scheme, new Point(0, 0));
        //    Assert.IsTrue(construction.IsProvide(level.Orders.CurrentOrder.Recipes.First()));

        //    Assert.IsNotNull(level.Orders.CurrentOrder);
        //    Assert.IsNotNull(levelViewModel.Screen.Order?.View);
        //    Assert.IsNotNull(levelViewModel.Screen.Order.Recipies.First().View);
        //}

        //#endregion

        //#region Orders

        //[Test]
        //public void OrdersAreSelectedOnlyIfWeCanProcessThem()
        //{
        //    var levelProto = new TestLevelPrototype();

        //    var weHaveThisIngridient = new TestIngredientPrototype();
        //    var weDontHaveThisIngridient = new TestIngredientPrototype();
        //    CreateOrder(levelProto, weHaveThisIngridient);
        //    CreateOrder(levelProto, weDontHaveThisIngridient);

        //    var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel(levelProto);

        //    Assert.AreEqual(0, level.Orders.GetAvailableOrders().Count);

        //    CreateBuilding(level, weHaveThisIngridient);

        //    Assert.AreEqual(1, level.Orders.GetAvailableOrders().Count);
        //    Assert.IsTrue(level.Orders.GetAvailableOrders().First().Have(weHaveThisIngridient));
        //    Assert.IsTrue(level.Orders.CurrentOrder.Have(weHaveThisIngridient));
        //}

        //[Test]
        //public void CloseOrderIfAllRecipiesAreComplited()
        //{
        //    var levelProto = new TestLevelPrototype();
        //    var weHaveThisIngridient = new TestIngredientPrototype();
        //    CreateOrder(levelProto, weHaveThisIngridient);
        //    var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel(levelProto);

        //    CreateBuilding(level, weHaveThisIngridient);

        //    Assert.IsTrue(level.Orders.CurrentOrder.IsOpen());

        //    var order = level.Orders.CurrentOrder;

        //    level.Orders.CurrentOrder.Recipes.First().Progress(1000);

        //    Assert.IsFalse(order.IsOpen());

        //    Assert.AreNotEqual(order, level.Orders.CurrentOrder); // current order is changed.
        //}

        //#endregion


        //#region Recepies

        //[Test]
        //public void WorkProcessing()
        //{
        //    var levelProto = new TestLevelPrototype();
        //    var weHaveThisIngridient = new TestIngredientPrototype();
        //    CreateOrder(levelProto, weHaveThisIngridient);
        //    var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel(levelProto);

        //    Assert.IsNotNull(level.Work.MainWorker);
        //    Assert.IsNull(level.Work.MainWorker.Job);

        //    CreateBuilding(level, weHaveThisIngridient);
        //    Assert.IsTrue(level.Orders.CurrentOrder.IsOpen());

        //    Assert.IsNotNull(level.Work.MainWorker);
        //    Assert.IsNotNull(level.Work.MainWorker.Job);

        //    var order = level.Orders.CurrentOrder;
        //    Assert.IsTrue(order.IsOpen());
        //    var recipe = order.Recipes.First();
        //    Assert.IsTrue(recipe.IsOpen());

        //    level.Time.MoveTime(100f);

        //    Assert.IsTrue(!recipe.IsOpen());
        //}

        //[Test]
        //public void CompliteRecipesOneByOne()
        //{
        //    var levelProto = new TestLevelPrototype();
        //    var weHaveThisIngridient = new TestIngredientPrototype();
        //    var orderProto = new TestOrderPrototype();
        //    orderProto.Add(new TestRecipePrototype(weHaveThisIngridient));
        //    orderProto.Add(new TestRecipePrototype(weHaveThisIngridient));
        //    orderProto.Add(new TestRecipePrototype(weHaveThisIngridient));
        //    levelProto.Add(orderProto);
        //    var (level, levelViewModel, levelView) = new LevelShortcuts().LoadLevel(levelProto);
        //    CreateBuilding(level, weHaveThisIngridient);

        //    var order = level.Orders.CurrentOrder;
        //    Assert.IsTrue(order.IsOpen());
        //    var recipe1 = order.Recipes[0];
        //    var recipe2 = order.Recipes[1];
        //    var recipe3 = order.Recipes[2];
        //    Assert.IsTrue(recipe1.IsOpen());
        //    Assert.IsTrue(recipe2.IsOpen());

        //    var construction = level.Placement.Constructions.First();
        //    level.Time.MoveTime(construction.WorkTime);
           
        //    Assert.AreEqual(construction.WorkProgressPerHit, recipe1.CurrentProgress);
        //    Assert.AreEqual(0, recipe2.CurrentProgress);
        //    Assert.IsTrue(recipe1.IsOpen());
        //    Assert.IsTrue(recipe2.IsOpen());

        //    for (int i = 2; i <= 9; i++)
        //    {
        //        level.Time.MoveTime(construction.WorkTime);
        //        Assert.AreEqual(i*10, recipe1.CurrentProgress);
        //    }

        //    level.Time.MoveTime(construction.WorkTime * 2);
        //    Assert.AreEqual(100, recipe1.CurrentProgress);
        //    Assert.AreEqual(10, recipe2.CurrentProgress);
        //    Assert.IsTrue(!recipe1.IsOpen());
        //    Assert.IsTrue(recipe2.IsOpen());
        //    Assert.IsTrue(recipe3.IsOpen());

        //    level.Time.MoveTime(construction.WorkTime);
        //    Assert.AreEqual(20, recipe2.CurrentProgress);

        //    level.Time.MoveTime(1000f);

        //    Assert.IsTrue(!recipe1.IsOpen());
        //    Assert.IsTrue(!recipe2.IsOpen());
        //    Assert.IsTrue(!recipe3.IsOpen());

        //    Assert.IsTrue(!order.IsOpen());
        //}

        //#endregion


        //static void CreateBuilding(GameLevel level, TestIngredientPrototype ingredientPrototype)
        //{
        //    var buildingProto = new TestBuildingPrototype() { ProvideIngredient = ingredientPrototype };
        //    var scheme = new ConstructionScheme(buildingProto);
        //    level.Placement.Place(scheme, new Point(0, 0));
        //}

        //static void CreateOrder(TestLevelPrototype levelPrototype, TestIngredientPrototype ingredientPrototype)
        //{
        //    var order = new TestOrderPrototype();
        //    order.Add(new TestRecipePrototype(ingredientPrototype));
        //    levelPrototype.Add(order);
        //}
    }
}
