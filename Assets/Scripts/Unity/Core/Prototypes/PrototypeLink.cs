using System;
using UnityEngine;

namespace Assets.Scripts.Core.Prototypes
{
    // Incapsulate a gameobject creation logic. Cos game logic does't need to know how exacly object will be created. 
    [DisallowMultipleComponent]
    public abstract class PrototypeLink : MonoBehaviour
    {
        public abstract void ForceInit();

        public GameObject Create(string name = null, bool active = true)
        {
            return Create(transform.parent, name, active);
        }

        public T Create<T>(string name = null, bool active = true) where T : MonoBehaviour
        {
            return Create<T>(transform.parent, name, active);
        }

        public abstract T Create<T>(Transform parent, string name = null, bool active = true) where T : MonoBehaviour;

        public abstract GameObject Create(Transform parent, string name = null, bool active = true);

        public abstract void DestroySpawned();

        protected static T CreatePrefab<T>(GameObject prefab, Transform parent,
            string name = null, bool active = true) where T : MonoBehaviour
        {
            var go = CreatePrefab(prefab, parent, name, active);

            var component = go.GetComponent<T>();
            if (component == null)
                throw new Exception($"Component {typeof(T).Name} doesn't exist in prototype root {go.name}");

            return component;
        }

        protected static GameObject CreatePrefab(GameObject prefab, Transform parent,
            string name = null, bool active = true)
        {
            var go = Instantiate(prefab, parent);

            if (string.IsNullOrEmpty(name))
                go.name = prefab.name;
            else
                go.name = name;

            go.SetActive(active);

            return go;
        }
    }
}
