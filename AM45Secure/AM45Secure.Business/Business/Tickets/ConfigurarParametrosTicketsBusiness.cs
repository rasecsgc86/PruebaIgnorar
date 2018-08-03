using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AM45Secure.Business.IBusiness.Tickets;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Tickets;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.IDataAccess.Tickets;
using Zero.Exceptions;
using Zero.Handlers.Response;
using Zero.Utils;
using Zero.Utils.Models;

namespace AM45Secure.Business.Business.Tickets
{
    public class ConfigurarParametrosTicketsBusiness : IConfigurarParametrosTicketsBusiness
    {
        private readonly IConfigurarParametrosTicketsDataAccess iConfigurarParametrosTicketsDataAccess;

        public ConfigurarParametrosTicketsBusiness(IConfigurarParametrosTicketsDataAccess iConfigurarParametrosTicketsDataAccess)
        {
            this.iConfigurarParametrosTicketsDataAccess = iConfigurarParametrosTicketsDataAccess;
        }

        public SingleResponse<IList<ConfigurarParametrosTicketsModelo>> ConsultarConfigurarParametros(ConfigurarParametrosTicketsModelo configurarParametros)
        {
            SingleResponse<IList<ConfigurarParametrosTicketsModelo>> response = new SingleResponse<IList<ConfigurarParametrosTicketsModelo>>();
            try
            {
                if (configurarParametros == null)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(configurarParametros, new OptionsValidation
                                                                                             {
                                                                                                 ValidateIntCero = true,
                                                                                                 ExcludeOptionals = false
                                                                                             });
                if (validations.Count>0)
                {
                    throw new DomainValidationsException(validations);
                }
                IList<ConfigurarParametrosTicketsModelo> listaConfiParan = iConfigurarParametrosTicketsDataAccess.ConsultarConfigurarParametros(configurarParametros);

                response.Done(listaConfiParan, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }

        public SingleResponse<IList<ClienteProductoModel>> ConsultarClientesConfigurarParametros(ClienteProductoModel clienteProductoModel)
        {
            SingleResponse<IList<ClienteProductoModel>> response = new SingleResponse<IList<ClienteProductoModel>>();
            try
            {
                IList<ClienteProductoModel> listaClienteProducto = iConfigurarParametrosTicketsDataAccess.ConsultarClientesConfigurarParametros(clienteProductoModel);
                var qry = listaClienteProducto.GroupBy(cm => new
                                                             {
                                                                 cm.IdCliente,
                                                                 cm.NombreCliente
                                                             },
                                                       (key, group) => new
                                                                       {
                                                                           Key1 = key.IdCliente,
                                                                           Key2 = key.NombreCliente
                                                                       });

                List<ClienteProductoModel> clientesAgrupados = new List<ClienteProductoModel>();
                foreach (var cliente in qry)
                {
                    ClienteProductoModel clienteProductoModel2 = new ClienteProductoModel
                                                                 {
                                                                     IdCliente = cliente.Key1,
                                                                     NombreCliente = cliente.Key2
                                                                 };
                    clientesAgrupados.Add(clienteProductoModel2);
                }
                response.Done(clientesAgrupados, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }

        public SingleResponse<IList<PersonaResponsableModel>> BuscarUsuarioResponsable(PersonaResponsableModel personaResponsableModel)
        {
            SingleResponse<IList<PersonaResponsableModel>> response = new SingleResponse<IList<PersonaResponsableModel>>();
            try
            {
                IList<PersonaResponsableModel> listaPersonas = iConfigurarParametrosTicketsDataAccess.BuscarUsuarioResponsable(personaResponsableModel);
                response.Done(listaPersonas, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_02, e));
            }
            return response;
        }

        public SingleResponse<TiposTicketsClientesModel> GuardarTiposTicketsClientes(TiposTicketsClientesModel tiposTicketsClientesModel)
        {
            SingleResponse<TiposTicketsClientesModel> response = new SingleResponse<TiposTicketsClientesModel>();
            try
            {
                if (tiposTicketsClientesModel == null)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(tiposTicketsClientesModel, new OptionsValidation
                                                                                                  {
                                                                                                      ValidateIntCero = true
                                                                                                  });
                IList<Validation> validationsDos = ValidatorZero.Validate(tiposTicketsClientesModel.TiposTicket);
                if (validations.Count>0 || validationsDos.Count>0)
                {
                    IList<Validation> validaciones = new List<Validation>();
                    foreach (Validation validation in validationsDos)
                    {
                        validaciones.Add(validation);
                    }
                    foreach (Validation validation in validations)
                    {
                        validaciones.Add(validation);
                    }
                    throw new DomainValidationsException(validaciones);
                }
                TiposTicketsClientesModel save = iConfigurarParametrosTicketsDataAccess.GuardarTiposTicketsClientes(tiposTicketsClientesModel);
                response.Done(save, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }

        public SingleResponse<bool> EliminarTipoTicketsCliente(TiposTicketModel tiposTicket)
        {
            SingleResponse<bool> response = new SingleResponse<bool>();
            try
            {
                response.Done(iConfigurarParametrosTicketsDataAccess.EliminarTipoTicketsCliente(tiposTicket), string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }

        public SingleResponse<TiposTicketsClientesModel> ActulizarTiposTicketsClientes(TiposTicketsClientesModel tiposTicketsClientesModel)
        {
            SingleResponse<TiposTicketsClientesModel> response = new SingleResponse<TiposTicketsClientesModel>();
            try
            {
                if (tiposTicketsClientesModel == null)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(tiposTicketsClientesModel, new OptionsValidation
                                                                                                  {
                                                                                                      ValidateIntCero = true
                                                                                                  });
                IList<Validation> validationsDos = ValidatorZero.Validate(tiposTicketsClientesModel.TiposTicket);
                if (validations.Count>0 || validationsDos.Count>0)
                {
                    IList<Validation> validaciones = new List<Validation>();
                    foreach (Validation validation in validationsDos)
                    {
                        validaciones.Add(validation);
                    }
                    foreach (Validation validation in validations)
                    {
                        validaciones.Add(validation);
                    }
                    throw new DomainValidationsException(validaciones);
                }
                TiposTicketsClientesModel update = iConfigurarParametrosTicketsDataAccess.ActulizarTiposTicketsClientes(tiposTicketsClientesModel);
                response.Done(update, string.Empty);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(CodesConfigParamTickets.ERR_08_03, e));
            }
            return response;
        }
    }
}