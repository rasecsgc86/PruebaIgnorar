using AM45Secure.DataAccess.DataAccess.Comparador;
using AM45Secure.DataAccess.DataAccess.Cotizador;
using AM45Secure.DataAccess.DataAccess.Emitir;
using AM45Secure.DataAccess.IDataAccess.ICotizador;
using AM45Secure.DataAccess.IDataAccess.IEmitir;
using Ninject.Modules;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using AM45Secure.DataAccess.DataAccess.Tickets;
using AM45Secure.DataAccess.IDataAccess.IComparador;
using AM45Secure.DataAccess.IDataAccess.IImprimir;
using AM45Secure.DataAccess.DataAccess.Imprimir;
using AM45Secure.DataAccess.DataAccess.Seguridad;
using AM45Secure.DataAccess.IDataAccess.ISeguridad;
using AM45Secure.DataAccess.IDataAccess.IConfigurador;
using AM45Secure.DataAccess.DataAccess.Configurador;
using AM45Secure.DataAccess.IDataAccess.IManuales;
using AM45Secure.DataAccess.DataAccess.Manuales;
using AM45Secure.DataAccess.IDataAccess.IimagenCliente;
using AM45Secure.DataAccess.DataAccess.ImagenCliente;

namespace AM45Secure.RESTServices.Infrastructure.Ninject
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ICotizadorDataAccess>().To<CotizadorDataAccess>();
            Kernel.Bind<IEmitirDataAccess>().To<EmitirDataAccess>();
            Kernel.Bind<IReporteDataAccess>().To<ReporteDataAccess>();
            Kernel.Bind<ICalendarioTicketsDataAcces>().To<CalendarioTicketsDataAccess>();
            Kernel.Bind<IComparadorDataAccess>().To<ComparadorDataAccess>();
            Kernel.Bind<IConfigurarParametrosTicketsDataAccess>().To<ConfigurarParametrosTicketsDataAccess>();
            Kernel.Bind<ISeguimientoTicketsDataAccess>().To<SeguimientoTicketsDataAccess>();
            Kernel.Bind<IImprimirDataAccess>().To<ImprimirDataAccess>();
            Kernel.Bind<ISeguridadDataAccess>().To<SeguridadDataAccess>();
            Kernel.Bind<IConfiguradorDataAccess>().To<ConfiguradorDataAccess>();
            Kernel.Bind<IManualesDataAccess>().To<ManualesDataAccess>();
            Kernel.Bind<IimagenClienteDataAccess>().To<ImagenClienteDataAccess>();
        }
    }
}