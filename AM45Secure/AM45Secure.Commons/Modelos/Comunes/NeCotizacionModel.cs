using System;

namespace AM45Secure.Commons.Modelos.Comunes
{
    public class NeCotizacionModel
    {
        public int CotizacionId { set; get; }
        public DateTime FechaRegistro { set; get; }
        public string PolizaAnterior { set; get; }
        public int FormaPagoId { set; get; }
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
        public int StatusId { set; get; }
        public int AgenciaId { set; get; }
        public int ClienteId { set; get; }
        public string UsuarioId { set; get; }
        public string ClaveVehiculo { set; get; }
        public string Poliza { set; get; }
        public int EjecutivoId { set; get; }
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
        public int FlotillaId { set; get; }
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
        public int CotizaWssf { set; get; }
        public string ClavePlan { set; get; }
        public string DeduciblesConfigurables { set; get; }
        public int CoberturaSa { set; get; }
        public string SumaAseguradaCobertura { set; get; }
        public string TipoCarga { set; get; }
        public int Remolques { set; get; }
        public int TipoArrendamiento { set; get; }
        public string NumeroFactura { set; get; }
        public DateTime FechaFactura { set; get; }
        public string ParametrosMapfre { set; get; }
    }
}
