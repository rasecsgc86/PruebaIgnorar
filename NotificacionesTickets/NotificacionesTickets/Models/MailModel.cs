using System.Collections.Generic;

namespace NotificacionesTickets.Models
{
    public class MailModel
    {
        public List<string> MailsTo { set; get; }
        public List<string> MailsCc { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }
        public List<string> AttachementsPathsFiles { set; get; }
    }
}
