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
            var levelProto = new TestLevelPrototype();
            var order = new TestOrderPrototype();
            levelProto.Add(order);

            var (models, presenters, views) = game.LoadLevel(levelProto);

            Assert.IsFalse(models.Clashes.IsInClash);
            Assert.IsNull(models.Orders.CurrentOrder);
            Assert.IsNull(views.Screen.Order.Value);
            Assert.IsNotNull(views.Screen.Clashes.Value);
            Assert.IsTrue(views.Screen.Clashes.Value.StartClash.IsShowing);

            views.Screen.Clashes.Value.StartClash.Click();

            Assert.IsTrue(models.Clashes.IsInClash);
            Assert.IsFalse(views.Screen.Clashes.Value.StartClash.IsShowing);
            Assert.IsNotNull(models.Orders.CurrentOrder);
            Assert.IsNotNull(views.Screen.Order.Value);

            Assert.AreEqual(20, models.Clashes.GetClashesTime());

            game.PushTime(10);

            Assert.IsTrue(models.Clashes.IsInClash);

            game.PushTime(10);

            Assert.IsFalse(models.Clashes.IsInClash);
            Assert.IsNull(models.Orders.CurrentOrder);
            Assert.IsNull(views.Screen.Order.Value);
            Assert.IsNotNull(views.Screen.Clashes.Value);
            Assert.IsTrue(views.Screen.Clashes.Value.StartClash.IsShowing);

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }

}