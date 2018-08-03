using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AM45Secure.Commons.Modelos.Emitir
{
    public class InsertConfigMultipleModel
    {
        public int Aseguradora { get; set; }
        public int Producto { get; set; }
        public int Perfil { get; set; }
        public int Usuario { get; set; }
        public int PermiteEmisionMultiple { get; set; }
        public int PermiteCapContratos { get; set; }
        public int IsUpdate { get; set; }
        public string Tkn { get; set; }
        
    }
}



