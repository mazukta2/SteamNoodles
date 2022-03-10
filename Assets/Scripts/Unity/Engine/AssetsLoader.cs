using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Assets;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
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

        public ViewPrefab<T> GetScreen<T>() where T : ScreenViewPresenter
        {
            var name = typeof(T).Name;
            name = name.Replace("ViewPresenter", "");
            var prefab = LoadResource<GameObject>("Prefabs/Screens/" + name);
            if (prefab == null)
                throw new System.Exception($"Cant find screen prefab named : {name}");

            return new ScreenPrefab<T>(prefab);

        }

        public class ScreenPrefab<T> : ViewPrefab<T> where T : ViewPresenter
        {
            private GameObject _prefab;

            public ScreenPrefab(GameObject prefab)
            {
                _prefab = prefab;
            }

            public override T Create(ContainerViewPresenter conteiner)
            {
                var go = GameObject.Instantiate(_prefab, conteiner.GetPointer());
                var view = go.GetComponent<View<T>>();
                if (view == null)
                    throw new System.Exception("Cant find view preseneter " + typeof(View<T>).Name);
                return view.GetViewPresenter();
            }
        }
    }
}
