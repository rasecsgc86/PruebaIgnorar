using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.Tickets;
using Zero.Handlers.Response;

namespace AM45Secure.Business.IBusiness.Tickets
{
    public interface IReporteBusiness
    {
        SingleResponse<IList<ReporteModel>> ConsultarTicketsReporte(ReporteModel reporteModel);
        SingleResponse<IList<EstatusModel>> ConsultarEstatusTickets();
        byte[] ConsultarTicketsReporteExcel(ReporteModel reporteModel);
    }
}
