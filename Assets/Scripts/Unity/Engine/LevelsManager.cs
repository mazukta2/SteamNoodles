using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class LevelsManager : ILevelsManager
    {
        public event Action OnLoadFinished = delegate { };
        public static IViews Collection { get; private set; }

        public LevelsManager()
        {
        }

        public void Load(string scene, IViews views)
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

            if (Collection != null)
            {
                Collection.Dispose();
                Collection = null;

                SceneManager.UnloadScene(SceneManager.GetActiveScene());
            }
        }

    }
}
