using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Buildings;
using Game.Tests.Mocks.Settings.Buildings.Features;
using Game.Tests.Mocks.Settings.Customers.Features;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Customers
{
    public class EatingSpeedCustomerFeatureTests
    {

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsFeatureWorking(bool added)
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();

            var customerSettings = (CustomerSettings)models.Clashes.CurrentClash.Customers.GetCustomersPool().First();
            if (added)
                customerSettings.AddFeature(new EatingSpeedFeatureSettings()
                {
                    TimeModificator = new PercentModificator(PercentModificator.ActionType.Add, 100f)
                });

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));
            views.Screen.Clashes.Value.StartClash.Click();
            models.Clashes.CurrentClash.Customers.Queue.Add();

            var customer = models.Clashes.CurrentClash.Customers.GetCustomers().Last().Unit;
            customer.TeleportToTarget();

            if (added)
            {
                Assert.AreEqual(2, customer.GetEatingTime());
                game.PushTime(3);
                customer.TeleportToTarget();
                Assert.IsFalse(customer.IsServed);
                game.PushTime(1);
                customer.TeleportToTarget();
                Assert.IsTrue(customer.IsServed);
            }
            else
            {
                Assert.AreEqual(1, customer.GetEatingTime());
                game.PushTime(3);
                customer.TeleportToTarget();
                Assert.IsTrue(customer.IsServed);
            }

            game.Exit();
        }

        [Test]
        public void IsHaveLowLimit()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();

            var customerSettings = (CustomerSettings)models.Clashes.CurrentClash.Customers.GetCustomersPool().First();
            customerSettings.AddFeature(new EatingSpeedFeatureSettings()
            {
                TimeModificator = new PercentModificator(PercentModificator.ActionType.Remove, 100f)
            });

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));
            views.Screen.Clashes.Value.StartClash.Click();
            models.Clashes.CurrentClash.Customers.Queue.Add();

            var customer = models.Clashes.CurrentClash.Customers.GetCustomers().Last().Unit;
             Assert.AreEqual(1f, customer.GetEatingTime());

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
