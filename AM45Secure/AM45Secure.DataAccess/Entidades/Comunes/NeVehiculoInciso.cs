using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("neVehiculoInciso")]
    public class NeVehiculoInciso : IEntity
    {
        public string Poliza { set; get; }
        public int Inciso { set; get; }
        public string Endoso { set; get; }
        public string ClaveVehiculo { set; get; }
        public string EstadoCirculacion { set; get; }
        public string Marca { set; get; }
        public string Submarca { set; get; }
        public string Modelo { set; get; }
        public string Descripcion { set; get; }
        public string Serie { set; get; }
        public string Motor { set; get; }
        public string Placas { set; get; }
        public string RFV { set; get;  }
        public decimal ValorFactura { set; get; }
        public string Uso { set; get; }
        public string Servicio { set; get; }
        public int Pasajeros { set; get; }
        public string EquipoEspecial { set; get; }

        [Column("DistribuidoraID")]
        public string DistribuidoraId { set; get; }

        [Column("GrupoID")]
        public string GrupoId { set; get; }
        public string Integrante { set; get; }

        [Column("OpcionID")]
        public float OpcionId { set; get; }
        public string OpcionValor { set; get; }
        public int Tipo { set; get; }
        public string Color { set; get; }
        public string Vendedor { set; get; }
        public string Contrato { set; get; }
        public string NoEconomico { set; get; }

        [Column("caratula")]
        public string Caratula { set; get; }

        [Column("clienteInterno")]
        public string ClienteInterno { set; get; }

        [Column("LegalizadoID")]
        public bool LegalizadoId { set; get; }
        public string ClaveVehiculoCia { set; get; }
        public string Adaptaciones { set; get; }
        public string Carga { set; get; }

        [Column("REPUVE")]
        public string Repuve { set; get; }
        public string NumeroUnidad { set; get; }
        public string LogNumber { set; get; }
        public string DescripcionCarga { set; get; }
        public string NombreConductor { set; get; }

        [Column("GrupoRC")]
        public string GrupoRc { set; get; }
        public string DispositivoSatelital { set; get; }
        public string ConductorRFC { set; get; }
        public string ClaveCliente { set; get; }
        public string RamoAseguradora { set; get; }
        public int Kilomentraje { set; get; }

        /*[Column("CoberturasSA")]
        public int CoberturasSa { set; get; }
        public string SumaAseguradaCobertura { set; get; }*/
        public int Remolques { set; get; }
    }
}
