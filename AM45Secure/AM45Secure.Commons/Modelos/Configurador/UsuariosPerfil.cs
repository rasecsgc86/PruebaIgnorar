using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class UsuariosPerfil :IEntity
    {
        public int PersonaID { get; set; }
        public int PerfilUsuarioID { get; set; }
        public string Nombre { get; set; }

    }
}
