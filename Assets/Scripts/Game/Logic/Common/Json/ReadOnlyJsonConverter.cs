using System;
using Newtonsoft.Json;

namespace Game.Assets.Scripts.Game.Logic.Common.Json
{
    public abstract class ReadOnlyJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
