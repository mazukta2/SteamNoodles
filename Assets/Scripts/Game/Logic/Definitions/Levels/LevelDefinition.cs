using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels
{
    public class LevelDefinition
    {
        public string SceneName { get; set; }

        [JsonConverter(typeof(DefinitionsConventer<ConstructionDefinition>))]
        public IReadOnlyCollection<ConstructionDefinition> StartingHand { get; set; } = new List<ConstructionDefinition>();

        public PlacementFieldDefinition PlacementField { get; set; }

        public FloatPoint QueuePosition;

        public int CrowdUnitsAmount { get; set; }

        [JsonConverter(typeof(DefinitionsDictionaryConventer<CustomerDefinition, int>))]
        public IReadOnlyDictionary<CustomerDefinition, int> BaseCrowdUnits { get; set; } = new Dictionary<CustomerDefinition, int>();
        public FloatRect UnitsRect { get; set; }

        [JsonConverter(typeof(DefinitionsDictionaryConventer<ConstructionDefinition, int>))]
        public IReadOnlyDictionary<ConstructionDefinition, int> ConstructionsReward { get; set; } = new Dictionary<ConstructionDefinition, int>();

        public int ConstructionsForNextWave { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(SceneName))
                throw new Exception($"{nameof(SceneName)} is empty");

            if (StartingHand.Count == 0)
                throw new Exception($"{nameof(StartingHand)} is empty");

            if (PlacementField == null)
                throw new Exception($"{nameof(PlacementField)} is empty");

            if (CrowdUnitsAmount <= 0)
                throw new Exception($"{nameof(CrowdUnitsAmount)} is zero");

            if (BaseCrowdUnits.Count == 0)
                throw new Exception($"{nameof(BaseCrowdUnits)} is empty");

            if (UnitsRect.IsZero())
                throw new Exception($"{nameof(UnitsRect)} is empty");

            if (QueuePosition.IsZero())
                throw new Exception($"{nameof(QueuePosition)} is null");

            if (ConstructionsReward.Count == 0)
                throw new Exception($"{nameof(ConstructionsReward)} is empty");

            if (ConstructionsForNextWave == 0)
                throw new Exception($"{nameof(ConstructionsForNextWave)} is empty");
        }
    }
}
