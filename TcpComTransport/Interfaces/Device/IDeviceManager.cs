using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpComTransport.Interfaces.Device
{
    internal interface IDeviceManager
    {
        void Open();

        void Close();

        void SendRequest();

        void SendRequestWithoutResponse();
    }
}
