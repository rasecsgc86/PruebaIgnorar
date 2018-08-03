using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class SolicitudPasajerosModel
    {
        [Required(FieldName = "Id de Tipo Unidad")]
        public string TipoId { set; get; }

        [Required(FieldName = "Tipo Unidad")]
        public string Tipo { set; get; }

        [Required(FieldName = "Producto")]
        public string Producto { set; get; }

        [Required(FieldName = "Es Producto Flexible?")]
        public bool EsFlexible { set; get; }

        [Required(FieldName = "Servicio")]
        public string Servicio { set; get; }

        [Required(FieldName = "Modelo")]
        public int Modelo { set; get; }

        [Required(FieldName = "Armadora")]
        public string Marca { set; get; }

        [Required(FieldName = "Submarca")]
        public string Submarca { set; get; }
        [Required(FieldName = "Versión")]
        public string Version { set; get; }
    }
}