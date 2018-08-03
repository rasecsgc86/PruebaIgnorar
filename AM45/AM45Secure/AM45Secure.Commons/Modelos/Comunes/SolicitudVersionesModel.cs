using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{ //{"vehiculoModel":{"Tipo":"CAMIONETA","Servicio":"50","Modelo":2015,"Marca":"SAAB","Submarca":"SPRINTER"}}:

    public class SolicitudVersionesModel
    {
        [Required(FieldName = "Tipo Unidad")]
        public string Tipo { set; get; }

        [Required(FieldName = "Servicio")]
        public string Servicio { set; get; }

        [Required(FieldName = "Modelo")]
        public int Modelo { set; get; }

        [Required(FieldName = "Marca")]
        public string Marca { set; get; }

        [Required(FieldName = "Submarca")]
        public string Submarca { set; get; }
        [Required(FieldName = "Filtro de Versiones")]
        public int Filtro { set; get; }
    }
}