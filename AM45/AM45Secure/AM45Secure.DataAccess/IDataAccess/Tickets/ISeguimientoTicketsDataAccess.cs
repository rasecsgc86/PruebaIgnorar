using System;
using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Tickets;

namespace AM45Secure.DataAccess.IDataAccess.Tickets
{
    public interface ISeguimientoTicketsDataAccess
    {
        SeguiminetoTicketsModel ConsultarInformacionTicket(SeguiminetoTicketsModel seguiminetoTicketsModel);
        SeguiminetoTicketsModel ConsultarInformacionTicketLectura(SeguiminetoTicketsModel seguiminetoTicketsModel);
        IList<CatEstatusTicketsModel> ObetnerEstatusByUsuario(SeguiminetoTicketsModel seguiminetoTicketsModel);
        ComentariosTicketModel GuardarComentariosTicket(ComentariosTicketModel comentariosTicketModel);
        IList<ComentariosTicketModel> ListarComentariosTicket(ComentariosTicketModel comentariosTicketModel);
        IList<ArchivoTicketsModel> ListaArchivosTickets(SeguiminetoTicketsModel seguiminetoTicketsModel);
        IList<ArchivoTicketsModel> ListaArchivosTickets(int ticketId);
        bool EliminarArchivo(ArchivoTicketsModel idArchivoTicket);
        ArchivoTicketsModel GuardarArchivoSeguimiento(ArchivoTicketsModel archivoTicketsModel);
        ConsultarDatosCorreoModel ObtenerDatosCorreo(int ticketId);
        IList<PersonaResponsableModel> BuscarUsuarioResponsable(PersonaResponsableModel personaResponsableModel);
        RegistroTicketsModel ReasignarResposnable(RegistroTicketsModel registroTicketsModel);
        TicketsEstatusModel GuardarSeguimientoCierreSinArchivo(TicketsEstatusModel ticketsEstatusModel);
        IList<CatDiasInhabilesModel> ConsultarDiasInhabiles();
        TicketModel BuscarTicketSeguimiento(ComentariosTicketModel comentariosTicketModel);
        IList<CorreosCopiaTicketsModel> ObtenerCorreosCopiaTickets(int ticketId);
        TiposTicketsClientesModel ActualizaResposnableTiposTicketClientes(ConsultarDatosCorreoModel consultarDatosCorreo);
        TiempoDeAtencionModel CalculaTiempoAtencion(DateTime fechaRecepcion, int horasAtencion, DateTime fechaFinal);
        IList<TicketsEstatusModel> ValidaEstatusTicket(TicketsEstatusModel ticketsEstatus);
        PersonaResponsableModel BuscaPersonaResposnableTicket(RegistroTicketsModel registroTickets);
    }
}