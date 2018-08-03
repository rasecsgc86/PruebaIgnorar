using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("neIncisos")]
    public class NeIncisos : IEntity
    {
        public string Poliza { set; get; }
        public int Inciso { set; get; }
        public DateTime InicioVigencia { set; get; }
        public DateTime FinVigencia { set; get; }

        [Column("EstatusID")]
        public int EstatusId { set; get; }

        [Column("CotizacionID")]
        public float CotizacionId { set; get; }
        
        [Column("FormaPagoID")]
        public float FormaPagoId { set; get; }

        [Column("InstrumentoPagoID")]
        public float InstrumentoPagoId { set; get; }

        [Column("MonedaID")]
        public float MonedaId { set; get; }
        public DateTime FechaEmision { set; get; }
        public DateTime FechaCancelacion { set; get; }
        public DateTime FechaRehabilitacion { set; get; }

        [Column("ProductoID")]
        public float ProductoId { set; get; }

        [Column("PaqueteID")]
        public float PaqueteId { set; get; }
        public string Zona { set; get; }
        public string Tarifa { set; get; }

        [Column("SubramoID")]
        public float SubramoId { set; get; }
        public string EndosoBaja { set; get; }
        public string EndosoCambio { set; get; }
        public string PolizaAnterior { set; get; }

        [Column("TarjetaBancariaID")]
        public float TarjetaBancariaId { set; get; }

        [Column("RamoID")]
        public float RamoId { set; get; }

        [Column("BitacoraCargaID")]
        public float BitacoraCargaId { set; get; }
        public float OtRenovacion { set; get; }
        public float LoteRenovacion { set; get; }
        public int PlazoSeguro { set; get; }
        public int PlazoCredito { set; get; }
        public string FolioExterno { set; get; }

        [Column("LoteCargaID")]
        public float LoteCargaId { set; get; }

        [Column("RenovadoID")]
        public int RenovadoId { set; get;}

        [Column("AltaID")]
        public float AltaId { set; get; }
        public string ObservacionAlta { set; get; }
        public int ComplementoTarifa { set; get; }

        [Column("ultimoendoso")]
        public string UltimoEndoso { set; get; }

        [Column("fechaUltimoEndoso")]
        public DateTime FechaUltimoEndoso { set; get; }
        public string PrimaCancelacion { set; get; }
        public string ReferenciaBancaria { set; get; }
        public bool DisclaimReimpre { set; get; }
        public DateTime FechaAceptacion { set; get; }

        [Column("UsuarioID")]
        public int UsuarioId { set; get; }
        public int Renovacion { set; get; }
        public int Procedencia { set; get; }

        [Column("mfRamoID")]
        public int MfRamoId { set; get; }

        [Column("mfSubramoID")]
        public int MfSubramoId { set; get; }

        [Column("mfSegmentoID")]
        public int MfSegmentoId { set; get; }
        public string NumeroContrato { set; get; }
        public DateTime FinContrato { set; get; }
        public string TipoArrendamiento { set; get; }
        public bool Duplicado { set; get; }

        [Column("LoteRepetidoID")]
        public float LoteRepetidoId { set; get; }

        [Column("iClavePaqueteDerivacion")]
        public int ClavePaqueteDerivacion { set; get; }

        [Column("AltoRiesgoRT")]
        public bool AltoRiesgoRt { set; get; }
        public string Negocio { set; get; }
        public float Comision { set; get; }
        public float Descuento { set; get; }
        public string Reporte { set; get; }
        public string Agente { set; get; }
        public float Cesion { set; get; }
        public bool ConfDerivacionActualizada { set; get; }
        public string UsoDerivacion { set; get; }
        public int DiasPlazo { set; get; }

        [Column("CotizaWSSF")]
        public int CotizaWssf { set; get; }
    }
}
