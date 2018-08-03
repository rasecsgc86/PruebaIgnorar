using AM45Secure.Commons.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class TextoAuxiliarUsoVehiculoModel : AbstractModel
    {
        [Required(FieldName = "ClienteID", Optional = true)]
        public int ClienteID { get; set; }
        public int TipoVehiculoID { get; set; }
        public string Descripcion { get; set; }
    }
}
