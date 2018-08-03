using System;
using System.Collections.Generic;
using AM45Secure.Business.IBusiness.Tickets;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using Zero.Exceptions;
using Zero.Handlers.Response;

namespace AM45Secure.Business.Business.Tickets
{
    public class CalendarioTicketsBusiness : ICalendarioTicketsBusiness
    {
        private readonly ICalendarioTicketsDataAcces iCalendarioTicketsDataAcces;

        public CalendarioTicketsBusiness(ICalendarioTicketsDataAcces iCalendarioTicketsDataAcces)
        {
            this.iCalendarioTicketsDataAcces = iCalendarioTicketsDataAcces;
        }
        public SingleResponse<CalendarioModel> GuardarCalendario(CalendarioModel calendarioModel)
        {
            SingleResponse<CalendarioModel> response = new SingleResponse<CalendarioModel>();
            try
            {
                CalendarioModel calModel = iCalendarioTicketsDataAcces.GuardarCalendario(calendarioModel);
                response.Done(calModel, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesCalendario.ERR_07_03, e));
            }
            return response;
        }

        public SingleResponse<IList<CalendarioModel>> ConsultarCalendario()
        {
            SingleResponse<IList<CalendarioModel>> response = new SingleResponse<IList<CalendarioModel>>();
            try
            {
                IList<CalendarioModel> lista = iCalendarioTicketsDataAcces.ConsultarCalendario();
                response.Done(lista, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesCalendario.ERR_07_03, e));
            }
            return response;
        }

        public SingleResponse<bool> EliminarCalendario(CalendarioModel calendarioModel)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
           
            bool ban = iCalendarioTicketsDataAcces.EliminarCalendario(calendarioModel);
            response.Done(ban, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesCalendario.ERR_07_04, e));
            }
            return response;
        }
    }
}
