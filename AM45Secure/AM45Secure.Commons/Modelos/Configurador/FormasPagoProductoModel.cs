using AM45Secure.Commons.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class FormasPagoProductoModel : AbstractModel
    {

        [Required(FieldName = "FormaPagoID", Optional = true)]
        public int FormaPagoID { get; set; }

        [Required(FieldName = "ProductoID", Optional = true)]
        public int ProductoID { get; set; }
        public bool Predeterminado { get; set; }
    }
}
