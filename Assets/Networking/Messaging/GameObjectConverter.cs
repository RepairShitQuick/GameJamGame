using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Networking.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Assets.Networking.Messaging
{
    public class GameObjectConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var jObject = JObject.FromObject(value);
            jObject.Remove("gameObject");
            jObject.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return serializer.Deserialize(reader, objectType);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(INetworkEntity).IsAssignableFrom(objectType);
        }
    }
}
