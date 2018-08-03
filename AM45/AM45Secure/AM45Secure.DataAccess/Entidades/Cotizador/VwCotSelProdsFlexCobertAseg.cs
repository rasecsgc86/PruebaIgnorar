using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Cotizador
{
    [Table("vwCOTSelProdsFlexCobertAseg")]
    public class VwCotSelProdsFlexCobertAseg : IEntity
    {
        [Column("ProductoID")]
        public int IdProducto { set; get; }

        public String NombreProducto { set; get; }

        public int IdProductoFlex { set; get; }

        public int IdTipoVehiculo { set; get; }

        public string TipoVehiculo { set; get; }

        public int IdCondicionVehiculo { set; get; }

        public string CondicionVehiculo { set; get; }

        public string IdTipoServicioVehiculo { set; get; }

        public string ServicionVehiculo { set; get; }

        [Column("Submarca")]
        public string SubMarca { get; set; }

        public int IdProductoFlexAseguradora { set; get; }

        public int IdAseguradora { set; get; }

        public string NombreAseguradora { set; get; }

        public int IdCobertura { set; get; }

        public string NombreCobertura { set; get; }

        public string Dependencia { get; set; }

        public bool Enmascaramiento { get; set; }

        public string Homologacion { set; get; }

        public string Tooltip { set; get; }

        [Column("RangosJSON")]
        public string RangosJson { set; get; }

        public string Detalle { set; get; }
    }
}