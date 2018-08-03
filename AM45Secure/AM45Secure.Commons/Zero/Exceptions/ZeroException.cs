using System;
using Zero.Utils;

namespace Zero.Exceptions
{
    public class ZeroException : Exception
    {
        public string Mensaje { get; set; }
        public Exception ErrorOrigen { get; }

        public ZeroException(string message)
        {
            Mensaje = message;
            Logger.Error(message);
        }

        public ZeroException(string message, Exception e)
        {
            Mensaje = message;
            ErrorOrigen = e;
            Logger.Error(message, e);
        }
    }
}
