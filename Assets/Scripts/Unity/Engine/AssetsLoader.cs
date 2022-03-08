using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
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

        public IScreenAsset<T> GetScreen<T>() where T : ScreenView
        {
            return new InnerResource<T>(this);
        }

        public class InnerResource<T> : IScreenAsset<T> where T : ScreenView
        {
            private AssetsLoader _assets;

            public InnerResource(AssetsLoader assets)
            {
                _assets = assets;
            }

            public T Create(ViewContainer container)
            {
                return Instantiate(container);
            }

            private T Instantiate(ViewContainer parent)
            {
                var name = typeof(T).Name;
                var prefab = LoadResource<GameObject>("Prefabs/Screens/" + name);
                if (prefab == null)
                    throw new System.Exception($"Cant find screen prefab named : {name}");

                var go = GameObject.Instantiate(prefab, parent.GetPointer());
                return go.GetComponent<T>();
            }
        }
    }
}
