using AM45Secure.Business.Business.Comparador;
using AM45Secure.Business.Business.Cotizador;
using AM45Secure.Business.Business.Emitir;
using AM45Secure.Business.IBusiness.ICotizador;
using Ninject.Modules;
using AM45Secure.Business.IBusiness.Tickets;
using AM45Secure.Business.Business.Tickets;
using AM45Secure.Business.IBusiness.IEmitir;
using AM45Secure.Business.IBusiness.IComparador;
using AM45Secure.Business.Business.Imprimir;
using AM45Secure.Business.Business.Seguridad;
using AM45Secure.Business.IBusiness.IImprimir;
using AM45Secure.Business.IBusiness.ISeguridad;
using AM45Secure.Business.IBusiness.IConfigurador;
using AM45Secure.Business.Business.Configurador;
using AM45Secure.Business.IBusiness.IManuales;
using AM45Secure.Business.Business.Manuales;
using AM45Secure.Business.IBusiness.IimagenCliente;
using AM45Secure.Business.Business.ImagenCliente;

namespace AM45Secure.RESTServices.Infrastructure.Ninject
{
    public class RestServicesModule : NinjectModule
    {

        public override void Load()
        {
            Kernel.Bind<ICotizadorBusiness>().To<CotizadorBusiness>();
            Kernel.Bind<IReporteBusiness>().To<ReporteBusiness>();
            Kernel.Bind<IGestionBussiness>().To<GestionBusiness>();
            Kernel.Bind<IEmitirBusiness>().To<EmitirBusiness>();
            Kernel.Bind<IComparadorBusiness>().To<ComparadorBusiness>();
            Kernel.Bind<ICalendarioTicketsBusiness>().To<CalendarioTicketsBusiness>();
            Kernel.Bind<IConfigurarParametrosTicketsBusiness>().To<ConfigurarParametrosTicketsBusiness>();
            Kernel.Bind<ISeguimientoTicketsBusiness>().To<SeguimientoTicketsBusiness>();
            Kernel.Bind<IImprimirBusiness>().To<ImprimirBusiness>();
            Kernel.Bind<ISeguridadBusiness>().To<SeguridadBusiness>();
            Kernel.Bind<IConfiguradorBusiness>().To<ConfiguradorBusiness>();
            Kernel.Bind<IManualesBussines>().To<ManualesBusiness>();
            Kernel.Bind<IimagenClienteBusiness>().To<ImagenClienteBusiness>();

        }
    }
}