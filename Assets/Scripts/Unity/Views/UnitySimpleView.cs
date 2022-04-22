﻿using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views
{
    public abstract class UnitySimpleView : MonoBehaviour, IView
    {
        public event Action OnDispose = delegate { };

        public LevelView Level { get; private set; }
        public bool IsDisposed { get; private set; }

        protected void Awake()
        {
            Level = ILevelsManager.Default.GetCurrentLevel();
            Level.Add(this);
        }

        protected void OnDestroy()
        {
            Level.Remove(this);
            IsDisposed = true;
            OnDispose();
        }

        public void Dispose()
        {
            DestroyImmediate(gameObject);
        }

    }
}