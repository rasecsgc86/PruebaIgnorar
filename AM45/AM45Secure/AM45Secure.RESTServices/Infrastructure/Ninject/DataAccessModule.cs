using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AM45Secure.DataAccess.DataAccess.Generic;
using AM45Secure.DataAccess.DataAccess.Tickets;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using Ninject.Modules;

namespace AM45Secure.RESTServices.Infrastructure.Ninject
{
    public class DataAccessModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IGenericDataAccess>().To<GenericDataAccess>();
            Kernel.Bind<IGestionDataAccess>().To<GestionDataAccess>();
        }
    }
}