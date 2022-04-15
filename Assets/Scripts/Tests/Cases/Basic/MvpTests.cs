using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment.Views;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Assets.Scripts.Tests.Mocks.Levels;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class MvpTests
    {
        [Test]
        public void ViewIsCreatingPresenter()
        {
            var build = new GameTestConstructor().Build();
            var view = new TestView(build.CurrentLevel);

            Assert.IsTrue(view.IsInited);
            Assert.IsNotNull(view.Presenter);
            var presenter = view.Presenter;

            view.Dispose();

            Assert.IsTrue(view.IsDisposed);
            Assert.IsTrue(presenter.IsDisposed);

            build.Dispose();
        }

        private class TestView : PresenterView<TestPresenter>
        {
            public bool IsInited { get; private set; }

            public TestView(LevelView level) : base(level)
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
