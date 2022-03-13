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

        public ViewPrefab GetScreen<T>() where T : ScreenView
        {
            var name = typeof(T).Name;
            name = name.Replace("View", "");
            var prefab = LoadResource<GameObject>("Prefabs/Screens/" + name);
            if (prefab == null)
                throw new System.Exception($"Cant find screen prefab named : {name}");

            return new ScreenPrefab(prefab);

        }

        public class ScreenPrefab : ViewPrefab
        {
            private GameObject _prefab;

            public ScreenPrefab(GameObject prefab)
            {
                _prefab = prefab;
            }

            public override object Create<T>(ContainerView conteiner)
            {
                var unityConteiner = ContainerUnityView.Find(conteiner);
                var createdView = unityConteiner.View.Create(Creator);
                return createdView;

                T Creator(ILevel level)
                {
                    var go = GameObject.Instantiate(_prefab, unityConteiner.GetPointer());
                    var view = go.GetComponent<UnityView<T>>();
                    if (view == null)
                        throw new System.Exception("Cant find view preseneter " + typeof(UnityView<T>).Name);
                    return view.View;
                }

            }

        }
    }
}
