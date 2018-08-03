using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class CoberturasEnmascaramientoDeducible : IEntity
    {
        public int IdCoberturaEnmascaramientoDeducible { get; set; }
        public int IdCliente { get; set; }
        public int IdCobertura { get; set; }
        public string DescripcionEnmascarado { get; set; }
        public bool Enmascaramiento { get; set; }
    }
}
