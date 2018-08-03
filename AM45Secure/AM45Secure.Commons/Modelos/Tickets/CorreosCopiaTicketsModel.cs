using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class CorreosCopiaTicketsModel
    {
        public int IdCorreoCopiaTicket { get; set; }
        public int TicketId { get; set; }
        public string Correo { get; set; }
    }
}
