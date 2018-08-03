using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Tickets
{
    [Table("vwContarEstatusReporteTickets")]
    public class VwContarEstatusReporteTickets : IEntity
    {
        public int Abiertos { get; set; }
        public int Terminados { get; set; }
        public int Espera { get; set; }
    }
}
