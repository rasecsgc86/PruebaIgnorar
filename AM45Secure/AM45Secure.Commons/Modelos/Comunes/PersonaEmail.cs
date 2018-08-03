using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Comunes
{
  public  class PersonaEmail:AbstractModel
    {

        public int PersonaId { set; get; }
        public string Nombre { get; set; }

        public string Correo { get; set; }

        public string Usuario { get; set; }
    }
}
