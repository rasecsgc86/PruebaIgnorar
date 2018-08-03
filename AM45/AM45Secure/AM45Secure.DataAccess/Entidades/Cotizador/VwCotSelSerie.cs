using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Cotizador
{
    [Table("vwCOTSelSerie")]
    public class VwCotSelSerie : IEntity
    {
        public string Poliza { get; set; }
        public string Serie { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FinVigencia { get; set; }
    }
}