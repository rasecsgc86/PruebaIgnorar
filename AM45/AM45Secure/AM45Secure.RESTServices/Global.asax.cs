using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AM45Secure.Commons.Recursos;
using AM45Secure.Commons.Zero.Exceptions.Codes;
using Zero.Handlers.Messages;

namespace AM45Secure.RESTServices
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            Messages.DefaultMessage.Add(ZeroCodes.MSJ_00_01, "REQ_00_01");//Mensaje default requeridos
            Messages.DefaultMessage.Add(ZeroCodes.MSJ_00_02, "REQ_00_02");//Mensaje default validaciones
            Messages.LoadMessages(typeof(Codes));
            Messages.LoadMessages(typeof(CodesCotizador));
        }
    }
}
