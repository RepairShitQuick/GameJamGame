using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Assets.Networking.Identity;
using Assets.Networking.Messaging;
using Assets.Networking.Messaging.ConnectionHandlers;
using UnityEngine;

namespace Assets.Networking
{
    public class ServerClient : MonoBehaviour
    {
        private TcpClient _tcpClient;
        private EstablishedConnection _establishedConnection;
        private IPEndPoint _iPEndPoint;

        public ServerClient()
        {
            var ipAddress = ServerClientInfo.IpAddress;
            var portNum = ServerClientInfo.PortNum;
            var ipAddressObj = IPAddress.Parse(ipAddress);
            _iPEndPoint = new IPEndPoint(ipAddressObj, portNum);
            _tcpClient = new TcpClient();
        }

        public IEnumerable<string> GetMessages()
        {
            while (_establishedConnection.TcpConnectionHandler.Messages.Any())
            {
                yield return _establishedConnection.TcpConnectionHandler.Messages.Pop();
            }

            while (_establishedConnection.UdpConnectHandler.Messages.Any())
            {
                yield return _establishedConnection.UdpConnectHandler.Messages.Pop();
            }
        }

        public bool TryConnect()
        {
            if(!_tcpClient.Connected)
            {
                try
                {
                    _tcpClient.Connect(_iPEndPoint);
                }
                catch(Exception)
                {
                    return false;
                }
                return true;
            }
            else if(_establishedConnection == null)
            {
                _establishedConnection = new EstablishedConnection(_tcpClient.Client);
            }
            return true;
        }


        void Update()
        {
            if (Time.frameCount % 3 == 0 && TryConnect())
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
