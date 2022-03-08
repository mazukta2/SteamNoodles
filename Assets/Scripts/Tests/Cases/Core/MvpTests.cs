using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Core
{
    public class MvpTests
    {
        /*
        [Test]
        public void ViewIsCreatingPresenter()
        {
            var build = new TestGameConstructor().LoadLevel(new EmptyLevel()).Build();

            //var obj = new GameObject();
            //var view = obj.AddComponent<TestView>();
            //Assert.IsTrue(view.IsInited);
            //Assert.IsNotNull(view.Presenter);
            //var presenter = view.Presenter;

            //GameObject.DestroyImmediate(obj);

            //Assert.IsTrue(view.IsDisposed);
            //Assert.IsTrue(presenter.IsDisposed);

            build.Dispose();
        }


        private class TestView : View
        {
            public bool IsInited { get; private set; }
            public TestPresenter Presenter { get; private set; }

            protected override BasePresenter CreatePresenter()
            {
                Presenter = new TestPresenter(this);
                return Presenter;
            }

            protected override void CreatedInner()
            {
                base.CreatedInner();
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

            protected override void DisposeInner()
            {
            }
        }

        */
        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
