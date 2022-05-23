using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using System;
using System.Collections.Generic;
using UnityEngine;
using static GameUnity.Assets.Scripts.Unity.Engine.AssetsLoader;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public class ContainerUnityView : UnitySimpleView, IViewContainer
    {
        [SerializeField] Transform _pointer;

        private ViewsCollection _viewsCollection = new ViewsCollection();
        private List<GameObject> _spawnedGo = new List<GameObject>();

        public TView Spawn<TView>(IViewPrefab prefab) where TView : class, IView
        {
            if (prefab is GamePrefab screenPrefab)
            {
                var go = GameObject.Instantiate(screenPrefab.GameObject, _pointer);
                var view = go.GetComponent<TView>();
                if (view == null)
                    throw new System.Exception("Cant find view preseneter " + typeof(TView).Name);
                _viewsCollection.Add(view);
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
                _viewsCollection.Add(view);
                return view;
            }

            throw new Exception("Unknown prefab: " + prefab);
        }


        public void Spawn(IViewPrefab prefab, GameVector3 position)
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
                go.transform.position = position.ToVector();
                go.SetActive(true);
                _spawnedGo.Add(go);
                return;
            }

            throw new Exception("Unknown prefab: " + prefab);
        }

        public void Spawn(IViewPrefab prefab)
        {
            Spawn(prefab, transform.position.ToVector());
        }

        public void Clear()
        {
            _viewsCollection.Clear();

            foreach (var item in _spawnedGo)
                Destroy(item);

            _spawnedGo.Clear();
        }

        public T FindView<T>(bool recursively = true) where T : IView
        {
            return _viewsCollection.FindView<T>(recursively);
        }

        public IReadOnlyCollection<T> FindViews<T>(bool recursively = true) where T : IView
        {
            return _viewsCollection.FindViews<T>(recursively);
        }

        public void Remove(IView view)
        {
            _viewsCollection.Add(view);
        }

        public void Add(IView view)
        {
            _viewsCollection.Remove(view);
        }
    }
}
