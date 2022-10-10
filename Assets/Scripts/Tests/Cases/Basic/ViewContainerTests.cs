using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Views;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class ViewContainerTests
    {
        [Test]
        public void IsCollectionDisposeViews()
        {
            var container = new DefaultViews();
            var view = new ViewMock(container);
            container.Dispose();
            Assert.IsTrue(view.IsDisposed);
        }

        [Test]
        public void IsContainerDisposeViews()
        {
            var collection = new DefaultViews();
            var container = new ContainerViewMock(collection);
            var view = new ViewMock(container);

            collection.Dispose();
            Assert.IsTrue(container.IsDisposed);
            Assert.IsTrue(view.IsDisposed);
        }


        [Test]
        public void IsViewDisposeRemovesItFromCollection()
        {
            var collection = new DefaultViews();
            var view = new ViewMock(collection);
            Assert.AreEqual(1, collection.Views.Count);

            view.Dispose();
            Assert.IsTrue(view.IsDisposed);
            Assert.AreEqual(0, collection.Views.Count);
            collection.Dispose();
        }

        [Test]
        public void IsViewDisposeRemovesItFromContrainer()
        {
            var collection = new DefaultViews();
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
            public ViewMock(IViews level) : base(level)
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
