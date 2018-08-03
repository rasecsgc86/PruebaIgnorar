using Zero.Ado;
using Zero.Attributes;

namespace AM45Secure.DataAccess.Entidades.Comunes
{
    [Table("nePersonasInciso")]
    public class NePersonasInciso : IEntity
    {
        public string Poliza { get; set; }
        public int Inciso { get; set; }
        public string Endoso { get; set; }
        [Column("AseguradoraID")]
        public int AseguradoraId { get; set; }
        [Column("AgenteID")]
        public int AgenteId { get; set; }
        [Column("ClienteID")]
        public int ClienteId { get; set; }
        [Column("AgenciaID")]
        public int AgenciaId { get; set; }
        [Column("AseguradoID")]
        public int AseguradoId { get; set; }
        [Column("UsuarioID")]
        public int UsuarioId { get; set; }
        [Column("EjecutivoID")]
        public int EjecutivoId { get; set; }
        [Column("TipoID")]
        public int TipoId { get; set; }
        [Column("EmpleadoID")]
        public string EmpleadoId { get; set; }
        [Column("Y_O")]
        public string Yo { get; set; }
        public string BeneficiarioPreferente { get; set; }
        public string CorreoBeneficiario { get; set; }
        public string Nombre { get; set; }
        public string Paterno { get; set; }
        public string Materno { get; set; }
        public string RFC { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Delegacion { get; set; }
        public string Colonia { get; set; }
        public string CalleNumero { get; set; }
        [Column("CP")]
        public string Cp { get; set; }
        public string TelefonoParticular { get; set; }
        public int Tipo { get; set; }
        public string CuentaClabe { get; set; }
        public string Ubicacion { get; set; }
        public string NumeroEmpleado { get; set; }
        [Column("numerocliente")]
        public string NumeroCliente { get; set; }
        [Column("IDGrupo")]
        public string IdGrupo { get; set; }
        [Column("UENID")]
        public int UeNiD { get; set; }
        public string Observaciones { get; set; }
        public string Sexo { get; set; }
        public string NumeroExterior { get; set; }
        [Column("GrupoID")]
        public int GrupoId { get; set; }
        public string GrupoEconomico { get; set; }
        public int TipoCliente { get; set; }
        public string Curp { get; set; }
        public string CargoEnElGobierno { get; set; }
        public string Nombredelapoderadolegal { get; set; }
        public string Nombredelfuncionario { get; set; }
        public int PoliticoExpuesto { get; set; }
        public string DescripcionCargo { get; set; }
        public string SubGrupoAgencia { get; set; }
        public string RfcBeneficiarioPreferente { get; set; }
        public int AgenteSecundario { get; set; }
        [Column("Cve_Estado")]
        public int CveEstado { get; set; }
        [Column("Cve_Provincia")]
        public int CveProvincia { get; set; }
        [Column("mfEjecutivoID")]
        public int MfEjecutivoId { get; set; }
        public string Telefono2 { get; set; }
        public int AsesorId { get; set; }
    }
}