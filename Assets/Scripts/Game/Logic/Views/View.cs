using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using System;
#if UNITY
    using UnityEngine;
#endif

namespace Game.Assets.Scripts.Game.Unity.Views
{
    public abstract class View<TViewPresenter>
#if UNITY
        : MonoBehaviour, IDisposable where TViewPresenter : ViewPresenter
#else
        : Disposable where TViewPresenter : ViewPresenter
#endif
    {
#if UNITY
        public bool IsDisposed { get; private set; }
        public event Action OnDispose = delegate { };

        public abstract TViewPresenter GetViewPresenter();

        protected void Awake()
        {
            Level = CoreAccessPoint.Core.Engine.Levels.GetCurrentLevel();
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
        public void Awake(Tests.Environment.LevelInTests level)
        {
            Level = level;
            CreatedInner();
        }
#endif

        public ILevel Level { get; private set; }
        protected virtual void CreatedInner() { }
    }
}