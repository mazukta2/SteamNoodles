using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class LevelsManager : ILevelsManager
    {
        private static LevelController _currentLevel;
        private static GameLevel _model;

        public IViewsCollection Collection => _currentLevel.Collection;

        public LevelsManager()
        {
        }

        public void Load(string scene, Action<IViewsCollection> onFinished)
        {
            if (_currentLevel != null)
                throw new Exception("Loading before unloading");

            _currentLevel = new LevelController(_model.Definition);
            ICurrentLevel.Default = _model;

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
                _currentLevel.Initialize();
                onFinished(_currentLevel.Collection);
                _currentLevel.Start();
            }
        }

        public void Unload()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            _currentLevel.Dispose();
            _currentLevel = null;
            _model = null;
            ICurrentLevel.Default = null;
        }


        public void Load(GameLevel model, LevelDefinition prototype, Action<IViewsCollection> onFinished)
        {
            _model = model;
            Load(prototype.SceneName, onFinished);
        }

    }
}
