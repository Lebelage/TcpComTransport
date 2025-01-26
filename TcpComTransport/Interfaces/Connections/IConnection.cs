using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpComTransport.Interfaces.Connections
{
    internal interface IConnection : IDisposable
    {

        /// <summary>
        /// Событие, возникающее при получении новых данных.
        /// </summary>
        event Action<byte[]> DataReceived;

        /// <summary>
        /// Connect to the device.
        /// </summary>
        void Connect();

        /// <summary>
        /// Disconnect.
        /// </summary>
        void Disconnect();

        /// <summary>
        /// Send data to the specified port.
        /// </summary>
        /// <param name="data">Selected data</param>
        /// <returns></returns>
        void Send(byte[] data, int offset, int count);

        /// <summary>
        /// Reads data from port.
        /// </summary>
        /// <param name="buffer">Buffer that will be filled</param>
        /// <param name="offset">Prefered offset</param>
        /// <param name="count">Number of elements that will be buffered</param>
        /// <returns></returns>
        int Read(byte[] buffer,int offset, int count);

    }
}
