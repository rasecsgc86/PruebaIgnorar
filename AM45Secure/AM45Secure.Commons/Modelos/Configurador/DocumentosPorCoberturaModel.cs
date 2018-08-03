using AM45Secure.Commons.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class DocumentosPorCoberturaModel : AbstractModel
    {
        [Required(FieldName = "IdProducto", Optional = true)]
        public int IdProducto { get; set; }
        public int IdCobertura { get; set; }

        public string Nombre { get; set; }
        public string UrlArchivoCobertura { get; set; }
    }
}
