using System;
using System.Collections.Generic;
using System.Linq;
using AM45Secure.Commons.Zero.Exceptions.Codes;
using Zero.Utils.Models;

namespace Zero.Handlers.Response
{
    public class HandlerError
    {

        public List<string> Errors { get; private set; }
        public List<Validation> Validations { get; }
        public List<string> Infos { get; private set; }

        public HandlerError()
        {
            Infos = new List<string>();
            Errors = new List<string>();
            Validations = new List<Validation>();
        }

        public void Add<TResponse>(SingleResponse<TResponse> response)
        {
            if (response != null)
            {
                if (response.IsOk)
                {
                    Infos.Add(response.Message);
                    Infos = Infos.Select(x => x).Distinct().ToList();
                }
                else
                {
                    if (response.Validations.Any())
                    {
                        Validations.AddRange(response.Validations);
                    }
                    else
                    {
                        Errors.Add(response.Message);
                        Errors = Errors.Select(x => x).Distinct().ToList();
                    }
                    
                }
            }
            else
            {
                Errors.Add(ZeroCodes.WRN_00_00);
            }
        }

        public string Message()
        {
            string mensaje = string.Empty;
            if (Validations.Any())
            {
                foreach (Validation validation in Validations)
                {
                    mensaje += validation.Message;
                    mensaje += @"\n";
                }
            }

            if (Errors.Any())
            {
                foreach (string error in Errors)
                {
                    mensaje += error;
                    mensaje += @"\n";
                }
            }
            
            return mensaje;
        }

        public string SimpleMessage()
        {
            string mensaje = string.Empty;
            if (Validations.Any())
            {
                foreach (Validation validation in Validations)
                {
                    mensaje += validation.Message;
                    mensaje += "\n";
                }
            }

            if (Errors.Any())
            {
                foreach (string error in Errors)
                {
                    mensaje += error;
                    mensaje += "\n";
                }
            }

            return mensaje;
        }

        public bool ToPrint()
        {
            return Errors.Count > 0 || Validations.Count > 0;
        }

        public static string MessageError(string message, Exception e)
        {
            return message;
        }

        public static string MessageError(string message)
        {
            return message;
        }

        public static string MessageInfo(string message)
        {
            return message;
        }
    }
}
