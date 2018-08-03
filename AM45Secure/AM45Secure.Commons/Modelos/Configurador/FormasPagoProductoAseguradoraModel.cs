using AM45Secure.Commons.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class FormasPagoProductoAseguradoraModel : IEntity
    {
        public int Id { get; set; }
        public string AseguradoraID { get; set; }
        public string FormaPagoID { get; set; }
        public string Plazo { get; set; }
        public string Codigo { get; set; }
    }
}
