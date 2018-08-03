using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Tickets
{
    public class ContarEstatusModel
    {
        public int Abiertos { get; set; }
        public int Terminados { get; set; }
        public int Espera { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string DescripcionEstatus { get; set; }
        public int CveEstatus { get; set; }
    }
}
