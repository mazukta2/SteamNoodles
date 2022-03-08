using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Tests.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
#if UNITY
using UnityEngine;
#endif

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class ViewPrototype : View
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

        private Func<LevelInTests, View> _creator;
        public void SetCreator(Func<LevelInTests, View> action)
        {
            _creator = action;
        }

        public T Create<T>(ViewContainer viewContainer) where T : View
        {
            return viewContainer.Create(Create);

            T Create(LevelInTests level)
            {
                return (T)_creator(level);
            }
        }
#endif
    }
}
