using System.Collections.Generic;
using AM45Secure.Commons.Modelos.Comunes;


namespace AM45Secure.DataAccess.IDataAccess.Tickets
{
    public interface ICalendarioTicketsDataAcces
    {
        CalendarioModel GuardarCalendario(CalendarioModel calendarioModel);
        bool EliminarCalendario(CalendarioModel calendarioModel);
        IList<CalendarioModel> ConsultarCalendario();
    }
}
