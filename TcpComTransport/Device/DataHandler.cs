using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcpComTransport.Interfaces.Connections;
using TcpComTransport.Interfaces.Device;

namespace TcpComTransport.DeviceManager
{
    internal class DataHandler : IDataHandler
    {
        private IConnection _connection;
        private Task _workTask;

        private CancellationTokenSource _cts;


        public DataHandler(IConnection connection) {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public void ParseCollectedBytes(CancellationToken token) { }

        public void Start()
        {
            if (_connection == null) {
                throw new ArgumentNullException("No connection");
            }
            if(_workTask?.Status == TaskStatus.Running)
            {
                return;
            }

            _workTask = Task.Factory.StartNew(()=> ParseCollectedBytes(_cts.Token),_cts.Token);
        }

        public void Stop()
        {
            _cts.Cancel();

            try
            {
                _workTask.Wait(500);
            }

        }
    }
}
