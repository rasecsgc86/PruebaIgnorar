using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class FormaPagoModel : IEntity
    {
        public int FormaPagoID { get; set; }

        public int ProductoID { get; set; }
        public string FormaPago { get; set; }
        public string Producto { get; set; }
        public bool Predeterminado { get; set; }
    }
}
