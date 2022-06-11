using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine.Definitions
{
    public class UnityDefinitions : IDefinitions
    {
        public string[] GetDefintionPaths(string folder)
        {
            var list = Resources.LoadAll<TextAsset>("Definitions/" + folder);
            return list.Select(x => x.name).ToArray();
        }

        public string LoadResourceTextfile(string path)
        {
            var targetFile = Resources.Load<TextAsset>("Definitions/" + path);
            if (targetFile == null)
            {
                Debug.LogError("Cant find resource: " + path);
                return null;
            }
            return targetFile.text;
        }
    }
}
