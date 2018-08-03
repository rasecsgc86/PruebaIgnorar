using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Tickets;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.DataAccess.Entidades.Comunes;

namespace AM45Secure.DataAccess.IDataAccess.Tickets
{
    public interface IConfigurarParametrosTicketsDataAccess
    {
        TiposTicketsClientesModel GuardarTiposTicketsClientes(TiposTicketsClientesModel tiposTicketsClientesModel);
        TiposTicketsClientesModel ActulizarTiposTicketsClientes(TiposTicketsClientesModel tiposTicketsClientesModel);
        bool EliminarTipoTicketsCliente(TiposTicketModel tiposTicket);
        IList<ConfigurarParametrosTicketsModelo> ConsultarConfigurarParametros(ConfigurarParametrosTicketsModelo configurarParametros);
        IList<ClienteProductoModel> ConsultarClientesConfigurarParametros(ClienteProductoModel clienteProductoModel);
        IList<PersonaResponsableModel> BuscarUsuarioResponsable(PersonaResponsableModel personaResponsableModel);
    }
}