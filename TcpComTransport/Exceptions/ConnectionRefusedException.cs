namespace TcpComTransport.Exceptions
{
    [Serializable]
    internal class ConnectionRefusedException : Exception
    {
        public ConnectionRefusedException()
        {
        }

        public ConnectionRefusedException(string? message) : base(message)
        {
        }

        public ConnectionRefusedException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}