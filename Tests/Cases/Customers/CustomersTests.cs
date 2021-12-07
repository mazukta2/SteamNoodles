using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
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
            var levelProto = new LevelSettings();

            var (models, presenters, views) = game.LoadLevel(levelProto);
            views.Screen.Clashes.Value.StartClash.Click();

            Assert.IsTrue(models.Clashes.IsInClash);

            Assert.IsNotNull(models.Customers.CurrentCustomer);

            game.Exit();
        }

        #endregion

        [Test]
        public void IsCustumerPoolGiveRightCustumers()
        {
            var game = new GameController();
            
            var (models, presenters, views) = game.LoadLevel();
            var customer1 = models.Customers.GetCustomersPool().GetItems().First().Key;
            var customer2 = new CustomerSettings();
            models.AddCustumer(customer2);
            models.AddCustumer(customer2);
            views.Screen.Clashes.Value.StartClash.Click();

            var customers = new List<Unit>();

            AddUnit();
            AddUnit();
            AddUnit();

            Assert.AreEqual(1, customers.Count(x => x.Settings == customer1));
            Assert.AreEqual(2, customers.Count(x => x.Settings == customer2));

            void AddUnit()
            {
                var customer = models.Customers.CurrentCustomer.Unit;

                Assert.IsFalse(customers.Contains(customer));
                customer.TeleportToTarget();
                game.PushTime(3);
                customer.TeleportToTarget();
                Assert.IsTrue(customer.IsServed);
                Assert.IsNotNull(customer.Settings);
                Assert.AreNotEqual(models.Customers.CurrentCustomer.Unit, customer);
                Assert.IsFalse(customers.Contains(customer));

                customers.Add(customer);
            }

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
