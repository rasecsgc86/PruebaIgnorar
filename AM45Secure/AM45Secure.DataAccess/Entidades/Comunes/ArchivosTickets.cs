using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("ArchivosTickets")]
    public class ArchivosTickets : IEntity
    {
        [IdColumn(Identity = true)]
        public int IdArchivoTicket { get; set; }
        public int? TicketId { get; set; }
        public string NombreArchivo { get; set; }
        public string RutaArchivo { get; set; }
        public int? IdEstatusTicket { get; set; }
    }
}
