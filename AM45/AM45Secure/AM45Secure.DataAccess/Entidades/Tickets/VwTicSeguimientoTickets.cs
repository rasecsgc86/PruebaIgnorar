using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("vwTICSeguimientoTickets")]
    public class VwTicSeguimientoTickets : IEntity
    {
        public int TicketId { get; set; }
        [Column("PersonaID")]
        public int PersonaId { get; set; }
        public int TipoId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public string DescripcionTicket { get; set; }
        public string NombrePer { get; set; }
        public string PaternoPer { get; set; }
        public string MaternoPer { get; set; }
        public string DescripcionEstatus { get; set; }
        public int TiempoAtencion { get; set; }
        public int CveEstatus { get; set; }
        public int IdEstatusTicket { get; set; }
        public int IdTicketEstatus { get; set; }
        public int IdCliente { get; set; }
        public string Duenio { get; set; }
        public int UsuarioId { get; set; }
    }
}
