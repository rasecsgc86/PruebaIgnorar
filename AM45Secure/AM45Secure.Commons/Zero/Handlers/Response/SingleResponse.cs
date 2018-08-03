using System.Collections.Generic;
using System.Linq;
using AM45Secure.Commons.Recursos;
using Zero.Exceptions;
using Zero.Utils;
using Zero.Utils.Models;

namespace Zero.Handlers.Response
{
    public class SingleResponse<TResponse>
    {
        public TResponse Response { get; private set; }
        
        public bool IsOk { get; private set; }
        
        public string Message { get; private set; }
        private IList<Validation> validations;
        
        public IList<Validation> Validations
        {
            get
            {
                if (validations == null)
                {
                    validations = new List<Validation>();
                }
                return validations;
            }
            private set { validations = value; }
        }

        public SingleResponse(){}

     

        public void SetValidations(IList<Validation> validations)
        {
            this.validations = validations;
            Response = default(TResponse);
            IsOk = false;
            Logger.Info(Codes.SingleResponseValidations);
        }

        public void Done(TResponse response, string message, params object[] parameters)
        {
            Response = response;
            Message = string.Format(
                                    message,
                                    parameters);
            IsOk = true;
        }

        public void Error(DomainException exception)
        {
            IsOk = false;
            Response = default(TResponse);
            Message = exception.Mensaje;
            if (exception.ErrorOrigen == null)
            {
                Logger.Error(Message);
            }
            else
            {
                Logger.Error(Message,exception.ErrorOrigen);
            }
            
        }

        public void Error(DalException exception)
        {
            IsOk = false;
            Response = default(TResponse);
            Message = exception.Mensaje;
            if (exception.ErrorOrigen == null)
            {
                Logger.Error(Message);
            }
            else
            {
                Logger.Error(Message, exception.ErrorOrigen);
            }

        }

        public void ThrowIfNotOk()
        {
            if (!IsOk && !Validations.Any())
            {
                throw new DomainException(Message);
            }
            if (!IsOk && Validations.Any())
            {
                throw new DomainValidationsException(Validations);
            }
        }
        
    }
}
