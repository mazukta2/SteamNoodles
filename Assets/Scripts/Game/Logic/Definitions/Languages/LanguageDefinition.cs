using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Languages
{
    public class LanguageDefinition
    {
        public IReadOnlyDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();

    }
}
