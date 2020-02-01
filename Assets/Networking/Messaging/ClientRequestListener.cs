using System.Collections.Generic;
using Assets.Networking.Identity;
using Assets.Networking.Utilities;
using Newtonsoft.Json;
using UnityEngine;

namespace Assets.Networking.Messaging
{
    public class ClientRequestListener : BaseListener
    {
        private ServerClient _serverClient;

        public void Awake()
        {
            _serverClient = FindObjectOfType<ServerClient>();
        }

        public override IEnumerable<string> GetMessages() => _serverClient.GetMessages();
    }
}
