using AM45Secure.Commons.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class UsuariosPerfilModel : AbstractModel
    {
        [Required(FieldName = "PerfilUsuarioID", Optional = true)]
        public int PerfilUsuarioID { set; get; }
    }
}
