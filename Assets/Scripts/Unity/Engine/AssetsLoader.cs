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

        public IViewPrefab GetConstruction(string path)
        {
            var prefab = LoadResource<GameObject>("Assets/" + path);
            if (prefab == null)
                throw new System.Exception($"Cant find construction prefab named : {path}");

            return new ScreenPrefab(prefab);
        }

        public IViewPrefab GetScreen<T>() where T : ScreenView
        {
            var name = typeof(T).Name;
            name = name.Replace("View", "");
            var prefab = LoadResource<GameObject>("Prefabs/Screens/" + name);
            if (prefab == null)
                throw new System.Exception($"Cant find screen prefab named : {name}");

            return new ScreenPrefab(prefab);

        }

        public class ScreenPrefab : IViewPrefab
        {
            public GameObject GameObject { get; private set; }

            public ScreenPrefab(GameObject prefab)
            {
                GameObject = prefab;
            }
        }
    }
}
