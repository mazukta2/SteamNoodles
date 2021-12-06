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
            var customer1 = new CustomerSettings();
            var customer2 = new CustomerSettings();

            models.Hand.Add(new ConstructionSettings() { 
                Features = new Assets.Scripts.Game.Logic.Settings.Constructions.IConstructionFeatureSettings[] { 
                    new NewCustomerConstructionFeatureSettings(customer1)
                }
            });


            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
