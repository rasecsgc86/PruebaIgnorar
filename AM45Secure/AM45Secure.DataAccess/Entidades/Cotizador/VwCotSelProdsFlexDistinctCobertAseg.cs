using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Cotizador
{
    [Table("vwCOTSelProdsFlexDistinctCobertAseg")]
    public class VwCotSelProdsFlexDistinctCobertAseg : IEntity
    {
        [Column("ProductoID")]
        public int IdProducto { set; get; }

        public string NombreProducto { set; get; }

        public int IdProductoFlex { set; get; }

        public int IdTipoVehiculo { set; get; }

        public string TipoVehiculo { set; get; }

        public int IdCondicionVehiculo { set; get; }

        [Column("CondicionVehiculod")]
        public string CondicionVehiculo { set; get; }

        public string IdTipoServicioVehiculo { set; get; }

        public string ServicioVehiculo { set; get; }

        [Column("Submarca")]
        public string SubMarca { get; set; }

        public int IdProductoFlexAseguradora { set; get; }

        [Column("AseguradoraId")]
        public int IdAseguradora { set; get; }

        public string NombreAseguradora { set; get; }

        [Column("CoberturaId")]
        public int IdCobertura { set; get; }

        public string NombreCobertura { set; get; }

        public bool CoberturaFija { set; get; }

        public bool PerfilCoberturaFija { set; get; }

        public string SumaAseguradaDefault { set; get; }

        public bool PerfilSumaAsegurada { set; get; }
        public bool PerfilDeducible { set; get; }

        public string DeducibleDefault { set; get; }

        public string TooltipCobertura { set; get; }

        public bool IsEspecial { set; get; }
    }
}