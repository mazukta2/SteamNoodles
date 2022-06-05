using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class MvpTests
    {
        [Test, Order(TestCore.EssentialOrder)]
        public void IsViewPresenterConnecting()
        {
            var viewCollection = new ViewsCollection();
            var view = new TestView(viewCollection);
            var presenter = new TestPresenter(view);

            Assert.IsNotNull(view.Presenter);
            view.Dispose();

            Assert.IsTrue(view.IsDisposed);
            Assert.IsTrue(presenter.IsDisposed);

            viewCollection.Dispose();
        }

        private class TestView : ViewWithPresenter<TestPresenter>
        {
            public TestView(IViewsCollection level) : base(level)
            {
            }

            protected override void DisposeInner()
            {
            }
        }

        private class TestPresenter : BasePresenter<TestView>
        {
            public TestPresenter(TestView view) : base(view)
            {

            }
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
