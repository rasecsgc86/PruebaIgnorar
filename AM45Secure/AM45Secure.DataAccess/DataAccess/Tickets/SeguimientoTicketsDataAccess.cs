using System;
using System.Linq;
using System.Text;
using Zero.Ado.Models;
using Zero.Exceptions;
using System.Collections.Generic;
using System.Globalization;
using AM45Secure.Commons.Recursos;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Tickets;
using AM45Secure.Commons.Constantes.Comunes;
using AM45Secure.DataAccess.Entidades.Comunes;
using AM45Secure.DataAccess.Entidades.Tickets;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using AM45Secure.DataAccess.IDataAccess.IGeneric;

namespace AM45Secure.DataAccess.DataAccess.Tickets
{
    public class SeguimientoTicketsDataAccess : ISeguimientoTicketsDataAccess
    {
        private readonly IGenericDataAccess iGenericDataAccess;
        private int REGISTRADO = 1;
        private int PROCESO = 2;
        private int TRAMITE = 3;
        private int CERRADO = 6;
        private int CANCELADO = 7;
        private string FiltroDiasInhabiles = "Dia > GETDATE() order by Dia ASC";
        private readonly IFormatProvider formato = new CultureInfo("pt-PT");

        public SeguimientoTicketsDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        public SeguiminetoTicketsModel ConsultarInformacionTicket(SeguiminetoTicketsModel seguiminetoTicketsModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                VwTicObtenerInformacionTickets vwTicSeguimiento = iGenericDataAccess.BuscarUno(new VwTicObtenerInformacionTickets
                                                                                               {
                                                                                                   TicketId = seguiminetoTicketsModel.TicketId,
                                                                                                   PersonaIdTipoUsuarioTicket = seguiminetoTicketsModel.GetIdUsuarioSesion()
                                                                                               },
                                                                                               new OptionsQueryZero
                                                                                               {
                                                                                                   ExcludeNumericsDefaults = true,
                                                                                                   ExcludeBool = true
                                                                                               });
                SeguiminetoTicketsModel model2 = null;

                if (vwTicSeguimiento != null)
                {
                    SeguiminetoTicketsModel model = new SeguiminetoTicketsModel
                                                    {
                                                        TicketId = vwTicSeguimiento.TicketId,
                                                        IdEstatusTicket = vwTicSeguimiento.IdEstatusTicket,
                                                        CveEstatus = vwTicSeguimiento.CveEstatus,
                                                        IdTicketEstatus = vwTicSeguimiento.IdTicketEstatus,
                                                        PersonaId = vwTicSeguimiento.PersonaId,
                                                        IdCliente = vwTicSeguimiento.IdCliente,
                                                        NombreCliente = vwTicSeguimiento.NombreCliente,
                                                        Caratula = vwTicSeguimiento.Caratula
                                                    };

                    VwCotSelClientProdAgenAseg flotilla = iGenericDataAccess.BuscarUno(new VwCotSelClientProdAgenAseg
                                                                                       {
                                                                                           PersonaIdOpcionA = model.IdCliente,
                                                                                           ValorIdB = 118, //Producto Flotillas
                                                                                           OpcionA = ConstElementos.Clientes,
                                                                                           OpcionB = ConstElementos.Productos
                                                                                       },
                                                                                       new OptionsQueryZero
                                                                                       {
                                                                                           ExcludeNumericsDefaults = true,
                                                                                           ExcludeBool = true
                                                                                       });

                    if ((model.CveEstatus == REGISTRADO && seguiminetoTicketsModel.IsCarga
                         && vwTicSeguimiento.PersonaId == seguiminetoTicketsModel.GetIdUsuarioSesion())
                        || (model.CveEstatus == REGISTRADO && seguiminetoTicketsModel.IsCarga
                            && vwTicSeguimiento.PersonaIdTipoUsuarioTicket == seguiminetoTicketsModel.GetIdUsuarioSesion()
                            && vwTicSeguimiento.TipoUsuario.Equals("Escalamiento")))
                    {
                        CatEstatusTickets catEstatusTickets = iGenericDataAccess.BuscarUno(new CatEstatusTickets
                                                                                           {
                                                                                               CveEstatus = PROCESO
                                                                                           },
                                                                                           new OptionsQueryZero
                                                                                           {
                                                                                               ExcludeNumericsDefaults = true,
                                                                                               ExcludeBool = true
                                                                                           });
                        if (catEstatusTickets != null)
                        {
                            iGenericDataAccess.Actualizar(new TicketsEstatus
                                                          {
                                                              Activo = false,
                                                              IdTicketEstatus = model.IdTicketEstatus,
                                                              TicketId = model.TicketId,
                                                              PersonaId = model.PersonaId,
                                                              IdEstatusTicket = model.IdEstatusTicket,
                                                              FechaRegistro = DateTime.Today
                                                          });

                            iGenericDataAccess.Guardar(new TicketsEstatus
                                                       {
                                                           IdEstatusTicket = catEstatusTickets.IdEstatusTicket,
                                                           PersonaId = model.PersonaId,
                                                           TicketId = model.TicketId,
                                                           FechaRegistro = DateTime.Today,
                                                           Activo = true
                                                       });
                        }
                        else
                        {
                            throw new DalException(CodesSeguiminetorTickets.INF_08_02); //clave estatus no encontrada, revisar valores
                        }
                    }

                    VwTicObtenerInformacionTickets vwTicSeguimiento2 = iGenericDataAccess.BuscarUno(new VwTicObtenerInformacionTickets
                                                                                                    {
                                                                                                        TicketId = seguiminetoTicketsModel.TicketId,
                                                                                                        PersonaIdTipoUsuarioTicket = seguiminetoTicketsModel.GetIdUsuarioSesion()
                                                                                                    },
                                                                                                    new OptionsQueryZero
                                                                                                    {
                                                                                                        ExcludeNumericsDefaults = true,
                                                                                                        ExcludeBool = true
                                                                                                    });

                    model2 = new SeguiminetoTicketsModel();
                    TiempoAtencionModel tiempoAtencion = new TiempoAtencionModel();
                    model2.TicketId = vwTicSeguimiento2.TicketId;
                    model2.TipoTicket = vwTicSeguimiento2.DescripcionTicket;
                    model2.DescripcionTicket = vwTicSeguimiento2.DescripcionTicket;
                    model2.NombrePer = vwTicSeguimiento2.NombrePer;
                    model2.PaternoPer = vwTicSeguimiento2.PaternoPer;
                    model2.MaternoPer = vwTicSeguimiento2.MaternoPer;
                    tiempoAtencion.DiasInhabiles = ConsultarTotalDiasInhabiles(vwTicSeguimiento2.FechaRecepcion, (vwTicSeguimiento2.FechaCierre == "N/A") ? DateTime.Now : Convert.ToDateTime(vwTicSeguimiento2.FechaCierre, formato));
                    tiempoAtencion.FechaRecepcion = vwTicSeguimiento2.FechaRecepcion;
                    //var horas = tiempoAtencion.Calculate();
                    TiempoDeAtencionModel tiempoDeAtencion = CalculaTiempoAtencion(tiempoAtencion.FechaRecepcion, vwTicSeguimiento2.HorasAtencion, (vwTicSeguimiento2.FechaCierre == "N/A") ? DateTime.Now : Convert.ToDateTime(vwTicSeguimiento2.FechaCierre, formato));
                    model2.TiempoAtencion = tiempoDeAtencion.Dias + " dia(s) , " + tiempoDeAtencion.Horas + " hora(s)";
                    model2.EstatusAtencion = tiempoDeAtencion.EnTiempo;
                    model2.IdEstatusTicket = vwTicSeguimiento2.IdEstatusTicket;
                    model2.CveEstatus = vwTicSeguimiento2.CveEstatus;
                    model2.IdTicketEstatus = vwTicSeguimiento2.IdTicketEstatus;
                    model2.PersonaId = vwTicSeguimiento2.PersonaId;
                    model2.FechaRegistroDate = vwTicSeguimiento2.FechaRecepcion;
                    model2.IdCliente = vwTicSeguimiento2.IdCliente;
                    model2.PersonaIdTipoUsuarioTicket = vwTicSeguimiento2.PersonaIdTipoUsuarioTicket;
                    model2.TipoUsuario = vwTicSeguimiento2.TipoUsuario;
                    model2.UsuarioSesion = vwTicSeguimiento2.PersonaIdTipoUsuarioTicket;
                    model2.NumeroOt = vwTicSeguimiento2.NumeroOt;
                    model2.NumeroOtsics = vwTicSeguimiento2.NumeroOtsics;
                    model2.Duenio = vwTicSeguimiento2.Duenio;
                    model2.SiFlotilla = flotilla != null;
                    model2.DescripcionTicket = vwTicSeguimiento2.Descripcion;
                    model2.DescripcionEstatus = vwTicSeguimiento2.DescripcionEstatus;
                    model2.AseguradoraId = vwTicSeguimiento2.AseguradoraId;
                    model2.Nombre = vwTicSeguimiento2.Nombre;
                    model2.NombreCliente = vwTicSeguimiento2.NombreCliente;
                    model2.Caratula = vwTicSeguimiento2.Caratula ?? "N/A";
                }

                iGenericDataAccess.CloseConnection();
                return model2;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public SeguiminetoTicketsModel ConsultarInformacionTicketLectura(SeguiminetoTicketsModel seguiminetoTicketsModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                VwTicObtenerInformacionTickets vwTicSeguimiento = iGenericDataAccess.
                    BuscarUno(new VwTicObtenerInformacionTickets
                              {
                                  TicketId = seguiminetoTicketsModel.TicketId,
                                  PersonaIdTipoUsuarioTicket = seguiminetoTicketsModel.GetIdUsuarioSesion()
                              }, new OptionsQueryZero
                                 {
                                     ExcludeNumericsDefaults = true,
                                     ExcludeBool = true
                                 });
                iGenericDataAccess.CloseConnection();
                SeguiminetoTicketsModel model = null;
                if (vwTicSeguimiento != null)
                {
                    model = new SeguiminetoTicketsModel
                            {
                                TicketId = vwTicSeguimiento.TicketId,
                                CveEstatus = vwTicSeguimiento.CveEstatus,
                                TipoUsuario = vwTicSeguimiento.TipoUsuario
                            };
                }
                return model;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public ComentariosTicketModel GuardarComentariosTicket(ComentariosTicketModel comentariosTicketModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                CatEstatusTickets catEstatusTickets = new CatEstatusTickets();

                if (comentariosTicketModel.CveEstatus != CANCELADO && comentariosTicketModel.CveEstatus != CERRADO && !comentariosTicketModel.Cerrado)
                {
                    RegistrosTicket updateTicket1 = iGenericDataAccess.Consultar(new RegistrosTicket()
                                                                                 {
                                                                                     TicketId = comentariosTicketModel.TicketModelUpdate.TicketId
                                                                                 }, new OptionsQueryZero()
                                                                                    {
                                                                                        ExcludeNumericsDefaults = true,
                                                                                        ExcludeBool = true
                                                                                    })[0];
                    updateTicket1.AseguradoraId = comentariosTicketModel.AseguradoraId;

                    iGenericDataAccess.Actualizar(updateTicket1);
                }
                if (comentariosTicketModel.CveEstatus == TRAMITE && comentariosTicketModel.TicketModelUpdate.ResponsableId == comentariosTicketModel.GetIdUsuarioSesion())
                {
                    RegistrosTicket updateTicket3 = iGenericDataAccess.Consultar(new RegistrosTicket()
                                                                                 {
                                                                                     TicketId = comentariosTicketModel.TicketModelUpdate.TicketId
                                                                                 }, new OptionsQueryZero()
                                                                                    {
                                                                                        ExcludeNumericsDefaults = true,
                                                                                        ExcludeBool = true
                                                                                    })[0];
                    updateTicket3.NumeroOT = comentariosTicketModel.RegistroTicketsUpdate.NumeroOt;
                    updateTicket3.NumeroOTSICS = comentariosTicketModel.RegistroTicketsUpdate.NumeroOtsics;
                    iGenericDataAccess.Actualizar(updateTicket3);
                }
                if (comentariosTicketModel.Cerrado)
                {
                    catEstatusTickets = iGenericDataAccess.
                        BuscarUno(new CatEstatusTickets
                                  {
                                      CveEstatus = CERRADO
                                  }, new OptionsQueryZero
                                     {
                                         ExcludeNumericsDefaults = true,
                                         ExcludeBool = true
                                     });
                }
                ComentariosTicket comentariosTicket = new ComentariosTicket
                                                      {
                                                          PersonaId = comentariosTicketModel.GetIdUsuarioSesion(),
                                                          Comentario = string.IsNullOrWhiteSpace(comentariosTicketModel.Comentario) ? string.Empty : comentariosTicketModel.Comentario,
                                                          TicketId = comentariosTicketModel.TicketId,
                                                          FechaRegistro = DateTime.Now,
                                                          IdEstatusTicket = catEstatusTickets.IdEstatusTicket
                                                      };
                if (comentariosTicketModel.IdEstatusTicket == 0)
                {
                    comentariosTicket.IdEstatusTicket = comentariosTicketModel.IdEstatusTicketActual;
                    iGenericDataAccess.Guardar(comentariosTicket);
                }
                else
                {
                    comentariosTicket.IdEstatusTicket = comentariosTicketModel.IdEstatusTicket;
                    ComentariosTicket entidad = iGenericDataAccess.Guardar(comentariosTicket);
                    if (comentariosTicketModel.Cerrado)
                    {
                        catEstatusTickets = iGenericDataAccess.
                            BuscarUno(new CatEstatusTickets
                                      {
                                          CveEstatus = CERRADO
                                      }, new OptionsQueryZero
                                         {
                                             ExcludeNumericsDefaults = true,
                                             ExcludeBool = true
                                         });
                    }
                    else
                    {
                        catEstatusTickets.IdEstatusTicket = comentariosTicketModel.IdEstatusTicket;
                    }

                    comentariosTicketModel.ComentarioId = entidad.ComentarioId;

                    if (comentariosTicketModel.CveEstatus != REGISTRADO)
                    {
                        if (comentariosTicketModel.IdEstatusTicket != comentariosTicketModel.IdEstatusTicketActual)
                        {
                            iGenericDataAccess.Actualizar(new TicketsEstatus()
                                                          {
                                                              IdTicketEstatus = comentariosTicketModel.TicketsEstatusUpdate.IdTicketEstatus,
                                                              TicketId = comentariosTicketModel.TicketsEstatusUpdate.TicketId,
                                                              IdEstatusTicket = comentariosTicketModel.IdEstatusTicketActual,
                                                              PersonaId = comentariosTicketModel.PersonaId,
                                                              FechaRegistro = DateTime.Now,
                                                              Activo = false
                                                          });

                            //Insertar el nuevo estatus del ticket
                            iGenericDataAccess.Guardar(new TicketsEstatus
                                                       {
                                                           TicketId = comentariosTicketModel.TicketsEstatusUpdate.TicketId,
                                                           IdEstatusTicket = comentariosTicketModel.IdEstatusTicket,
                                                           PersonaId = comentariosTicketModel.PersonaId,
                                                           FechaRegistro = DateTime.Now,
                                                           NombreArchivoTicketCerrado = "",
                                                           RutaArchivoTicketCerrado = "",
                                                           Activo = true
                                                       });
                            RegistrosTicket updateTicket = iGenericDataAccess.Consultar(new RegistrosTicket()
                                                                                        {
                                                                                            TicketId = comentariosTicketModel.TicketModelUpdate.TicketId
                                                                                        }, new OptionsQueryZero()
                                                                                           {
                                                                                               ExcludeNumericsDefaults = true,
                                                                                               ExcludeBool = true
                                                                                           })[0];

                            updateTicket.TicketId = comentariosTicketModel.TicketModelUpdate.TicketId;
                            updateTicket.FechaRegistro = comentariosTicketModel.TicketModelUpdate.FechaRegistro;
                            updateTicket.FechaRecepcion = comentariosTicketModel.TicketModelUpdate.FechaRecepcion;
                            updateTicket.TipoId = comentariosTicketModel.TicketModelUpdate.TipoId;
                            updateTicket.Descripcion = comentariosTicketModel.TicketModelUpdate.DescripcionTicket;
                            updateTicket.ResponsableId = comentariosTicketModel.TicketModelUpdate.ResponsableId;
                            updateTicket.UsuarioId = comentariosTicketModel.TicketModelUpdate.UsuarioId;
                            updateTicket.IdCliente = comentariosTicketModel.TicketModelUpdate.IdCliente;
                            updateTicket.NumeroOT = comentariosTicketModel.RegistroTicketsUpdate.NumeroOt;
                            updateTicket.NumeroOTSICS = comentariosTicketModel.RegistroTicketsUpdate.NumeroOtsics;
                            iGenericDataAccess.Actualizar(updateTicket);
                        }
                    }
                    if (catEstatusTickets.CveEstatus == CERRADO)
                    {
                        iGenericDataAccess.Actualizar(new TicketsEstatus()
                                                      {
                                                          IdTicketEstatus = comentariosTicketModel.TicketsEstatusUpdate.IdTicketEstatus,
                                                          TicketId = comentariosTicketModel.TicketId,
                                                          IdEstatusTicket = catEstatusTickets.IdEstatusTicket,
                                                          PersonaId = comentariosTicketModel.PersonaId,
                                                          FechaRegistro = DateTime.Now,
                                                          Activo = false
                                                      });

                        //Insertar el nuevo estatus del ticket
                        iGenericDataAccess.Guardar(new TicketsEstatus
                                                   {
                                                       TicketId = comentariosTicketModel.TicketId,
                                                       IdEstatusTicket = catEstatusTickets.IdEstatusTicket,
                                                       PersonaId = comentariosTicketModel.PersonaId,
                                                       FechaRegistro = DateTime.Now,
                                                       NombreArchivoTicketCerrado = comentariosTicketModel.ArchivoTickets.NombreArchivo,
                                                       RutaArchivoTicketCerrado = comentariosTicketModel.ArchivoTickets.RutaArchivo,
                                                       Activo = true
                                                   });
                    }
                    if (comentariosTicketModel.CveEstatus == REGISTRADO) // si cambia a registrado 
                    {
                        iGenericDataAccess.BuscarUno(new CatEstatusTickets
                                                     {
                                                         CveEstatus = REGISTRADO
                                                     }, new OptionsQueryZero
                                                        {
                                                            ExcludeNumericsDefaults = true,
                                                            ExcludeBool = true
                                                        });

                        RegistrosTicket updateTicket2 = iGenericDataAccess.Consultar(new RegistrosTicket()
                                                                                     {
                                                                                         TicketId = comentariosTicketModel.TicketModelUpdate.TicketId
                                                                                     }, new OptionsQueryZero()
                                                                                        {
                                                                                            ExcludeNumericsDefaults = true,
                                                                                            ExcludeBool = true
                                                                                        })[0];

                        updateTicket2.TicketId = comentariosTicketModel.TicketModelUpdate.TicketId;
                        updateTicket2.FechaRegistro = comentariosTicketModel.TicketModelUpdate.FechaRegistro;
                        updateTicket2.FechaRecepcion = comentariosTicketModel.TicketModelUpdate.FechaRecepcion;
                        updateTicket2.TipoId = comentariosTicketModel.TicketModelUpdate.TipoId;
                        updateTicket2.Descripcion = comentariosTicketModel.TicketModelUpdate.DescripcionTicket;
                        updateTicket2.ResponsableId = comentariosTicketModel.TicketModelUpdate.ResponsableId;
                        updateTicket2.UsuarioId = comentariosTicketModel.TicketModelUpdate.UsuarioId;
                        updateTicket2.IdCliente = comentariosTicketModel.TicketModelUpdate.IdCliente;
                        updateTicket2.NumeroOT = comentariosTicketModel.RegistroTicketsUpdate.NumeroOt;
                        updateTicket2.NumeroOTSICS = comentariosTicketModel.RegistroTicketsUpdate.NumeroOtsics;
                        iGenericDataAccess.Actualizar(updateTicket2);
                        iGenericDataAccess.Actualizar(new TicketsEstatus()
                                                      {
                                                          IdTicketEstatus = comentariosTicketModel.TicketsEstatusUpdate.IdTicketEstatus,
                                                          TicketId = comentariosTicketModel.TicketsEstatusUpdate.TicketId,
                                                          IdEstatusTicket = comentariosTicketModel.IdEstatusTicketActual,
                                                          PersonaId = comentariosTicketModel.PersonaId,
                                                          FechaRegistro = DateTime.Now,
                                                          Activo = false
                                                      });

                        //Insertar el nuevo estatus del ticket
                        iGenericDataAccess.Guardar(new TicketsEstatus
                                                   {
                                                       TicketId = comentariosTicketModel.TicketsEstatusUpdate.TicketId,
                                                       IdEstatusTicket = comentariosTicketModel.IdEstatusTicket,
                                                       PersonaId = comentariosTicketModel.PersonaId,
                                                       FechaRegistro = DateTime.Now,
                                                       NombreArchivoTicketCerrado = "",
                                                       RutaArchivoTicketCerrado = "",
                                                       Activo = true
                                                   });
                    }
                }

                iGenericDataAccess.CloseConnection();
                return comentariosTicketModel;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);
            }
        }

        public IList<ComentariosTicketModel> ListarComentariosTicket(ComentariosTicketModel comentariosTicketModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<VwTicSeleccionarComentariosTickets> vwComentarios = iGenericDataAccess.
                    Consultar(new VwTicSeleccionarComentariosTickets
                              {
                                  TicketId = comentariosTicketModel.TicketId
                              },
                              new OptionsQueryZero
                              {
                                  ExcludeBool = true,
                                  ExcludeNumericsDefaults = true,
                                  WhereComplementary = "Comentario !=''"
                              });
                iGenericDataAccess.CloseConnection();
                IList<ComentariosTicketModel> listaComentarios = vwComentarios.Select(x => new ComentariosTicketModel()
                                                                                           {
                                                                                               TicketId = x.TicketId,
                                                                                               Comentario = x.Comentario,
                                                                                               Nombre = x.Nombre,
                                                                                               Materno = x.Materno,
                                                                                               Paterno = x.Paterno,
                                                                                               FechaComentario = x.FechaRegistro.ToString("d"),
                                                                                               Estatus = x.Estatus
                                                                                           }).ToList();
                return listaComentarios;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_01, e);
            }
        }

        public IList<CatEstatusTicketsModel> ObetnerEstatusByUsuario(SeguiminetoTicketsModel seguiminetoTicketsModel)
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
                IList<CatEstatusTicketsModel> estatusList = catEstatusTickets.Select(
                                                                                     x => new CatEstatusTicketsModel()
                                                                                          {
                                                                                              IdEstatusTicket = x.IdEstatusTicket,
                                                                                              Descripcion = x.Descripcion,
                                                                                              CveEstatus = x.CveEstatus
                                                                                          }
                                                                                    ).ToList();
                estatusList.Add(new CatEstatusTicketsModel()
                                {
                                    IdEstatusTicket = 0,
                                    Descripcion = "Seleccionar",
                                    CveEstatus = 0
                                });

                return estatusList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_01, e);
            }
        }


        //public bool CalculaTiempoEstatus(int horasAtencion, DateTime fechaRecepcion)
        //{
        //    var addDias = horasAtencion / 9;
        //    var addHoras = horasAtencion % 9;
        //    var horasComplementariasDia = fechaRecepcion.Hour + addHoras;
        //    var horasComplementariasHorario = 17 - fechaRecepcion.Hour;
        //    var cantidadDiasFestivos = ConsultarTotalDiasInhabiles(fechaRecepcion);
        //    var diasMover = cantidadDiasFestivos;
        //    var actual = DateTime.Now;
        //    var fechaFinal = fechaRecepcion.AddDays(addDias);

        //    // Valida si las Horas Complementarias por dia [Horas de la Fecha Recepción + Horas del reciduo de las horas de Atención]
        //    if (horasComplementariasDia>17)
        //    {
        //        diasMover += 1;
        //        TimeSpan horaNueva = TimeSpan.Parse("08:00:00"); // Se inicia el dia a las 8 horas.
        //        fechaFinal = fechaFinal.Date + horaNueva;
        //        fechaFinal = fechaFinal.AddHours(addHoras + (horasComplementariasHorario<0 ? 0 : horasComplementariasHorario));
        //    }
        //    else
        //    {
        //        fechaFinal = fechaFinal.AddHours(addHoras);
        //    }

        //    var inicio = fechaRecepcion;

        //    while (inicio<=actual)
        //    {
        //        if (inicio.DayOfWeek == DayOfWeek.Saturday || inicio.DayOfWeek == DayOfWeek.Sunday)
        //        {
        //            diasMover++;
        //        }
        //        inicio = inicio.AddDays(1);
        //    }

        //    fechaFinal = fechaFinal.AddDays(diasMover);

        //    return (actual<=fechaFinal);
        //}


        //public string CalculaTiempo(int minutos)
        //{
        //    TimeSpan t2 = TimeSpan.FromMinutes(minutos);
        //    string timepoAtencion = t2.Hours.ToString() + " hora(s)";
        //    return timepoAtencion;
        //}

        public IList<ArchivoTicketsModel> ListaArchivosTickets(SeguiminetoTicketsModel seguiminetoTicketsModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                IList<ArchivosTickets> listaArchivosTicketses = iGenericDataAccess.
                    Consultar(new ArchivosTickets
                              {
                                  TicketId = seguiminetoTicketsModel.TicketId
                              }, new OptionsQueryZero
                                 {
                                     ExcludeNumericsDefaults = true,
                                     ExcludeBool = true
                                 });
                iGenericDataAccess.CloseConnection();
                IList<ArchivoTicketsModel> lista = listaArchivosTicketses.
                    Select(x => new ArchivoTicketsModel
                                {
                                    NombreArchivo = x.NombreArchivo,
                                    RutaArchivo = x.RutaArchivo,
                                    IdArchivoTicket = x.IdArchivoTicket
                                }).ToList();
                return lista;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_01, e);
            }
        }

        public IList<ArchivoTicketsModel> ListaArchivosTickets(int ticketId)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                IList<ArchivosTickets> listaArchivosTicketses = iGenericDataAccess.
                    Consultar(new ArchivosTickets
                    {
                        TicketId = ticketId
                    }, new OptionsQueryZero
                    {
                        ExcludeNumericsDefaults = true,
                        ExcludeBool = true
                    });
                iGenericDataAccess.CloseConnection();
                IList<ArchivoTicketsModel> lista = listaArchivosTicketses.
                    Select(x => new ArchivoTicketsModel
                    {
                        NombreArchivo = x.NombreArchivo,
                        RutaArchivo = x.RutaArchivo,
                        IdArchivoTicket = x.IdArchivoTicket
                    }).ToList();
                return lista;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_01, e);
            }
        }

        public bool EliminarArchivo(ArchivoTicketsModel archivoTicketsModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                iGenericDataAccess.Eliminar(new ArchivosTickets()
                                            {
                                                IdArchivoTicket = archivoTicketsModel.IdArchivoTicket
                                            });

                iGenericDataAccess.CloseConnection();

                return true;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_14, e);
            }
        }

        public ArchivoTicketsModel GuardarArchivoSeguimiento(ArchivoTicketsModel archivoTicketsModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                ArchivosTickets archivoTickets = iGenericDataAccess.Guardar(new ArchivosTickets()
                                                                            {
                                                                                NombreArchivo = archivoTicketsModel.NombreArchivo,
                                                                                RutaArchivo = archivoTicketsModel.RutaArchivo,
                                                                                TicketId = archivoTicketsModel.TicketId,
                                                                                IdEstatusTicket = archivoTicketsModel.IdEstatusTicket
                                                                            });
                iGenericDataAccess.CloseConnection();
                archivoTicketsModel.IdArchivoTicket = archivoTickets.IdArchivoTicket;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_13, e);
            }
            return archivoTicketsModel;
        }


        public ConsultarDatosCorreoModel ObtenerDatosCorreo(int ticketId)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                VwTicConsultarDatosCorreo obj = iGenericDataAccess.
                    BuscarUno(new VwTicConsultarDatosCorreo
                              {
                                  TicketId = ticketId
                              },
                              new OptionsQueryZero
                              {
                                  ExcludeBool = true,
                                  ExcludeNumericsDefaults = true
                              });
                iGenericDataAccess.CloseConnection();
                ConsultarDatosCorreoModel model = new ConsultarDatosCorreoModel
                                                  {
                                                      TicketId = obj.TicketId,
                                                      Descripcion = obj.Descripcion,
                                                      IdCliente = obj.IdCliente,
                                                      TipoId = obj.TipoId,
                                                      FechaRegistro = obj.FechaRegistro,
                                                      FechaRecepcion = obj.FechaRecepcion,
                                                      DescripcionTicket = obj.DescripcionTicket,
                                                      UsuarioLevanto = obj.UsuarioLevanto,
                                                      MailLevanto = obj.MailLevanto,
                                                      MailResponsable = obj.MailResponsable,
                                                      MailResponsableInicial = obj.MailResponsableInicial,
                                                      IdPersonaResponsable = obj.IdPersonaResponsable,
                                                      PersonaResponsable = obj.PersonaResponsable,
                                                      IdPersonaResponsableInicial = obj.IdPersonaResponsableInicial,
                                                      PersonaResponsableInicial = obj.PersonaResponsableInicial,
                                                      QuienCerroTicket = obj.QuienCerroTicket,
                                                      MailQuienCerroTicket = obj.MailQuienCerroTicket,
                                                      NombreReporta = obj.NombreReporta,
                                                      MailReporta = obj.MailReporta
                                                  };
                return model;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_13, e);
            }
        }

        public IList<PersonaResponsableModel> BuscarUsuarioResponsable(PersonaResponsableModel personaResponsableModel)
        {
            try
            {
                StringBuilder filtro = new StringBuilder();
                filtro.Append(" Responsable LIKE '%" + personaResponsableModel.Nombre + "%'");

                iGenericDataAccess.OpenConnection();
                IList<VwTicConsultaResponsables> vwTicConsultaResponsableses = iGenericDataAccess.Consultar(new VwTicConsultaResponsables(),
                                                                                                            new OptionsQueryZero
                                                                                                            {
                                                                                                                ExcludeNumericsDefaults = true,
                                                                                                                ExcludeBool = true,
                                                                                                                WhereComplementary = filtro.ToString()
                                                                                                            });
                iGenericDataAccess.CloseConnection();

                IList<PersonaResponsableModel> listPersonaResponsable = vwTicConsultaResponsableses.Select(x => new PersonaResponsableModel()
                                                                                                                {
                                                                                                                    PersonaId = x.IdResponsable,
                                                                                                                    Nombre = x.Responsable,
                                                                                                                    Mail = x.Mail,
                                                                                                                    MailResponsable = x.Mail,
                                                                                                                    MailEscalemiento1 = x.Mail,
                                                                                                                    MailEscalamiento2 = x.Mail
                                                                                                                }).ToList();
                return listPersonaResponsable;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesConfigParamTickets.ERR_08_02, e);
            }
        }

        public RegistroTicketsModel ReasignarResposnable(RegistroTicketsModel registroTicketsModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                iGenericDataAccess.BeginTran();

                RegistrosTicket registrosT = iGenericDataAccess.BuscarUno(new RegistrosTicket
                                                                          {
                                                                              TicketId = registroTicketsModel.TicketId
                                                                          }, new OptionsQueryZero
                                                                             {
                                                                                 ExcludeBool = true,
                                                                                 ExcludeNumericsDefaults = true
                                                                             });
                RegistroTicketsModel registro = new RegistroTicketsModel
                                                {
                                                    ResponsableId = registrosT.ResponsableId
                                                };

                registrosT.ResponsableId = registroTicketsModel.ResponsableId;
                iGenericDataAccess.Actualizar(registrosT);
                iGenericDataAccess.CommitTran();
                iGenericDataAccess.CloseConnection();
                registroTicketsModel.ResponsableId = registro.ResponsableId;
                return registroTicketsModel;
            }
            catch (Exception e)
            {
                iGenericDataAccess.RollbackTran();
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesConfigParamTickets.ERR_08_02, e);
            }
        }

        public TiposTicketsClientesModel ActualizaResposnableTiposTicketClientes(ConsultarDatosCorreoModel consultarDatosCorreo)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                TiposTicketsClientes tiposTicketsClientes = iGenericDataAccess.BuscarUno(new TiposTicketsClientes
                                                                                         {
                                                                                             TipoId = consultarDatosCorreo.TipoId,
                                                                                             IdCliente = consultarDatosCorreo.IdCliente
                                                                                         }, new OptionsQueryZero
                                                                                            {
                                                                                                ExcludeBool = true,
                                                                                                ExcludeNumericsDefaults = true
                                                                                            });

                tiposTicketsClientes.IdPersonaResponsable = consultarDatosCorreo.IdPersonaResponsable;
                TiposTicketsClientes tiposTicketsClientesAct = iGenericDataAccess.Actualizar(tiposTicketsClientes);
                iGenericDataAccess.CloseConnection();

                TiposTicketsClientesModel tiposTicketsClientesModel = new TiposTicketsClientesModel
                                                                      {
                                                                          IdPersonaResponsable = tiposTicketsClientesAct.IdPersonaResponsable ?? 0
                                                                      };

                return tiposTicketsClientesModel;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesConfigParamTickets.ERR_08_02, e);
            }
        }

        public TicketsEstatusModel GuardarSeguimientoCierreSinArchivo(TicketsEstatusModel archivoTicketsModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                CatEstatusTickets catEstatusTickets = iGenericDataAccess.
                    BuscarUno(new CatEstatusTickets
                              {
                                  CveEstatus = CERRADO
                              }, new OptionsQueryZero
                                 {
                                     ExcludeNumericsDefaults = true,
                                     ExcludeBool = true
                                 });

                iGenericDataAccess.Actualizar(new TicketsEstatus
                                              {
                                                  Activo = false,
                                                  IdTicketEstatus = archivoTicketsModel.IdTicketEstatus,
                                                  TicketId = archivoTicketsModel.TicketId,
                                                  PersonaId = archivoTicketsModel.PersonaId,
                                                  IdEstatusTicket = archivoTicketsModel.IdEstatusTicket,
                                                  FechaRegistro = DateTime.Now
                                              });

                TicketsEstatus ticketsEstatus = iGenericDataAccess.Guardar(new TicketsEstatus
                                                                           {
                                                                               NombreArchivoTicketCerrado = archivoTicketsModel.NombreArchivoTicketCerrado,
                                                                               RutaArchivoTicketCerrado = archivoTicketsModel.RutaArchivoTicketCerrado,
                                                                               TicketId = archivoTicketsModel.TicketId,
                                                                               IdEstatusTicket = catEstatusTickets.IdEstatusTicket,
                                                                               FechaRegistro = DateTime.Now,
                                                                               PersonaId = archivoTicketsModel.GetIdUsuarioSesion(),
                                                                               Activo = true
                                                                           });
                iGenericDataAccess.CloseConnection();
                archivoTicketsModel.TicketId = ticketsEstatus.TicketId;
                archivoTicketsModel.NombreArchivoTicketCerrado = ticketsEstatus.NombreArchivoTicketCerrado;
                archivoTicketsModel.RutaArchivoTicketCerrado = ticketsEstatus.RutaArchivoTicketCerrado;
                archivoTicketsModel.IdEstatusTicket = ticketsEstatus.IdEstatusTicket;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_13, e);
            }
            return archivoTicketsModel;
        }


        public IList<CatDiasInhabilesModel> ConsultarDiasInhabiles()
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
                                                                                                WhereComplementary = string.Format(FiltroDiasInhabiles, DateTime.Now.ToString("dd-MM-yyyy"))
                                                                                            });

                IList<CatDiasInhabilesModel> listCatInhabiles = listCatDiasInhabiles.Select(
                                                                                            x => new CatDiasInhabilesModel()
                                                                                                 {
                                                                                                     IdDiaHabil = x.IdDiaHabil,
                                                                                                     Dia = x.Dia,
                                                                                                     PersonaID = x.PersonaID,
                                                                                                     FechaRegistro = x.FechaRegistro,
                                                                                                 }).ToList();
                return listCatInhabiles;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesSeguiminetorTickets.ERR_00_11, e);
            }
        }

        public TicketModel BuscarTicketSeguimiento(ComentariosTicketModel comentariosTicketModel)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                RegistrosTicket registrosTicket = iGenericDataAccess.BuscarUno(new RegistrosTicket
                                                                               {
                                                                                   TicketId = comentariosTicketModel.TicketId
                                                                               }, new OptionsQueryZero
                                                                                  {
                                                                                      ExcludeBool = true,
                                                                                      ExcludeNumericsDefaults = true
                                                                                  });
                TiposTicket tiposticket = iGenericDataAccess.BuscarUno(new TiposTicket
                                                                       {
                                                                           TipoId = registrosTicket.TipoId
                                                                       }, new OptionsQueryZero
                                                                          {
                                                                              ExcludeBool = true,
                                                                              ExcludeNumericsDefaults = true
                                                                          });

                iGenericDataAccess.CloseConnection();
                TicketModel ticket = new TicketModel
                                     {
                                         TicketId = registrosTicket.TicketId,
                                         FechaRegistro = registrosTicket.FechaRegistro,
                                         TipoId = tiposticket.TipoId,
                                         Tipo = tiposticket.Descripcion,
                                         DescripcionTicket = registrosTicket.Descripcion,
                                         FechaRecepcion = registrosTicket.FechaRecepcion,
                                         ResponsableId = registrosTicket.ResponsableId,
                                         UsuarioId = registrosTicket.UsuarioId,
                                         IdCliente = registrosTicket.IdCliente
                                     };

                return ticket;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_11, e);
            }
        }

        public IList<CorreosCopiaTicketsModel> ObtenerCorreosCopiaTickets(int ticketId)
        {
            try
            {
                iGenericDataAccess.OpenConnection();
                IList<CorreosCopiaTickets> vista = iGenericDataAccess.Consultar(new CorreosCopiaTickets
                                                                                {
                                                                                    TicketId = ticketId
                                                                                }, new OptionsQueryZero
                                                                                   {
                                                                                       ExcludeBool = true,
                                                                                       ExcludeNumericsDefaults = true
                                                                                   });
                iGenericDataAccess.CloseConnection();
                IList<CorreosCopiaTicketsModel> model = vista.Select(x => new CorreosCopiaTicketsModel
                                                                          {
                                                                              IdCorreoCopiaTicket = x.IdCorreoCopiaTicket,
                                                                              TicketId = x.TicketId,
                                                                              Correo = x.Correo
                                                                          }).ToList();
                return model;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesTickets.ERR_00_11, e);
            }
        }

        public TiempoDeAtencionModel CalculaTiempoAtencion(DateTime fechaRecepcion, int horasAtencion, DateTime fechaFinal)
        {
            try
            {
                TiempoDeAtencionModel tiempoAtencion = new TiempoDeAtencionModel();

                int totalHoras = 0;
                var diasLaborables = 0;
                var fechaActual = fechaFinal;
                var horaFechaActual = fechaActual.Hour;
                var minutoFechaActual = fechaActual.Minute;
                var horaFechaRecepcion = fechaRecepcion.Hour;
                var minutosFechaRecepcion = (fechaRecepcion.Minute == 0) ? 60 : fechaRecepcion.Minute;

                if (fechaRecepcion.Date<=fechaActual.Date)
                {
                    TiempoDeAtencionModel tiempoActualTranscurrido = new TiempoDeAtencionModel();

                    var fechaRecepcionEspejo = fechaRecepcion.AddDays(1);
                    var diasInhabiles = ConsultarTotalDiasInhabiles(fechaRecepcion, fechaActual);
                    var diaFechaRecepcion = fechaRecepcion.Date<fechaActual.Date && (horaFechaRecepcion == 8 && (minutosFechaRecepcion == 0 || minutosFechaRecepcion == 60)) ? 1 : 0;
                    var horasFaltantesDeHorario = 0;

                    if (fechaRecepcion.Date != fechaActual.Date)
                    {
                        if (horaFechaRecepcion == 8 && (minutosFechaRecepcion == 0 || minutosFechaRecepcion == 60))
                        {
                            horasFaltantesDeHorario = 0;
                        }
                        else if (horaFechaRecepcion == 8 && minutosFechaRecepcion>0 && minutosFechaRecepcion<60 || horaFechaRecepcion>=8)
                        {
                            horasFaltantesDeHorario = 17 - fechaRecepcion.Hour;
                            horasFaltantesDeHorario = (fechaRecepcion.Minute>0) ? horasFaltantesDeHorario - 1 : horasFaltantesDeHorario;
                        }
                    }

                    while (fechaRecepcionEspejo.Date<fechaActual.Date)
                    {
                        if (fechaRecepcionEspejo.DayOfWeek != DayOfWeek.Saturday && fechaRecepcionEspejo.DayOfWeek != DayOfWeek.Sunday)
                        {
                            diasLaborables++;
                        }
                        fechaRecepcionEspejo = fechaRecepcionEspejo.AddDays(1);
                    }

                    if (EsDiaInhabil(fechaActual))
                    {
                        diasLaborables++;
                        diasLaborables = (diasLaborables - diasInhabiles) + diaFechaRecepcion;
                        totalHoras = (fechaRecepcion.Date == fechaActual.Date) ? 0 : horasFaltantesDeHorario;
                    }
                    else
                    {
                        if (fechaActual.DayOfWeek != DayOfWeek.Saturday && fechaActual.DayOfWeek != DayOfWeek.Sunday)
                        {
                            if (fechaRecepcion.Date == fechaActual.Date)
                            {
                                tiempoActualTranscurrido.Dias = (fechaRecepcion.Hour == 8 && fechaRecepcion.Minute == 0 && horaFechaActual>=17) ? 1 : 0;
                            }
                            else
                            {
                                tiempoActualTranscurrido.Dias = horaFechaActual<17 ? 0 : 1;
                            }

                            if ((fechaRecepcion.Date == fechaActual.Date || fechaRecepcion.Date != fechaActual.Date) && horaFechaActual<8)
                            {
                                tiempoActualTranscurrido.Horas = 0;
                            }
                            else if (horaFechaActual == 8 && horaFechaRecepcion == 8 && fechaRecepcion.Minute<=minutoFechaActual)
                            {
                                if (fechaRecepcion.Date != fechaActual.Date)
                                {
                                    if (fechaRecepcion.Minute == 0 && fechaActual.Minute == 0)
                                    {
                                        tiempoActualTranscurrido.Horas = 0;
                                    }
                                    else
                                    {
                                        tiempoActualTranscurrido.Horas = fechaRecepcion.Date != fechaActual.Date ? 1 : 0;
                                    }
                                }
                            }
                            else if ((fechaRecepcion.Date == fechaActual.Date || fechaRecepcion.Date != fechaActual.Date) && horaFechaRecepcion == 8 && minutosFechaRecepcion == 0)
                            {
                                tiempoActualTranscurrido.Horas = (horaFechaActual>=17) ? 0 : horaFechaActual - 8;
                                tiempoActualTranscurrido.Horas = (fechaRecepcion.Date != fechaActual.Date) ? tiempoActualTranscurrido.Horas + 1 : tiempoActualTranscurrido.Horas;
                            }
                            else if (fechaRecepcion.Date == fechaActual.Date && (horaFechaRecepcion == 8 && minutosFechaRecepcion>=0 && minutosFechaRecepcion<60 || horaFechaRecepcion>8))
                            {
                                if (horaFechaActual>17)
                                {
                                    tiempoActualTranscurrido.Horas = (minutosFechaRecepcion>0 && minutosFechaRecepcion<60) ? 17 - horaFechaRecepcion - 1 : 17 - horaFechaRecepcion;
                                }
                                else
                                {
                                    tiempoActualTranscurrido.Horas = (fechaRecepcion.Minute<=minutoFechaActual) ? horaFechaActual - horaFechaRecepcion : horaFechaActual - horaFechaRecepcion - 1;
                                }
                            }
                            else if (fechaRecepcion.Date != fechaActual.Date && (horaFechaRecepcion == 8 && minutosFechaRecepcion>0 && minutosFechaRecepcion<60 || horaFechaRecepcion>8))
                            {
                                if (horaFechaActual>=17)
                                {
                                    tiempoActualTranscurrido.Horas = 0;
                                }
                                else
                                {
                                    tiempoActualTranscurrido.Horas = (minutosFechaRecepcion<=minutoFechaActual) ? horaFechaActual - 7 : horaFechaActual - 8;
                                }
                            }
                            else if (fechaRecepcion.Date != fechaActual.Date && horaFechaActual>=8)
                            {
                                if (horaFechaActual == 8 && minutoFechaActual == 0 || horaFechaActual>17)
                                {
                                    tiempoActualTranscurrido.Horas = 0;
                                }
                                else if (horaFechaActual == 8 && minutoFechaActual != 0)
                                {
                                    tiempoActualTranscurrido.Horas = fechaRecepcion.Minute<=minutoFechaActual ? 1 : 0;
                                }
                                else if (horaFechaActual>=8 && horaFechaActual<=17)
                                {
                                    tiempoActualTranscurrido.Horas = fechaRecepcion.Minute<=minutoFechaActual ? horaFechaActual - 8 : horaFechaActual - 9;
                                }
                            }
                            else if (fechaRecepcion.Date == fechaActual.Date && fechaActual.Hour<17)
                            {
                                tiempoActualTranscurrido.Horas = horaFechaActual - 8;
                            }
                        }
                        else
                        {
                            tiempoActualTranscurrido.Horas = 0;
                            tiempoActualTranscurrido.Dias = 0;
                        }

                        diasLaborables = (tiempoActualTranscurrido.Horas + horasFaltantesDeHorario>=9) ? diasLaborables + 1 : diasLaborables;
                        diasLaborables = (diasLaborables - diasInhabiles) + tiempoActualTranscurrido.Dias + diaFechaRecepcion;
                        totalHoras = (tiempoActualTranscurrido.Horas + horasFaltantesDeHorario>=9) ? (tiempoActualTranscurrido.Horas + horasFaltantesDeHorario) - 9 : tiempoActualTranscurrido.Horas + horasFaltantesDeHorario;
                    }
                }

                var minutos = (horaFechaActual>=17) ? 60 : minutoFechaActual;

                tiempoAtencion.Dias = diasLaborables;
                tiempoAtencion.Horas = totalHoras;
                tiempoAtencion.EnTiempo = horasAtencion>(diasLaborables * 9) + totalHoras || ((horasAtencion == (diasLaborables * 9) + totalHoras) && minutosFechaRecepcion == minutos);

                return tiempoAtencion;
            }
            catch (Exception e)
            {
                throw new DalException(CodesTickets.ERR_00_19, e);
            }
        }

        public int ConsultarTotalDiasInhabiles(DateTime fechaRecepcion, DateTime fechaFin)
        {
            try
            {
                var queryFinal = "SELECT COUNT(Dia) totalInhabiles FROM CatDiasHabiles WHERE Dia >= {FechaRecepcion} AND Dia <= {FechaFin}";

                iGenericDataAccess.OpenConnection();

                queryFinal = queryFinal.Replace("{FechaRecepcion}", "'" + fechaRecepcion.ToString(("yyyy-MM-dd hh:mm:ss")) + "'");
                queryFinal = queryFinal.Replace("{FechaFin}", "'" + fechaFin.ToString(("yyyy-MM-dd hh:mm:ss")) + "'");

                IList<CatDiasHabilesEntidad> inhabiles = iGenericDataAccess.ExecuteQuery<CatDiasHabilesEntidad>(queryFinal);

                iGenericDataAccess.CloseConnection();

                return inhabiles[0].TotalInhabiles;
            }
            catch (Exception e)
            {
                throw new DalException("Error:::", e);
            }
        }

        public bool EsDiaInhabil(DateTime fechaFin)
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
                                                                                                WhereComplementary = "dia = CONVERT(DATE, '" + fechaFin.ToString("yyyy-MM-dd hh:mm:ss") + "')"
                                                                                            });

                iGenericDataAccess.CloseConnection();

                return listCatDiasInhabiles.Count>0;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesSeguiminetorTickets.ERR_00_11, e);
            }
        }

        public IList<TicketsEstatusModel> ValidaEstatusTicket(TicketsEstatusModel ticketsEstatus)
        {
            try
            {
                IList<TicketsEstatusModel> ticketsEstatusList = new List<TicketsEstatusModel>();

                iGenericDataAccess.OpenConnection();
                IList<VwTicSelEstatusTicket> vwTicSelEstatusTickets = iGenericDataAccess.Consultar(
                                                                                                   new VwTicSelEstatusTicket()
                                                                                                   {
                                                                                                       TicketId = ticketsEstatus.TicketId
                                                                                                   },
                                                                                                   new OptionsQueryZero()
                                                                                                   {
                                                                                                       ExcludeNumericsDefaults = true,
                                                                                                       ExcludeBool = true
                                                                                                   });
                iGenericDataAccess.CloseConnection();

                if (vwTicSelEstatusTickets.Count>0)
                {
                    ticketsEstatusList = vwTicSelEstatusTickets.Select(x => new TicketsEstatusModel
                                                                            {
                                                                                TicketId = x.TicketId,
                                                                                Estatus = x.Estatus,
                                                                                Activo = x.Cerrado
                                                                            }).ToList();
                }
                else
                {
                    ticketsEstatusList.Add(ticketsEstatus);
                }

                return ticketsEstatusList;
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesSeguiminetorTickets.ERR_00_01, e);
            }
        }

        public PersonaResponsableModel BuscaPersonaResposnableTicket(RegistroTicketsModel registroTickets)
        {
            try
            {
                iGenericDataAccess.OpenConnection();

                IList<NePersonas> personas = iGenericDataAccess.Consultar(new NePersonas()
                                                                          {
                                                                              PersonaId = registroTickets.ResponsableId
                                                                          }, new OptionsQueryZero
                                                                             {
                                                                                 ExcludeBool = true,
                                                                                 ExcludeNumericsDefaults = true
                                                                             });

                IList<PersonaResponsableModel> personaResponsable = personas.Select(x => new PersonaResponsableModel()
                                                                                         {
                                                                                             PersonaId = x.PersonaId,
                                                                                             MailResponsable = x.Mail,
                                                                                             Nombre = x.Nombre,
                                                                                             Paterno = x.Paterno,
                                                                                             Materno = x.Materno
                                                                                         }).ToList();

                return personaResponsable[0];
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(CodesConfigParamTickets.ERR_08_02, e);
            }
        }
    }
}