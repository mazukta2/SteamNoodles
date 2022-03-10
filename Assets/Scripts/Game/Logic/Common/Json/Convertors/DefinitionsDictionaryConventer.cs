using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors
{
    public class DefinitionsDictionaryConventer<TKey, TValue> : ReadOnlyJsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var obj = JObject.ReadFrom(reader);
            var dictionary = obj.ToObject<ReadOnlyDictionary<string, TValue>>();
            var result = new Dictionary<TKey, TValue>();
            foreach (var item in dictionary)
            {
                result.Add(CoreAccessPoint.Core.Engine.Settings.Get<TKey>(item.Key), item.Value);
            }
            return (IReadOnlyDictionary<TKey, TValue>)new ReadOnlyDictionary<TKey, TValue>(result);
        }
    }
}
