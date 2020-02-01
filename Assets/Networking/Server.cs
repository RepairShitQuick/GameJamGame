using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Assets.Networking.Identity;
using Assets.Networking.Messaging;
using Assets.Networking.Messaging.ConnectionHandlers;
using Assets.Networking.Messaging.Requests;
using UnityEngine;

namespace Assets.Networking
{
    public class Server : MonoBehaviour
    {
        private ConnectionInitializationListener _listener;
        private ICollection<EstablishedConnection> _connections;
        public string PlayerPrefabPath = "Assets/Prefbs/PlayerPrefab.prefab";
        private Dictionary<MonoBehaviour, SnapShot> _snapShotsSinceLastBroadcast;


        public void Awake()
        {
            _snapShotsSinceLastBroadcast = new Dictionary<MonoBehaviour, SnapShot>();
            _connections = new List<EstablishedConnection>();
            _listener = new ConnectionInitializationListener();
            _listener.NewSocketEvent += ((_, socket) => OnNewClient(socket));
        }

        public IEnumerable<string> GetMessages()
        {
            foreach (var connection in _connections)
            {
                while (connection.TcpConnectionHandler.Messages.Any())
                {
                    yield return connection.TcpConnectionHandler.Messages.Pop();
                }

                while (connection.UdpConnectHandler.Messages.Any())
                {
                    yield return connection.UdpConnectHandler.Messages.Pop();
                }
            }
        }

        public void OnNewClient(Socket socket)
        {
            var connection = new EstablishedConnection(socket);
            _connections.Add(connection);
            var createResource = new CreateRequest {ResourcePath = PlayerPrefabPath};
            connection.CreateObjectOverNetwork(createResource);
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
            foreach (var obj in objs)
            {
                _snapShotsSinceLastBroadcast.Remove(obj);
            }

            foreach (var connection in _connections)
            {
                connection.SendUpdateBroadcast(objs);
            }

            foreach (var gameObjectSnapShot in _snapShotsSinceLastBroadcast)
            {
                foreach (var connection in _connections)
                {
                    connection.SendDeleteMessage(new DeleteRequest(gameObjectSnapShot.Value));
                }
            }
            _snapShotsSinceLastBroadcast.Clear();

            foreach (var obj in objs)
            {
                _snapShotsSinceLastBroadcast.Add(obj, new SnapShot(obj));
            }
        }
    }
}
