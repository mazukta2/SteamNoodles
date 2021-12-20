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


            var customerManager = models.Clashes.CurrentClash.Customers;
            models.Clashes.CurrentClash.Customers.Queue.Add();

            var customer1 = models.Clashes.CurrentClash.Customers.GetCustomers().Last();
            // first consumer moving
            Assert.AreEqual(Phase.MovingTo, customer1.CurrentPhase);
            Assert.IsFalse(customerManager.UnitPlacement.IsAnybodyPlacedTo(tableBuilding));
            Assert.IsFalse(customerManager.UnitPlacement.IsAnybodyPlacedTo(orderingBuilding));
            Assert.AreEqual(1, models.Clashes.CurrentClash.Customers.GetCustomers().Count);
            customer1.Unit.TeleportToTarget();

            // first consumer ordering
            Assert.AreEqual(Phase.Ordering, customer1.CurrentPhase);
            Assert.AreEqual(1, models.Clashes.CurrentClash.Customers.GetCustomers().Count);
            Assert.IsFalse(customerManager.UnitPlacement.IsAnybodyPlacedTo(tableBuilding));
            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(orderingBuilding));
            Assert.AreEqual(1, models.Clashes.CurrentClash.Customers.GetCustomers().Count);

            // first consumer waiting for food at table. second - is moving
            game.PushTime(1);
            models.Clashes.CurrentClash.Customers.Queue.Add();
            Assert.AreEqual(2, models.Clashes.CurrentClash.Customers.GetCustomers().Count);
            var customer2 = models.Clashes.CurrentClash.Customers.GetCustomers().Last();
            Assert.AreNotEqual(customer1, customer2);

            Assert.AreEqual(Phase.WaitCooking, customer1.CurrentPhase);
            Assert.AreEqual(Phase.MovingTo, customer2.CurrentPhase);

            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(tableBuilding));
            Assert.IsFalse(customerManager.UnitPlacement.IsAnybodyPlacedTo(orderingBuilding));

            // first consummer waiting for food at table. second - ordering at order place.
            customer2.Unit.TeleportToTarget();

            Assert.AreEqual(Phase.WaitCooking, customer1.CurrentPhase);
            Assert.AreEqual(Phase.Ordering, customer2.CurrentPhase);

            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(tableBuilding));
            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(orderingBuilding));

            // first is eating, second - placed at ordering place.
            game.PushTime(1);
            Assert.AreEqual(2, models.Clashes.CurrentClash.Customers.GetCustomers().Count);


            Assert.AreEqual(Phase.Eating, customer1.CurrentPhase);
            Assert.AreEqual(Phase.WaitCooking, customer2.CurrentPhase);

            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(tableBuilding));
            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(orderingBuilding));

            // first is finiesh, second is eating
            game.PushTime(1);
            Assert.AreEqual(2, models.Clashes.CurrentClash.Customers.GetCustomers().Count);

            Assert.AreEqual(Phase.MovingAway, customer1.CurrentPhase);
            Assert.AreEqual(Phase.Eating, customer2.CurrentPhase);

            Assert.IsFalse(customerManager.UnitPlacement.IsAnybodyPlacedTo(tableBuilding));
            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(orderingBuilding));

            game.Exit();
        }


        [Test]
        public void IsOrderingPlaceAndTables()
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
            var customerSettings = (CustomerSettings)models.Clashes.CurrentClash.Customers.GetCustomersPool().First();
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


            var customerManager = models.Clashes.CurrentClash.Customers;
            models.Clashes.CurrentClash.Customers.Queue.Add();

            // take order and cook for 1.
            var customer1 = models.Clashes.CurrentClash.Customers.GetCustomers().Last();
            customer1.Unit.TeleportToTarget();
            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(orderingBuilding));
            Assert.IsFalse(customerManager.UnitPlacement.IsAnybodyPlacedTo(tableBuilding));
            Assert.AreEqual(1, models.Clashes.CurrentClash.Customers.GetCustomers().Count);

            // first is eating, and second is ordering.
            game.PushTime(2);
            models.Clashes.CurrentClash.Customers.Queue.Add();
            Assert.AreEqual(2, models.Clashes.CurrentClash.Customers.GetCustomers().Count);
            var customer2 = models.Clashes.CurrentClash.Customers.GetCustomers().Last();
            customer2.Unit.TeleportToTarget();
            Assert.AreNotEqual(customer2, customer1);
            Assert.AreNotEqual(customer2.Unit, customer1.Unit);

            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(orderingBuilding));
            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(tableBuilding));
            Assert.AreEqual(Phase.Eating, customer1.CurrentPhase);
            Assert.AreEqual(Phase.Ordering, customer2.CurrentPhase);
            Assert.AreEqual(customer2, models.Clashes.CurrentClash.Customers.GetCustomers().Last());
            Assert.AreEqual(2, models.Clashes.CurrentClash.Customers.GetCustomers().Count);

            // both are eating in difrent tables
            game.PushTime(2);

            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(orderingBuilding));
            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(tableBuilding));
            Assert.AreEqual(Phase.Eating, customer1.CurrentPhase);
            Assert.AreEqual(Phase.Eating, customer2.CurrentPhase);
            Assert.AreEqual(2, models.Clashes.CurrentClash.Customers.GetCustomers().Count);

            // first one is finihed
            game.PushTime(8);
            Assert.IsTrue(customerManager.UnitPlacement.IsAnybodyPlacedTo(orderingBuilding));
            Assert.IsFalse(customerManager.UnitPlacement.IsAnybodyPlacedTo(tableBuilding));
            Assert.AreEqual(Phase.MovingAway, customer1.CurrentPhase);
            Assert.AreEqual(Phase.Eating, customer2.CurrentPhase);
            Assert.IsTrue(models.Clashes.CurrentClash.Customers.GetCustomers().Contains(customer1));
            Assert.AreEqual(2, models.Clashes.CurrentClash.Customers.GetCustomers().Count);

            // second one is finihed
            game.PushTime(2);
            Assert.AreEqual(Phase.MovingAway, customer2.CurrentPhase);
            Assert.IsTrue(models.Clashes.CurrentClash.Customers.GetCustomers().Contains(customer2));

            // they both moved
            customer1.Unit.TeleportToTarget();
            customer2.Unit.TeleportToTarget();

            Assert.AreEqual(Phase.Exiting, customer1.CurrentPhase);
            Assert.AreEqual(Phase.Exiting, customer2.CurrentPhase);
            Assert.IsFalse(models.Clashes.CurrentClash.Customers.GetCustomers().Contains(customer1));
            Assert.IsFalse(models.Clashes.CurrentClash.Customers.GetCustomers().Contains(customer2));

            game.Exit();
        }


        [Test]
        public void IsUnitsArePushedOverTimeToQueueWorking()
        {
            var game = new GameController();
            var levelProto = new LevelSettings();

            var (models, _, views) = game.LoadLevel(levelProto);
            var settings = (LevelSettings)models.Clashes.Settings;
            settings.SpawnQueueTime = 5;
            settings.MaxQueue = 3;

            var customerSettings = (CustomerSettings)models.Clashes.CurrentClash.Customers.GetCustomersPool().First();
            customerSettings.OrderingTime = 800;
            customerSettings.CookingTime = 800;
            customerSettings.EatingTime = 800;

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));
            views.Screen.Clashes.Value.StartClash.Click();

            Assert.AreEqual(3, models.Clashes.CurrentClash.Customers.Queue.TargetCount);
            Assert.AreEqual(0, models.Clashes.CurrentClash.Customers.Queue.ActualCount);

            Assert.AreEqual(1, models.Clashes.CurrentClash.Customers.Queue.GetAddingChance());

            game.PushTime(5);

            Assert.IsTrue(1 <= models.Clashes.CurrentClash.Customers.Queue.ActualCount && models.Clashes.CurrentClash.Customers.Queue.ActualCount <= 3);

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
