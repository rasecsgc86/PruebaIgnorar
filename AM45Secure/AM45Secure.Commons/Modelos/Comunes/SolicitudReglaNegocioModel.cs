using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class SolicitudReglaNegocioModel
    {
        [Required(FieldName = "IdRegla")]
        public int IdRegla { set; get; }

        [Required(FieldName = "IdProducto")]
        public string IdProducto { set; get; }
        public int ProductoFlex { set; get; }
        public string IdCliente { set; get; }
        public string IdTipoVehiculo { set; get; }
        public string EstadoVehiculo { set; get; }
        public string Servicio { set; get; }
        public string TipoVehiculo { set; get; }
        public string Modelo { set; get; }
        public string Marca { set; get; }
        public string Submarca { set; get; }
        public string Estado { set; get; }
        public int AseguradoraId { set; get; }
        public int Usuario { set; get; }
        public string TipoArrendamiento { set; get; }
        public string IdCobertura { set; get; }
    }
}