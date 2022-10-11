using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Infrastructure.Mapping;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Views;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class ViewPresenterMapperTests
    {

        [Test]
        public void IsMapperCreatesAndDisposesPresenter()
        {
            var views = new DefaultViews();
            var mapper = new ViewPresenterMapping(views);
            mapper.Map<TestView, TestPresenter>();

            Assert.AreEqual(0, TestPresenter.ListOfPresenters.Count);

            var view = new TestView(views);

            mapper.Init();

            Assert.AreEqual(1, TestPresenter.ListOfPresenters.Count);

            view.Dispose();

            Assert.AreEqual(0, TestPresenter.ListOfPresenters.Count);

            views.Dispose();
        }


        [Test]
        public void IsPresenterDisposalRemovesView()
        {
            var views = new DefaultViews();
            var mapper = new ViewPresenterMapping(views);
            mapper.Map<TestView, TestPresenter>();

            Assert.AreEqual(0, views.FindViews<IView>().Count);

            var view = new TestView(views);

            Assert.AreEqual(1, views.FindViews<IView>().Count);

            mapper.Init();

            TestPresenter.ListOfPresenters.First().Dispose();

            Assert.AreEqual(0, TestPresenter.ListOfPresenters.Count);
            Assert.AreEqual(0, views.FindViews<IView>().Count);

            views.Dispose();

        }
        private class TestView : View
        {

            public TestView(IViews level) : base(level)
            {
            }

            protected override void DisposeInner()
            {
            }
        }

        private class TestPresenter : Disposable, IPresenter
        {
            public static List<TestPresenter> ListOfPresenters = new List<TestPresenter>();

            public TestPresenter(TestView view)
            {
                ListOfPresenters.Add(this);
            }

            protected override void DisposeInner()
            {
                ListOfPresenters.Remove(this);
            }
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
