using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Newtonsoft.Json;

namespace Game.Assets.Scripts.Game.Logic.Definitions
{
    public class MainDefinition
    {
        [JsonConverter(typeof(DefinitionsConventer<LevelDefinition>))]
        public LevelDefinition StartLevel { get; set; }
    }
}
