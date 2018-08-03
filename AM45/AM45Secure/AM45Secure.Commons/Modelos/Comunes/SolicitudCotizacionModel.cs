using System;
using AM45Secure.Commons.Utils;
using Zero.Attributes;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class SolicitudCotizacionModel : AbstractModel
    {
        [DataColumn]
        [Required(Optional = true, FieldName =  "Id de Solicitud")]
        public int SolicitudId { set; get; }
        [DataColumn]
        public int ClienteId { set; get; }
        [DataColumn]
        public int ProductoId { set; get; }
        [DataColumn]
        public int TipoVehiculoId { set; get; }
        [DataColumn]
        public int Renovacion { set; get; }
        [DataColumn]
        public int ServicioId { set; get; }
        [DataColumn]
        public decimal ValorFactura { set; get; }
        [DataColumn]
        public int UsoId { set; get; }
        [DataColumn]
        public string Modelo { set; get; }
        [DataColumn]
        public string Marca { set; get; }
        [DataColumn]
        public string SubMarca { set; get; }
        [DataColumn]
        public string Descripcion { set; get; }
        [DataColumn]
        public string EstadoCirculacion { set; get; }
        [DataColumn]
        public int EstadoCirculacionId { set; get; }
        [DataColumn]
        public int Plazo { set; get; }
        [DataColumn]
        public string MonedaId { set; get; }
        [DataColumn]
        public int LoJack { set; get; }
        [DataColumn]
        public int AgenciaId { set; get; }
        [DataColumn]
        public int UsuarioId { set; get; }
        [DataColumn]
        public decimal SumaEEspecial { set; get; }
        [DataColumn]
        public string DescripcionEe { set; get; }
        [DataColumn]
        public decimal SumaAdaptaciones { set; get; }
        [DataColumn]
        public string DescripcionAdaptaciones { set; get; }
        [DataColumn]
        public int CotizacionId { set; get; }
        [DataColumn]
        public string ClaveVehiculoMarsh { set; get; }
        [DataColumn]
        public DateTime InicioVigencia { set; get; }
        [DataColumn]
        public DateTime FinVigencia { set; get; }
        [DataColumn]
        public decimal Derechos { set; get; }
        [DataColumn]
        public int Ocupantes { set; get; }
        [DataColumn]
        public string Deducible { set; get; }
        [DataColumn]
        public string DeducibleOpcion { set; get; }
        [DataColumn]
        public string DeducibleDm { set; get; }
        [DataColumn]
        public string DeducibleRt { set; get; }
        [DataColumn]
        public string TarifaIdProducto { set; get; }
        [DataColumn]
        public string PorcentajeUdi { set; get; }
        [DataColumn]
        public string Gnp { set; get; }
        [DataColumn]
        public string Qlt { set; get; }
        [DataColumn]
        public string Rsa { set; get; }
        [DataColumn]
        public string Mfr { set; get; }
        [DataColumn]
        public string Axa { set; get; }
        [DataColumn]
        public string Udis { set; get; }
        [DataColumn]
        public string Kilometraje { set; get; }
        [DataColumn]
        public string Bbva { set; get; }
        [DataColumn]
        public string Aba { set; get; }
        [DataColumn]
        public string Hdi { set; get; }
        [DataColumn]
        public string Zurich { set; get; }
        [DataColumn]
        public int CotizaWssf { set; get; }
        [DataColumn]
        public string TipoPago { set; get; }
        [DataColumn]
        public string PolizaRen { set; get; }
        [DataColumn]
        public int IncisoRen { set; get; }
        [DataColumn]
        public string TipoCarga { set; get; }
        [DataColumn]
        public int Remolques { set; get; }
        [DataColumn]
        public int TipoArrendamiento { set; get; }
        [DataColumn]
        public int IdTipoVehiculo { set; get; }
        [DataColumn]
        public string TipoVehiculo { set; get; }
        [DataColumn]
        public int IdServicio { set; get; }
        [DataColumn]
        public string Servicio { set; get; }
        [DataColumn]
        public bool Flexible { set; get; }
        [DataColumn]
        public string CodigoPostal { get; set; }
        [DataColumn]
        public string Pais { get; set; }
        [DataColumn]
        public string Estado { get; set; }
        [DataColumn]
        public int EstadoId { get; set; }
        [DataColumn]
        public string Municipio { get; set; }
        [DataColumn]
        public string Delegacion { get; set; }
        [DataColumn]
        public string Colonia { get; set; }
        [DataColumn]
        public int Asentamiento { get; set; }
        [DataColumn]
        public int IdMunicipio { get; set; }
        [DataColumn]
        public bool ExisteDm { set; get; }
    }
}
