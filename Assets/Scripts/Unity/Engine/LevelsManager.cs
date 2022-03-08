using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class LevelsManager : ILevelsManager
    {
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
        }

        public void Load(ILevelDefinition prototype, Action<ILevel> onFinished)
        {
            Load(prototype.SceneName, onFinished);
        }

        public ILevel GetCurrentLevel()
        {
            return GameObject.FindObjectOfType<LevelMain>();
        }

    }
}
