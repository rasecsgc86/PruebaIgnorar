using System;

namespace NotificacionesTickets.Exceptions
{
    public class MailException : Exception
    {
        public MailException(string ownMessage)
            : base(ownMessage)
        {
            OwnMessage = ownMessage;
        }

        public MailException(string ownMessage,
                                   Exception innerException) : base(ownMessage, innerException)
        {

        }

        public string OwnMessage { get; set; }
    }
}
