using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table ("SolicitudCotizacion")]
    public class SolicitudCotizacion : IEntity
    {
        [IdColumn(true)]
        [ Column ("IDSolicitud")]
        public int SolicitudId { set; get; }
        [ Column("ClienteID")]
        public int ClienteId { set; get; }
        [ Column ("ProductoID")]
        public int ProductoId { set; get; }
        [ Column("TipoVehiculoID")]
        public int TipoVehiculoId { set; get; }
        public int Renovacion { set; get; }
        [ Column("ServicioID")]
        public int ServicioId { set; get; }
        public decimal ValorFactura { set; get; }
        [Column("UsoID")]
        public int UsoId { set; get; }
        public string Modelo { set; get; }
        public string Marca { set; get; }
        public string SubMarca { set; get; }
        public string Descripcion { set; get; }
        public string EstadoCirculacion { set; get; }
        [ Column("EstadoCirculacionID")]
        public int EstadoCirculacionId { set; get; }
        public int Plazo { set; get; }
        public DateTime FechaRegistro { set; get; }
        [Column("MonedaID")]
        public string MonedaId { set; get; }
        public int LoJack { set; get; }
        [ Column("AgenciaID")]
        public int AgenciaId { set; get; }
        [ Column("UsuarioID")]
        public int UsuarioId { set; get; }
        [ Column("SumaEEspicial")]
        public decimal SumaEEspecial { set; get; }
        [ Column("DescripcionEE")]
        public string DescripcionEe { set; get; }
        public decimal SumaAdaptaciones { set; get; }
        public string DescripcionAdaptaciones { set; get; }
        [ Column("CotizacionID")]
        public int CotizacionId { set; get; }
        public string ClaveVehiculoMarsh { set; get; }
        public DateTime InicioVigencia { set; get; }
        public DateTime FinVigencia { set; get; }
        public decimal Derechos { set; get; }
        public int Ocupantes { set; get; }
        public string Deducible { set; get; }
        public string DeducibleOpcion { set; get; }
        [Column("DeducibleDM")]
        public string DeducibleDm { set; get; }
        [Column("DeducibleRT")]
        public string DeducibleRt { set; get; }
        [ Column("TarifaIDProducto")]
        public string TarifaIdProducto { set; get; }
        [ Column("PorcentajeUDI")]
        public string PorcentajeUdi { set; get; }
        [ Column("GNP")]
        public string Gnp { set; get; }
        [ Column("QLT")]
        public string Qlt { set; get; }
        [ Column("RSA")]
        public string Rsa { set; get; }
        [ Column("MFR")]
        public string Mfr { set; get; }
        [Column("AXA")]
        public string Axa { set; get; }
        [Column("UDIS")]
        public string Udis { set; get; }
        public string Kilometraje { set; get; }
        [ Column("BBVA")]
        public string Bbva { set; get; }
        [ Column("Aba")]
        public string Aba { set; get; }
        [ Column("HDI")]
        public string Hdi { set; get; }
        public string Zurich { set; get; }
        [ Column("CotizaWSSF")]
        public int CotizaWssf { set; get; }
        public string TipoPago { set; get; }
        public string PolizaRen { set; get; }
        public int IncisoRen { set; get; }
        public string TipoCarga { set; get; }
        public string Remolques { set; get; }
        public int TipoArrendamiento { set; get; }
    }
}
