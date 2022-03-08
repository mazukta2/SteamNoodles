using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using GameUnity.Assets.Scripts.Unity.Engine.Definitions.Levels;
using Newtonsoft.Json;

namespace GameUnity.Assets.Scripts.Unity.Engine.Definitions
{
    public class MainDefinition
    {
        [JsonConverter(typeof(DefinitionsConventer<LevelDefinition>))]
        public LevelDefinition StartLevel { get; set; }
    }
}
