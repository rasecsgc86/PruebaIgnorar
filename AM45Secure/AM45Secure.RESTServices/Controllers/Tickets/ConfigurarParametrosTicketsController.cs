using System.Collections.Generic;
using System.Web.Http;
using AM45Secure.Business.IBusiness.Tickets;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Tickets;
using Zero.Handlers.Response;

namespace AM45Secure.RESTServices.Controllers.Tickets
{
    public class ConfigurarParametrosTicketsController : ApiController
    {
        private readonly IConfigurarParametrosTicketsBusiness iConfigurarParametrosTicketsBusiness;

        public ConfigurarParametrosTicketsController(IConfigurarParametrosTicketsBusiness iConfigurarParametrosTicketsBusiness)
        {
            this.iConfigurarParametrosTicketsBusiness = iConfigurarParametrosTicketsBusiness;
        }

        [HttpPost]
        public SingleResponse<TiposTicketsClientesModel> GuardarTiposTicketsClientes(TiposTicketsClientesModel tiposTicketsClientesModel)
        {
            return iConfigurarParametrosTicketsBusiness.GuardarTiposTicketsClientes(tiposTicketsClientesModel);
        }

        [HttpPost]
        public SingleResponse<IList<ConfigurarParametrosTicketsModelo>> ConsultarConfigurarParametros(ConfigurarParametrosTicketsModelo configurarParametros)
        {
            return iConfigurarParametrosTicketsBusiness.ConsultarConfigurarParametros(configurarParametros);
        }

        [HttpPost]
        public SingleResponse<IList<ClienteProductoModel>> ConsultarClientesConfigurarParametros(ClienteProductoModel clienteProductoModel)
        {
            return iConfigurarParametrosTicketsBusiness.ConsultarClientesConfigurarParametros(clienteProductoModel);
        }

        [HttpPost]
        public SingleResponse<IList<PersonaResponsableModel>> BuscarUsuarioResponsable(PersonaResponsableModel personaResponsableModel)
        {
            return iConfigurarParametrosTicketsBusiness.BuscarUsuarioResponsable(personaResponsableModel);
        }

        [HttpPost]
        public SingleResponse<bool> EliminarTipoTicketsCliente(TiposTicketModel tiposTicket)
        {
            return iConfigurarParametrosTicketsBusiness.EliminarTipoTicketsCliente(tiposTicket);
        }

        [HttpPost]
        public SingleResponse<TiposTicketsClientesModel> ActulizarTiposTicketsClientes(TiposTicketsClientesModel tiposTicketsClientesModel)
        {
            return iConfigurarParametrosTicketsBusiness.ActulizarTiposTicketsClientes(tiposTicketsClientesModel);
        }
    }
}