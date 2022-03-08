using Game.Assets.Scripts.Game.External;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Engine.Definitions
{
    public class GameDefinitions : IDefinitions
    {
        private Dictionary<string, object> _cached = new Dictionary<string, object>();

        public T Get<T>(string id)
        {
            var name = typeof(T).Name;
            var path = name + "/" + id;
            if (_cached.ContainsKey(path))
                return (T)_cached[path];

            var text = LoadResourceTextfile(path);
            var item = JsonConvert.DeserializeObject<T>(text);
            _cached.Add(path, item);

            return item;
        }

        public T Get<T>()
        {
            var path = typeof(T).Name;
            if (_cached.ContainsKey(path))
                return (T)_cached[path];

            var text = LoadResourceTextfile(path);
            var item = JsonConvert.DeserializeObject<T>(text);
            _cached.Add(path, item);
            return item;
        }

        private static string LoadResourceTextfile(string path)
        {
            var filePath = "Definitions/" + path;
            var targetFile = Resources.Load<TextAsset>(filePath);
            if (targetFile == null)
            {
                Debug.LogError("Cant find resource: " + filePath);
                return null;
            }
            return targetFile.text;
        }

        private static ulong CalculateHash(string str)
        {
            unchecked
            {
                ulong hash1 = (5381 << 16) + 5381;
                ulong hash2 = hash1;

                for (int i = 0; i < str.Length; i += 2)
                {
                    hash1 = (hash1 << 5) + hash1 ^ str[i];
                    if (i == str.Length - 1)
                        break;
                    hash2 = (hash2 << 5) + hash2 ^ str[i + 1];
                }

                var result = hash1 + hash2 * 1566083941;
                if (result == 0)
                    throw new Exception("wrong cache id for: " + str);
                return result;
            }
        }
    }
}
