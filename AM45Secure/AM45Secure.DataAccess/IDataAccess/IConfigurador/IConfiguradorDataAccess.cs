using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Configurador;
using AM45Secure.DataAccess.Entidades.Cotizador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Handlers.Response;

namespace AM45Secure.DataAccess.IDataAccess.IConfigurador
{
    public interface IConfiguradorDataAccess
    {
        IList<nePersonasModel> ConsultarClientesConfigurador();

        IList<ProductoModel> ConsultaProductosFlexibles();
        IList<ElementoModel> ConsultaTipoAuto();

        IList<ElementoModel> ConsultaTipoServicio();
        IList<ElementoModel> ConsultaTipoSeguro();
        IList<ElementoModel> EsNuevo();
        IList<ElementoModel> CargoEnLinea();

        IList<ElementoModel> ConsultaAseguradoras();

        IList<ElementoModel> ConsultaEstatus();

        ProductoFlexModel GuardaProductoFlexible(ConfiguradorModel productoFlexModel);
        FormasPagoProductoModel GrabarFormaPagoProducto(ConfiguradorModel formasPagoProductoModel);

        IList<PerfilesUsuarioModel> ConsultaPerfilesSistema();

        IList<UsuariosPerfil> ConsultaUsuarioPorPerfil(ConfiguradorModel usuarioPerfilModel);

        IList<ElementoModel> ConsultaFormasPago();


        PerfilesFlexibleModel GuardaUsuarioFlexible(ConfiguradorModel perfilesFlexibleModel);

        IList<PerfilesFlexModel> ConsultarUsuariosFlexibles();

        IList<FormaPagoModel> ConsultarFormasPagoLista();

        IList<FormasPagoProductoAseguradoraModel> ConsultarFormasPagoAseguradoraLista();

        PerfilesFlexibleModel ActualizaStatusUdi(ConfiguradorModel perfilesFlexibleModel);

        FormasPagoProductoModel ActualizaPredeterminadoPago(ConfiguradorModel formasPagoProductoModel);

        PerfilesFlexibleModel EliminarUsuarioFlexible(ConfiguradorModel perfilesFlexibleModel);

        FormasPagoProductoModel EliminarFormaDePago(ConfiguradorModel formasPagoProductoModel);

        PanelConfiguradorModel ConsultaPanelConfiguradorFlex(ConfiguradorModel configuradorModel);

        RangosModel ConsultaRangosSumasAseguradas(ConfiguradorModel configuradorModel);

        FormasPagoProductoAseguradora GrabarFormasPagoProductoAseguradora(ConfiguradorModel formasPagoProductoAseguradora);

        FormasPagoProductoAseguradora EliminarFormaPagoProductoAseguradora(ConfiguradorModel formasPagoProductoAseguradora);

        CoberModel ActualizarHomologacionTooltip(ConfiguradorModel configuradorModel);

        CoberModel ActualizaRangosSumas(ConfiguradorModel configuradorModel);

        CoberModel ActualizaRangosDeducibles(ConfiguradorModel configuradorModel);
        EnmascaradoDeduciblesModel ConsultarEnmascaradoDeducibles(ConfiguradorModel configuradorModel);
        DocumentosPorCoberturaModel GuardarDocumentoCobertura(ConfiguradorModel configuradorModel);

        TextoAuxiliarUsoVehiculoModel GuardaTextoAuxiliarUso(ConfiguradorModel configuradorModel);
        IList<DocumentosCoberModel> ConsultaDocumentosPorCobertura(ConfiguradorModel configuradorModel);
        IList<DocumentosCoberModel> ConsultaDocumentosTodos();

    }
}
