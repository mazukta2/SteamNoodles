using Game.Assets.Scripts.Game.External;
using System.Collections.Generic;

namespace Game.Tests.Controllers
{
    public class TestDefinitions : IDefinitions
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
