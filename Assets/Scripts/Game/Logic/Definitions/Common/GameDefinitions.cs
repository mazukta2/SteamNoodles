using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Common
{
    public class GameDefinitions : IGameDefinitions
    {
        private Dictionary<string, object> _cached = new Dictionary<string, object>();
        private IDefinitions _definitions;

        public GameDefinitions(IDefinitions definitions)
        {
            _definitions = definitions;
        }

        public T Get<T>(string id)
        {
            var name = typeof(T).Name;
            var path = name + "/" + id;
            if (_cached.ContainsKey(path))
                return (T)_cached[path];

            var text = LoadResourceTextfile(path);
            try
            {
                var item = Activator.CreateInstance(typeof(T));
                Prepare(item, path);
                _cached.Add(path, item);
                JsonConvert.PopulateObject(text, item);
                return (T)item;
            }
            catch (Exception ex)
            {
                throw new Exception("Cant deserialize : " + path, ex);
            }
        }

        public T Get<T>()
        {
            var path = typeof(T).Name;
            if (_cached.ContainsKey(path))
                return (T)_cached[path];

            var text = LoadResourceTextfile(path);
            try
            {
                var item = Activator.CreateInstance(typeof(T));
                Prepare(item, path);
                _cached.Add(path, item);
                JsonConvert.PopulateObject(text, item);
                return (T)item;
            }
            catch (Exception ex)
            {
                throw new Exception("Cant deserialize : " + path, ex);
            }
        }

        public IReadOnlyCollection<T> GetList<T>()
        {
            var folderPath = typeof(T).Name;
            var list = GetDefintionPaths(folderPath);
            var result = new List<T>();
            foreach (var path in list)
            {
                if (_cached.ContainsKey(path))
                    result.Add((T)_cached[path]);
                else
                {
                    try
                    {
                        var text = LoadResourceTextfile(path);

                        var obj = Activator.CreateInstance(typeof(T));
                        Prepare(obj, path);
                        _cached.Add(path, obj);
                        JsonConvert.PopulateObject(text, obj);

                        result.Add((T)_cached[path]);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Cant deserialize : " + path, ex);
                    }
                }
            }
            return result.AsReadOnly();
        }

        private void Prepare(object item, string path)
        {
            if (item is IDefinition def)
                def.DefId = new DefId(path);
        }

        protected string LoadResourceTextfile(string path) => _definitions.LoadResourceTextfile(path);
        protected string[] GetDefintionPaths(string folder) => _definitions.GetDefintionPaths(folder);
    }
}
