using Assets.Scripts.Data;
using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using GameUnity.Assets.Scripts.Unity.Views;
using System;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class Game : ViewMonoBehaviour, IGameController, ILevelsController
    {
        [SerializeField] GameSessionData _gameSessionData;
        [SerializeField] GameView _view;

        public ILevelsController Levels => this;

        private GameModel _model;
        private GamePresenter _presenter;
        private Action<float> _moveTime;
        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _model = new GameModel(this);
            _presenter = new GamePresenter(_model, _view);

            _model.CreateSession();
            _model.Session.LoadLevel(_gameSessionData.StartLevel);
        }

        protected void OnDestroy()
        {
            _presenter.Dispose();
            _model.Dispose();
        }

        public void SetTimeMover(Action<float> moveTime)
        {
            _moveTime = moveTime;
        }

        public void Load(ILevelSettings prototype, Action onFinished)
        {
            ((GameLevelData)prototype).Load(onFinished);
        }

        protected void Update()
        {
            _moveTime(Time.deltaTime);
        }
    }
}