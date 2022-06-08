using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Models.Services;
using Game.Assets.Scripts.Game.Logic.Models.Services.Definitions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors
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
                    result.Add(IModelServices.Default.Get<DefinitionsService>().Get<T>(key));
                }
                return new ReadOnlyCollection<T>(result);
            }
            else
            {
                var key = obj.ToString();
                return IModelServices.Default.Get<DefinitionsService>().Get<T>(key);
            }

        }
    }
}
