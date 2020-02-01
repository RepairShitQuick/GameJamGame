using System;
using UnityEngine;

namespace Assets.Networking.Identity
{
    public class NetworkIdentity : MonoBehaviour
    {
        public NetworkIdentity()
        {
            Guid = Guid.NewGuid();
        }


        private Guid _guid;

        public Guid Guid
        {
            get
            {
                if (_guid == Guid.Empty)
                {
                    _guid = Guid.NewGuid();
                    return _guid;
                }

                return _guid;
            }

            set => _guid = value;
        }
    }
}
