using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors
{
    public class SettingsConventer<T> : ReadOnlyJsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var obj = JObject.ReadFrom(reader);
            var key = obj.ToString();
            return GameAccessPoint.Game.Settings.Get<T>(key);
        }
    }
}
