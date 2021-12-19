using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
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
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Customers
{
    public class TipsForTagCustomerFeatureTests
    {
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void IsFeatureWorking(bool added)
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();

            var constructionSettings = (ConstructionSettings)models.Hand.Cards.First().Settings;
            constructionSettings.TagsList.Add(Assets.Scripts.Game.Logic.Models.Buildings.ConstructionTag.Service, 101);
            constructionSettings.TagsList.Add(Assets.Scripts.Game.Logic.Models.Buildings.ConstructionTag.Machine, 2);

            var customerSettings = (CustomerSettings)models.Customers.GetCustomersPool().First();
            customerSettings.Money = 100;
            if (added)
                customerSettings.AddFeature(new TipsForConstructionTagCustomerFeatureSettings()
                {
                    TipModificator = new PercentModificator(PercentModificator.ActionType.Remove, 25f)
                });

            CommonTestActions.BuildConstruction(game, views);
            models.Customers.Queue.Add();

            Assert.AreEqual(0, models.Money);
            CommonTestActions.ServeCustumer(game, models);
            if (added)
                Assert.AreEqual(150, models.Money);
            else
                Assert.AreEqual(200, models.Money);

            game.Exit();
        }

        [Test]
        public void IsTipsHasLowLimit()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();

            var constructionSettings = (ConstructionSettings)models.Hand.Cards.First().Settings;
            constructionSettings.TagsList.Add(Assets.Scripts.Game.Logic.Models.Buildings.ConstructionTag.Service, 101);
            constructionSettings.TagsList.Add(Assets.Scripts.Game.Logic.Models.Buildings.ConstructionTag.Machine, 50);

            var customerSettings = (CustomerSettings)models.Customers.GetCustomersPool().First();
            customerSettings.Money = 100;
            customerSettings.AddFeature(new TipsForConstructionTagCustomerFeatureSettings()
            {
                TipModificator = new PercentModificator(PercentModificator.ActionType.Remove, 25f)
            });

            CommonTestActions.BuildConstruction(game, views);
            models.Customers.Queue.Add();

            Assert.AreEqual(0, models.Money);
            CommonTestActions.ServeCustumer(game, models);
            Assert.AreEqual(100, models.Money);

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
} 
