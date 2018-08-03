using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("CatOrigenTickets")]
    public class CatOrigenTickets : IEntity
    {
        public int IdOrigenTicket { get; set; }
        public string OrigenTicket { get; set; }
    }
}
