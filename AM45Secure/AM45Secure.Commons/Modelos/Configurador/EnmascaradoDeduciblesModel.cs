using AM45Secure.Commons.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class EnmascaradoDeduciblesModel : AbstractModel
    {
        [Required(FieldName = "IdCliente")]
        public int IdCliente { get; set; }
        [Required(FieldName = "IdCobertura", Optional = true)]
        public int IdCobertura { get; set; }
        [Required(FieldName = "EnmascaradoDeducibles", Optional = true)]
        public IList<CoberturasEnmascaramientoDeducible> EnmascaradoDeducibles { set; get; }
    }
}
