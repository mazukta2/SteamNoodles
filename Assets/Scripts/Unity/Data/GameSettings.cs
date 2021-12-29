using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Data
{
    public static class GameSettings
    {
        private static Dictionary<ulong, object> _settings = new Dictionary<ulong, object>();

        public static T Get<T>(SettingsId id)
        {
            return (T)_settings[id.GetId()];
        }


        public static T Get<T>()
        {
            var list = _settings.Keys.OfType<T>();
            if (list.Count() != 1)
                throw new Exception("Wrong amount of settings");
            return list.First();
        }

        public static void CacheData()
        {
            _settings.Clear();

            var list = Resources.LoadAll<TextAsset>("Settings");
            //if (Settings == null)
            //    throw new Exception("Can't find settings file");

            //var dataFiles = Resources.LoadAll<GameData>("Data");
            //foreach (var data in dataFiles)
            //{
            //    Reqister(data.GetId(), data);
            //}
        }

        //private static void Reqister(ulong id, GameData data)
        //{
        //    if (id == 0)
        //        throw new Exception("Wrong data id");

        //    if (_list.ContainsKey(id))
        //        throw new Exception("Dublicate key for " + data.name + " and " + _list[id].name);

        //    _list[id] = data;
        //}

        //public static T Get<T>(ulong id) where T : GameData
        //{
        //    if (_list.Count == 0)
        //        throw new Exception("Data manager is not inited");

        //    if (!_list.ContainsKey(id))
        //        throw new Exception("Key " + id + " is not exist anymore.");

        //    if (!(_list[id] is T))
        //        throw new Exception("Key " + id + " is not exist anymore.");

        //    return (T)_list[id];
        //}

        //public static T As<T>(ulong id) where T : GameData
        //{
        //    if (!_list.ContainsKey(id))
        //        throw new Exception("Key " + id + " is not exist anymore.");

        //    return _list[id] as T;
        //}
    }
}
