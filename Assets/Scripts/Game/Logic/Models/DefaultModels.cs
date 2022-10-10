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

        public void Add<T>(T model)
        {
            _dictonary.Add(typeof(T), model);
        }
    }
}

