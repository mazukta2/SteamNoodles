using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
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

        public PrototypeViewPresenter ViewPresenter { get; private set; }

        protected override void CreatedInner()
        {
            ViewPresenter = new PrototypeViewPresenter(Level);
        }

        protected override void DisposeInner()
        {
            ViewPresenter.Dispose();
        }
#endif
    }

    public class PrototypeViewPresenter : ViewPresenter
    {
        private IPrefab _prefab;

        public PrototypeViewPresenter(ILevel level) : base(level)
        {
        }

        public PrototypeViewPresenter(ILevel level, IPrefab prefab) : this(level)
        {
            _prefab = prefab;
        }

        public void Set(IPrefab prefab)
        {
            _prefab = prefab;
        }
        
        public T Create<T>(ContainerViewPresenter viewContainer) where T : ViewPresenter
        {
            var prefab = (ViewPrefab<T>)_prefab;
            return prefab.Create(viewContainer);
        }
    }
}
