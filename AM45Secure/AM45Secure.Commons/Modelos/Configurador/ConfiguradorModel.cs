using AM45Secure.Commons.Modelos.Comunes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM45Secure.Commons.Modelos.Configurador
{
    public class ConfiguradorModel : AbstractModel
    {
        public ProductoFlexModel ProductoFlexModel { set; get; }

        public UsuariosPerfilModel UsuariosPerfilModel { set; get; }

        public PerfilesFlexibleModel PerfilesFlexibleModel { set; get; }

        public FormasPagoProductoModel FormasPagoProductoModel { set; get; }

        public FormasPagoProductoAseguradora FormasPagoProductoAseguradora { set; get; }

        public PanelConfiguradorModel PanelConfiguradorModel { set; get; }

        public CoberModel CoberModel { set; get; }

        public SolicitudReglaNegocioModel SolicitudRegla { set; get; }

        public EnmascaradoDeduciblesModel CoberturaEnmascaramientoDeducible { set; get; }

        public DocumentosPorCoberturaModel DocumentoPorCobertura { set; get; }

        public CotizacionModel CotizacionModel { set; get; }

        public TextoAuxiliarUsoVehiculoModel TextoAuxModel { set; get; }
    }
}
