using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using GameUnity.Assets.Scripts.Unity.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Settings.Data
{
    public class AssetsLoader : IAssetsController
    {
        public ISprite GetSprite(string path)
        {
            return new UnitySprite(LoadResource<Sprite>(path));
        }

        public IVisual GetVisual(string path)
        {
            return new UnityView(LoadResource<GameObject>(path));
        }

        private static T LoadResource<T>(string path) where T : UnityEngine.Object
        {
            var filePath = "Assets/" + path;
            var targetFile = Resources.Load<T>(filePath);
            if (targetFile == null)
            {
                Debug.LogError("Cant find resource: " + filePath);
                return null;
            }    
            return targetFile;
        }

    }
}
