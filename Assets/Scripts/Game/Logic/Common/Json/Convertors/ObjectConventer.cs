using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Game.Assets.Scripts.Game.Logic.Common.Json.Convertors
{
    public class ObjectConventer<TMain, TInterface>: ReadOnlyJsonConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var obj = JObject.ReadFrom(reader);
            if (obj is JArray array)
            {
                var result = new List<TInterface>();
                foreach (var key in array)
                    result.Add((TInterface)Convert(key, serializer));

                return (IReadOnlyCollection<TInterface>)new ReadOnlyCollection<TInterface>(result);
            }
            else
            {
                return (TInterface)Convert(obj, serializer);
            }
        }

        private object Convert(JToken token, JsonSerializer serializer)
        {
            var type = token["Type"].Value<string>();
            var assembly = typeof(TMain).Assembly;
            var target = assembly.CreateInstance(type);
            if (target == null)
                throw new Exception("Cant create type : " + type + " of assembly " + assembly.FullName);

            serializer.Populate(token.CreateReader(), target);
            return target;
        }
    }
}
