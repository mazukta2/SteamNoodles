using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Ui;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Screens
{
    public class ScreenTests
    {
        [Test, Order(TestCore.EssentialOrder)]
        public void IsViewCreating()
        {
            var assets = new AssetsMock();
            assets.AddPrefab("Screens/TestScreenInterface", new DefaultViewPrefab(x => new TestScreenView(x)));

            var viewCollection = new ViewsCollection();
            var view = new ScreenManagerView(viewCollection);
            var presenter = new ScreenManagerPresenter(view, new GameAssets(assets));

            Assert.AreEqual(0, view.Screen.Count);

            presenter.Open<ITestScreenInterfaceView>(v => new TestScreenPresenter(v, 1));

            Assert.AreEqual(1, view.Screen.Count);
            Assert.IsTrue(view.Screen.FindView<ITestScreenInterfaceView>().Presenter is TestScreenPresenter);

            viewCollection.Dispose();
        }

        private interface ITestScreenInterfaceView : IScreenView
        {

        }

        private class TestScreenView : ScreenView<TestScreenPresenter>, ITestScreenInterfaceView
        {
            public TestScreenView(IViewsCollection level) : base(level) { }
        }

        private class TestScreenPresenter : BasePresenter<ITestScreenInterfaceView>
        {
            public TestScreenPresenter(ITestScreenInterfaceView view, int param) : base(view) { Param = param; }

            public int Param { get; }
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
