using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core.Prototypes
{
    [DisallowMultipleComponent]
    public class RemotePrototypeLink : PrototypeLink
    {
        [SerializeField] GameObject _prefab;

        private List<Action> _requests = new List<Action>();
        private bool _isInited;

        public override void Create<T>(Transform parent, Action<T> result, string name = null, bool active = true)
        {
            result(CreatePrefab<T>(_prefab, parent, name, active));
        }

        public override void Create(Transform parent, Action<GameObject> result, string name = null, bool active = true)
        {
            result(CreatePrefab(_prefab, parent, name, active));
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
    }
}
