using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using TcpComTransport.Interfaces;
using System.Net.Sockets;

namespace TcpComTransport.Transports
{
    internal class ComTransport : ITransport
    {
        private readonly SerialPort _serialPort;
        public bool IsConnected => _serialPort.IsOpen;

        public event EventHandler<byte[]> DataReceived;

        public ComTransport(string portName,int baudRate)
        {
            _serialPort = new SerialPort(portName, baudRate);
            _serialPort.DataReceived += OnComTransportDataReceived;

        }

        ~ComTransport() {

            _serialPort.DataReceived -= OnComTransportDataReceived;
            Dispose();           
        }

        void OnComTransportDataReceived(object sender,SerialDataReceivedEventArgs e)
        {
            try
            {
                byte[] data = new byte[_serialPort.BytesToRead];
                _serialPort.Read(data, 0, data.Length);
            }
            catch { }

        }

        public void Close()
        {
            if(IsConnected) 
                _serialPort.Close();
        }

        public int Read(byte[] data, int offset)
        {
            try
            {
                byte[] buffer = new byte[1024 * 4];             
                if (_serialPort.IsOpen)
                {
                int bytesReaded = _serialPort.Read(buffer, 0, buffer.Length);
                    if (bytesReaded > 0)
                    {
                        Array.Copy(buffer, 0, data, offset, bytesReaded);
                        return bytesReaded;
                    }
                    
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }

        public void Open()
        {
            if (IsConnected != true)
               _serialPort.Open();
            
        }

        public void Send(byte[] data, int offset, int length)
        {
            if(IsConnected)
                _serialPort.Write(data, offset, length);
        }
        public void Dispose()
        {
            Close();
            _serialPort.Dispose();
        }
    }
}
