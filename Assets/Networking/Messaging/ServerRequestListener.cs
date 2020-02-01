using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Networking.Messaging
{
    public class ServerRequestListener : BaseListener
    {
        private Server _server;

        public void Start()
        {
            _server = FindObjectOfType<Server>();
        }

        public override IEnumerable<string> GetMessages() => _server.GetMessages();
    }
}
