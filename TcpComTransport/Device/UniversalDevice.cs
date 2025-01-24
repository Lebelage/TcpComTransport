using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpComTransport.Interfaces;

namespace TcpComTransport.Device
{
    internal class UniversalDevice : IDisposable
    {
        private readonly ITransport _transport;
        private Task _workTask;

        private byte[] innerBuffer = new byte[1024 * 1024];
        

        public UniversalDevice(ITransport transport) { 
        
            _transport = transport;

        }

        public void Start(ITransport transport, CancellationToken token)
        {
            if(transport == null)
            {
                throw new ArgumentNullException(nameof(transport) + "is null");
            }

            if (_workTask?.Status == TaskStatus.Running)
                return;

            _workTask = Task.Factory.StartNew(() => ReadResponse(), token);
        }

        private void ReadResponse()
        {
            int bytesReaded = 0;
            try
            {
                while (true)
                {
                    bytesReaded += _transport.Read(innerBuffer, bytesReaded);


                }
            }
            catch 
            { }
        }

        private void FlushBuffer(byte[] buffer) {
        
        
        }


        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
