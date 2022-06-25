using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Definitions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Game.Assets.Scripts.Game.Logic.Common.Json.Convertors
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
                result.Add(IModelServices.Default.Get<DefinitionsService>().Get<TKey>(item.Key), item.Value);
            }
            return (IReadOnlyDictionary<TKey, TValue>)new ReadOnlyDictionary<TKey, TValue>(result);
        }
    }
}
