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
        public event Action OnLoadFinished = delegate { };
        public static IViewsCollection Collection { get; private set; }

        public LevelsManager()
        {
        }

        public void Load(LevelDefinition prototype, IViewsCollection views)
        {
            Load(prototype.SceneName, views);
        }

        public void Load(string scene, IViewsCollection views)
        {
            if (Collection != null)
                throw new Exception("Loading before unloading");

            Collection = views;
            var loading = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
            if (loading.isDone)
                OnLoadFinished();
            else
                loading.completed += Complited;

            void Complited(AsyncOperation operation)
            {
                loading.completed -= Complited;
                OnLoadFinished();
            }
        }

        public void Unload()
        {
            if (GameCoreInitialize.IsGameExit)
                return;

            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            Collection.Dispose();
            Collection = null;
        }

    }
}
