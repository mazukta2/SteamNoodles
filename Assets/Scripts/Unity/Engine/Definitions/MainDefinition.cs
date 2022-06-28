using Game.Assets.Scripts.Game.Logic.Common.Json.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Newtonsoft.Json;

namespace GameUnity.Assets.Scripts.Unity.Engine.Definitions
{
    public class MainDefinition
    {
        [JsonConverter(typeof(DefinitionsConventer<LevelDefinition>))]
        public LevelDefinition StartLevel { get; set; }
    }
}
