using UnityEngine;
using System.Threading.Tasks;
using System;

namespace Assets.Scripts.Core.Prototypes
{
    [DisallowMultipleComponent]
    public class LocalPrototypeLink : PrototypeLink
    {
        private bool _itsOriginal = true;

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

        public override void Create<T>(Transform parent, Action<T> result, string name = null, bool active = true)
        {
            var go = Instantiate(gameObject, parent);
            var child = go.GetComponent<LocalPrototypeLink>();
            child._itsOriginal = false;
            if (!string.IsNullOrEmpty(name)) go.name = name;
            go.SetActive(active);
            result(go.GetComponent<T>());
        }

        public override void Create(Transform parent, Action<GameObject> result, string name = null, bool active = true)
        {
            var go = Instantiate(gameObject, parent);
            var child = go.GetComponent<LocalPrototypeLink>();
            child._itsOriginal = false;
            if (!string.IsNullOrEmpty(name)) go.name = name;
            go.SetActive(active);

            result(go);
        }

    }
}
