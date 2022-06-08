using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Resources
{
    public class CoinsResourcesTests
    {
        [Test, Order(TestCore.PresenterOrder)]
        public void IsCoinsViewWorking()
        {
            var coins = new CoinsService();
            var collection = new ViewsCollection();

            var view = new CustumerCoinsView(collection);
            new CustumerCoinsPresenter(view, coins);

            Assert.AreEqual("0", view.Text.Value);
            coins.Change(2);
            Assert.AreEqual("2", view.Text.Value);
            coins.Change(2);
            Assert.AreEqual("4", view.Text.Value);
            coins.Change(-10);
            Assert.AreEqual("0", view.Text.Value);

            collection.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
