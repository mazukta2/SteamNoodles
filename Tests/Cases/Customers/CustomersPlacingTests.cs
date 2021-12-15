﻿using Assets.Scripts.Logic.Prototypes.Levels;
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
    public class CustomersPlacingTests
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
            var customer = models.Customers.CurrentCustomer;
            Assert.AreEqual(Phase.MovingTo, customer.CurrentPhase);
            Assert.AreEqual(null, customerManager.GetOrderer());
            Assert.AreEqual(null, customerManager.IsOccupied(tableBuilding));
            Assert.AreEqual(null, customerManager.IsOccupied(orderingBuilding));
            customer.Unit.TeleportToTarget();
            Assert.AreEqual(Phase.Ordering, customer.CurrentPhase);
            Assert.AreEqual(null, customer.PlacementOccupied);
            Assert.AreEqual(orderingBuilding, customer.OrderingOccupied);
            game.PushTime(1);
            Assert.AreEqual(Phase.WaitCooking, customer.CurrentPhase);
            Assert.AreEqual(tableBuilding, customer.PlacementOccupied);
            Assert.AreEqual(null, customer.OrderingOccupied);
            game.PushTime(1);
            Assert.AreEqual(Phase.Eating, customer.CurrentPhase);
            Assert.AreEqual(tableBuilding, customer.PlacementOccupied);
            Assert.AreEqual(null, customer.OrderingOccupied);
            game.PushTime(1);
            Assert.AreEqual(Phase.MovingAway, customer.CurrentPhase);

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}