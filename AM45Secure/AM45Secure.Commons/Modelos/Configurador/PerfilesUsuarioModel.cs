using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class PerfilesUsuarioModel : IEntity
    {
        public int PerfilUsuarioID { get; set; }
        public string Nombre { get; set; }
        public int PerfilPadreID { get; set; }
        public bool Activo { get; set; }
        public string OpcionAcceso { get; set; }
        public string OpcionAccesoB { get; set; }
    }
}
