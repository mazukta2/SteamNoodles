using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Game.Assets.Scripts.Game.Environment.Engine;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Common
{
    public class GameDefinitions : IDefinitions
    {
        private Dictionary<string, object> _cached = new Dictionary<string, object>();
        private IDefinitionsLoader _definitions;

        public GameDefinitions(IDefinitionsLoader definitions)
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

        protected string LoadResourceTextfile(string path) => _definitions.LoadResourceTextfile(path);
        protected string[] GetDefintionPaths(string folder) => _definitions.GetDefintionPaths(folder);
    }
}
