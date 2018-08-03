using AM45Secure.Commons.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Manuales
{
   public class ManualRequest: AbstractModel
    {


        public int Cliente { get; set; }

        public int Producto { get; set; }

        public int Categoria { get; set; }
        public string Texto { get; set; }

        public int Todo { get; set; }
  
    }
}
