using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Environment.Common.Creation
{

    public class TestContainerView : View, IViewContainer
    {
        private List<View> _views = new List<View>();

        public TestContainerView(ILevel level) : base(level)
        {
        }

        protected override void DisposeInner()
        {
            Clear();
        }

        public void Clear()
        {
            foreach (var item in _views.ToList())
                item.Dispose();
            _views.Clear();
        }

        public T Create<T>(TestViewPrefab viewPrefab) where T : View
        {
            if (viewPrefab == null) throw new Exception($"Cant find view");
            return (T)viewPrefab.Create<T>(this);
        }

        public T Create<T>(Func<ILevel, T> creator) where T : View
        {
            var viewPresenter = creator(Level);
            _views.Add(viewPresenter);
            viewPresenter.OnDispose += ViewPresenter_OnDispose;
            return viewPresenter;

            void ViewPresenter_OnDispose()
            {
                viewPresenter.OnDispose -= ViewPresenter_OnDispose;
                _views.Remove(viewPresenter);
            }
        }

        public bool Has<T>()
        {
            return _views.OfType<T>().Any();
        }

        public IReadOnlyCollection<T> Get<T>() where T : View
        {
            return _views.OfType<T>().AsReadOnly();
        }

        public T Add<T>(T view) where T : View
        {
            _views.Add(view);
            return view;
        }

        public T Spawn<T>(IViewPrefab prefab) where T : View
        {
            if (prefab is TestPrototypeView prototypeView)
            {
                return prototypeView.Create<T>(this);
            }
            else if (prefab is TestViewPrefab viewPrefab)
            {
                return Create<T>(viewPrefab);
            }
            throw new Exception("Uknown prefab type: " + prefab);
        }
    }
}
