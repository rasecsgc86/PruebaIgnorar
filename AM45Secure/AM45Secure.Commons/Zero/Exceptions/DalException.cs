using System;
using Zero.Utils;

namespace Zero.Exceptions
{
    public class DalException : Exception
    {
        public string Mensaje { get; }
        public Exception ErrorOrigen { get; }

        public DalException(string mensaje)
        {
            Mensaje = mensaje;
            Logger.Error(mensaje);
        }

        public DalException(string mensaje, Exception exception)
        {
            ErrorOrigen = exception;
            Mensaje = mensaje;
            Logger.Error(mensaje, exception);
        }
    
    }
}
