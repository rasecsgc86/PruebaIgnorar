using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Manuales
{
  public  class ManualesModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Url { get; set; }

        public string Descripcion { get; set; }
        public string Usuario { get; set; }

        public DateTime Fecha { get; set; }

        public int ClienteId { get; set; }
        public int ProductoID { get; set; }

        public int IdCategoria { get; set; }
    }
}
