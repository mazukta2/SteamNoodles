using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels
{
    public class LanguageDefinition
    {
        public IReadOnlyDictionary<string, string> Values { get; set; } = new Dictionary<string, string>();

    }
}
