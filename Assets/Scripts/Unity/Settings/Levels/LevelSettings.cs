using Assets.Scripts.Data.Buildings;
using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Settings.Rewards;
using GameUnity.Assets.Scripts.Unity.Settings;
using GameUnity.Assets.Scripts.Unity.Settings.Rewards;
using Newtonsoft.Json;
using System.Collections.Generic;
using Rect = Game.Assets.Scripts.Game.Logic.Common.Math.Rect;

namespace GameUnity.Assets.Scripts.Unity.Data.Levels
{
    public class LevelSettings : ILevelSettings
    {
        [JsonConverter(typeof(PointConventer))]
        public Point Size { get; set; }
        [JsonConverter(typeof(RectConventer))]
        public Rect UnitsSpawnRect { get; set; }
        [JsonConverter(typeof(SettingsDictionaryConventer<ICustomerSettings, CustomerSettings, int>))]
        public IReadOnlyDictionary<ICustomerSettings, int> Deck { get; set; }

        [JsonConverter(typeof(ObjectConventer<Reward>))]
        public IReward ClashReward { get; set; }
        public int MaxQueue { get; set; }
        public float SpawnQueueTime { get; set; } 
        public int NeedToServe { get; set; }
        public IConstructionSettings[] StartingHand { get; set; } 
        public int HandSize { get; set; }
        public string SceneName { get; set; } 
    }
}
