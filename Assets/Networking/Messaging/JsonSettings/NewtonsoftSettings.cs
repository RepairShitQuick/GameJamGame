using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Assets.Networking.Messaging.JsonSettings
{
    public static class NewtonsoftSettings
    {
        public static JsonSerializerSettings Settings;

        static NewtonsoftSettings()
        {
            Settings = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter>
                {
                    new GameObjectConverter()
                },
                
            };
        }
    }
}
