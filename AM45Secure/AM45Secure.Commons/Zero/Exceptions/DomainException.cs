using System;
using Zero.Utils;

namespace Zero.Exceptions
{
    public class DomainException : Exception
    {
        public string Mensaje { get; }
        public Exception ErrorOrigen { get; }

        public DomainException(string mensaje)
        {
            Mensaje = mensaje;
            Logger.Error(mensaje);
        }

        public DomainException(string mensaje, Exception exception)
        {
            ErrorOrigen = exception;
            Mensaje = mensaje;
            Logger.Error(mensaje, exception);
        }
    
    }
}
