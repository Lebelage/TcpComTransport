using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpComTransport.Interfaces;
using TcpComTransport.Network;

namespace TcpComTransport.Transports
{
    internal class TcpTransport : ITransport
    {
        private ConnectionParameters _connectionParameters;
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;

        public bool IsConnected { get; private set; }

        public event EventHandler<byte[]> DataReceived;

        public TcpTransport(ConnectionParameters connectionParameters)
        {
            _connectionParameters = connectionParameters ?? throw new ArgumentNullException(nameof(connectionParameters));
            
        }

        ~TcpTransport() { 
            Dispose();
        }


        public void Close()
        {
            if(_networkStream != null)
                _networkStream.Close(); 

            if (IsConnected)
                _tcpClient.Close();
           
        }

        public void Open()
        {
            try {

                _tcpClient = new TcpClient();
                _tcpClient.Connect(_connectionParameters._endPoint);
                _networkStream = _tcpClient.GetStream();
                IsConnected = _tcpClient.Connected;               
            }
            catch { 
                IsConnected = _tcpClient.Connected;
            }
        }  

        public void Send(byte[] data, int offset, int count)
        {
            if (!IsConnected)
                throw new InvalidOperationException("Client is not connected");

            _networkStream.Write(data, offset, count);
        }

        public int Read(byte[] data, int offset)
        {
            try {
                byte[] buffer = new byte[1024*4];
                
                if(_networkStream.CanRead && IsConnected)
                {
                    if (_networkStream.DataAvailable)
                    {
                        int bytesReaded = _networkStream.Read(buffer, 0, buffer.Length);
                        if (bytesReaded > 0)
                        {
                            Array.Copy(buffer, 0, data, offset, bytesReaded);

                            return bytesReaded;
                        }
                    }
                }              
            }
            catch { 
                return 0;
            }
            return 0;
        }

        public void Dispose()
        {
            Close();
        }
    }
}
