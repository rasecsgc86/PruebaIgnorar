using AM45Secure.Commons.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class FormasPagoProductoAseguradora : AbstractModel
    {
        [Required(FieldName = "Id", Optional = true)]
        public int Id { get; set; }
        public int AseguradoraID { get; set; }
        public int FormaPagoID { get; set; }
        public int Plazo { get; set; }
        public string Codigo { get; set; }

    }
}
