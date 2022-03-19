using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class LevelsManager : ILevelsManager
    {
        private static LevelMain _currentLevel;
        private static GameLevel _model;
        private static IGameEngine _engine;

        public LevelsManager(IGameEngine engine)
        {
            _engine = engine;
        }

        public void Load(string scene, Action<ILevel> onFinished)
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
                var view = GameObject.FindObjectOfType<LevelMain>();
                if (view == null) throw new Exception("Cant find level view in scene");

                onFinished(view);
            }
        }

        public void Unload()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            _currentLevel = null;
            _model = null;
        }


        public void Load(GameLevel model, LevelDefinition prototype, Action<ILevel> onFinished)
        {
            _model = model;
            Load(prototype.SceneName, onFinished);
        }

        public ILevel GetCurrentLevel()
        {
            return GameObject.FindObjectOfType<LevelMain>();
        }

        public static void Init(LevelMain levelMain)
        {
            _currentLevel = levelMain;
            _currentLevel.Model = _model;
            _currentLevel.Engine = _engine;
        }

    }
}
