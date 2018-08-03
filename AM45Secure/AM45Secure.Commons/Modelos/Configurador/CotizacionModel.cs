using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class CotizacionModel
    {
        [Required(FieldName = "CotizacionId", Optional = true)]
        public int CotizacionId { get; set; }
    }
}
