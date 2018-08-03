using AM45Secure.Commons.Modelos.Comunes;
using System;

namespace AM45Secure.Commons.Modelos.Cotizador
{
    public class DatosCotizacionModel
    {
        public ClaveValorModel Plazo { get; set; }
        public string Paquete { get; set; }
        public ClaveValorModel Udi { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FinVigencia { get; set; }
        public string Udis { get; set; }
        public RegionCodigoPostalModel CP { set; get; }
        public RegionCodigoPostalModel Estado { get; set; }
    }
}