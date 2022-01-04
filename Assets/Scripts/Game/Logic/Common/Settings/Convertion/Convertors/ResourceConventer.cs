using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors
{
    public class SettingsConventer<T> : ReadOnlyJsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var obj = JObject.ReadFrom(reader);

            if (obj is JArray)
            {
                var list = obj.ToObject<string[]>();
                var result = new List<T>();
                foreach (var key in list)
                {
                    result.Add(GameAccessPoint.Game.Settings.Get<T>(key));
                }
                return new ReadOnlyCollection<T>(result);
            }
            else
            {
                var key = obj.ToString();
                return GameAccessPoint.Game.Settings.Get<T>(key);
            }

        }
    }
}
