using Assets.Scripts.Data;
using Assets.Scripts.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using GameUnity.Assets.Scripts.Unity.Data;
using GameUnity.Assets.Scripts.Unity.Data.Game;
using GameUnity.Assets.Scripts.Unity.Data.Levels;
using GameUnity.Assets.Scripts.Unity.Views;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Core
{
    public class Game : ViewMonoBehaviour, IGameController, ILevelsController
    {
        [SerializeField] GameView _view;

        public ILevelsController Levels => this;

        private GameModel _model;
        private GamePresenter _presenter;
        private Action<float> _moveTime;
        private MainSettings _settings;

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _model = new GameModel(this);
            _presenter = new GamePresenter(_model, _view);
            _settings = GameSettings.Get<MainSettings>();

            _model.CreateSession();
            _model.Session.LoadLevel(_settings.StartLevel);
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
            Load(prototype.SceneName, onFinished);
        }

        protected void Update()
        {
            _moveTime(Time.deltaTime);
        }

        public void Load(string scene, Action onFinished)
        {
            var loading = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
            if (loading.isDone)
                Finish();
            else
                loading.completed += Complited;

            void Complited(AsyncOperation operation)
            {
                loading.completed -= Complited;
                Finish();
            }

            void Finish()
            {
                var view = UnityEngine.Object.FindObjectOfType<LevelView>();
                if (view == null) throw new Exception("Cant find level view in scene");

                onFinished();
            }
        }
    }
}