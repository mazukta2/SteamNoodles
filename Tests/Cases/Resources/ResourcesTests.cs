﻿using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Customers
{
    public class ResourcesTests
    {
        [Test]
        public void IsChangingMoneyChangeView()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();

            Assert.AreEqual(0, models.Money);
            Assert.AreEqual(0, views.Screen.Resources.Value.Money.GetValue());

            models.ChangeMoney(2);

            Assert.AreEqual(2, models.Money);
            Assert.AreEqual(2, views.Screen.Resources.Value.Money.GetValue());

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
