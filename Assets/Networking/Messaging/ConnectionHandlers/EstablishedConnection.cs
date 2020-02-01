using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Assets.Networking.Identity;

namespace Assets.Networking.Messaging.ConnectionHandlers
{
    public class EstablishedConnection
    {
        public readonly TcpConnectionHandler TcpConnectionHandler;
        public readonly UdpConnectionHandler UdpConnectHandler;

        public EstablishedConnection(Socket socket)
        {
            TcpConnectionHandler = new TcpConnectionHandler(socket);
            UdpConnectHandler = new UdpConnectionHandler(socket);
        }

        public void SendUpdateBroadcast(IEnumerable<INetworkEntity> objects)
        {
            foreach (var value in objects)
            {
                var message = MessageBuilder.GetMessage(value, value.AssociatedNetworkIdentity, MessageStrategy.NoHeader);
                UdpConnectHandler.SendMessage(message);
            }
        }

        public void CallFunction(RemoteMethodCall call)
        {
            var message = MessageBuilder.GetMessage(call, call.AssociatedNetworkGuid);
            UdpConnectHandler.SendMessage(message);
        }

        public void CreateObjectOverNetwork(object obj)
        {
            var message = MessageBuilder.GetMessage(obj, Guid.Empty, MessageStrategy.Header);
            TcpConnectionHandler.SendMessageSync(message);
        }
    }
}
