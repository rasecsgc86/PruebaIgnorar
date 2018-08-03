using System;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class AmVersionSistemaModel
    {
        public int IdVersion { get; set; }
        public string Version { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public string Ot { get; set; }
    }
}