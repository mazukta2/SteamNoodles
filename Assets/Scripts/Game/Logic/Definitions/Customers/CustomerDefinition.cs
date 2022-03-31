using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Customers
{
    public class CustomerDefinition
    {
        [JsonConverter(typeof(DefinitionsDictionaryConventer<ConstructionDefinition, int>))]
        public IReadOnlyDictionary<ConstructionDefinition, int> ConstructionsReward { get; set; } = new Dictionary<ConstructionDefinition, int>();

        public void Validate()
        {
            if (ConstructionsReward.Count == 0)
                throw new Exception($"{nameof(ConstructionsReward)} is empty");
        }
    }
}
