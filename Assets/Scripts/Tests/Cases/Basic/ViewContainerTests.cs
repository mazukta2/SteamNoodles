using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using Game.Tests.Cases;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class ViewContainerTests
    {
        [Test, Order(TestCore.EssentialOrder)]
        public void IsCollectionDisposeViews()
        {
            var container = new ViewsCollection();
            var view = new ViewMock(container);
            container.Dispose();
            Assert.IsTrue(view.IsDisposed);
        }

        [Test, Order(TestCore.EssentialOrder)]
        public void IsContainerDisposeViews()
        {
            var collection = new ViewsCollection();
            var container = new ContainerViewMock(collection);
            var view = new ViewMock(container);

            collection.Dispose();
            Assert.IsTrue(container.IsDisposed);
            Assert.IsTrue(view.IsDisposed);
        }


        [Test, Order(TestCore.EssentialOrder)]
        public void IsViewDisposeRemovesItFromCollection()
        {
            var collection = new ViewsCollection();
            var view = new ViewMock(collection);
            Assert.AreEqual(1, collection.Views.Count);

            view.Dispose();
            Assert.IsTrue(view.IsDisposed);
            Assert.AreEqual(0, collection.Views.Count);
            collection.Dispose();
        }

        [Test, Order(TestCore.EssentialOrder)]
        public void IsViewDisposeRemovesItFromContrainer()
        {
            var collection = new ViewsCollection();
            var container = new ContainerViewMock(collection);
            var view = new ViewMock(container);
            Assert.AreEqual(1, collection.Views.Count);
            Assert.AreEqual(1, container.Views.Count);

            view.Dispose();
            Assert.IsTrue(view.IsDisposed);
            Assert.AreEqual(1, collection.Views.Count);
            Assert.AreEqual(0, container.Views.Count);
            collection.Dispose();
        }

        private class ViewMock : View
        {
            public ViewMock(IViewsCollection level) : base(level)
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
