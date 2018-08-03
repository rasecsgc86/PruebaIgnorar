using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table ("nePersonas")]
    public class NePersonas : IEntity
    {
        [Column ("PersonaID")]
        public int PersonaId { set; get; }

        [Column ("Nombre")]
        public string Nombre { set; get; }

        public string Paterno { set; get; }

        public string Materno { set; get; }

        public string RFC { set; get; }

        public DateTime FechaNacimiento { set; get; }

        public string Pais { set; get; }

        public string Estado { set; get; }

        public string Delegacion { set; get; }

        public string Colonia { set; get; }

        public string CalleNumero { set; get; }

        public string CP { set; get; }

        public string TelefonoOficina { set; get; }

        public string TelefonoParticular { set; get; }

        public string Fax { set; get; }

        public string Movil { set; get; }

        public string Mail { set; get; }

        public string UENID { set; get; }

        public string ClaveAlterna { set; get; }

        public string IdInterno { set; get; }

        public string CURP { set; get; }

        public string NoEmpleado { set; get; }

        public decimal Tipo { set; get; }

        public decimal Sexo { set; get; }

        public DateTime FechaRegistro { set; get; }

        public string Alias { set; get; }

        [Column ("BitacoraCargaID")]
        public int BitacoraCargaId { set; get; }

        public string NoCuentaBancaria { set; get; }

        public string ClabeBancaria { set; get; }

        [Column ("banco")]
        public string Banco { set; get; }

        public string Planta { set; get; }

        public decimal MarkUp { set; get; }

        public int Vigente { set; get; }

        [Column ("IDIVA")]
        public int IdIVA { set; get; }

        public Boolean ProductivoAutomarsh { set; get; }

        public int IdentificaTipoPersona { set; get; }

        public int GrupoCli { set; get; }

        public string IdEstatusPer { set; get; }

        public decimal Segmento { set; get; }

        public string Ejecutivo { set; get; }

        public string SibVinculoComer { set; get; }

        public string Placement { set; get; }

        public string Riesgo { set; get; }

        public string NombreCorto { set; get; }

        public string ClaveExterna { set; get; }

        [Column ("GrupoClienteID")]
        public int GrupoClienteId { set; get; }

        public int MaximoPolizasEmpleado { set; get; }

        public int Zona { set; get; }

        public int AsentamientoId { set; get; }

        public int MunicipioId { set; get; }

        public string NumeroExterior { set; get; }

        public string NumeroInterior { set; get; }

        [Column ("pws")]
        public string Pws { set; get; }

        public decimal NacionalidadId { set; get; }

        public string FIEL { set; get; }

        public decimal DocumentoIdentificacionId { set; get; }

        public string NumeroDocumentoIdentificacion { set; get; }

        public decimal GiroComercialId { set; get; }

        public decimal ProfesionId { set; get; }

        public decimal CargoGobId { set; get; }

        public string DescripcionCargo { set; get; }

        public int RegimenFiscalId { set; get; }

        public int OcupacionId { set; get; }

        //public string NumPeopleSoft { set; get; }
    }
}
