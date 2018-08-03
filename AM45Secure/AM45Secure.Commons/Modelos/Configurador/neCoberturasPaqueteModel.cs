using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class neCoberturasPaqueteModel : IEntity 
    {
        public int AseguradoraID { get; set; }
        public int PaqueteID { get; set; }
        public int CoberturaID { get; set; }
        public string TipoID { get; set; }
    }
}
