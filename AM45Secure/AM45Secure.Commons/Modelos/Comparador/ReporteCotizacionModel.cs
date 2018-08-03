using System;

namespace AM45Secure.Commons.Modelos.Comparador
{
    public class ReporteCotizacionModel
    {
        public int SolicitudId { get; set; }
        public int ClienteId { get; set; }
        public int AseguradoraId { get; set; }
        public int ServicioId { get; set; }
        public string ValorFactura { get; set; }
        public int UsoId { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
        public string Submarca { get; set; }
        public string Descripcion { get; set; }
        public string EstadoCirculacion { get; set; }
        public string MondaId { get; set; }
        public string LoJack { get; set; }
        public int UsuarioId { get; set; }
        public int CotizacionId { get; set; }
        public string ClaveVehiculoMarsh { get; set; }
        public string TipoVehiculo { get; set; }
        public string Cliente { get; set; }
        public string FechaRegistro { get; set; }
        public string InicioVigencia { get; set; }
        public string FinVigencia { get; set; }
        public string Cotizante { get; set; }
        public string Nombre { get; set; }
        public decimal PrimaNeta { get; set; }
        public decimal Recargo { get; set; }
        public decimal Derechos { get; set; }
        public decimal Iva { get; set; }
        public decimal PrimaTotal { get; set; }
        public int RecargoFraccionado { get; set; }
        public bool Especial { get; set; }
        public bool Adaptaciones { get; set; }
        public string ClaveAseg { get; set; }
        public decimal Subtotal { set; get; }
        public string PaqueteN { get; set; }
        public int PaqueteId { get; set; }
        public int Pasajeros { get; set; }
        public string Carga { get; set; }
        public int Remolques { get; set; }
        public bool ShowCargas { get; set; }
        public bool ShowRemolques { get; set; }
        public string FechaRegistroString { get; set; }
        public string InicioVigenciaString { get; set; }
        public string FinVigenciaString { get; set; }
    }
}