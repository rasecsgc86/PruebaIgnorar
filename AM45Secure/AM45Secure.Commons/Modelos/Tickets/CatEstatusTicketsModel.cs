using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class CatEstatusTicketsModel
    {
        public int IdEstatusTicket { get; set; }
        public int CveEstatus { get; set; }
        public string Estatus { get; set; }
        public string Descripcion { get; set; }
    }
}
