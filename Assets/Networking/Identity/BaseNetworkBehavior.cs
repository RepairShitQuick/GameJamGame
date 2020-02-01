using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Networking.Identity
{
    [RequireComponent(typeof(NetworkIdentity))]
    public abstract class BaseNetworkBehavior : MonoBehaviour, INetworkEntity
    {
        void Awake()
        {
            IdentityStore.NetworkedGameObjectsByGuid.Add(this.AssociatedNetworkIdentity, this.gameObject);
        }
        public Guid AssociatedNetworkIdentity { get; set; }
        public bool PlayerOwned { get; set; }
    }
}
