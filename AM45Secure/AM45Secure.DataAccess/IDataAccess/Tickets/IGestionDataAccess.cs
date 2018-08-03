using System;
using AM45Secure.Commons.Modelos.Tickets;
using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;

namespace AM45Secure.DataAccess.IDataAccess.Tickets
{
    public interface IGestionDataAccess
    {
        IList<TicketModel> ConsultarTickest(TicketModel ticketModel);
        IList<ClienteProductoModel> ConsultarClientes(ClienteProductoModel clienteProductoModel);
        IList<AgenciasModel> ConsultarAgencias(AgenciasClienteModel agenciasCliente);
        bool ConsultarSiEsClienteFlotillas(ClienteProductoModel clienteProductoModel);
        IList<ClienteProductoModel> ConsultarCaratula(ClienteProductoModel clienteProductoModel);
        IList<ClienteProductoModel> ConsultarResponsable(ClienteProductoModel clienteProductoModel);
        IList<ClienteProductoModel> ConsultarTiposTickets(ClienteProductoModel clienteProductoModel);
        IList<CatOrigenTicketsModel> ConsultarReportaA();
        IList<CatEstatusTicketsModel> ConsultaEstatusTickets();
        TicketModel GuardarTicket(TicketModel ticketModel);
        ArchivoTicketsModel GuardarArchivo(ArchivoTicketsModel archivoTicketsModel);
        bool EliminarArchivo(ArchivoTicketsModel idArchivoTicket);
        IList<CatDiasInhabilesModel> ConsultarDiasInhabiles(DateTime fechaRecepcion);
        IList<AsegModel> ConsultaAseguradoras(ClienteAsegModel clienteAseg);
    }
}