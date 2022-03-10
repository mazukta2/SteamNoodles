using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY
using UnityEngine;
#endif

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class ViewContainer : View<ContainerViewPresenter>
    {
#if UNITY
        [SerializeField] Transform _pointer;
#endif
        public ContainerViewPresenter ViewPresenter { get; private set; }
        public override ContainerViewPresenter GetViewPresenter() => ViewPresenter;

        protected override void CreatedInner()
        {
#if UNITY
            ViewPresenter = new ContainerViewPresenter(Level, _pointer);
#else
            ViewPresenter = new ContainerViewPresenter(Level);
#endif
        }

        protected override void DisposeInner()
        {
            ViewPresenter.Dispose();
        }
    }

    // Container is keeping spawned elements
    public class ContainerViewPresenter : ViewPresenter
    {
        private List<ViewPresenter> _views = new List<ViewPresenter>();

#if UNITY
        private Transform _pointer;
        public ContainerViewPresenter(ILevel level, Transform pointer) : base(level)
        {
            _pointer = pointer;
        }
#else
        public ContainerViewPresenter(ILevel level) : base(level)
        {
        }
#endif

        protected override void DisposeInner()
        {
            Clear();
        }

        public void Clear()
        {
            foreach (var item in _views)
                item.Dispose();
            _views.Clear();

#if UNITY
            foreach (Transform item in _pointer)
            {
                GameObject.Destroy(item.gameObject);
            }
#endif
        }

        public T Create<T>(Func<ILevel, T> creator) where T : ViewPresenter
        {
            var viewPresenter = creator(Level);
            _views.Add(viewPresenter);
            return viewPresenter;
        }

        public bool Has<T>()
        {
            return _views.OfType<T>().Any();
        }

        public IReadOnlyCollection<T> Get<T>() where T : ViewPresenter
        {
            return _views.OfType<T>().AsReadOnly();
        }

#if UNITY
        public Transform GetPointer()
        {
            return _pointer;
        }
#endif
    }
}
