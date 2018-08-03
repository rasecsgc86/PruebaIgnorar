using AM45Secure.Commons.Modelos.Comunes;
using System.Collections.Generic;

namespace AM45Secure.Commons.Modelos.Comparador
{
    public class ComparadorModel
    {
        /********************************************************************
         * DatosCotizacionModel - Recibe los parametros
         */
        public DetalleSolicitudCotizacionModel DetalleSolicitudCotizacionModel { get; set; }
        public NeCotizacionModel ListNeCotizacion { get; set; }
        public IList<AseguradorasProductoModel> AseguradorasProducto { get; set; }
        public string Errores { get; set; } // FJQP


    }
}