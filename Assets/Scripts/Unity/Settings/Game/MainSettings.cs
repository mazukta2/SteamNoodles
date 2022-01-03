using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using GameUnity.Assets.Scripts.Unity.Data.Levels;
using Newtonsoft.Json;

namespace GameUnity.Assets.Scripts.Unity.Data.Game
{
    public class MainSettings
    {
        [JsonConverter(typeof(SettingsConventer<LevelSettings>))]
        public LevelSettings StartLevel { get; set; }
    }
}
