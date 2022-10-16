using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Languages
{
    public class LanguageDefinition
    {
        public IReadOnlyDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();
        public string Name { get; set; }

        public LanguagePack Create()
        {
            return new LanguagePack(this);
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new Exception();
        }
    }
}
