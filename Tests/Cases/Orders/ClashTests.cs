using Game.Tests.Controllers;
using Game.Tests.Mocks.Prototypes.Levels;
using NUnit.Framework;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Orders
{
    public class ClashTests
    {
        [Test]
        public void IsClashStartedAndFinished()
        {
            var game = new GameController();
            var proto = new TestBuildingPrototype();
            var levelProto = new TestLevelPrototype();
            var order = new TestOrderPrototype();
            levelProto.Add(order);

            var (models, presenters, views) = game.LoadLevel(levelProto);

            Assert.IsNull(models.Orders.CurrentOrder);
            Assert.IsNull(presenters.Screen.Order?.View);
            Assert.IsNotNull(views.Screen.Clashes);

            //var scheme = new ConstructionScheme(proto);
            //var construction = models.Placement.Place(scheme, new Point(0, 0));
            //Assert.IsTrue(construction.IsProvide(models.Orders.CurrentOrder.Recipes.First()));

            //Assert.IsNotNull(models.Orders.CurrentOrder);
            //Assert.IsNotNull(presenters.Screen.Order?.View);
            //Assert.IsNotNull(presenters.Screen.Order.Recipies.First().View);
            game.Exit();
        }
    }

}