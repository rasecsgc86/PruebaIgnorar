using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.Entidades.Comunes;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using Zero.Ado.Models;
using Zero.Exceptions;

namespace AM45Secure.DataAccess.DataAccess.Tickets
{
    public class CalendarioTicketsDataAccess : ICalendarioTicketsDataAcces
    {
        private readonly IGenericDataAccess iGenericDataAccess;

        public CalendarioTicketsDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        public IList<CalendarioModel> ConsultarCalendario()
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<CatDiasHabiles> listaCal = iGenericDataAccess.Consultar(
                                                                              new CatDiasHabiles(),
                                                                              new OptionsQueryZero()
                                                                              {
                                                                                  ExcludeNumericsDefaults = true,
                                                                                  ExcludeBool = true,
                                                                                  ExcludeWhere = true
                                                                              });
                iGenericDataAccess.CloseConnection();
                IList<CalendarioModel> calendarioModels = listaCal.
                    Select(
                           x => new CalendarioModel()
                                {
                                    IdDiaHabil = x.IdDiaHabil,
                                    Dia = x.Dia,
                                    FechaDia = DateToString(x.Dia)
                                }).ToList().OrderBy(x => x.Dia).ToList();
                return calendarioModels;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesCalendario.ERR_07_02, e);
            }
        }


        public CalendarioModel GuardarCalendario(CalendarioModel calendarioModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<CatDiasHabiles> findDia = iGenericDataAccess.
                    Consultar(new CatDiasHabiles()
                    {
                        Dia = calendarioModel.Dia.Date
                    },
                                                                             new OptionsQueryZero()
                                                                             {
                                                                                 ExcludeNumericsDefaults = true,
                                                                                 ExcludeBool = true
                                                                             });
                if (findDia.Count > 0)
                {
                    throw new DalException(CodesCalendario.INF_07_00);
                }
                else
                {
                    CatDiasHabiles obj = iGenericDataAccess.
                        Guardar(new CatDiasHabiles()
                        {
                            PersonaId = 177080,
                            Dia = calendarioModel.Dia,
                            FechaRegistro = DateTime.Today
                        });
                    iGenericDataAccess.OpenConnection();
                    calendarioModel.IdDiaHabil = obj.IdDiaHabil;
                    calendarioModel.PersonaId = obj.PersonaId;
                    calendarioModel.FechaDia = DateToString(obj.Dia);
                }

            }
            catch (DalException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesCalendario.ERR_07_03, e);
            }
            return calendarioModel;
        }

        public bool EliminarCalendario(CalendarioModel calendarioModel)
        {
            bool ban = true;
            try
            {
                if (!EsDiaInhabil(calendarioModel))
                {
                    iGenericDataAccess.OpenConnection();
                    iGenericDataAccess.Eliminar(new CatDiasHabiles()
                                                {
                                                    IdDiaHabil = calendarioModel.IdDiaHabil
                                                });
                    iGenericDataAccess.CloseConnection();
                }
                else
                {
                    throw new DomainException(CodesCalendario.INF_07_01);
                }
            }
            catch (DomainException e)
            {
                ban = false;
                iGenericDataAccess.CloseConnection();
                throw new DomainException(CodesCalendario.INF_07_01, e);
            }
            catch (Exception e)
            {
                ban = false;
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesCalendario.ERR_07_04, e);
            }
            return ban;

        }

        public bool EsDiaInhabil(CalendarioModel calendario)
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
                                                                                                WhereComplementary = "IdDiaHabil = " + calendario.IdDiaHabil
                                                                                            });

                iGenericDataAccess.CloseConnection();

                return listCatDiasInhabiles[0].Dia.Date <= DateTime.Today;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesSeguiminetorTickets.ERR_00_11, e);
            }
        }

        public string DateToString(DateTime date)
        {
            CultureInfo culture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentCulture = culture;
            string fecha = date.ToString("dd/MM/yyyy");
            return fecha;
        }
    }
}
