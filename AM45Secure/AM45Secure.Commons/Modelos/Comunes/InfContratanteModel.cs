using System;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class InfContratanteModel
    {
        public string PersonaId { get; set; }
        public string TipoPersona { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string RFC { get; set; }
        public ElementoModel Nacionalidad { get; set; }
        public string Genero { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }
    }
}