using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Buildings;
using Game.Tests.Mocks.Settings.Buildings.Features;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;
using static Game.Assets.Scripts.Game.Logic.Models.Orders.ServingCustomerProcess;

namespace Game.Tests.Cases.Customers
{
    public class CustomersMovementTests
    {

        [Test]
        public void IsCostumerPlacedAtTable()
        {
            var game = new GameController();
            var levelProto = new LevelSettings();

            var (models, _, views) = game.LoadLevel(levelProto);
            models.Hand.Add(new ConstructionSettings() {
                FeaturesList = new List<Assets.Scripts.Game.Logic.Settings.Constructions.IConstructionFeatureSettings>()
                {
                    new PlaceToEatConstructionFeatureSettings()
                }
            });

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(-0.5f, -0.5f));

            Assert.AreEqual(2, models.Placement.Constructions.Count);

            var orderingBuilding = models.Placement.Constructions.First();
            var tableBuilding = models.Placement.Constructions.Last();
            views.Screen.Clashes.Value.StartClash.Click();


            var customerManager = models.Customers;
            var customer = models.Customers.ServingCustomer;
            Assert.AreEqual(Phase.MovingTo, customer.CurrentPhase);
            Assert.IsFalse(customerManager.IsOccupied(tableBuilding));
            Assert.IsFalse(customerManager.IsOccupied(orderingBuilding));
            customer.Unit.TeleportToTarget();
            Assert.AreEqual(Phase.Ordering, customer.CurrentPhase);
            Assert.IsFalse(customerManager.IsOccupied(tableBuilding));
            Assert.IsTrue(customerManager.IsOccupied(orderingBuilding));
            game.PushTime(1);
            Assert.AreEqual(Phase.WaitCooking, customer.CurrentPhase);
            Assert.AreEqual(customer, models.Customers.ServingCustomer);
            Assert.IsTrue(customerManager.IsOccupied(tableBuilding));
            Assert.IsFalse(customerManager.IsOccupied(orderingBuilding));
            game.PushTime(1);
            Assert.AreEqual(Phase.Eating, customer.CurrentPhase);
            Assert.AreNotEqual(customer, models.Customers.ServingCustomer);
            Assert.IsTrue(customerManager.IsOccupied(tableBuilding));
            Assert.IsFalse(customerManager.IsOccupied(orderingBuilding));
            game.PushTime(1);
            Assert.AreEqual(Phase.MovingAway, customer.CurrentPhase);
            Assert.IsFalse(customerManager.IsOccupied(tableBuilding));
            Assert.IsFalse(customerManager.IsOccupied(orderingBuilding));

            game.Exit();
        }


        [Test]
        public void IsOrlderingPlaceAndTables()
        {
            var game = new GameController();
            var levelProto = new LevelSettings();

            var (models, _, views) = game.LoadLevel(levelProto);
            models.Hand.Add(new ConstructionSettings()
            {
                FeaturesList = new List<Assets.Scripts.Game.Logic.Settings.Constructions.IConstructionFeatureSettings>()
                {
                    new PlaceToEatConstructionFeatureSettings()
                }
            });
            var customerSettings = (CustomerSettings)models.Customers.GetCustomersPool().GetItems().First().Key;
            customerSettings.OrderingTime = 1;
            customerSettings.CookingTime = 1;
            customerSettings.EatingTime = 10;

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(-0.5f, -0.5f));

            Assert.AreEqual(2, models.Placement.Constructions.Count);

            var orderingBuilding = models.Placement.Constructions.First();
            var tableBuilding = models.Placement.Constructions.Last();
            views.Screen.Clashes.Value.StartClash.Click();


            var customerManager = models.Customers;
            var customer1 = models.Customers.ServingCustomer;
            customer1.Unit.TeleportToTarget();
            Assert.IsTrue(customerManager.IsOccupied(orderingBuilding));
            Assert.IsFalse(customerManager.IsOccupied(tableBuilding));
            game.PushTime(2); // odering and cooking for 1.
            var customer2 = models.Customers.ServingCustomer;
            customer2.Unit.TeleportToTarget();
            Assert.AreNotEqual(customer2, customer1);
            Assert.AreNotEqual(customer2.Unit, customer1.Unit);

            Assert.IsTrue(customerManager.IsOccupied(orderingBuilding));
            Assert.IsTrue(customerManager.IsOccupied(tableBuilding));
            Assert.AreEqual(Phase.Eating, customer1.CurrentPhase);
            Assert.AreEqual(Phase.Ordering, customer2.CurrentPhase);
            Assert.AreEqual(customer2, customerManager.ServingCustomer);

            game.PushTime(2);

            Assert.IsTrue(customerManager.IsOccupied(orderingBuilding));
            Assert.IsTrue(customerManager.IsOccupied(tableBuilding));
            Assert.AreEqual(Phase.Eating, customer1.CurrentPhase);
            Assert.AreEqual(Phase.Eating, customer2.CurrentPhase);
            Assert.AreEqual(null, customerManager.ServingCustomer);

            game.PushTime(8);

            Assert.IsTrue(customerManager.IsOccupied(orderingBuilding));
            Assert.IsFalse(customerManager.IsOccupied(tableBuilding));
            Assert.AreEqual(Phase.MovingAway, customer1.CurrentPhase);
            Assert.AreEqual(Phase.Eating, customer2.CurrentPhase);
            Assert.AreEqual(null, customerManager.ServingCustomer);

            game.PushTime(2);
            Assert.AreEqual(Phase.MovingAway, customer2.CurrentPhase);
            Assert.AreNotEqual(null, customerManager.ServingCustomer);
            Assert.AreNotEqual(customer2, customerManager.ServingCustomer);

            game.Exit();
        }


        [Test]
        public void IsUnitsArePushedToQueueWorking()
        {
            var game = new GameController();
            var levelProto = new LevelSettings();

            var (models, _, views) = game.LoadLevel(levelProto);
            var settings = (LevelSettings)models.Clashes.Settings;
            settings.MinQueue = 3;
            settings.MaxQueue = 3;

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));

            Assert.AreEqual(3, models.Customers.Queue.TargetCount);
            Assert.AreEqual(3, models.Customers.Queue.ActualCount);

            //var orderingBuilding = models.Placement.Constructions.First();
            //var tableBuilding = models.Placement.Constructions.Last();
            //views.Screen.Clashes.Value.StartClash.Click();

            //var customerManager = models.Customers;
            //var customer1 = models.Customers.ServingCustomer;
            //customer1.Unit.TeleportToTarget();

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
