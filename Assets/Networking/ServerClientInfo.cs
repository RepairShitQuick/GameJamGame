using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Networking
{
    public static class ServerClientInfo
    {
        /// <summary>
        /// Don't set this.
        /// </summary>
        public static bool IsServer { get; set; }

        public static string IpAddress { get; set; }
        public const int PortNum = 4437;
    }
}
