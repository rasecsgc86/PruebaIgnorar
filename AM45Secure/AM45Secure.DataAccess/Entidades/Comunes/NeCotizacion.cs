using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("neCotizacion")]
    public class NeCotizacion : IEntity
    {
        [IdColumn(true)]
        [Column("CotizacionID")]
        public int CotizacionId { set; get; }
        public DateTime FechaRegistro { set; get; }
        public string PolizaAnterior { set; get; }
        [Column("FormaPagoID")]
        public int FormaPagoId { set; get; }
        [Column("MonedaID")]
        public int MonedaId { set; get; }
        public DateTime InicioVigencia { set; get; }
        public DateTime FinVigencia { set; get; }
        public string Marca { set; get; }
        public string Submarca { set; get; }
        public string Vehiculo { set; get; }
        public int Modelo { set; get; }
        public string EstadoCirculacion { set; get; }
        public string ValorFactura { set; get; }
        public int Ocupantes { set; get; }
        public int DiasVigencia { set; get; }
        [Column("StatusID")]
        public int StatusId { set; get; }
        [Column("AgenciaID")]
        public int AgenciaId { set; get; }
        [Column("ClienteID")]
        public int ClienteId { set; get; }
        [Column("UsuarioID")]
        public string UsuarioId { set; get; }
        public string ClaveVehiculo { set; get; }
        public string Poliza { set; get; }
        [Column("EjecutivoID")]
        public int EjecutivoId { set; get; }
        [Column("BitacoraCargaID")]
        public int BitacoraCargaId { set; get; }
        public int PlazoSeguro { set; get; }
        public int PlazoCredito { set; get; }
        public int FolioOriginal { set; get; }
        public int Uso { set; get; }
        public int Servicio { set; get; }
        public int TipoVehiculo { set; get; }
        public string EstadoVehiculo { set; get; }
        public string EEspecial { set; get; }
        public string Adaptaciones { set; get; }
        [Column("FlotillaID")]
        public int FlotillaId { set; get; }
        [Column("GrupoID")]
        public int GrupoId { set; get; }
        public string GrupoEconomico { set; get; }
        public int LoJack { set; get; }
        public bool VehiculoLegalizado { set; get; }
        public int Renovacion { set; get; }
        public string InformacionLegalizado { set; get; }
        public int FolioSolicitud { set; get; }
        public int SeguroGratis { set; get; }
        public int Kilometraje { set; get; }
        public int DiasPlazoAbierto { set; get; }
        [Column("CotizaWSSF")]
        public int CotizaWssf { set; get; }
        public string ClavePlan { set; get; }
        /*public string DeduciblesConfigurables { set; get; }
        [Column("CoberturaSA")]
        public int CoberturaSa { set; get; }
        public string SumaAseguradaCobertura { set; get; }*/
        public string TipoCarga { set; get; }
        public int Remolques { set; get; }
        public int TipoArrendamiento { set; get; }
        public string NumeroFactura { set; get; }
        public DateTime FechaFactura { set; get; }
        public string ParametrosMapfre { set; get; }
    }
}
