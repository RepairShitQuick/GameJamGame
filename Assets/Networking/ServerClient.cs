using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Assets.Networking.Identity;
using Assets.Networking.Messaging;
using Assets.Networking.Messaging.ConnectionHandlers;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Networking
{
    public class ServerClient : MonoBehaviour
    {
        private EstablishedConnection _establishedConnection;
        public void Awake()
        {
            var ipAddress = ServerClientInfo.IpAddress;
            var portNum = ServerClientInfo.PortNum;
            var endPoint = new IPEndPoint(long.Parse(ipAddress), portNum);
            var tcpClient = new TcpClient(endPoint);
            _establishedConnection = new EstablishedConnection(tcpClient.Client);
        }


        void Update()
        {
            if (Time.frameCount % 3 == 0)
            {
                var playerOwned = GameObject.FindObjectsOfType<BaseNetworkBehavior>().Where(t => t.PlayerOwned);
                _establishedConnection.SendUpdateBroadcast(playerOwned);
            }
        }

        public void RemoteFunctionCall(BaseNetworkBehavior instance, string functionName, object[] args)
        {
            var remoteCall = new RemoteMethodCall(instance.GetType(), instance.AssociatedNetworkIdentity, functionName, args);
            _establishedConnection.CallFunction(remoteCall);
        }
    }
}
