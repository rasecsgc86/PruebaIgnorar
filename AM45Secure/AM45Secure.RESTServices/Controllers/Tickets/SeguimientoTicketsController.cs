using System.Collections.Generic;
using System.Web.Http;
using AM45Secure.Business.IBusiness.Tickets;
using AM45Secure.Commons.Modelos.Tickets;
using Zero.Handlers.Response;


namespace AM45Secure.RESTServices.Controllers.Tickets
{
    public class SeguimientoTicketsController : ApiController
    {
        private readonly ISeguimientoTicketsBusiness iSeguimientoTicketsBusiness;

        public SeguimientoTicketsController(ISeguimientoTicketsBusiness iSeguimientoTicketsBusiness)
        {
            this.iSeguimientoTicketsBusiness = iSeguimientoTicketsBusiness;
        }

        [HttpPost]
        public SingleResponse<SeguiminetoTicketsModel> ConsultarInformacionTicket(SeguiminetoTicketsModel seguiminetoTicketsModel)
        {
            return iSeguimientoTicketsBusiness.ConsultarInformacionTicket(seguiminetoTicketsModel);
        }

        [HttpPost]
        public SingleResponse<IList<CatEstatusTicketsModel>> ObetnerEstatusByUsuario(SeguiminetoTicketsModel seguiminetoTicketsModel)
        {
            return iSeguimientoTicketsBusiness.ObetnerEstatusByUsuario(seguiminetoTicketsModel);
        }

        [HttpPost]
        public SingleResponse<ComentariosTicketModel> GuardarComentariosTicket(ComentariosTicketModel comentariosTicketModel)
        {
            return comentariosTicketModel.IdEstatusTicket == 0 && !comentariosTicketModel.Cerrado ? iSeguimientoTicketsBusiness.GuardaComentarioTicket(comentariosTicketModel) : iSeguimientoTicketsBusiness.GuardarComentariosTicket(comentariosTicketModel);
        }

        [HttpPost]
        public SingleResponse<IList<ComentariosTicketModel>> ListarComentariosTicket(ComentariosTicketModel comentariosTicketModel)
        {
            return iSeguimientoTicketsBusiness.ListarComentariosTicket(comentariosTicketModel);
        }

        [HttpPost]
        public SingleResponse<IList<ArchivoTicketsModel>> ListaArchivosTickets(SeguiminetoTicketsModel seguiminetoTicketsModel)
        {
            return iSeguimientoTicketsBusiness.ListaArchivosTickets(seguiminetoTicketsModel);
        }

        [HttpPost]
        public SingleResponse<bool> EliminarArchivo(ArchivoTicketsModel archivoTicketsModel)
        {
            return iSeguimientoTicketsBusiness.EliminarArchivo(archivoTicketsModel);
        }

        [HttpPost]
        public SingleResponse<ArchivoTicketsModel> CargarArchivoSeguimiento(string ticketId, string idEstatusTicket)
        {
            GuardaSeguimientoTicketsModel guardaSeguimientoTickets = new GuardaSeguimientoTicketsModel
                                                                     {
                                                                         TicketId = ticketId,
                                                                         IdEstatusTicket = idEstatusTicket
                                                                     };

            return iSeguimientoTicketsBusiness.CargarArchivoSeguimiento(guardaSeguimientoTickets);
        }

        [HttpPost]
        public SingleResponse<IList<PersonaResponsableModel>> BuscarUsuarioResponsableSeguimiento(PersonaResponsableModel personaResponsableModel)
        {
            return iSeguimientoTicketsBusiness.BuscarUsuarioResponsable(personaResponsableModel);
        }

        public SingleResponse<RegistroTicketsModel> ReasignarResposnable(RegistroTicketsModel registroTicketsModel)
        {
            return iSeguimientoTicketsBusiness.ReasignarResposnable(registroTicketsModel);
        }

        public SingleResponse<TicketsEstatusModel> GuardarArchivoSeguimientoCierre(string ticketId, string idEstatusTicket, string idTicketEstatus, string personaId, string tkn)
        {
            GuardaSeguimientoTicketsModel guardaSeguimientoTickets = new GuardaSeguimientoTicketsModel
                                                                     {
                                                                         TicketId = ticketId,
                                                                         IdEstatusTicket = idEstatusTicket,
                                                                         IdTicketEstatus = idTicketEstatus,
                                                                         PersonaId = personaId,
                                                                         Tkn = tkn
                                                                     };

            return iSeguimientoTicketsBusiness.GuardarArchivoSeguimientoCierre(guardaSeguimientoTickets);
        }

        [HttpPost]
        public SingleResponse<TicketsEstatusModel> GuardarSeguimientoCierreSinArchivo(TicketsEstatusModel ticketsEstatusModel)
        {
            return iSeguimientoTicketsBusiness.GuardarSeguimientoCierreSinArchivo(ticketsEstatusModel);
        }
    }
}