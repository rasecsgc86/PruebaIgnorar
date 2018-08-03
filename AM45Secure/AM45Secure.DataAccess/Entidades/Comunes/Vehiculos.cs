using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("Vehiculos")]
    public class Vehiculos : IEntity
    {
        [Column("VehiculoID")]
        public int VehiculoId { set; get; }

        public string Marca { set; get; }

        public string Submarca { set; get; }

        public string Descripcion { set; get; }

        public string ClaveInterna { set; get; }

        public int Modelo { set; get; }

        [Column("GNP")]
        public string Gnp { set; get; }

        [Column("AIG")]
        public string Aig { set; get; }

        [Column("ABA")]
        public string Aba { set; get; }

        [Column("ING")]
        public string Ing { set; get; }

        [Column("ATLAS")]
        public string Atlas { set; get; }

        [Column("ROYAL")]
        public string Royal { set; get; }

        [Column("QUALITAS")]
        public string Qualitas { set; get; }

        public string Tipo { set; get; }

        public string Servicio { set; get; }

        public string Pasajeros { set; get; }

        [Column("JATOID")]
        public string JatoId { set; get; }

        [Column("JATOIDUSADOS")]
        public string JatoIdUsados { set; get; }

        [Column("ZURICH")]
        public string Zurich { set; get; }

        [Column("PNG")]
        public string Png { set; get; }

        [Column("MAPFRE")]
        public string Mapfre { set; get; }

        [Column("BANORTE")]
        public string Banorte { set; get; }

        [Column("CHARTIS")]
        public string Chartis { set; get; }

        [Column("VWBANK")]
        public string VwBank { set; get; }

        [Column("ClaveMARSH")]
        public string ClaveMarsh { set; get; }

        //[Column("msrepl_tran_version")]
        //public Guid MsReplTranVersion { set; get; }

        [Column("HDI")]
        public string Hdi { set; get; }

        [Column("BANCOMER")]
        public string Bancomer { set; get; }

        public string RangoPasajeros { set; get; }
    }
}