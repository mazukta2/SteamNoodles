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
        protected ILevel Level { get; private set; }

        private bool _isAwake = false;

        private TView _view;

        protected void ForceAwake()
        {
            Awake();
        }

        protected void Awake()
        {
            if (_isAwake)
                return;

            Level = CoreAccessPoint.Core.Engine.Levels.GetCurrentLevel();

            _view = CreateView();
            _view.OnDispose += Dispose;
            CreatedInner();
            _isAwake = true;
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            IsDisposed = true;
            _view.OnDispose -= Dispose;
            _view.Dispose();
            DisposeInner();
            Destroy(gameObject);
        }

        public TView View
        {
            get
            {
                if (_view == null)
                    ForceAwake();

                return _view;
            }
        }

        protected abstract TView CreateView();

        protected virtual void CreatedInner() { }
        protected virtual void DisposeInner() { }
    }
}