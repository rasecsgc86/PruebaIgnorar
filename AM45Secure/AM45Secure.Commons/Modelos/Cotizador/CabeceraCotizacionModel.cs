using AM45Secure.Commons.Modelos.Comunes;
using System;
using AM45Secure.Commons.Recursos;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Cotizador
{
    public class CabeceraCotizacionModel : AbstractModel
    {
        public int IdSolicitud { set; get; }
        [Required(MessageRequired = "REQ_01_00")]
        public DatosClienteModel Cliente { set; get; }
        [Required(MessageRequired = "REQ_01_01")]
        public DatosVehiculoModel Vehiculo { set; get; }
        [Required(MessageRequired = "REQ_01_02")]
        public DatosCotizacionModel Cotizacion { set; get; }
        [Required(FieldName = "", Optional = true)]
        public PanelCotizadorModel Panel { set; get; }

    }
}
