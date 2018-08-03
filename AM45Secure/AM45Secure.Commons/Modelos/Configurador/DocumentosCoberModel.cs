using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class DocumentosCoberModel : IEntity
    {
        public int IdProducto { get; set; }
        public int IdCobertura { get; set; }
        public string Nombre { get; set; }
        public string UrlArchivoCobertura { get; set; }
    }
}
