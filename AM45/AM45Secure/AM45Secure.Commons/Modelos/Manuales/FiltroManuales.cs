using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Manuales
{
    public class FiltroManuales
    {


        public List<Clientes> ClientesList { get; set; }


        public IList<Productos> ProductosList { get; set; }

    }

  public  class Clientes
    {
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }

   public class Productos
    {
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }


    public class requestFiltro
    {
        public string Tkn { get; set; }
    }
}
