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
    public class ViewContainer : View
    {
#if UNITY
        [SerializeField] Transform _pointer;

        protected override void CreatedInner()
        {
        }

        protected override void DisposeInner()
        {
        }

        public void Clear()
        {
            foreach (Transform item in _pointer)
            {
                GameObject.Destroy(item.gameObject);
            }
        }

        public Transform GetPointer()
        {
            return _pointer;
        }

#else
        public ContainerViewPresenter ViewPresenter { get; private set; }

        protected override void CreatedInner()
        {
            ViewPresenter = new ContainerViewPresenter(Level);
        }

        protected override void DisposeInner()
        {
            ViewPresenter.Dispose();
        }
#endif
    }

    // Container is keeping spawned elements
    public class ContainerViewPresenter : ViewPresenter
    {
        private List<ViewPresenter> _views = new List<ViewPresenter>();

        public ContainerViewPresenter(ILevel level) : base(level)
        {
        }

        protected override void DisposeInner()
        {
            Clear();
        }

        public void Clear()
        {
            foreach (var item in _views)
                item.Dispose();
            _views.Clear();
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
    }
}
