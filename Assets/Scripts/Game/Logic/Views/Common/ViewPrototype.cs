using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Tests.Environment;
using System;
#if UNITY
using UnityEngine;
#endif

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class ViewPrototype : View
    {
#if UNITY
        private bool _itsOriginal = true;
        protected override void CreatedInner()
        {
            if (!_itsOriginal)
                Destroy(this);
            else
                gameObject.SetActive(false);
        }

        protected override void DisposeInner()
        {
        }

        public T Create<T>(ViewContainer viewContainer) where T : View
        {
            var go = GameObject.Instantiate(gameObject, viewContainer.GetPointer());
            go.GetComponent<ViewPrototype>()._itsOriginal = false;
            go.SetActive(true);
            return go.GetComponent<T>();
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
