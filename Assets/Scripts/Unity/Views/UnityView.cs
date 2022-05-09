using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using GameUnity.Assets.Scripts.Unity.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Assets.Scripts.Game.Unity.Views
{
    public abstract class UnityView<TPresenter> : MonoBehaviour, 
        IViewWithGenericPresenter<TPresenter>,
        IPointerEnterHandler,
        IPointerExitHandler
        where TPresenter : IPresenter
    {
        public event Action OnDispose = delegate { };
        public bool IsHighlihgted { get; private set; }
        public event Action OnHighlihgtedEnter = delegate { };
        public event Action OnHighlihgtedExit = delegate { };

        public LevelView Level { get; private set; }
        public TPresenter Presenter { get; set; }
        public bool IsDisposed { get; private set; }
        IPresenter IViewWithPresenter.Presenter { get => Presenter; set => Presenter = (TPresenter)value; }

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

        public void OnPointerEnter(PointerEventData eventData)
        {
            IsHighlihgted = true;
            OnHighlihgtedEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsHighlihgted = false;
            OnHighlihgtedExit();
        }
    }

}