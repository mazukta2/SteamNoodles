using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Json.Convertors;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels
{
    public class LevelDefinition
    {
        public string SceneName { get; set; }

        [JsonConverter(typeof(DefinitionsConventer<ConstructionDefinition>))]
        public IReadOnlyCollection<ConstructionDefinition> StartingHand { get; set; } = new List<ConstructionDefinition>();

        public PlacementFieldDefinition PlacementField { get; set; }

        public GameVector3 QueuePosition { get; set; }
        public GameVector3 QueueFirstPositionOffset { get; set; }

        public int CrowdUnitsAmount { get; set; }

        [JsonConverter(typeof(DefinitionsDictionaryConventer<CustomerDefinition, int>))]
        public IReadOnlyDictionary<CustomerDefinition, int> BaseCrowdUnits { get; set; } = new Dictionary<CustomerDefinition, int>();
        public FloatRect UnitsRect { get; set; }

        [JsonConverter(typeof(DefinitionsDictionaryConventer<ConstructionDefinition, int>))]
        public IReadOnlyDictionary<ConstructionDefinition, int> ConstructionsReward { get; set; } = new Dictionary<ConstructionDefinition, int>();

        public int ConstructionsForNextWave { get; set; }

        [JsonConverter(typeof(ObjectConventer<LevelStarter, LevelStarter>))]
        public LevelStarter Starter { get; set; }
        public int Waves { get; set; }

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

            if (QueueFirstPositionOffset.IsZero())
                throw new Exception($"{nameof(QueueFirstPositionOffset)} is null");

            if (ConstructionsReward.Count == 0)
                throw new Exception($"{nameof(ConstructionsReward)} is empty");

            if (ConstructionsForNextWave == 0)
                throw new Exception($"{nameof(ConstructionsForNextWave)} is empty");

            if (Starter == null)
                throw new Exception($"{nameof(Starter)} is empty");

            if (PlacementField.Size.X % 2 == 0 || PlacementField.Size.Y % 2 == 0)
                throw new Exception("Even sized fields are not supported. Sorry :(");

            if (Waves == 0)
                throw new Exception($"{nameof(Waves)} is empty");

        }
    }
}
