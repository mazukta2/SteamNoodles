using Game.Assets.Scripts.Game.Logic.Common.Core;
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
        private List<View> _views = new List<View>();

        public void Clear()
        {
            foreach (var item in _views)
                item.Dispose();
            _views.Clear();
        }

        public T Create<T>(Func<Tests.Environment.LevelInTests, T> creator) where T : View
        {
            var view = creator(Level);
            _views.Add(view);
            return view;
        }

        public bool Has<T>()
        {
            return _views.OfType<T>().Any();
        }

        protected override void CreatedInner()
        {
        }

        protected override void DisposeInner()
        {
            Clear();
        }

        public IReadOnlyCollection<T> Get<T>()
        {
            return _views.OfType<T>().AsReadOnly();
        }

#endif
    }
}
