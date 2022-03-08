using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors
{
    public class SettingsDictionaryConventer<TKeyInterface, TKey, TValue> : ReadOnlyJsonConverter where TKey : TKeyInterface
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var obj = JObject.ReadFrom(reader);
            var dictionary = obj.ToObject<ReadOnlyDictionary<string, TValue>>();
            var result = new Dictionary<TKeyInterface, TValue>();
            foreach (var item in dictionary)
            {
                result.Add(CoreAccessPoint.Core.Engine.Settings.Get<TKey>(item.Key), item.Value);
            }
            return (IReadOnlyDictionary<TKeyInterface, TValue>)new ReadOnlyDictionary<TKeyInterface, TValue>(result);
        }
    }
}
