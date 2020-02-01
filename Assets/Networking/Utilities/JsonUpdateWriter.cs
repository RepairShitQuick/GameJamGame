using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Assets.Networking.Utilities
{
    public static class JsonUpdateWriter
    {
        public static void UpdateComponent(Component component, object obj)
        {
            var jObject = (JObject) obj;
            JsonUtility.FromJsonOverwrite(jObject.ToObject<string>(), component);
        }
    }
}
