using Game.Tests.Controllers;
using Game.Tests.Mocks.Prototypes.Levels;
using NUnit.Framework;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Customers
{
    public class ClashTests
    {
        [Test]
        public void IsClashStartedAndFinished()
        {
            var game = new GameController();
            var levelProto = new LevelPrototype();
            var (models, presenters, views) = game.LoadLevel(levelProto);

            Assert.IsFalse(models.Clashes.IsInClash);
            Assert.IsNull(models.Customers.CurrentCustomer);
            Assert.IsNull(views.Screen.Customers.Value);
            Assert.IsNotNull(views.Screen.Clashes.Value);
            Assert.IsTrue(views.Screen.Clashes.Value.StartClash.IsShowing);

            views.Screen.Clashes.Value.StartClash.Click();

            Assert.IsTrue(models.Clashes.IsInClash);
            Assert.IsFalse(views.Screen.Clashes.Value.StartClash.IsShowing);
            Assert.IsNotNull(models.Customers.CurrentCustomer);
            Assert.IsNotNull(views.Screen.Customers.Value);

            Assert.AreEqual(20, models.Clashes.GetClashesTime());

            game.PushTime(10);

            Assert.IsTrue(models.Clashes.IsInClash);

            game.PushTime(10);

            Assert.IsFalse(models.Clashes.IsInClash);
            Assert.IsNull(models.Customers.CurrentCustomer);
            Assert.IsNull(views.Screen.Customers.Value);
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