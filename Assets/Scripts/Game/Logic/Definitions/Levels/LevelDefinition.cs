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
        [JsonConverter(typeof(ObjectConventer<LevelVariation, LevelVariation>))]
        public LevelVariation Variation { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Variation.SceneName))
                throw new Exception($"{nameof(Variation.SceneName)} is empty");

            if (Variation != null)
                Variation.Validate();

        }
    }
}
