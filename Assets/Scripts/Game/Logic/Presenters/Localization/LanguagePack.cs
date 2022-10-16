using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Definitions.Languages;
using Newtonsoft.Json.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public class LanguagePack
    {
        private IReadOnlyDictionary<string, string> _values { get; set; } = new Dictionary<string, string>();
        public string Name { get; private set; }

        public LanguagePack(LanguageDefinition definition) : this(definition.Name, definition.Values)
        {
        }

        public LanguagePack(string name, IReadOnlyDictionary<string, string> values)
        {
            Name = name;
            _values = values;
        }

        public bool ContainsKey(string key)
        {
            return _values.ContainsKey(key);
        }

        public string Get(string key)
        {
            return _values[key];
        }
    }
}

