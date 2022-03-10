using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class PrototypeUnityView : UnityView<PrototypeView>
    {
        private bool _itsOriginal = true;
        public void SetOriginal(bool v)
        {
            _itsOriginal = v;
        }

        public PrototypeView ViewPresenter { get; private set; }
        public override PrototypeView GetView() => ViewPresenter;

        protected override void CreatedInner()
        {
            ViewPresenter = new PrototypeView(Level, new GameObjectViewPrefab(gameObject));

            if (!_itsOriginal)
                Destroy(this);
            else
                gameObject.SetActive(false);
        }

        protected override void DisposeInner()
        {
            ViewPresenter.Dispose();
        }

        private class GameObjectViewPrefab : ViewPrefab
        {
            private GameObject _gameObject;

            public GameObjectViewPrefab(GameObject gameObject)
            {
                _gameObject = gameObject;
            }

            public override object Create<T>(ContainerView conteiner) 
            {
                var unityContainer = ContainerUnityView.Find(conteiner);
                var go = GameObject.Instantiate(_gameObject, unityContainer.GetPointer());
                go.GetComponent<PrototypeUnityView>().SetOriginal(false);
                go.SetActive(true);
                var view = go.GetComponent<UnityView<T>>();
                if (view == null)
                    throw new Exception("Cant find view preseneter " + typeof(T).Name);

                return (T)view.GetView();
            }
        }
    }
}
