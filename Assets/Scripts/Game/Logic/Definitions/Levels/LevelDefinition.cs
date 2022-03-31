using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
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
        public int HandSize { get; set; }

        public IReadOnlyCollection<PlacementFieldDefinition> PlacementFields { get; set; } = new List<PlacementFieldDefinition>();

        public FloatPoint QueuePosition;

        public int CrowdUnitsAmount { get; set; }

        [JsonConverter(typeof(DefinitionsDictionaryConventer<CustomerDefinition, int>))]
        public IReadOnlyDictionary<CustomerDefinition, int> BaseCrowdUnits { get; set; } = new Dictionary<CustomerDefinition, int>();
        public FloatRect UnitsRect { get; set; }


        public void Validate()
        {
            if (string.IsNullOrEmpty(SceneName))
                throw new Exception($"{nameof(SceneName)} is empty");

            if (StartingHand.Count == 0)
                throw new Exception($"{nameof(StartingHand)} is empty");

            if (HandSize <= 0)
                throw new Exception($"{nameof(HandSize)} is zero");

            if (PlacementFields.Count == 0)
                throw new Exception($"{nameof(PlacementFields)} is empty");

            if (CrowdUnitsAmount <= 0)
                throw new Exception($"{nameof(CrowdUnitsAmount)} is zero");

            if (BaseCrowdUnits.Count == 0)
                throw new Exception($"{nameof(BaseCrowdUnits)} is empty");

            if (UnitsRect.IsZero())
                throw new Exception($"{nameof(UnitsRect)} is empty");

            if (QueuePosition.IsZero())
                throw new Exception($"{nameof(QueuePosition)} is null");

        }
    }
}
