using AM45Secure.Commons.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class PerfilesFlexibleModel : AbstractModel
    {
        [Required(FieldName = "PerfilId", Optional = true)]

        public int PerfilFlexibleId { get; set; }
        public int PerfilId { get; set; }
        public string PersonaId { get; set; }
        
        public string Comentario { get; set; }

        public bool manejaUdi { get; set; }
    }
}
