using System;
using System.Linq;
using Zero.Exceptions;
using Zero.Ado.Models;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using AM45Secure.Commons.Recursos;
using AM45Secure.Commons.Modelos.Tickets;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.DataAccess.Entidades.Tickets;
using AM45Secure.DataAccess.Entidades.Comunes;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using AM45Secure.DataAccess.IDataAccess.IGeneric;

namespace AM45Secure.DataAccess.DataAccess.Tickets
{
    public class ReporteDataAccess : IReporteDataAccess
    {
        private readonly IGenericDataAccess iGenericDataAccess;
        private readonly ISeguimientoTicketsDataAccess iSeguimientoTicketsDataAccess;
        private const string FILTRO_DIAS_INHABILES = "Dia > '{0}' order by Dia ASC";
        private readonly IFormatProvider fotmato = new CultureInfo("pt-PT");

        public ReporteDataAccess(IGenericDataAccess iGenericDataAccess, ISeguimientoTicketsDataAccess iSeguimientoTicketsDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
            this.iSeguimientoTicketsDataAccess = iSeguimientoTicketsDataAccess;
        }

        public IList<EstatusModel> ConsultarEstatusTicketsReporte()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<CatEstatusTickets> catEstatusTickets = iGenericDataAccess.Consultar(
                                                                                          new CatEstatusTickets(),
                                                                                          new OptionsQueryZero()
                                                                                          {
                                                                                              ExcludeNumericsDefaults = true,
                                                                                              ExcludeBool = true,
                                                                                              ExcludeWhere = true
                                                                                          });
                iGenericDataAccess.CloseConnection();
                // Create a list of parts.
                IList<EstatusModel> estatusList = catEstatusTickets.Select(
                                                                           x => new EstatusModel()
                                                                                {
                                                                                    IdEstatusTicket = x.IdEstatusTicket,
                                                                                    Descripcion = x.Descripcion
                                                                                }
                                                                          ).ToList();
                estatusList.Add(new EstatusModel()
                                {
                                    IdEstatusTicket = 0,
                                    Descripcion = "Todos"
                                });
                return estatusList;
            }
            catch (Exception e)
            {
                throw new DalException(CodesTickets.ERR_00_01, e);
            }
        }

        public IList<ReporteModel> ConsultarTicketsReporte(ReporteModel reporteModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                string filtroConsultaReporte = String.Empty;

                VwTicketsRepSelTicketsReporte vwTicketsRep = new VwTicketsRepSelTicketsReporte
                                                             {
                                                                 DescripcionEstatus = reporteModel.DescripcionEstatus
                                                             };

                if (!string.IsNullOrEmpty(reporteModel.FechaInicio) && !string.IsNullOrEmpty(reporteModel.FechaFin))
                {
                    filtroConsultaReporte = "CAST(FechaRegistro AS DATE) >= CAST( '" + reporteModel.FechaInicio + "' AS DATE) AND CAST(FechaRegistro AS DATE) <= CAST('" + reporteModel.FechaFin + "' AS DATE)";
                }

                IList<VwTicketsRepSelTicketsReporte> vwTicketsReporte = iGenericDataAccess.Consultar(
                                                                                                     vwTicketsRep,
                                                                                                     new OptionsQueryZero()
                                                                                                     {
                                                                                                         ExcludeNumericsDefaults = true,
                                                                                                         ExcludeBool = true,
                                                                                                         WhereComplementary = filtroConsultaReporte
                                                                                                     });
                iGenericDataAccess.CloseConnection();

                IList<ReporteModel> ticketsList = new List<ReporteModel>();
                TiempoAtencionModel tiempoAtencion = new TiempoAtencionModel();

                for (int i = 0; i<vwTicketsReporte.Count; i++)
                {
                    TiempoDeAtencionModel tiempoDeAtencion = iSeguimientoTicketsDataAccess.CalculaTiempoAtencion(vwTicketsReporte[i].FechaRecepcion, vwTicketsReporte[i].HorasAtencion, (vwTicketsReporte[i].FechaCierre == "N/A") ? DateTime.Now : Convert.ToDateTime(vwTicketsReporte[i].FechaCierre, fotmato));

                    tiempoAtencion.DiasInhabiles = vwTicketsReporte[i].DiasInhabiles;
                    tiempoAtencion.FechaRecepcion = vwTicketsReporte[i].FechaRecepcion;

                    ReporteModel reporteModelFinal = new ReporteModel
                                                     {
                                                         TicketId = vwTicketsReporte[i].TicketId,
                                                         FechaRegistro = DateToString(vwTicketsReporte[i].FechaRegistro),
                                                         FechaRecepcion = DateToString(vwTicketsReporte[i].FechaRecepcion),
                                                         FechaCierre = vwTicketsReporte[i].FechaCierre,
                                                         Cliente = vwTicketsReporte[i].Cliente,
                                                         Caratula = vwTicketsReporte[i].Caratula,
                                                         DescripcionTicket = vwTicketsReporte[i].DescripcionTicket,
                                                         NombrePer = vwTicketsReporte[i].NombrePer,
                                                         PaternoPer = string.IsNullOrEmpty(vwTicketsReporte[i].PaternoPer) ? string.Empty : vwTicketsReporte[i].PaternoPer,
                                                         MaternoPer = string.IsNullOrEmpty(vwTicketsReporte[i].MaternoPer) ? string.Empty : vwTicketsReporte[i].MaternoPer,
                                                         HorasAtencion = vwTicketsReporte[i].HorasAtencion,
                                                         DescripcionEstatus = vwTicketsReporte[i].DescripcionEstatus,
                                                         TiempoAtencion = tiempoDeAtencion.Dias + " dia(s) , " + tiempoDeAtencion.Horas + " hora(s)",
                                                         CveEstatus = vwTicketsReporte[i].CveEstatus,
                                                         NumTicket = i + 1,
                                                         AseguradoraId = vwTicketsReporte[i].AseguradoraId,
                                                         Nombre = (vwTicketsReporte[i].AseguradoraId == 0) ? "NA" : vwTicketsReporte[i].Nombre,
                                                         NumeroOt = vwTicketsReporte[i].NumeroOt,
                                                         NumeroOtSics = vwTicketsReporte[i].NumeroOtSics,
                                                         EstatusAtencion = tiempoDeAtencion.EnTiempo
                                                     };

                    ticketsList.Add(reporteModelFinal);
                }

                return ticketsList.OrderBy(x => x.TicketId).ToList();
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_01, e);
            }
        }

        public IList<ReporteExcelModel> ConsultarTicketsReporteExcel(ReporteModel reporteModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                string filtroConsultaReporte = String.Empty;

                VwTicketsRepSelTicketsReporteExcel vwTicketsRep = new VwTicketsRepSelTicketsReporteExcel();

                if (reporteModel.DescripcionEstatus != "Todos")
                {
                    filtroConsultaReporte = "DescripcionEstatus = '" + reporteModel.DescripcionEstatus + "' AND ";
                }

                if (!string.IsNullOrEmpty(reporteModel.FechaInicio) && !string.IsNullOrEmpty(reporteModel.FechaFin))
                {
                    filtroConsultaReporte = filtroConsultaReporte + "CAST(FechaRegistro AS DATE) >= CAST( '" + reporteModel.FechaInicio + "' AS DATE) AND CAST(FechaRegistro AS DATE) <= CAST('" + reporteModel.FechaFin + "' AS DATE)";
                }

                IList<VwTicketsRepSelTicketsReporteExcel> vwTicketsReporte = iGenericDataAccess.Consultar(
                                                                                                          vwTicketsRep,
                                                                                                          new OptionsQueryZero()
                                                                                                          {
                                                                                                              ExcludeNumericsDefaults = true,
                                                                                                              ExcludeBool = true,
                                                                                                              WhereComplementary = filtroConsultaReporte
                                                                                                          });
                iGenericDataAccess.CloseConnection();

                IList<ReporteExcelModel> ticketsList = new List<ReporteExcelModel>();
                TiempoAtencionModel tiempoAtencion = new TiempoAtencionModel();

                for (int i = 0; i<vwTicketsReporte.Count; i++)
                {
                    tiempoAtencion.DiasInhabiles = vwTicketsReporte[i].DiasInhabiles;
                    tiempoAtencion.FechaRecepcion = vwTicketsReporte[i].FechaRecepcion;

                    TiempoDeAtencionModel tiempoDeAtencion = iSeguimientoTicketsDataAccess.CalculaTiempoAtencion(vwTicketsReporte[i].FechaRecepcion, vwTicketsReporte[i].HorasAtencion, (vwTicketsReporte[i].FechaCierre == "N/A") ? DateTime.Now : Convert.ToDateTime(vwTicketsReporte[i].FechaCierre, fotmato));

                    ReporteExcelModel reporteModelFinal = new ReporteExcelModel
                                                          {
                                                              TicketId = vwTicketsReporte[i].TicketId,
                                                              FechaRegistro = DateToString(vwTicketsReporte[i].FechaRegistro),
                                                              FechaRecepcion = DateToString(vwTicketsReporte[i].FechaRecepcion),
                                                              FechaCierre = vwTicketsReporte[i].FechaCierre,
                                                              Cliente = vwTicketsReporte[i].Cliente,
                                                              Caratula = vwTicketsReporte[i].Caratula,
                                                              TipoTicket = vwTicketsReporte[i].TipoTicket,
                                                              DescripcionTicket = vwTicketsReporte[i].DescripcionTicket,
                                                              NombrePer = vwTicketsReporte[i].NombrePer,
                                                              PaternoPer = string.IsNullOrEmpty(vwTicketsReporte[i].PaternoPer) ? string.Empty : vwTicketsReporte[i].PaternoPer,
                                                              MaternoPer = string.IsNullOrEmpty(vwTicketsReporte[i].MaternoPer) ? string.Empty : vwTicketsReporte[i].MaternoPer,
                                                              HorasAtencion = vwTicketsReporte[i].HorasAtencion,
                                                              EnTiempo = vwTicketsReporte[i].EnTiempo,
                                                              DescripcionEstatus = vwTicketsReporte[i].DescripcionEstatus,
                                                              TiempoAtencion = tiempoAtencion.Calculate(),
                                                              CveEstatus = vwTicketsReporte[i].CveEstatus,
                                                              NumTicket = i + 1,
                                                              AseguradoraId = vwTicketsReporte[i].AseguradoraId,
                                                              Nombre = (vwTicketsReporte[i].AseguradoraId == 0) ? "NA" : vwTicketsReporte[i].Nombre,
                                                              NumeroOt = vwTicketsReporte[i].NumeroOt,
                                                              NumeroOtSics = vwTicketsReporte[i].NumeroOtSics,
                                                              Usuario = vwTicketsReporte[i].Usuario,
                                                              Alias = vwTicketsReporte[i].Alias,
                                                              Perfil = vwTicketsReporte[i].Perfil,
                                                              Comentario = vwTicketsReporte[i].Comentario,
                                                              CalTiempo = tiempoDeAtencion.Dias + " Dias," + tiempoDeAtencion.Horas + " Horas"
                                                          };

                    ticketsList.Add(reporteModelFinal);
                }

                return ticketsList.OrderBy(x => x.TicketId).ToList();
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_01, e);
            }
        }

        public string DateToString(DateTime date)
        {
            CultureInfo culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            string fecha = date.ToString("dd/MM/yyyy HH:mm:ss");
            return fecha;
        }

        public int ConsultarTotalDiasInhabiles(DateTime fechaRecepcion)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                var queryFinal = "SELECT COUNT(Dia) totalInhabiles FROM CatDiasHabiles WHERE Dia >= {FechaRecepcion} AND Dia < GETDATE()";
                queryFinal = queryFinal.Replace("{FechaRecepcion}", "'" + fechaRecepcion.ToString(("yyyy-MM-dd hh:mm:ss")) + "'");
                IList<CatDiasHabilesEntidad> inhabiles = iGenericDataAccess.ExecuteQuery<CatDiasHabilesEntidad>(queryFinal);

                return inhabiles[0].TotalInhabiles;
            }
            catch (Exception e)
            {
                throw new DalException("Error:::", e);
            }
        }

        public string CalculaTiempoAtencion(string segundos)
        {
            TimeSpan t2 = TimeSpan.FromSeconds(double.Parse(segundos));
            string timepoAtencion = t2.Days.ToString() + " dia(s) y " +
                                    t2.Hours.ToString() + " hora(s)";
            return timepoAtencion;
        }

        public string DiasInhabiles(string segundos, DateTime fechaRecepcion)
        {
            int diasMover = 0;
            int diasInhabiles = ConsultarDiasInhabiles();
            DateTime inicio = fechaRecepcion; // Se inicializa la variable inicio con la fecha de recepcion
            DateTime fechaFinal = DateTime.Today;
            while (inicio<=fechaFinal)
            {
                //Se excluyen los fines de semana para restarlos en la fecha final
                if (inicio.DayOfWeek == DayOfWeek.Saturday || inicio.DayOfWeek == DayOfWeek.Sunday)
                {
                    diasMover += 86400;
                }
                inicio = inicio.AddDays(1);
            }
            diasInhabiles = diasInhabiles * 86400;

            var final = Math.Abs(Int32.Parse(segundos)) - diasMover - diasInhabiles;
            segundos = CalculaTiempoAtencion(final.ToString());
            return segundos;
        }

        public int ConsultarDiasInhabiles()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<CatDiasInhabiles> listCatDiasInhabiles = iGenericDataAccess.Consultar(
                                                                                            new CatDiasInhabiles(),
                                                                                            new OptionsQueryZero()
                                                                                            {
                                                                                                ExcludeNumericsDefaults = true,
                                                                                                ExcludeBool = true,
                                                                                                WhereComplementary = string.Format(FILTRO_DIAS_INHABILES, DateTime.Now.ToString("yyyy-MM-dd"))
                                                                                            });
                iGenericDataAccess.CloseConnection();
                IList<CatDiasInhabilesModel> listCatInhabiles = listCatDiasInhabiles.Select(
                                                                                            x => new CatDiasInhabilesModel()
                                                                                                 {
                                                                                                     IdDiaHabil = x.IdDiaHabil,
                                                                                                     Dia = x.Dia,
                                                                                                     PersonaID = x.PersonaID,
                                                                                                     FechaRegistro = x.FechaRegistro,
                                                                                                 }).ToList();
                return listCatInhabiles.Count;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_15, e);
            }
        }
    }
}