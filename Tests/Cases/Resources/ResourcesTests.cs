using Game.Tests.Controllers;
using NUnit.Framework;

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
            Assert.AreEqual(0, views.Screen.Resources.Money.GetValue());

            models.ChangeMoney(2);

            Assert.AreEqual(2, models.Money);
            Assert.AreEqual(2, views.Screen.Resources.Money.GetValue());

            game.Exit();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
