using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class GuardaSeguimientoTicketsModel : AbstractModel
    {
        public string TicketId { get; set; }
        public string IdEstatusTicket { get; set; }
        public string IdTicketEstatus { get; set; }
        public string PersonaId { get; set; }
    }
}