using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Tickets;
using Zero.Handlers.Response;

namespace AM45Secure.Business.IBusiness.Tickets
{
    public interface ISeguimientoTicketsBusiness
    {
        SingleResponse<SeguiminetoTicketsModel> ConsultarInformacionTicket(SeguiminetoTicketsModel seguiminetoTicketsModel);
        SingleResponse<SeguiminetoTicketsModel> ConsultarInformacionTicketLectura(SeguiminetoTicketsModel seguiminetoTicketsModel);
        SingleResponse<IList<CatEstatusTicketsModel>> ObetnerEstatusByUsuario(SeguiminetoTicketsModel seguiminetoTicketsModel);
        SingleResponse<ComentariosTicketModel> GuardarComentariosTicket(ComentariosTicketModel comentariosTicketModel);
        SingleResponse<IList<ComentariosTicketModel>> ListarComentariosTicket(ComentariosTicketModel comentariosTicketModel);
        SingleResponse<IList<ArchivoTicketsModel>> ListaArchivosTickets(SeguiminetoTicketsModel seguiminetoTicketsModel);
        SingleResponse<bool> EliminarArchivo(ArchivoTicketsModel archivoTicketsModel);
        SingleResponse<ArchivoTicketsModel> GuardarArchivoSeguimiento(ArchivoTicketsModel archivoTicketsModel);
        SingleResponse<IList<PersonaResponsableModel>> BuscarUsuarioResponsable(PersonaResponsableModel personaResponsableModel);
        SingleResponse<RegistroTicketsModel> ReasignarResposnable(RegistroTicketsModel registroTicketsModel);
        SingleResponse<TicketsEstatusModel> GuardarSeguimientoCierreSinArchivo(TicketsEstatusModel ticketsEstatusModel);
        SingleResponse<TicketsEstatusModel> GuardarArchivoSeguimientoCierre(GuardaSeguimientoTicketsModel guardaSeguimientoTickets);
        SingleResponse<ArchivoTicketsModel> CargarArchivoSeguimiento(GuardaSeguimientoTicketsModel guardaSeguimientoTickets);
        IList<TicketsEstatusModel> ValidaEstatusTicket(TicketsEstatusModel ticketsEstatus);
        SingleResponse<ComentariosTicketModel> GuardaComentarioTicket(ComentariosTicketModel comentariosTicketModel);
    }
}