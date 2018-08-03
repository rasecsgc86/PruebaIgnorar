using System.Collections.Generic;
using AM45Secure.Business.IBusiness.Tickets;
using AM45Secure.Commons.Modelos.Comunes;
using Zero.Handlers.Response;
using System.Web.Http;

namespace AM45Secure.RESTServices.Controllers.Tickets
{
    public class CalendarioTicketsController : ApiController
    {
        private readonly ICalendarioTicketsBusiness iCalendarioTicketsBusiness;

        public CalendarioTicketsController(ICalendarioTicketsBusiness iCalendarioTicketsBusiness)
        {
            this.iCalendarioTicketsBusiness = iCalendarioTicketsBusiness;
        }
        [HttpPost]
        public SingleResponse<CalendarioModel> GuardarCalendario(CalendarioModel calendarioModel)
        {
            return iCalendarioTicketsBusiness.GuardarCalendario(calendarioModel);
        }
        [HttpPost]
        public SingleResponse<IList<CalendarioModel>> ConsultarCalendario()
        {
            return iCalendarioTicketsBusiness.ConsultarCalendario();
        }
        [HttpPost]
        public SingleResponse<bool> EliminarCalendario(CalendarioModel calendarioModel)
        {
            return iCalendarioTicketsBusiness.EliminarCalendario(calendarioModel);
        }

    }
}