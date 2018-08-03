using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class CoberturasProductosFlexAseguradoras :IEntity
    {
        public int IdProductoFlexAseguradora { set; get; }
        public int IdCobertura { set; get; }

        public string Homologacion { set; get; }

        public string ToolTip { set; get; }

        public string RangosJson { set; get; }
        public string Detalle { set; get; }
        public string PrimaNeta { set; get; }
        public string IndicadorXml { set; get; }
    }
}
