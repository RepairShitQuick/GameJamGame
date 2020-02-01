using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Networking.Identity
{
    public static class IdentityStore
    {
        static IdentityStore()
        {
            NetworkedGameObjectsByGuid = new Dictionary<Guid, GameObject>();
        }
        public static Dictionary<Guid, GameObject> NetworkedGameObjectsByGuid { get; }
    }
}
