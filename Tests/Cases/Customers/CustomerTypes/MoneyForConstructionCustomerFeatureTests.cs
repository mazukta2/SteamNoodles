using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Buildings;
using Game.Tests.Mocks.Settings.Buildings.Features;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Customers
{
    public class MoneyForConstructionCustomerFeatureTests
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsFeatureWorking(bool added)
        {
            var game = new GameController();
            
            var (models, presenters, views) = game.LoadLevel();
            var construction = (ConstructionSettings)models.Hand.Cards.First().Settings;
            var customerSettings = (CustomerSettings)models.Customers.GetCustomersPool().GetItems().First().Key;
            if (added)
                customerSettings.AddFeature(new MoneyForConstructionCustomerFeatureSettings(construction) { Money = 10 });

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));
            views.Screen.Clashes.Value.StartClash.Click();

            Assert.AreEqual(0, models.Money);

            var customer = models.Customers.ServingCustomer.Unit;
            customer.TeleportToTarget();
            game.PushTime(3);
            customer.TeleportToTarget();
            Assert.IsTrue(customer.IsServed);
            if (added)
                Assert.AreEqual(11, models.Money);
            else
                Assert.AreEqual(1, models.Money);

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
