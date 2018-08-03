using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Tickets;
using Zero.Handlers.Response;

namespace AM45Secure.Business.IBusiness.Tickets
{
    public interface IConfigurarParametrosTicketsBusiness
    {
        SingleResponse<IList<ConfigurarParametrosTicketsModelo>> ConsultarConfigurarParametros(ConfigurarParametrosTicketsModelo configurarParametros);
        SingleResponse<IList<ClienteProductoModel>> ConsultarClientesConfigurarParametros(ClienteProductoModel clienteProductoModel);
        SingleResponse<IList<PersonaResponsableModel>> BuscarUsuarioResponsable(PersonaResponsableModel personaResponsableModel);
        SingleResponse<TiposTicketsClientesModel> GuardarTiposTicketsClientes(TiposTicketsClientesModel tiposTicketsClientesModel);
        SingleResponse<bool> EliminarTipoTicketsCliente(TiposTicketModel tiposTicket);
        SingleResponse<TiposTicketsClientesModel> ActulizarTiposTicketsClientes(TiposTicketsClientesModel tiposTicketsClientesModel);
    }
}