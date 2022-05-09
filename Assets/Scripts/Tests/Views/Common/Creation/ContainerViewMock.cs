using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Views.Common.Creation
{

    public class ContainerViewMock : View, IViewContainer
    {
        private List<IView> _views = new List<IView>();

        public ContainerViewMock(ILevelView level) : base(level)
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

        public T Create<T>(ViewPrefabMock viewPrefab) where T : IView
        {
            if (viewPrefab == null) throw new Exception($"Cant find view");
            return (T)viewPrefab.CreateInContainer<T>(this);
        }

        public T Create<T>(Func<ILevelView, T> creator) where T : IView
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

        public T Spawn<T>(IViewPrefab prefab) where T : class, IView
        {
            if (prefab is PrototypeViewMock prototypeView)
            {
                return prototypeView.Create<T>(this);
            }
            else if (prefab is ViewPrefabMock viewPrefab)
            {
                return Create<T>(viewPrefab);
            }
            throw new Exception("Uknown prefab type: " + prefab);
        }

        public void Spawn(IViewPrefab prefab)
        {
            throw new NotImplementedException();
        }
    }
}
