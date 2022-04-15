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
        private List<GameObject> _spawnedGo = new List<GameObject>();

        public TView Spawn<TView>(IViewPrefab prefab) where TView : class, IView
        {
            if (prefab is GamePrefab screenPrefab)
            {
                var go = GameObject.Instantiate(screenPrefab.GameObject, _pointer);
                var view = go.GetComponent<TView>();
                if (view == null)
                    throw new System.Exception("Cant find view preseneter " + typeof(TView).Name);
                _spawned.Add(view);
                return view;
            }

            if (prefab is PrototypeUnityView prototype)
            {
                var go = GameObject.Instantiate(prototype.gameObject, _pointer);
                go.GetComponent<PrototypeUnityView>().SetOriginal(false);
                go.SetActive(true);
                var view = go.GetComponent<TView>();
                if (view == null)
                    throw new Exception("Cant find view " + typeof(TView).Name);
                _spawned.Add(view);
                return view;
            }

            throw new Exception("Unknown prefab: " + prefab);
        }

        public void Spawn(IViewPrefab prefab)
        {
            if (prefab is GamePrefab screenPrefab)
            {
                var go = GameObject.Instantiate(screenPrefab.GameObject, _pointer);
                _spawnedGo.Add(go);
                return;
            }

            if (prefab is PrototypeUnityView prototype)
            {
                var go = GameObject.Instantiate(prototype.gameObject, _pointer);
                go.GetComponent<PrototypeUnityView>().SetOriginal(false);
                go.SetActive(true);
                _spawnedGo.Add(go);
                return;
            }

            throw new Exception("Unknown prefab: " + prefab);
        }

        public void Clear()
        {
            foreach (var item in _spawned)
                item.Dispose();

            foreach (var item in _spawnedGo)
                Destroy(item);

            _spawned.Clear();
            _spawnedGo.Clear();
        }

    }
}
