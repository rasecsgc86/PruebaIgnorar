using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class PersonaResponsableModel
    {
        public int PersonaId { set; get; }
        public string Nombre { set; get; }
        public string Paterno { set; get; }
        public string Materno { set; get; }
        public string Mail { set; get; }
        public decimal Tipo { set; get; }
        public string MailResponsable { get; set; }
        public string MailEscalemiento1 { get; set; }
        public string MailEscalamiento2 { get; set; }
    }
}
