using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Data;
using Assets.Scripts.Views.Levels;
using Game.Assets.Scripts.Game.Logic.Controllers;
using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using GameUnity.Assets.Scripts.Unity.Data.Levels;
using GameUnity.Assets.Scripts.Unity.Settings;
using GameUnity.Assets.Scripts.Unity.Settings.Data;
using GameUnity.Assets.Scripts.Unity.Views;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameUnity.Assets.Scripts.Unity.Core
{
    public class Game : ViewMonoBehaviour, IGameController, ILevelsController
    {
        [SerializeField] PrototypeLink _view;

        public ILevelsController Levels => this;
        public ISettingsController Settings => _settings;
        public IAssetsController Assets => _assets;

        private GameSettings _settings;
        private AssetsLoader _assets;
        private GameModel _model;
        private GamePresenter _presenter;
        private Action<float> _moveTime;

        protected void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _settings = new GameSettings();
            _assets = new AssetsLoader();
            _model = new GameModel(this);
            _presenter = new GamePresenter(_model, _view.Create<GameView>());
            _model.CreateSession();

            _model.Session.LoadLevel(_settings.Get<MainSettings>().StartLevel);
        }

        protected void OnApplicationQuit()
        {
            _presenter.Dispose();
            _model.Dispose();
        }

        protected void OnDestroy()
        {
            if (!_model.IsDisposed)
                throw new Exception("Model is not disposed for some reasons");
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
            _moveTime?.Invoke(Time.deltaTime);
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
                var view = FindObjectOfType<LevelView>();
                if (view == null) throw new Exception("Cant find level view in scene");

                onFinished();
            }
        }
    }
}