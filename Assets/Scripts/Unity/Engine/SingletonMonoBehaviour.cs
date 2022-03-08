using UnityEngine;
using System.Collections;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
    {
        private static T _instance;

        public static T Instance
        {
            get => _instance;
            set
            {
                if (_instance != null)
                    throw new System.Exception("Singleton can be initialize only once");

                _instance = value;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
                _instance = (T)this;
        }

        protected virtual void OnDestroy()
        {
            _instance = null;
        }
    }
}
