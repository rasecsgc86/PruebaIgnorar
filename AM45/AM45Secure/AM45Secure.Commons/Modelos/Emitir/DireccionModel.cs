using AM45Secure.Commons.Utils;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Emitir
{
    public class DireccionModel
    {
        [DataColumn]
        [Required(FieldName = "Pais")]
        public string Pais { set; get; }

        [DataColumn]
        [Required(FieldName = "Estado")]
        public string Estado { set; get; }

        [DataColumn]
        public string EstadoId { set; get; }

        [DataColumn]
        public bool ValidaEstado { set; get; }

        [DataColumn]
        [Required(FieldName = "Delegación")]
        public string Delegacion { set; get; }

        [DataColumn]
        public string DelegacionId { set; get; }

        [DataColumn]
        [Required(FieldName = "Calle")]
        public string Calle { set; get; }

        [DataColumn]
        [Required(FieldName = "Número")]
        public string Numero { set; get; }

        [DataColumn]
        [Required(FieldName = "Colonia")]
        public string Colonia { set; get; }

        [DataColumn]
        [Required(FieldName = "Código Postal")]
        public string CodigoPostal { set; get; }
    }
}