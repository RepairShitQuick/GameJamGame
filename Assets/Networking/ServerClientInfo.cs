using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Assets.Networking
{
    public static class ServerClientInfo
    {
        static ServerClientInfo()
        {
            if (!File.Exists("./gameConfig.json")) return;
            var configFile = File.ReadAllText("./gameConfig.json");
            var jObject = JObject.Parse(configFile);
            var properties = typeof(ServerClientInfo).GetProperties(BindingFlags.Static | BindingFlags.Public);
            foreach (var property in properties)
            {
                var worked = jObject.TryGetValue(property.Name, out var value);
                if (worked)
                {
                    property.SetValue(null, value.ToObject(property.PropertyType));
                }
            }
        }
        /// <summary>
        /// Don't set this.
        /// </summary>
        public static bool IsServer { get; set; }

        public static string IpAddress { get; set; }
        public const int PortNum = 4437;
    }
}
