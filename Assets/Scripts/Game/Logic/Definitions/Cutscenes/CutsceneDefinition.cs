using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Json.Convertors;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels
{
    public class CutsceneDefinition
    {
        [JsonConverter(typeof(ObjectConventer<CutsceneStepVariation, CutsceneStepVariation>))]
        public IReadOnlyCollection<CutsceneStepVariation> Steps { get; set; } = new List<CutsceneStepVariation>();

        public void Validate()
        {
            foreach (var item in Steps)
                item.Validate();

        }
    }
}
