using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("CorreosCopiaTickets")]
    public class CorreosCopiaTickets : IEntity
    {
        [IdColumn(identity:true)]
        public int IdCorreoCopiaTicket { get; set; }
        public int TicketId { get; set; }
        public string Correo { get; set; }
    }
}
