using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("Productos")]
    public class Productos : IEntity
    {
        [Column("ProductoID")]
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public string Alia { get; set; }
        public int ClaveAlterna { get; set; }
        public int Ramo { get; set; }
        public int SubRamo { get; set; }
        public int Estatus { get; set; }
        public int TipoSeguro { get; set; }
        public int Recibos { get; set; }
        public bool OnLine { get; set; }
        [Column("cantidadMultianuales")]
        public int CantidadMultianuales { get; set; }
        [Column("cantidadMeses")]
        public int CantidadMeses { get; set; }
        [Column("CP")]
        public bool CodigoPostal { get; set; }
        [Column("TarifaID")]
        public int TarifaId { get; set; }
        public bool CargoLinea { get; set; }
        public bool Flexible { get; set; }
        public string NotasImportantes { get; set; }
    }
}