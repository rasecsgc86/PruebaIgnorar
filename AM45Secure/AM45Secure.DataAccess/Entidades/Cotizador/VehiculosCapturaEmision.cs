using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.DataAccess.Entidades.Cotizador
{
    class VehiculosCapturaEmision
    {
        public int idNoCotizacion { get; set; }
        public int idNoConsec { get; set; }
        public string sNoSerie { get; set; }
        public string sNoMotor { get; set; }
        public string sPlacas { get; set; }
        public string sContrato { get; set; }
        public int iEstatusReg { get; set; }
        public string sConductor { get; set; }
        public string sSolicitud { get; set; }
        public string sQLTS { get; set; }
        public string sCotizacion { get; set; }
        public string sPolizaQLT { get; set; }
    }
}
