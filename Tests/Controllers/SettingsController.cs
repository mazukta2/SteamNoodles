using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Tests.Controllers
{
    public class SettingsController : ISettingsController
    {
        private Dictionary<string, object> _list = new Dictionary<string, object>();
        public T Get<T>()
        {
            return (T)_list[typeof(T).Name];
        }

        public T Get<T>(string id)
        {
            return (T)_list[id];
        }

        public void Add(string id, object item)
        {
            _list.Add(id, item);
        }
    }
}
