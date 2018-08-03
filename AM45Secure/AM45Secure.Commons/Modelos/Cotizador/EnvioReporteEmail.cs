using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Cotizador
{
  public  class EnvioReporteEmail
    {



        public int cotizacionId { get; set; }
        public bool flexible { get; set; }
        public int paqueteId { get; set; }
        public int solicitudId { get; set; }
        public int numero { get; set; }

        public string tkn { get; set; }
        public string destinatarios { get; set; }
    
    }
}
