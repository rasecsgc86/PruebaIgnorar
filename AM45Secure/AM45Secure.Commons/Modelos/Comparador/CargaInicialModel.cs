using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Comparador
{
    public class CargaInicialModel
    {
        public SolicitudCotizacionModel SolicitudCotizacion { get; set; }
        public IList<FormasPagoProductoModel> FormasPagoProducto { get; set; }
        public string NotasImportantes { get; set; }
   }
}
