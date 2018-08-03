using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comparador
{
    [Table("vwCOTSelSolicitudCotizacionServTipUnidad")]
    public class VwCotSelSolicitudCotizacionServTipUnidad : IEntity
    {
        public int SolicitudId { set; get; }
        public int ClienteId { set; get; }
        public int ProductoId { set; get; }
        public int TipoVehiculoId { set; get; }
        public int Renovacion { set; get; }
        public int ServicioId { set; get; }
        public decimal ValorFactura { set; get; }
        public int UsoId { set; get; }
        public string Modelo { set; get; }
        public string Marca { set; get; }
        public string SubMarca { set; get; }
        public string Descripcion { set; get; }
        public string EstadoCirculacion { set; get; }
        public int EstadoCirculacionId { set; get; }
        public int Plazo { set; get; }
        public DateTime FechaRegistro { set; get; }
        [Column("MondaId")]
        public string MonedaId { set; get; }
        public int LoJack { set; get; }
        public int AgenciaId { set; get; }
        public int UsuarioId { set; get; }
        public decimal SumaeEspicial { set; get; }
        public string DescripcionEe { set; get; }
        public decimal SumaAdaptaciones { set; get; }
        public string DescripcionAdaptaciones { set; get; }
        public int CotizacionId { set; get; }
        public string ClaveVehiculoMarsh { set; get; }
        public DateTime InicioVigencia { set; get; }
        public DateTime FinVigencia { set; get; }
        public decimal Derechos { set; get; }
        public int Ocupantes { set; get; }
        public string Deducible { set; get; }
        public string DeducibleOpcion { set; get; }
        public string DeducibleDm { set; get; }
        public string DeducibleRt { set; get; }
        public string TarifaIdProducto { set; get; }
        public string PorcentajeUdi { set; get; }
        public string Gnp { set; get; }
        public string Qlt { set; get; }
        public string Rsa { set; get; }
        public string Mfr { set; get; }
        public string Axa { set; get; }
        public string Udi { set; get; }
        public string Kilometraje { set; get; }
        public string Bbva { set; get; }
        public string Aba { set; get; }
        public string Hdi { set; get; }
        public string Zurich { set; get; }
        public int CotizaWssf { set; get; }
        public string TipoPago { set; get; }
        public string PolizaRen { set; get; }
        public int IncisoRen { set; get; }
        public string TipoCarga { set; get; }
        public int Remolques { set; get; }
        public int TipoArrendamiento { set; get; }
        public int IdTipoVehiculo { set; get; }
        public string TipoVehiculo { set; get; }
        public int IdServicio { set; get; }
        public string Servicio { set; get; }
        public bool Flexible { set; get; }
        public string CodigoPostal { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public int EstadoId { get; set; }
        public string Municipio { get; set; }
        public string Delegacion { get; set; }
        public string Colonia { get; set; }
        public int Asentamiento { get; set; }
        public int IdMunicipio { get; set; }
    }
}
