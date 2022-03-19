using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Views;
using System;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views
{
    public abstract class UnityView<TView> : MonoBehaviour where TView : View
    {
        protected ILevel Level { get; private set; }

        private bool _isInited = false;
        private bool _isAwaked;

        private TView _view;

        protected void ForceInit()
        {
            Init();
        }

        protected void Awake()
        {
            _isAwaked = true;
            Init();
        }

        protected void Init()
        {
            if (_isInited)
                return;
            
            Level = CoreAccessPoint.Core.Engine.Levels.GetCurrentLevel();

            BeforeAwake();

            _view = CreateView();
            _view.OnDispose += DisposedByView;

            _isInited = true;

            AfterAwake();
        }

        protected virtual void BeforeAwake()
        {
        }

        protected virtual void AfterAwake()
        {
        }

        protected void OnApplicationQuit()
        {
            if (_view != null && !_view.IsDisposed)
            {
                OnDisposeView(_view);
                _view.OnDispose -= DisposedByView;
                _view.Dispose();
                _view = null;
            }
        }

        protected void OnDestroy()
        {
            if (_view != null && !_view.IsDisposed)
            {
                OnDisposeView(_view);
                _view.OnDispose -= DisposedByView;
                _view.Dispose();
                _view = null;
            }
            AfterDestroy();
        }

        protected virtual void OnDisposeView(TView view)
        {
        }

        protected virtual void AfterDestroy()
        {
        }

        protected void DestroyThisCompoenent()
        {
            if (_view != null && !_view.IsDisposed)
            {
                _view.OnDispose -= DisposedByView;
                _view.Dispose();
                _view = null;
            }

            Destroy(this);
        }


        private void DisposedByView()
        {
            OnDisposeView(_view);
            _view.OnDispose -= DisposedByView;
            _view = null;
            if (_isAwaked)
                Destroy(gameObject);
        }

        public TView View
        {
            get
            {
                if (_view == null)
                    ForceInit();

                return _view;
            }
        }

        protected abstract TView CreateView();
    }
}