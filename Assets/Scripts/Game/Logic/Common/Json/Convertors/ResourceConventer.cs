﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Game.Assets.Scripts.Game.Logic.Common.Json.Convertors
{
    public class DefinitionsConventer<T> : ReadOnlyJsonConverter
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
                    result.Add(IDefinitions.Default.Get<T>(key));
                }
                return new ReadOnlyCollection<T>(result);
            }
            else
            {
                var key = obj.ToString();
                return IDefinitions.Default.Get<T>(key);
            }

        }
    }
}
