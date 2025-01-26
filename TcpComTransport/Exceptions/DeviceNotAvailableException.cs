namespace TcpComTransport.Exceptions
{
    [Serializable]
    internal class DeviceNotAvailableException : Exception
    {
        public DeviceNotAvailableException()
        {
        }

        public DeviceNotAvailableException(string? message) : base(message)
        {
        }

        public DeviceNotAvailableException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}