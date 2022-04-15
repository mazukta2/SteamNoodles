using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Unity.Views;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class AssetsLoader : IAssets, IScreenAssets
    {
        public IScreenAssets Screens => this;

        private static T LoadResource<T>(string path) where T : Object
        {
            var filePath = path;
            var targetFile = Resources.Load<T>(filePath);
            if (targetFile == null)
            {
                Debug.LogError("Cant find resource: " + filePath);
                return null;
            }
            return targetFile;
        }

        public IViewPrefab GetPrefab(string path)
        {
            var prefab = LoadResource<GameObject>("Assets/" + path);
            if (prefab == null)
                throw new System.Exception($"Cant find prefab named : {path}");

            return new GamePrefab(prefab);
        }

        public IViewPrefab GetConstruction(string path)
        {
            var prefab = LoadResource<GameObject>("Assets/" + path);
            if (prefab == null)
                throw new System.Exception($"Cant find construction prefab named : {path}");

            return new GamePrefab(prefab);
        }

        public IViewPrefab GetScreen<T>() where T : IScreenView
        {
            var name = typeof(T).Name;
            name = name.Replace("View", "");
            var prefab = LoadResource<GameObject>("Assets/Screens/" + name);
            if (prefab == null)
                throw new System.Exception($"Cant find screen prefab named : {name}");

            return new GamePrefab(prefab);
        }

        public class GamePrefab : IViewPrefab
        {
            public GameObject GameObject { get; private set; }

            public GamePrefab(GameObject prefab)
            {
                GameObject = prefab;
            }
        }
    }
}
