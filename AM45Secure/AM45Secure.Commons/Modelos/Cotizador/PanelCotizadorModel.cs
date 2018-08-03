using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Cotizador
{
    public class PanelCotizadorModel : AbstractModel
    {
        [Required(FieldName = "Producto")]
        public int IdProducto { set; get; }

        [Required(FieldName = "Tipo de Unidad")]
        public int IdTipoVehiculo { set; get; }

        [Required(FieldName = "AntiguedadId")]
        public int IdCondicionVehiculo { set; get; }

        [Required(FieldName = "Servicio")]
        public string IdTipoServicioVehiculo { set; get; }

        public string Submarca { set; get; }

        [Required(FieldName = "Aseguradoras", Optional = true)]
        public IList<AseguradoraModel> Aseguradoras { set; get; }

        [Required(FieldName = "Coberturas", Optional = true)]
        public IList<CoberturaModel> Coberturas { set; get; }

        [Required(FieldName = "UDI")]
        public string UDI { set; get; }

        public IList<ValoresReglaModel> UdiList { set; get; }

        public string TipoCarga { set; get; }
    }
}