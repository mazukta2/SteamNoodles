using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Tests.Cases.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Tests.Controllers
{
    public class DefinitionsMock : IGameDefinitions
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

        public DefinitionsMock Add(string id, object item)
        {
            if (item is IDefinition d)
                d.DefId = new DefId(id);

            _list.Add(id, item);
            return this;
        }

        public DefinitionsMock Add(object item)
        {
            Add("not_impotant_" + _list.Count, item);
            return this;
        }

        public IReadOnlyCollection<T> GetList<T>()
        {
            return _list.Values.OfType<T>().AsReadOnly();
        }

    }
}
