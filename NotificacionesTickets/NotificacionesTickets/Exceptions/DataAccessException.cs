using System;

namespace NotificacionesTickets.Exceptions
{
    public class DataAccessException : Exception
    {
        public DataAccessException()
        {
            
        }

        public DataAccessException(string ownMessage)
            : base(ownMessage)
        {
            OwnMessage = ownMessage;
        }

        public DataAccessException(string ownMessage,
                                   Exception innerException) : base(ownMessage, innerException)
        {
            
        }

        public string OwnMessage { get; set; }
    }
}
