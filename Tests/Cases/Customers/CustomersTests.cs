using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Buildings;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Customers
{
    public class CustomersTests
    {
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

        [Test]
        public void IsCurrentConsumerChanged()
        {
            var game = new GameController();
            var levelProto = new LevelSettings();

            var (models, presenters, views) = game.LoadLevel(levelProto);
            views.Screen.Clashes.Value.StartClash.Click();

            Assert.IsTrue(models.Clashes.IsInClash);

            var consumer = models.Customers.CurrentCustomer;
            Assert.IsNotNull(consumer);
            CommonTestActions.ServeCustumer(game, models);
            Assert.IsTrue(consumer.Unit.IsServed);
            Assert.AreNotEqual(consumer, models.Customers.CurrentCustomer);
            Assert.AreEqual(ServingCustomerProcess.Phase.Exiting, consumer.CurrentPhase);
            Assert.AreEqual(ServingCustomerProcess.Phase.MovingTo, models.Customers.CurrentCustomer.CurrentPhase);

            game.Exit();
        }

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

        [Test]
        public void IsCustomersGiveYourMoney()
        {
            var game = new GameController();

            var (models, presenters, views) = game.LoadLevel();
            var customer1 = (CustomerSettings)models.Customers.GetCustomersPool().GetItems().First().Key;
            customer1.Money = 3;
            views.Screen.Clashes.Value.StartClash.Click();

            Assert.AreEqual(0, models.Money);

            var customer = models.Customers.CurrentCustomer.Unit;
            CommonTestActions.ServeCustumer(game, models);
            Assert.IsTrue(customer.IsServed);
            Assert.AreEqual(3, models.Money);

            game.Exit();
        }

        [Test]
        public void IsCostumerGivesTips()
        {
            var game = new GameController();

            var (models, presenters, views) = game.LoadLevel();
            var unitSettings = (CustomerSettings)models.Customers.GetCustomersPool().GetItems().First().Key;
            var constructionSettings = (ConstructionSettings)models.Hand.Cards.First().Settings;
            unitSettings.BaseTipMultiplayer = 2;
            constructionSettings.TagsList.Add(Assets.Scripts.Game.Logic.Models.Buildings.ConstructionTag.Service, 101);

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));

            views.Screen.Clashes.Value.StartClash.Click();

            Assert.AreEqual(0, models.Money);
            Assert.AreEqual(101, models.Service);

            Assert.AreEqual(2, models.Customers.CurrentCustomer.Unit.GetTips());

            CommonTestActions.ServeCustumer(game, models);

            Assert.AreEqual(3, models.Money); // 1 for normal, 1*2 = tips;

            game.Exit();
        }

        [Test]
        public void IsCostumerServedRightTime()
        {
            var game = new GameController();

            var (models, presenters, views) = game.LoadLevel();
            var customer1 = (CustomerSettings)models.Customers.GetCustomersPool().GetItems().First().Key;
            customer1.OrderingTime = 2;
            customer1.CookingTime = 2;
            customer1.EatingTime = 2;
            views.Screen.Clashes.Value.StartClash.Click();

            Assert.AreEqual(0, models.Money);

            var customer = models.Customers.CurrentCustomer.Unit;
            Assert.IsTrue(customer.IsMoving());
            customer.TeleportToTarget();

            for (int i = 0; i < 6; i++)
            {
                Assert.IsFalse(customer.IsServed);
                Assert.IsFalse(customer.IsMoving());
                game.PushTime(1);
            }
            Assert.IsTrue(customer.IsMoving());
            customer.TeleportToTarget();
            Assert.IsTrue(customer.IsServed);

            game.Exit();
        }

        [Test]
        public void IsConsumerPassAllPhases()
        {
            var game = new GameController();

            var (models, presenters, views) = game.LoadLevel();
            var customer1 = (CustomerSettings)models.Customers.GetCustomersPool().GetItems().First().Key;
            customer1.OrderingTime = 2;
            customer1.CookingTime = 2;
            customer1.EatingTime = 2;
            views.Screen.Clashes.Value.StartClash.Click();

            Assert.AreEqual(0, models.Money);

            var service = models.Customers.CurrentCustomer;
            var customer = models.Customers.CurrentCustomer.Unit;
            Assert.IsTrue(customer.IsMoving());
            customer.TeleportToTarget();

            Assert.AreEqual(ServingCustomerProcess.Phase.Ordering, service.CurrentPhase);
            game.PushTime(2);
            Assert.AreEqual(ServingCustomerProcess.Phase.WaitCooking, service.CurrentPhase);
            game.PushTime(2);
            Assert.AreEqual(ServingCustomerProcess.Phase.Eating, service.CurrentPhase);
            game.PushTime(2);
            Assert.AreEqual(ServingCustomerProcess.Phase.MovingAway, service.CurrentPhase);

            Assert.IsTrue(customer.IsMoving());
            customer.TeleportToTarget();
            Assert.IsTrue(customer.IsServed);

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
