using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("vwTICSeleccionarComentariosTickets")]
    public class VwTicSeleccionarComentariosTickets : IEntity
    {
        public int ComentarioId { get; set; }
        public string Comentario { get; set; }
        [Column("PersonaID")]
        public int PersonaId { get; set; }
        public int TicketId { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Dias { get; set; }
        public int Horas { get; set; }
        public int Minutos { get; set; }
        public string Estatus { get; set; }
    }
}
