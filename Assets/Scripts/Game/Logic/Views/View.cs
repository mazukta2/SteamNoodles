using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Tests.Environment;
using System;
#if UNITY
    using UnityEngine;
#endif

namespace Game.Assets.Scripts.Game.Unity.Views
{
    public abstract class View 
#if UNITY
        : MonoBehaviour, IDisposable
#else
        : Disposable
#endif
    {
#if UNITY
        public bool IsDisposed { get; private set; }
        public event Action OnDispose = delegate { };

        protected void Awake()
        {
            CreatedInner();
        }
        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;
            OnDispose();
            DisposeInner();
            Destroy(gameObject);
        }

        protected virtual void DisposeInner() { }
#else
        public LevelInTests Level { get; private set; }
        public void Awake(LevelInTests level)
        {
            Level = level;
            CreatedInner();
        }
#endif

        protected virtual void CreatedInner() { }
    }
}