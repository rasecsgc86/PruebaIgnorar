

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class ArchivoTicketsModel
    {
        public int IdArchivoTicket { get; set; }
        public int TicketId { get; set; }

        public string NombreArchivo { get; set; }

        public string RutaArchivo { get; set; }

        public int IdEstatusTicket { get; set; }
       
    }
}
