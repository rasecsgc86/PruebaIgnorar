using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Configurador;
using AM45Secure.DataAccess.Entidades.Cotizador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Handlers.Response;

namespace AM45Secure.Business.IBusiness.IConfigurador
{
    public interface IConfiguradorBusiness
    {
        SingleResponse<IList<nePersonasModel>>ConsultarClientesConfigurador();

        SingleResponse<IList<ProductoModel>> ConsultaProductosFlexibles();

        SingleResponse<IList<ElementoModel>> ConsultaTipoAuto();

        SingleResponse<IList<ElementoModel>> ConsultaTipoServicio();
        SingleResponse<IList<ElementoModel>> ConsultaTipoSeguro();

        SingleResponse<IList<ElementoModel>> EsNuevo();

        SingleResponse<IList<ElementoModel>> CargoEnLinea();

        SingleResponse<IList<ElementoModel>> ConsultaAseguradoras();

        SingleResponse<IList<ElementoModel>> ConsultaEstatus();
        SingleResponse<ProductoFlexModel> GuardaProductoFlexible(ConfiguradorModel productoFlexModel);

        SingleResponse<IList<PerfilesUsuarioModel>> ConsultaPerfilesSistema();
        SingleResponse<IList<ElementoModel>> ConsultaFormasPago();

        SingleResponse<IList<UsuariosPerfil>> ConsultaUsuarioPorPerfil(ConfiguradorModel usuarioPerfilModel);

        SingleResponse<PerfilesFlexibleModel> GuardaUsuarioFlexible(ConfiguradorModel perfilesFlexibleModel);
        SingleResponse<IList<PerfilesFlexModel>> ConsultarUsuariosFlexibles();

        SingleResponse<IList<FormaPagoModel>> ConsultarFormasPagoLista();

        SingleResponse<IList<FormasPagoProductoAseguradoraModel>> ConsultarFormasPagoAseguradoraLista();

        SingleResponse<PerfilesFlexibleModel> ActualizaStatusUdi(ConfiguradorModel perfilesFlexibleModel);

        SingleResponse<FormasPagoProductoModel> ActualizaPredeterminadoPago(ConfiguradorModel formasPagoProductoModel);

        SingleResponse<CoberModel> ActualizarHomologacionTooltip(ConfiguradorModel configuradorModel);

        SingleResponse<PerfilesFlexibleModel> EliminarUsuarioFlexible(ConfiguradorModel perfilesFlexibleModel);
        SingleResponse<FormasPagoProductoModel> EliminarFormaDePago(ConfiguradorModel formasPagoProductoModel);

        SingleResponse<FormasPagoProductoModel> GrabarFormaPagoProducto(ConfiguradorModel formasPagoProductoModel);

        SingleResponse<ConfiguradorModel> ConsultaPanelConfiguradorFlex(ConfiguradorModel configuradorModel);

        SingleResponse<RangosModel> ConsultaRangosSumasAseguradas(ConfiguradorModel configuradorModel);

        SingleResponse<FormasPagoProductoAseguradora> GrabarFormasPagoProductoAseguradora(ConfiguradorModel formasPagoProductoAseguradora);

        SingleResponse<FormasPagoProductoAseguradora> EliminarFormaPagoProductoAseguradora(ConfiguradorModel formasPagoProductoAseguradora);

        SingleResponse<CoberModel> ActualizaRangosSumas(ConfiguradorModel configuradorModel);
        SingleResponse<CoberModel> ActualizaRangosDeducibles(ConfiguradorModel configuradorModel);

        SingleResponse<DocumentosPorCoberturaModel> GuardarDocumentoCobertura(ConfiguradorModel configuradorModel);
        SingleResponse<ConfiguradorModel> ConsultarEnmascaradoDeducibles(ConfiguradorModel configuradorModel);

        SingleResponse<TextoAuxiliarUsoVehiculoModel> GuardaTextoAuxiliarUso(ConfiguradorModel configuradorModel);
        SingleResponse<IList<DocumentosCoberModel>> ConsultaDocumentosPorCobertura(ConfiguradorModel configuradorModel);
        SingleResponse<IList<DocumentosCoberModel>> ConsultaDocumentosTodos();

        ///* INDRA FJQP Configuracion de directivas */
        SingleResponse<IList<DirectivasModel>> RecuperaInfoDirectivas(DirectivasModel directivasModel);
        SingleResponse<IList<DirectivasModel>> RecuperaListaCoberturas(DirectivasModel directivasModel);


    }
}
