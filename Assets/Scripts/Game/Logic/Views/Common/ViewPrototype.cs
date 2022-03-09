using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
#if UNITY
using UnityEngine;
#endif

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class ViewPrototype : View<PrototypeViewPresenter>
    {
#if UNITY
        private bool _itsOriginal = true;
        public void SetOriginal(bool v)
        {
            _itsOriginal = v;
        }
#endif

        public PrototypeViewPresenter ViewPresenter { get; private set; }
        public override PrototypeViewPresenter GetViewPresenter() => ViewPresenter;

        protected override void CreatedInner()
        {
#if UNITY
            ViewPresenter = new PrototypeViewPresenter(Level, gameObject);
            if (!_itsOriginal)
                Destroy(this);
            else
                gameObject.SetActive(false);
#else
            ViewPresenter = new PrototypeViewPresenter(Level);
#endif
        }

        protected override void DisposeInner()
        {
            ViewPresenter.Dispose();
        }

    }

    public class PrototypeViewPresenter : ViewPresenter
    {
        private IPrefab _prefab;

#if UNITY
        private GameObject _gameObject;
        public PrototypeViewPresenter(ILevel level, GameObject gameObject) : base(level)
        {
            _gameObject = gameObject;
        }
#endif
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
#if UNITY
            var go = GameObject.Instantiate(_gameObject, viewContainer.GetPointer());
            go.GetComponent<ViewPrototype>().SetOriginal(false);
            go.SetActive(true);
            var view = go.GetComponent<View<T>>();
            if (view == null)
                throw new Exception("Cant find view preseneter " + typeof(T).Name);
            return view.GetViewPresenter();
#else
            var prefab = (ViewPrefab<T>)_prefab;
            return prefab.Create(viewContainer);
#endif
        }
    }
}
