using System;
using UnityEngine;

namespace Assets.Scripts.Core.Prototypes
{
    // Incapsulate a gameobject creation logic. Cos game logic does't need to know how exacly object will be created. 
    [DisallowMultipleComponent]
    public abstract class PrototypeLink : MonoBehaviour
    {
        public abstract void ForceInit();

        public void Create(Action<GameObject> result = null, string name = null, bool active = true)
        {
            if (result == null) result = (g) => { };

            Create(transform.parent, result, name, active);
        }

        public void Create<T>(Action<T> result = null, string name = null, bool active = true) where T : MonoBehaviour
        {
            if (result == null) result = (t) => { };

            Create(transform.parent, result, name, active);
        }

        public abstract void Create<T>(Transform parent, Action<T> result, string name = null, bool active = true) where T : MonoBehaviour;

        public abstract void Create(Transform parent, Action<GameObject> result, string name = null, bool active = true);

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
