using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpComTransport.Interfaces
{
    internal interface ITransport : IDisposable
    {
        event EventHandler<byte[]> DataReceived;
        bool IsConnected { get; }

        void Open();

        void Close();

        int Read(byte[] data, int offset);

        void Send(byte[] data, int offset, int count);

    }
}
