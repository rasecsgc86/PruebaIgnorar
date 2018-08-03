using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Cotizador
{
    public class ElementoPanelCotizadorModel
    {
        public int IdAseguradora { set; get; }

        public string NombreAseguradora { set; get; }

        public int IdCobertura { set; get; }

        public string NombreCobertura { set; get; }

        public string Dependencia { set; get; }

        public Boolean ExisteRelacion { set; get; }

        public string IndicadorCobertura { set; get; }

        public RangosModel RangosModel { set; get; }

        public string Detalle { set; get; }
    }
}