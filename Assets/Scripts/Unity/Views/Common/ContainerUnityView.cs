using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GameUnity.Assets.Scripts.Unity.Engine.AssetsLoader;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class ContainerUnityView : MonoBehaviour, IViewContainer
    {
        [SerializeField] Transform _pointer;

        private List<IView> _spawned = new List<IView>();

        public T Spawn<T>(IViewPrefab prefab) where T : class, IView
        {
            if (prefab is ScreenPrefab screenPrefab)
            {
                var go = GameObject.Instantiate(screenPrefab.GameObject, _pointer);
                var view = go.GetComponent<UnityView<T>>();
                if (view == null)
                    throw new System.Exception("Cant find view preseneter " + typeof(UnityView<T>).Name);
                _spawned.Add(view.View);
                return view.View;
            }

            if (prefab is PrototypeUnityView prototype)
            {
                var go = GameObject.Instantiate(prototype.gameObject, _pointer);
                go.GetComponent<PrototypeUnityView>().SetOriginal(false);
                go.SetActive(true);
                var view = go.GetComponent<UnityView<T>>();
                if (view == null)
                    throw new Exception("Cant find view " + typeof(T).Name);
                _spawned.Add(view.View);
                return (T)view.View;
            }

            throw new Exception("Unknown prefab: " + prefab);
        }

        public void Clear()
        {
            foreach (var item in _spawned)
                item.Dispose();

            _spawned.Clear();
        }

    }
}
