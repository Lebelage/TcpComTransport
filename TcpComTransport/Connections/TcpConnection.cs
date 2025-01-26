using System;
using System.Net.Sockets;
using TcpComTransport.Connections.Helps;
using TcpComTransport.Exceptions;
using TcpComTransport.Interfaces.Connections;

namespace TcpComTransport.Connections
{
    internal class TcpConnection : IConnection
    {
        public readonly ConnectionParameters _connectionParameters;
        private TcpClient _tcpClient;

        public NetworkStream _networkStream { get; private set; }

        public bool _isConnetcted { get; private set; } 

        public event Action<byte[]> DataReceived;

        public TcpConnection(ConnectionParameters connectionParameters)
        {
            _connectionParameters = connectionParameters;
            _isConnetcted = false;
        }

        ~TcpConnection()
        {
            Dispose();
        }
        public void Connect()
        {
            try
            {
                _tcpClient = new TcpClient();
                _tcpClient.Connect(_connectionParameters._ipEndPoint);
                _networkStream = _tcpClient.GetStream();
                _isConnetcted = true;
            }
            catch(SocketException ex)
            {
                switch (ex.SocketErrorCode)
                {
                    case SocketError.TimedOut:
                        throw new DeviceNotAvailableException(ex.Message);

                    case SocketError.ConnectionRefused:
                        throw new ConnectionRefusedException(ex.Message);

                    default:
                        throw ex;
                }
            }
        }

        public void Disconnect()
        {
            if(_isConnetcted == true)
            {
                _networkStream?.Close();
                _tcpClient?.Close();
                _isConnetcted = false;
            }
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            try
            {
                if(_networkStream != null && _networkStream.CanRead == true)
                {
                    if (_networkStream.DataAvailable)
                    {
                        return _networkStream.Read(buffer, offset, count);
                    }                    
                }
                return 0;
            }
            catch (System.IO.IOException ex)
            {
                var inner_ex = ex.InnerException as SocketException;
                if (inner_ex?.SocketErrorCode == SocketError.TimedOut)
                {
                    throw new TimeoutException(ex.Message);
                }
                throw ex;
            }
        }

        public void Send(byte[] data, int offset, int count)
        {
            try
            {
                if (_isConnetcted == true)
                {
                    if (_networkStream != null && _networkStream.CanWrite == true)
                    {
                        _networkStream.Write(data, offset, count);
                    }
                }
            }
            catch { }
        }
        public void Dispose()
        {
            Disconnect();
        }
    }
}
