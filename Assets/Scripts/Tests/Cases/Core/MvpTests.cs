using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Core
{
    public class MvpTests
    {
        [Test]
        public void ViewIsCreatingPresenter()
        {
            var build = new GameTestConstructor().AddAndLoadLevel(new EmptyLevel()).Build();
            var view = new TestView(build.CurrentLevel);

            Assert.IsTrue(view.IsInited);
            Assert.IsNotNull(view.Presenter);
            var presenter = view.Presenter;

            view.Dispose();

            Assert.IsTrue(view.IsDisposed);
            Assert.IsTrue(presenter.IsDisposed);

            build.Dispose();
        }

        private class TestView : View
        {
            public bool IsInited { get; private set; }
            public TestPresenter Presenter { get; private set; }

            public TestView(ILevel level) : base(level)
            {
                Presenter = new TestPresenter(this);
                IsInited = true;
            }

            protected override void DisposeInner()
            {
            }
        }

        private class TestPresenter : BasePresenter
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
