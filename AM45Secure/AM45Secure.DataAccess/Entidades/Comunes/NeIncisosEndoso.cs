using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("neIncisosEndoso")]
    public class NeIncisosEndoso : IEntity
    {
        public string Poliza { set; get; }
        public int Inciso { set; get; }
        public string Endoso { set; get; }
    }
}
