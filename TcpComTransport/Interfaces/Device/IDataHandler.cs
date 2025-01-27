using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TcpComTransport.Interfaces.Device
{
    internal interface IDataHandler
    {
        /// <summary>
        /// Start parsing input data from port of choosen kind of connection.
        /// </summary>
        void Start();

        /// <summary>
        /// Stop parsing task
        /// </summary>
        void Stop();
    }
}
