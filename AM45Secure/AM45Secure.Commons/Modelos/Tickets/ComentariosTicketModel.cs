using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class ComentariosTicketModel : AbstractModel
    {
        public int ComentarioId { get; set; }
        public int TicketId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int PersonaId { get; set; }
        public string Comentario { get; set; }
        public int IdEstatusTicket { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string FechaComentario { get; set; }
        public string Estatus { get; set; }
        public RegistroTicketsModel RegistroTicketsUpdate { get; set; }
        public TicketsEstatusModel TicketsEstatusUpdate { get; set; }
        public int IdEstatusTicketActual { get; set; }
        public int CveEstatus { get; set; }
        public bool Cerrado { get; set; }
        public TicketModel TicketModelUpdate { get; set; }
        public ArchivoTicketsModel ArchivoTickets { get; set; }
        public int AseguradoraId { get; set; }
    }
}
