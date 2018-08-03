using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificacionesTickets.Models
{
    public class EscalamientoModel
    {
        public int IdEscalamientoTicket { get; set; }
        public int TicketId { get; set; }
        public DateTime FechaEscalamiento { get; set; }
        public int TipoEscalamiento { get; set; }
        public int CantidadFinesSemana { set; get; }
    }
}
