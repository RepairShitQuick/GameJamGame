using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Assets.Networking.Identity;
using Assets.Networking.Messaging.ConnectionHandlers;
using UnityEngine;

namespace Assets.Networking
{
    public class Server : MonoBehaviour
    {
        private ConnectionInitializationListener _listener;
        private ICollection<EstablishedConnection> _connections;
        public GameObject PlayerSpawnPrefab;

        public void Awake()
        {
            _connections = new List<EstablishedConnection>();
            _listener = new ConnectionInitializationListener();
            _listener.NewSocketEvent += ((_, socket) => OnNewClient(socket));
        }

        public void OnNewClient(Socket socket)
        {
            var connection = new EstablishedConnection(socket);
            _connections.Add(connection);
            var prefabMessage = Instantiate(PlayerSpawnPrefab, transform);
            connection.CreateObjectOverNetwork(prefabMessage);
            Destroy(prefabMessage);
        }

        void Update()
        {
            if (Time.frameCount % 10 == 0)
            {
                SendAllUpdates();
            }
        }

        private void SendAllUpdates()
        {
            var objs = GameObject.FindObjectsOfType<BaseNetworkBehavior>().Where(t => !t.PlayerOwned).ToList();
            foreach (var connection in _connections)
            {
                connection.SendUpdateBroadcast(objs);
            }
        }
    }
}
