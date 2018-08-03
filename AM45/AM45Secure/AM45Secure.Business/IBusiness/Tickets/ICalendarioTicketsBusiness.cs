using System;
using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;
using Zero.Handlers.Response;

namespace AM45Secure.Business.IBusiness.Tickets
{
    public interface ICalendarioTicketsBusiness
    {
        SingleResponse<CalendarioModel> GuardarCalendario(CalendarioModel calendarioModel);
        SingleResponse<IList<CalendarioModel>> ConsultarCalendario();
        SingleResponse<bool> EliminarCalendario(CalendarioModel calendarioModel);
    }
}
