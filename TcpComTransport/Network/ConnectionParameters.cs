using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TcpComTransport.Network
{
    internal class ConnectionParameters
    {
        public IPEndPoint _endPoint { get; private set; }

        public ConnectionParameters(IPAddress address, int port)
        {
            _endPoint = new IPEndPoint(address, port);
        }

        public ConnectionParameters(long address, int port)
        {
            _endPoint = new IPEndPoint(address, port);
        }

    }
}
