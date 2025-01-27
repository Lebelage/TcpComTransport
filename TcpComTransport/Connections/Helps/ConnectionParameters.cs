

using System.Net;

namespace TcpComTransport.Connections.Helps
{
    internal class ConnectionParameters
    {
        public IPEndPoint _ipEndPoint { get; }

        public ConnectionParameters(IPAddress ipAdress, int port) : 
            this(new IPEndPoint(ipAdress, port)) { }

        public ConnectionParameters(IPEndPoint ipEndPoint)
        {
            _ipEndPoint = ipEndPoint;
        }
    }
}
