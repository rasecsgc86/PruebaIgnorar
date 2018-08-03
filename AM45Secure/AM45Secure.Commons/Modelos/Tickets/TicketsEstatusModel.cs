using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class TicketsEstatusModel : AbstractModel
    {
        public int IdTicketEstatus { get; set; }
        public int TicketId { get; set; }
        public int IdEstatusTicket { get; set; }
        public int PersonaId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NombreArchivoTicketCerrado { get; set; }
        public string RutaArchivoTicketCerrado { get; set; }
        public bool Activo { get; set; }
        public string Estatus { get; set; }
    }
}