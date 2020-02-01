using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Assets.Networking.Identity;
using Assets.Networking.Messaging.JsonSettings;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Assets.Networking.Messaging
{
    public class GameObjectConverter : JsonConverter
    {

        private static HashSet<Type> unityTypes;
        private static HashSet<Type> serializingTypes;

        static GameObjectConverter()
        {
            unityTypes = new HashSet<Type>(typeof(GameObject).Assembly.GetTypes());
            unityTypes.Add(typeof(Transform));
            serializingTypes = new HashSet<Type>(new [] {typeof(Vector3), typeof(Quaternion)});
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value == null) return;
            Debug.Log($"Trying to serialize {value.GetType()}");
            
            if(value.GetType() == typeof(Transform) || value is GameObject)
            {
                writer.WriteNull();
                return;
            }
            else if (value is Vector3 || value is Quaternion)
            {
                writer.WriteRawValue(JsonUtility.ToJson(value));
            }
            else if (value is Component)
            {
                SerializeComponents(writer, value, serializer);
                return;
            }
        }

        private void SerializeComponents(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                  .Where(t => !t.GetCustomAttributes(typeof(ObsoleteAttribute)).Any())
                                  .Where(t => t.GetMethod.GetParameters().Length == 0);
            writer.WriteStartObject();
            foreach (var property in properties)
            {
                writer.WritePropertyName(property.Name);
                serializer.Serialize(writer, property.GetValue(value));
            }
            writer.WriteEndObject();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return JsonUtility.FromJson(reader.ReadAsString(), objectType);
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(Component).IsAssignableFrom(objectType) || unityTypes.Contains(objectType);
        }
    }
}
