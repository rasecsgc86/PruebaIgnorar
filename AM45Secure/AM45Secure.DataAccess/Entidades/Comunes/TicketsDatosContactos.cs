using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("TicketsDatosContactos")]
    public class TicketsDatosContactos : IEntity
    {
        [IdColumn(identity:false)]
        public int TicketId { get; set; }
        public int IdAgencia { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
