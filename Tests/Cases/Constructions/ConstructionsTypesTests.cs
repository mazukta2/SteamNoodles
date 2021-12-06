using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Buildings;
using Game.Tests.Mocks.Settings.Buildings.Features;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Constructions
{
    public class ConstructionsTypesTests
    {
        [Test]
        public void NewUnitTypeConstructionFeature()
        {
            var game = new GameController();
            var (models, presenters, views) = game.LoadLevel();

            Assert.AreEqual(1, models.Customers.GetCustomersPool().GetItems().Count());

            var newCustomer = new CustomerSettings();
            models.Hand.Add(new ConstructionSettings() { 
                Features = new Assets.Scripts.Game.Logic.Settings.Constructions.IConstructionFeatureSettings[] { 
                    new NewCustomerConstructionFeatureSettings(newCustomer)
                }
            });

            views.Screen.Hand.Value.Cards.List.Last().Button.Click();
            views.Placement.Value.Click(System.Numerics.Vector2.Zero);

            Assert.AreEqual(2, models.Customers.GetCustomersPool().GetItems().Count());

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
