using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("TicketsEstatus")]
    public class TicketsEstatus : IEntity
    {
        [IdColumn(Identity = true)]
        public int IdTicketEstatus { get; set; }
        public int TicketId { get; set; }
        public int IdEstatusTicket { get; set; }
        [Column("PersonaID")]
        public int PersonaId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string NombreArchivoTicketCerrado { get; set; }
        public string RutaArchivoTicketCerrado { get; set; }
        public bool Activo { get; set; }

        
    }
}
