using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Cotizador
{
    public class DatosClienteModel
    {
        public CotizanteModel Cotizante { get; set; }
        public ClientesModel Cliente { get; set; }
        public double ProductoFlex { get; set; }
        public ProductoModel Producto { get; set; }
        public ElementoModel TipoArrendamiento { get; set; }
        public AgenciasModel Agencia { get; set; }
        public ValoresReglaModel TipoArrendamientoRegla { get; set; }
    }
}