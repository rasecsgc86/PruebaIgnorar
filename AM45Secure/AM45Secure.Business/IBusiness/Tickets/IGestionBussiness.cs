using System;
using AM45Secure.Commons.Modelos.Tickets;
using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;
using Zero.Handlers.Response;

namespace AM45Secure.Business.IBusiness.Tickets
{
    public interface IGestionBussiness
    {
        SingleResponse<IList<TicketModel>> ConsultarTickets(TicketModel ticketModel);
        SingleResponse<IList<ClienteProductoModel>> ConsultarClientes(ClienteProductoModel clienteProductoModel);

        SingleResponse<bool> ConsultarSiEsClienteFlotillas(ClienteProductoModel clienteProductoModel);
        SingleResponse<IList<ClienteProductoModel>> ConsultarCaratula(ClienteProductoModel clienteProductoModel);

        SingleResponse<ClienteProductoModel> ConsultarResponsable(ClienteProductoModel clienteProductoModel);
        SingleResponse<IList<ClienteProductoModel>> ConsultarTiposTickets(ClienteProductoModel clienteProductoModel);
        SingleResponse<IList<CatOrigenTicketsModel>> ConsultarReportaA();
        SingleResponse<IList<CatEstatusTicketsModel>> ConsultaEstatusTickets();
        SingleResponse<IList<AgenciasModel>> ConsultarAgencias(AgenciasClienteModel agenciasCliente);
        SingleResponse<bool> GuardarTicket(TicketModel ticketModel);
        SingleResponse<ArchivoTicketsModel> GuardarArchivo(ArchivoTicketsModel archivoTicketsModel);
        SingleResponse<bool> EliminarArchivo(ArchivoTicketsModel archivoTicketsModel);
        SingleResponse<IList<AsegModel>> ConsultaAseguradoras(ClienteAsegModel clienteAseg);
        DateTime CalculoFechaRecepcion();
    }
}