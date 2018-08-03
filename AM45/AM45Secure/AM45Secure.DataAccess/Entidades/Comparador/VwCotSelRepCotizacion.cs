using System;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comparador
{
    [Table("vwCOTSelRepCotizacion")]
    public class VwCotSelRepCotizacion : IEntity
    {
        public int SolicitudId { get; set; }
        public int ClienteId { get; set; }
        public int AseguradoraId { get; set; }
        public int ServicioId { get; set; }
        public int ValorFactura { get; set; }
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
        public DateTime FechaRegistro { get; set; }
        public DateTime InicioVigencia { get; set; }
        public DateTime FinVigencia { get; set; }
        public string Cotizante { get; set; }
        public string Nombre { get; set; }
        public decimal PrimaNeta { get; set; }
        public decimal Recargo { get; set; }
        public decimal Derechos { get; set; }
        [Column("IVA")]
        public decimal Iva { get; set; }
        public decimal PrimaTotal { get; set; }
        public int RecargoFraccionado { get; set; }
        public string ClaveAseg { get; set; }
        public int Numero { get; set; }
        public string PaqueteN { get; set; }
        public int PaqueteId { get; set; }
    }
}