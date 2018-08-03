using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class TicketsDatosContactosModel
    {
        public int TicketId { get; set; }
        public int IdAgencia { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
