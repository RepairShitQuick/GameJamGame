using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Networking.Identity;
using UnityEngine;

namespace Assets.Networking
{
    /// <summary>
    /// we need to snapshot the items we previously broadcasted, the type and the guid
    /// </summary>
    public struct SnapShot
    {
        public GameObject GameObject { get; }
        public Type ObjectType { get; }
        public Guid NetworkGuid { get; }

        public SnapShot(BaseNetworkBehavior baseNetworkBehavior)
        {
            GameObject = baseNetworkBehavior.gameObject;
            ObjectType = baseNetworkBehavior.GetType();
            NetworkGuid = baseNetworkBehavior.AssociatedNetworkIdentity;
        }

        public bool IsDeleted()
        {
            return GameObject == null;
        }
    }
}
