using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using GameUnity.Assets.Scripts.Unity.Engine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Assets.Scripts.Game.Unity.Views
{
    public abstract class UnityView<TPresenter> : MonoBehaviour, 
        IViewWith<TPresenter>, IInitPresenter,
        IPointerEnterHandler,
        IPointerExitHandler
        where TPresenter : IPresenter
    {
        public event Action OnDispose = delegate { };

        public LevelView Level { get; private set; }
        public TPresenter Presenter { get; private set; }
        public bool IsDisposed { get; private set; }

        protected void Awake()
        {
            Level = ILevelsManager.Default.GetCurrentLevel();
            PreAwake();
            Level.Add(this);
        }

        protected virtual void PreAwake()
        {
        }

        protected void OnDestroy()
        {
            ((UnityControls)IControls.Default).ViewDestroyed(this);
            Level.Remove(this);
            IsDisposed = true;
            OnDispose();
        }

        public void Dispose()
        {
            if (IsDisposed)
                return;

            DestroyImmediate(gameObject);
        }

        void IInitPresenter.SetPresenter(IPresenter presenter)
        {
            Presenter = (TPresenter)presenter;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ((UnityControls)IControls.Default).SetPointerEnter(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ((UnityControls)IControls.Default).SetPointerExit(this);
        }

    }
}