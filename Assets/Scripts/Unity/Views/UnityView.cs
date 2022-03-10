using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views;
using System;
#if UNITY
    using UnityEngine;
#endif

namespace Game.Assets.Scripts.Game.Unity.Views
{
    public abstract class UnityView<TView> : MonoBehaviour, IDisposable where TView : View
    {
        public bool IsDisposed { get; private set; }
        public event Action OnDispose = delegate { };

        private bool _isAwake = false;

        public void ForceAwake()
        {
            Awake();
        }

        protected void Awake()
        {
            if (_isAwake)
                return;

            Level = CoreAccessPoint.Core.Engine.Levels.GetCurrentLevel();
            CreatedInner();
            _isAwake = true;
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

        public abstract TView GetView();
        public ILevel Level { get; private set; }

        protected virtual void CreatedInner() { }
        protected virtual void DisposeInner() { }
    }
}