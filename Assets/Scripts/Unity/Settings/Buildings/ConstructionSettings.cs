using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace GameUnity.Assets.Scripts.Unity.Data.Buildings
{
    public class ConstructionSettings : IConstructionSettings
    {
        [JsonConverter(typeof(PointConventer))]
        public Point Size { get; set; }
        public Requirements Requirements { get; set; }
        [JsonConverter(typeof(AssetConventer))]
        public ISprite HandIcon { get; set; }
        [JsonConverter(typeof(AssetConventer))]
        public IVisual BuildingView { get; set; }
        [JsonConverter(typeof(ObjectConventer<Core.Game, IConstructionFeatureSettings>))]
        public IReadOnlyCollection<IConstructionFeatureSettings> Features { get; set; }
        public IReadOnlyDictionary<ConstructionTag, int> Tags { get; set; }
    }
}

