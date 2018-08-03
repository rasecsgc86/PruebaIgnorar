using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class DirectivasModel : AbstractModel
    {
        public int iAccion { set; get; }
        public int IdCobertura { set; get; }
        public string Nombre { set; get; }
        public int idProductoFlex { set; get; }
        public int idProductoFlexAseguradora { set; get; }
        public int CoberturaFija { set; get; }
        public int PerfilCoberturaFija { set; get; }
        public string SumaAseguradaDefault { set; get; }
        public int PerfilSumaAsegurada { set; get; }
        public string DeducibleDefault { set; get; }
        public int PerfilDeducible { set; get; }
        public string ToolTipCobertura { set; get; }
        public int isEspecial { set; get; }
        public string ParametroDeducible { set; get; }
        public string ParametroSA { set; get; }
        public RangosModel rangosModel { set; get; }
        public int idProducto { set; get; }
        public int idTipoVehiculo { set; get; }
        public int idTipoServicioVehiculo { set; get; }


    }
}



