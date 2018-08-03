using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Utils;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class SolicitudPrimaCotizacion
    {
        [DataColumn]
        public int SolicitudId { set; get; }

        [DataColumn]
        public int Numero { set; get; }

        [DataColumn]
        public int CotizacionId { set; get; }

        [DataColumn]
        public bool InfoComplementaria { set; get; }

        [DataColumn]
        public string TipoArrendamiento { set; get; }
    }
}