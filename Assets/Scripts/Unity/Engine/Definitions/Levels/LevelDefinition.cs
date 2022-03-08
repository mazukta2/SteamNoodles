using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using GameUnity.Assets.Scripts.Unity.Data.Buildings;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GameUnity.Assets.Scripts.Unity.Engine.Definitions.Levels
{
    public class LevelDefinition : ILevelDefinition
    {
        public string SceneName { get; set; }

        [JsonConverter(typeof(DefinitionsConventer<ConstructionDefinition>))]
        public IReadOnlyCollection<IConstructionDefinition> StartingHand { get; set; }

        public int HandSize { get; set; }
    }
}
