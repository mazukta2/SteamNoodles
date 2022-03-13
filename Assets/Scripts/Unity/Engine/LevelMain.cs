using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class LevelMain : MonoBehaviour, ILevel
    {
        public GameLevel Model { get; set; }

        public Services Services { get; } = new Services();

        private List<View> _list = new List<View>();

        private bool _isDisposed;
        public event Action OnDispose;

        public void Add(View viewPresenter)
        {
            _list.Add(viewPresenter);
        }

        public void Remove(View viewPresenter)
        {
            _list.Remove(viewPresenter);
        }

        protected void Awake()
        {
            LevelsManager.Init(this);
        }

        protected void OnApplicationQuit()
        {
            Dispose();
        }

        protected void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_isDisposed)
                return;

            _isDisposed = true;
            OnDispose();
        }
    }
}