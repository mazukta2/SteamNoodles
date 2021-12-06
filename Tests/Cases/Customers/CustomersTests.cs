using Game.Tests.Controllers;
using Game.Tests.Mocks.Prototypes.Levels;
using NUnit.Framework;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Customers
{
    public class CustomersTests
    {
        #region Existance
        [Test]
        public void IsCurrentCustomerSetted()
        {
            var game = new GameController();
            var levelProto = new LevelPrototype();

            var (models, presenters, views) = game.LoadLevel(levelProto);
            views.Screen.Clashes.Value.StartClash.Click();

            Assert.IsTrue(models.Clashes.IsInClash);

            Assert.IsNotNull(models.Customers.CurrentCustomer);

            game.Exit();
        }

        #endregion

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
