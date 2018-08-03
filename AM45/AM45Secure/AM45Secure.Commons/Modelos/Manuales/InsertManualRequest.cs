using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Manuales
{
   public class InsertManualRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }
        public string Descripcion { get; set; }

        public string Tkn { get; set; }

        public int IdCategoria { get; set; }

        public int IdCliente { get; set; }

        public int IdProducto { get; set; }

        public int IsUpdate { get; set; }
    }
}
