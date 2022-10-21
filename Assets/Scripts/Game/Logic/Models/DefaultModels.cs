using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Models.Levels;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public class DefaultModels : IModels
    {
        private Dictionary<Type, object> _dictonary = new Dictionary<Type, object>();

        public DefaultModels()
        {
        }

        public T Find<T>()
        {
            return (T)_dictonary[typeof(T)];
        }

        public bool Has<T>()
        {
            return _dictonary.ContainsKey(typeof(T));
        }

        public void Add<T>(T model)
        {
            _dictonary.Add(typeof(T), model);
        }

        public void Dispose()
        {
            foreach (var item in _dictonary)
            {
                if (item.Value is IDisposable disposable)
                    disposable.Dispose();
            }
        }
    }
}

