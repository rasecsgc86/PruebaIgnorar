using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* INDRA FJQP Implementacion de config Emisión Multiple */

namespace AM45Secure.Commons.Modelos.ConfigMultiple
{
    public class FiltroConfigMultiple
    {

        public List<ClientesConfig> ClientesList { get; set; }


        public IList<ProductosConfig> ProductosList { get; set; }

    }

    public class ClientesConfig
    {

        public string Clave { get; set; }
        public string Nombre { get; set; }
    }

    public class ProductosConfig
    {
        public string Clave { get; set; }
        public string Nombre { get; set; }
    }

    public class requestFiltro
    {
        public string Tkn { get; set; }
    }

}





