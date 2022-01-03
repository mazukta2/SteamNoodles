using Game.Assets.Scripts.Game.Logic.Common.Math;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors
{
    public class RectConventer : ReadOnlyJsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var array = JArray.ReadFrom(reader).ToObject<int[]>();
            return new Rect(array[0], array[1], array[2], array[3]);
        }

    }
}
