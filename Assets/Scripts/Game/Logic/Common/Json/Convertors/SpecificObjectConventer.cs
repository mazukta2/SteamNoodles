using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Game.Assets.Scripts.Game.Logic.Common.Json.Convertors
{
    public class SpecificObjectConventer<T> : ReadOnlyJsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var obj = JObject.ReadFrom(reader);
            return obj.ToObject<T>();
        }
    }
}
