using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpComTransport.Message
{
    internal class DeviceRequest
    {
        public IEnumerable<byte> Command { get; set; }
        public bool WaitForResponse { get; set; }

        public Func<IEnumerable<byte>, bool> ResponseHandler { get; set; }
    }
}
