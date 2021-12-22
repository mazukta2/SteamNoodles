using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Settings.Buildings;
using Game.Tests.Mocks.Settings.Levels;
using Game.Tests.Mocks.Settings.Rewards;
using NUnit.Framework;
using System.Linq;
using Tests.Mocks.Prototypes.Levels;
using static Game.Assets.Scripts.Game.Logic.Models.Orders.ServingCustomerProcess;

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
            var customerSettings = (CustomerSettings)models.Units.GetPool().First();
            customerSettings.Speed = 1000;
       
            Assert.IsFalse(models.Clashes.IsInClash);

            Assert.AreEqual(1, models.Units.GetPool().Count());
            Assert.IsNull(views.Screen.Customers.Value);
            Assert.IsNotNull(views.Screen.Clashes.Value);
            Assert.IsTrue(views.Screen.Clashes.Value.StartClash.IsShowing);

            views.Screen.Hand.Value.Cards.List.First().Button.Click();
            views.Placement.Value.Click(new System.Numerics.Vector2(0, 0));
            views.Screen.Clashes.Value.StartClash.Click();
            models.Clashes.CurrentClash.Customers.Queue.Add();
            models.Clashes.CurrentClash.Customers.Queue.Add();
            models.Clashes.CurrentClash.Customers.Queue.Add();
            models.Clashes.CurrentClash.Customers.Queue.Add();
            models.Clashes.CurrentClash.Customers.Queue.Add();
            models.Clashes.CurrentClash.Customers.Queue.Add();

            Assert.IsTrue(models.Clashes.IsInClash);
            Assert.IsFalse(views.Screen.Clashes.Value.StartClash.IsShowing);
            Assert.AreEqual(6, models.Clashes.CurrentClash.Customers.GetCustomers().Count());

            Assert.AreEqual(6, models.Clashes.CurrentClash.NeedToServe);

            // you need 3 seconds to serve each customer
            var clash = models.Clashes.CurrentClash;
            var customer = clash.Customers.GetCustomers()[0];
            Assert.AreEqual(Phase.Ordering, customer.CurrentPhase);
            game.PushTime(1);
            Assert.AreEqual(Phase.WaitCooking, customer.CurrentPhase);
            game.PushTime(1);
            Assert.AreEqual(Phase.Eating, customer.CurrentPhase);
            game.PushTime(1);
            Assert.AreEqual(Phase.Exiting, customer.CurrentPhase);

            Assert.IsTrue(models.Clashes.IsInClash);
            Assert.AreEqual(1, clash.Served);
            Assert.AreEqual(5, clash.Customers.GetCustomers().Count());

            game.PushTime(3);
            Assert.AreEqual(2, clash.Served);
            Assert.IsTrue(models.Clashes.IsInClash);

            game.PushTime(3);

            Assert.IsTrue(models.Clashes.IsInClash);
            Assert.AreEqual(3, clash.Served);

            game.PushTime(9);

            Assert.IsFalse(models.Clashes.IsInClash);
            Assert.IsNull(models.Clashes.CurrentClash);
            Assert.AreEqual(6, clash.Served);
            Assert.AreEqual(0, clash.Customers.GetCustomers().Count());
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
            settings.NeedToServe = 1;
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
            models.Clashes.CurrentClash.Customers.Queue.Add();
            Assert.AreEqual(1, models.Clashes.CurrentClash.Customers.GetCustomers().Count());
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