using Game.Assets.Scripts.Game.Logic.Views.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors
{
    public class AssetConventer : ReadOnlyJsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var obj = JObject.ReadFrom(reader);
            var key = obj.ToString();

            if (objectType.IsAssignableFrom(typeof(ISprite)))
            {
                return GameAccessPoint.Game.Assets.GetSprite(key);
            }
            if (objectType.IsAssignableFrom(typeof(IVisual)))
            {
                return GameAccessPoint.Game.Assets.GetVisual(key);
            }
            throw new Exception("Unknown type of " + objectType.Name);
        }
    }
}
