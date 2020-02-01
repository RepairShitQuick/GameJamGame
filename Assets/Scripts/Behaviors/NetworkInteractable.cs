using System;
using Assets.Networking;
using Assets.Networking.Identity;
using UnityEngine;

namespace Assets.Scripts.Behaviors
{
    public abstract class NetworkInteractable : BaseNetworkBehavior
    {
        private static ServerClient _serverClient;
        public void Interact()
        {
            if (PlayerOwned)
            {
                LocalInteract();
            }
            else
            {
                RemoteCall();
            }
        }

        private void RemoteCall()
        {
            if (_serverClient == null)
            {
                _serverClient = GameObject.FindObjectOfType<ServerClient>();
            }
            _serverClient.RemoteFunctionCall(this, "LocalInteract", Array.Empty<object>());
        }

        protected abstract void LocalInteract();
    }
}
