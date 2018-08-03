using System;
using AM45Secure.Commons.Utils;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Emitir
{
    public class ComplementariaModel
    {
        [DataColumn]
        [Required(FieldName = "Pais de Nacimiento")]
        public string PaisNacimiento { set; get; }

        [DataColumn]
        [Required(FieldName = "Entidad Federativa de Nacimiento")]
        public string EntidadNacimiento { set; get; }

        [DataColumn]
        [Required(FieldName = "Docto de Identificación")]
        public string DoctoIdentificacion { set; get; }

        [DataColumn]
        [Required(FieldName = "No. de Docto")]
        public string NumDocto { set; get; }

        [DataColumn]
        [Required(FieldName = "Oficina que expide")]
        public string OficinaExpide { set; get; }

        [DataColumn]
        public string CURP { set; get; }

        [DataColumn]
        public string SerieFiel { set; get; }

        [DataColumn]
        [Required(FieldName = "Profesión")]
        public string Profesion { set; get; }

        [DataColumn]
        [Required(FieldName = "Ocupación")]
        public string Ocupacion { set; get; }

        [DataColumn]
        [Required(FieldName = "Actividad o giro de negocio")]
        public string GiroNegocio { set; get; }

        [DataColumn]
        [Required(FieldName = "Mando en el gobierno")]
        public string MandoGobierno { set; get; }

        [DataColumn]
        [Required(FieldName = "Descripción del Cargo", Optional = true)]
        public string DescripcionCargo { set; get; }

        [DataColumn]
        public string RegimenFiscal { set; get; }

        [DataColumn]
        public string ActividadPrincipal { set; get; }

        [DataColumn]
        public string ApoderadoLegal { set; get; }

        [DataColumn]
        public int FolioMercatil { set; get; }

        [DataColumn]
        public string EstructuraCoorporativa { set; get; }

        [DataColumn]
        public string InformacionAccionistas { set; get; }

        [DataColumn]
        public string ObjetivoSocial { set; get; }

        [DataColumn]
        public string NombreFuncionario { set; get; }

        [DataColumn]
        public DateTime FechaNacimientoApoderado { set; get; }

        [DataColumn]
        public int PaisNacimientoApoderado { set; get; }

        [DataColumn]
        public string EntidadApoderado { set; get; }
    }
}