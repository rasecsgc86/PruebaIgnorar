using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class MailModel
    {
        public List<string> MailsTo { set; get; }
        public List<string> MailsCc { set; get; }
        public string Subject { set; get; }
        public string Body { set; get; }
        public List<string> AttachementsPathsFiles { set; get; }
        public string AttachmentName { set; get; }
    }
}
