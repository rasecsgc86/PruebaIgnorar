using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("ComentariosTicket")]
    public class ComentariosTicket : IEntity
    {
        [IdColumn(identity:true)]
        public int ComentarioId { get; set; }
        public int TicketId { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int PersonaId { get; set; }
        public string Comentario { get; set; }
        public int IdEstatusTicket { get; set; }
    }
}
