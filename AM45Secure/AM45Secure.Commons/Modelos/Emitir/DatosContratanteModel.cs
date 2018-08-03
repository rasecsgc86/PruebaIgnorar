using System;
using AM45Secure.Commons.Utils;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Emitir
{
    public class DatosContratanteModel
    {
        [DataColumn]
        public string TipoPersona { set; get; }

        [DataColumn]
        [Required(FieldName = "Nombre")]
        public string Nombre { set; get; }

        [DataColumn]
        [Required(FieldName = "Apellido Paterno", Optional = true)]
        public string Paterno { set; get; }

        [DataColumn]
        [Required(FieldName = "Apellido Materno", Optional = true)]
        public string Materno { set; get; }

        [DataColumn]
        [Required(FieldName = "Fecha de Nacimiento")]
        public string FechaNacimiento { set; get; }

        [DataColumn]
        [Required(FieldName = "RFC")]
        public string RFC { set; get; }

        [DataColumn]
        [Required(FieldName = "Nacionalidad")]
        public string Nacionalidad { set; get; }

        [DataColumn]
        [Required(FieldName = "Género")]
        public string Genero { set; get; }

        [DataColumn]
        [Required(FieldName = "Correo Electrónico")]
        public string CorreoElectronico { set; get; }

        [DataColumn]
        [Required(FieldName = "Teléfono")]
        public string Telefono { set; get; }

        [DataColumn]
        public string Telefono2 { set; get; }
    }
}