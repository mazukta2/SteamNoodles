using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public class CustomerDefinition
    {
        //int Money { get; }
        //float OrderingTime { get; }
        //float CookingTime { get; }
        //float EatingTime { get; }
        //float BaseTipMultiplayer { get; }
        //IReadOnlyCollection<ICustomerFeatureSettings> Features { get; }
        //float Speed { get; }
        //IReadOnlyCollection<ICustomerFeatureSettings> Features { get; }

        [JsonConverter(typeof(DefinitionsDictionaryConventer<ConstructionDefinition, int>))]
        public IReadOnlyDictionary<ConstructionDefinition, int> ConstrcutionsReward { get; set; } = new Dictionary<ConstructionDefinition, int>();
    }
}
