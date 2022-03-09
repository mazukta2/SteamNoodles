using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class LevelMain : MonoBehaviour, ILevel
    {
        public GameLevel Model { get; set; }

        private List<ViewPresenter> _list = new List<ViewPresenter>();

        public event Action OnDispose;

        public void Add(ViewPresenter viewPresenter)
        {
            _list.Add(viewPresenter);
        }
        public void Remove(ViewPresenter viewPresenter)
        {
            _list.Remove(viewPresenter);
        }

        protected void Awake()
        {
            LevelsManager.Init(this);
        }

        protected void OnDestroy()
        {
            Dispose();
        }

        public void Dispose()
        {
            OnDispose();
        }
    }
}