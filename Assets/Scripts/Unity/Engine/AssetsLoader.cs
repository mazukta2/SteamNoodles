using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine
{
    public class AssetsLoader : IAssets
    {
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
