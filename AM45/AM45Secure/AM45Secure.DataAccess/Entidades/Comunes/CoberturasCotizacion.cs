using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("CoberturasCotizacion")]
    public class CoberturasCotizacion : IEntity
    {
        [Column("IDSolicitud")]
        public int IdSolicitud { get; set; }

        public int IdCobertura { get; set; }

        public string SumaAsegurada { get; set; }

        public string Deducible { get; set; }

        public string CoberturaEspecial { get; set; }

        public string SumaEspecial { get; set; }

        public bool IsEspecial { get; set; }

        public bool IsBasica { get; set; }

        public bool Seleccionada { get; set; }
    }
}