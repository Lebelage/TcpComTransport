using TcpComTransport.Interfaces.Connections;
using System.IO.Ports;
using TcpComTransport.Exceptions;

namespace TcpComTransport.Connections
{
    internal class ComConnection : IConnection
    {
        private readonly SerialPort _serialPort;
        public bool _isConnected { get; private set; }
        public int _readTimeout { get; set; }

        public event Action<byte[]> DataReceived;

        public ComConnection(SerialPort serialPort, int readTimeout)
        {
            _serialPort = serialPort;
            _readTimeout = readTimeout;
            _isConnected = false;
        }

        ~ComConnection() { 
            Dispose();
        }

        public void Connect()
        {
            try {
                if(_isConnected == false)
                {
                    _serialPort.Open();
                    _serialPort.ReadTimeout = _readTimeout;
                    _isConnected = true;                   
                }
            }
            catch (UnauthorizedAccessException uaex)
            {
                throw new ConnectionRefusedException(uaex.Message);
            }
            catch (IOException ioex)
            {
                if (ioex.Message.Contains("отклонил") || ioex.Message.Contains("rejected"))
                    throw new ConnectionRefusedException(ioex.Message);

                throw ioex;
            }
        }

        public void Disconnect()
        {
            if(_isConnected == true)
            {
                _serialPort?.Close();
            }
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return _serialPort.Read(buffer, offset, count);
        }

        public void Send(byte[] data, int offset, int count)
        {
            try {
                if(_isConnected == true)
                {
                    _serialPort.Write(data, offset, count);
                }

            }
            catch { 
            
            }
        }

        public void Dispose()
        {
            Disconnect();
            if (_serialPort != null)
            {
                _serialPort.Dispose();
            }
        }
    }
}
