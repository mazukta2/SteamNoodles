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
    public class GameDefinitions : BaseDefinitions
    {
        protected override string[] GetDefintionPaths(string folder)
        {
            var list = Resources.LoadAll<TextAsset>(folder);
            return list.Select(x => x.name).ToArray();
        }

        protected override string LoadResourceTextfile(string path)
        {
            var targetFile = Resources.Load<TextAsset>("Definitions/" + path);
            if (targetFile == null)
            {
                Debug.LogError("Cant find resource: " + path);
                return null;
            }
            return targetFile.text;
        }
        //private Dictionary<string, object> _cached = new Dictionary<string, object>();

        //public T Get<T>(string id)
        //{
        //    var name = typeof(T).Name;
        //    var path = name + "/" + id;
        //    if (_cached.ContainsKey(path))
        //        return (T)_cached[path];

        //    var text = LoadResourceTextfile(path);
        //    try
        //    {
        //        var item = JsonConvert.DeserializeObject<T>(text);
        //        _cached.Add(path, item);

        //        return item;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Cant deserialize : " + path, ex);
        //    }
        //}

        //public T Get<T>()
        //{
        //    var path = typeof(T).Name;
        //    if (_cached.ContainsKey(path))
        //        return (T)_cached[path];

        //    var text = LoadResourceTextfile(path);
        //    try
        //    {
        //        var item = JsonConvert.DeserializeObject<T>(text);
        //        _cached.Add(path, item);
        //        return item;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Cant deserialize : " + path, ex);
        //    }
        //}

        //public IReadOnlyCollection<T> GetList<T>()
        //{
        //    var folderPath = typeof(T).Name;
        //    var list = Resources.LoadAll<TextAsset>(folderPath);
        //    var result = new List<T>();
        //    foreach (var item in list)
        //    {
        //        var path = folderPath + "/" + item.name;
        //        if (_cached.ContainsKey(path))
        //            result.Add((T)_cached[path]);
        //        else
        //        {
        //            try
        //            {
        //                var obj = JsonConvert.DeserializeObject<T>(item.text);
        //                _cached.Add(path, obj);
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Cant deserialize : " + path, ex);
        //            }
        //        }
        //    }
        //    return result.AsReadOnly();
        //}

        //private static string LoadResourceTextfile(string path)
        //{
        //    var filePath = "Definitions/" + path;
        //    var targetFile = Resources.Load<TextAsset>(filePath);
        //    if (targetFile == null)
        //    {
        //        Debug.LogError("Cant find resource: " + filePath);
        //        return null;
        //    }
        //    return targetFile.text;
        //}

        //private static ulong CalculateHash(string str)
        //{
        //    unchecked
        //    {
        //        ulong hash1 = (5381 << 16) + 5381;
        //        ulong hash2 = hash1;

        //        for (int i = 0; i < str.Length; i += 2)
        //        {
        //            hash1 = (hash1 << 5) + hash1 ^ str[i];
        //            if (i == str.Length - 1)
        //                break;
        //            hash2 = (hash2 << 5) + hash2 ^ str[i + 1];
        //        }

        //        var result = hash1 + hash2 * 1566083941;
        //        if (result == 0)
        //            throw new Exception("wrong cache id for: " + str);
        //        return result;
        //    }
        //}
    }
}
