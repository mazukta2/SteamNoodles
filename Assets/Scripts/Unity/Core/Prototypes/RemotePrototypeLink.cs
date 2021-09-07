using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Prototypes
{
    [DisallowMultipleComponent]
    public class RemotePrototypeLink : PrototypeLink
    {
        [SerializeField] GameObject _prefab;

        private bool _isInited;
        private List<GameObject> _spawned = new List<GameObject>();

        public override void Create<T>(Transform parent, Action<T> result, string name = null, bool active = true)
        {
            var t = CreatePrefab<T>(_prefab, parent, name, active);
            _spawned.Add(t.gameObject);
            result(t);
        }

        public override void Create(Transform parent, Action<GameObject> result, string name = null, bool active = true)
        {
            var t = CreatePrefab(_prefab, parent, name, active);
            _spawned.Add(t);
            result(t);
        }

        protected void LazyInit()
        {
            if (_isInited)
                return;

            _isInited = true;
        }

        protected void OnDestroy()
        {
        }

        public override void ForceInit()
        {
            LazyInit();
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
