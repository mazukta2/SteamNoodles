using UnityEngine;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.Core.Prototypes
{
    [DisallowMultipleComponent]
    public class LocalPrototypeLink : PrototypeLink
    {
        private bool _itsOriginal = true;
        private List<GameObject> _spawned = new List<GameObject>();

        public override void ForceInit()
        {
            Awake();
        }

        protected void Awake()
        {
            if (!_itsOriginal)
                Destroy(this);
            else
                gameObject.SetActive(false);
        }


        public override T Create<T>(Transform parent, string name = null, bool active = true)
        {
            var go = Instantiate(gameObject, parent);
            var child = go.GetComponent<LocalPrototypeLink>();
            child._itsOriginal = false;
            if (!string.IsNullOrEmpty(name)) go.name = name;
            _spawned.Add(go);
            go.SetActive(active);
            return go.GetComponent<T>();
        }

        public override GameObject Create(Transform parent, string name = null, bool active = true)
        {
            var go = Instantiate(gameObject, parent);
            var child = go.GetComponent<LocalPrototypeLink>();
            child._itsOriginal = false;
            if (!string.IsNullOrEmpty(name)) go.name = name;
            _spawned.Add(go);
            go.SetActive(active);
            return go;
        }

        public override void DestroySpawned()
        {
            foreach (var item in _spawned)
            {
                Destroy(item);
            }

            _spawned.Clear();
        }
    }
}
