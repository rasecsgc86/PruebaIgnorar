using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class PerfilesFlexModel : IEntity
    {
        public int PerfilFlexibleId { get; set; }
        public string PerfilId { get; set; }
        public string PersonaId { get; set; }

        public string Comentario { get; set; }

        public bool maneja_udi { get; set; }
    }
}
