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
        public static IViewsCollection Collection { get; private set; }

        public LevelsManager()
        {
        }

        public void Load(LevelDefinition prototype, Action<IViewsCollection> onFinished)
        {
            Load(prototype.SceneName, onFinished);
        }

        public void Load(string scene, Action<IViewsCollection> onFinished)
        {
            if (Collection != null)
                throw new Exception("Loading before unloading");

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
                Collection = new ViewsCollection();
                onFinished(Collection);
            }
        }

        public void Unload()
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
            Collection.Dispose();
            Collection = null;
        }
    }
}
