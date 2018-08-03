using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.ImagenCliente
{
  public  class ImageClienteModel
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Url { get; set; }
        public int IdCliente { get; set; }
        public int IsUpdate { get; set; }
        public string imagen64 { get; set; }
        public int IdSolictud { get; set; } //Ayuda a recuperar la IMagen que aparece en la cotizacion,Por medio de la solcitud
    }
}
