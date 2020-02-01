using System;
using UnityEngine;

namespace Assets.Networking.Identity
{
    public class NetworkedPlayer : BaseNetworkBehavior
    {
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public void Update()
        {
            if (!PlayerOwned)
            {
                this.transform.position = Position;
                this.transform.rotation = Rotation;
            }
        }
    }
}
