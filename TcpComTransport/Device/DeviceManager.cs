using TcpComTransport.Interfaces.Connections;
using TcpComTransport.Interfaces.Device;

namespace TcpComTransport.DeviceManager
{
    internal class DeviceManager : IDeviceManager
    {
        public event EventHandler<bool> OnConnectionStatusChanged;
        public bool ConnectionStatus {  get; private set; }
        private readonly IConnection _connection;
        private DataHandler _dataHandler;
        

        public DeviceManager(IConnection connection) {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            _dataHandler = new DataHandler(connection);
        }
        public void Close()
        {
            _dataHandler.Stop();
            _connection.Disconnect();
            ConnectionStatus = false;
            OnConnectionStatusChanged.Invoke(this, false);
        }

        public void Open()
        {
            _connection.Connect();
            _dataHandler.Start();

            ConnectionStatus = true;
            OnConnectionStatusChanged.Invoke(this, true);
        }

        public void SendRequest()
        {
            throw new NotImplementedException();
        }

        public void SendRequestWithoutResponse()
        {
            throw new NotImplementedException();
        }

        private void SendBytes(IEnumerable<byte> message)
        {




        }
    }
}
