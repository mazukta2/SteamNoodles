using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Buildings;
using Game.Tests.Mocks.Settings.Levels;
using Game.Tests.Mocks.Settings.Rewards;
using NUnit.Framework;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases.Customers
{
    public class ClashTests
    {
        [Test]
        public void IsClashStartedAndFinished()
        {
            var game = new GameController();
            var levelProto = new LevelSettings();
            var (models, _, views) = game.LoadLevel(levelProto);

            Assert.IsFalse(models.Clashes.IsInClash);
            Assert.IsNull(models.Customers.ServingCustomer);
            Assert.IsNull(views.Screen.Customers.Value);
            Assert.IsNotNull(views.Screen.Clashes.Value);
            Assert.IsTrue(views.Screen.Clashes.Value.StartClash.IsShowing);

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));
            views.Screen.Clashes.Value.StartClash.Click();

            Assert.IsTrue(models.Clashes.IsInClash);
            Assert.IsFalse(views.Screen.Clashes.Value.StartClash.IsShowing);
            Assert.IsNotNull(models.Customers.ServingCustomer);
            Assert.IsNotNull(views.Screen.Customers.Value);

            Assert.AreEqual(20, models.Clashes.GetClashesTime());

            game.PushTime(10);

            Assert.IsTrue(models.Clashes.IsInClash);

            game.PushTime(10);

            Assert.IsFalse(models.Clashes.IsInClash);
            Assert.IsNull(models.Customers.ServingCustomer);
            Assert.IsNull(views.Screen.Customers.Value);
            Assert.IsNotNull(views.Screen.Clashes.Value);
            Assert.IsTrue(views.Screen.Clashes.Value.StartClash.IsShowing);

            game.Exit();
        }

        [Test]
        public void IsGivingNewCardsAfterClash()
        {
            var game = new GameController();
            var levelProto = new LevelSettings();
            var (models, _, views) = game.LoadLevel(levelProto);
            var settings = (LevelSettings)models.Clashes.Settings;
            settings.ClashReward = new Reward()
            {
                MinToHand = 4,
                MaxToHand = 4,
                ToHandSource = new System.Collections.Generic.Dictionary<Assets.Scripts.Game.Logic.Settings.Constructions.IConstructionSettings, int>()
                {
                    { new ConstructionSettings(), 1 }
                }
            };

            Assert.AreEqual(1, models.Hand.Cards.Count);
            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));
            views.Screen.Clashes.Value.StartClash.Click();
            game.PushTime(20);
            Assert.IsFalse(models.Clashes.IsInClash);
            Assert.AreEqual(4, models.Hand.Cards.Count);

            game.Exit();
        }


        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }

}