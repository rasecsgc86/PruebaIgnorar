using AM45Secure.Commons.Modelos.Tickets;
using System.Collections.Generic;

namespace AM45Secure.DataAccess.IDataAccess.Tickets
{
    public interface IReporteDataAccess
    {
        IList<ReporteModel> ConsultarTicketsReporte(ReporteModel reporteModel);
        IList<EstatusModel> ConsultarEstatusTicketsReporte();
        IList<ReporteExcelModel> ConsultarTicketsReporteExcel(ReporteModel reporteModel);
    }
}
