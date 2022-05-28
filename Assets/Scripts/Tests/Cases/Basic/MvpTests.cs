using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Views;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class MvpTests
    {
        [Test, Order(TestCore.EssentialOrder)]
        public void IsBuildedAndDisposed()
        {
            var build = new GameConstructor().Build();
            build.Dispose();
        }

        [Test, Order(TestCore.EssentialOrder)]
        public void ViewIsCreatingPresenter()
        {
            var build = new GameConstructor().Build();
            var view = new TestView(build.LevelCollection);

            Assert.IsTrue(view.IsInited);
            Assert.IsNotNull(view.Presenter);
            var presenter = view.Presenter;

            view.Dispose();

            Assert.IsTrue(view.IsDisposed);
            Assert.IsTrue(presenter.IsDisposed);

            build.Dispose();
        }

        private class TestView : ViewWithPresenter<TestPresenter>
        {
            public bool IsInited { get; private set; }

            public TestView(IViewsCollection level) : base(level)
            {
                new TestPresenter(this);
                IsInited = true;
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
