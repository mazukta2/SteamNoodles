﻿using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
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

        protected override PrototypeView CreateView()
        {
            return new PrototypeView(Level, new GameObjectViewPrefab(gameObject));
        }

        protected override void AfterAwake()
        {
            if (!_itsOriginal)
                DestroyThisCompoenent();
            else
                gameObject.SetActive(false);
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
                return conteiner.Create(Creator);

                T Creator(ILevel level)
                {
                    var go = GameObject.Instantiate(_gameObject, unityContainer.GetPointer());
                    go.GetComponent<PrototypeUnityView>().SetOriginal(false);
                    go.SetActive(true);
                    var view = go.GetComponent<UnityView<T>>();
                    if (view == null)
                        throw new Exception("Cant find view " + typeof(T).Name);
                    return (T)view.View;
                }
            }
        }
    }
}