using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Assets.Networking.Messaging.ConnectionHandlers
{
    public class TcpConnectionHandler : IConnectionHandler
    {
        private const int IntSize = sizeof(int);
        private readonly Socket _socket;
        public Stack<string> Messages { get; }
        public TcpConnectionHandler(Socket socket)
        {
            _socket = socket;
            Messages = new Stack<string>();
            new Thread(ListenOverSocket).Start();
        }

        private void ListenOverSocket()
        {
            while (true && _socket.Connected)
            {
                var messageSize = GetMessageSize();
                var messageArray = new byte[messageSize];
                _socket.Receive(messageArray);
                Messages.Push(Encoding.UTF8.GetString(messageArray));
            }
        }

        private int GetMessageSize()
        {
            var bytes = new byte[IntSize];
            var size = _socket.Receive(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        public void SendMessageSync(byte[] arr)
        {
            _socket.Send(arr);
        }
    }
}
