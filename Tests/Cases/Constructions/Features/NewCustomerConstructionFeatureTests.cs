using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Buildings;
using Game.Tests.Mocks.Settings.Buildings.Features;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Constructions
{
    public class NewCustomerConstructionFeatureTests
    {
        [Test]
        public void IsFeatureWorking()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();

            Assert.AreEqual(1, models.Units.GetPool().Count());

            var newCustomer = new CustomerSettings();
            models.Hand.Add(new ConstructionSettings() { 
                FeaturesList = new List<Assets.Scripts.Game.Logic.Settings.Constructions.IConstructionFeatureSettings> { 
                    new NewCustomerConstructionFeatureSettings(newCustomer)
                }
            });

            views.Screen.Hand.Value.Cards.List.Last().Button.Click();
            views.Placement.Value.Click(System.Numerics.Vector2.Zero);

            Assert.AreEqual(2, models.Units.GetPool().Count());

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
