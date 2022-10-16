using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class DefinitionsMock : IDefinitions
    {
        private Dictionary<string, object> _list = new Dictionary<string, object>();
        public T Get<T>()
        {
            return GetList<T>().FirstOrDefault();
        }

        public T Get<T>(string id)
        {
            return (T)_list[id];
        }

        public void Add(string id, object item)
        {
            _list.Add(id, item);
        }

        public IReadOnlyCollection<T> GetList<T>()
        {
            return _list.Values.OfType<T>().AsReadOnly();
        }

    }
}
