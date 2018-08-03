using AM45Secure.Business.IBusiness.IConfigurador;
using AM45Secure.Commons.Modelos.Comunes;
using AM45Secure.Commons.Modelos.Configurador;
using AM45Secure.Commons.Recursos;
using AM45Secure.DataAccess.Entidades.Cotizador;
using AM45Secure.DataAccess.IDataAccess.IConfigurador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zero.Exceptions;
using Zero.Handlers.Response;
using Zero.Utils;
using Zero.Utils.Models;

namespace AM45Secure.Business.Business.Configurador
{
    public class ConfiguradorBusiness : IConfiguradorBusiness
    {
        private readonly IConfiguradorDataAccess iConfiguradorDataAcess;

        public ConfiguradorBusiness(IConfiguradorDataAccess iConfiguradorDataAcess)
        {
            this.iConfiguradorDataAcess = iConfiguradorDataAcess;
        }

        public SingleResponse<IList<nePersonasModel>> ConsultarClientesConfigurador()
        {
            SingleResponse<IList<nePersonasModel>> response = new SingleResponse<IList<nePersonasModel>>();
            try
            {
                IList<nePersonasModel> cotizantes = iConfiguradorDataAcess.ConsultarClientesConfigurador();
                response.Done(cotizantes, string.Empty);

            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;

        }

        public SingleResponse<IList<ProductoModel>> ConsultaProductosFlexibles()
        {
            SingleResponse<IList<ProductoModel>> response = new SingleResponse<IList<ProductoModel>>();
            try
            {
                IList<ProductoModel> productos = iConfiguradorDataAcess.ConsultaProductosFlexibles();
                response.Done(productos, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<IList<ElementoModel>> ConsultaTipoAuto()
        {
            SingleResponse<IList<ElementoModel>> response = new SingleResponse<IList<ElementoModel>>();
            try
            {
                IList<ElementoModel> tiposAuto = iConfiguradorDataAcess.ConsultaTipoAuto();
                response.Done(tiposAuto, string.Empty);
            }

            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<ElementoModel>> ConsultaTipoServicio()
        {
            SingleResponse<IList<ElementoModel>> response = new SingleResponse<IList<ElementoModel>>();
            try
            {
                IList<ElementoModel> tipoServicios = iConfiguradorDataAcess.ConsultaTipoServicio();
                response.Done(tipoServicios, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;

        }

        public SingleResponse<IList<ElementoModel>> ConsultaTipoSeguro()
        {
            SingleResponse<IList<ElementoModel>> response = new SingleResponse<IList<ElementoModel>>();
            try
            {
                IList<ElementoModel> tipoSerguros = iConfiguradorDataAcess.ConsultaTipoSeguro();
                response.Done(tipoSerguros, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<ElementoModel>> EsNuevo()
        {
            SingleResponse<IList<ElementoModel>> response = new SingleResponse<IList<ElementoModel>>();
            try
            {
                IList<ElementoModel> esNuevo = iConfiguradorDataAcess.EsNuevo();
                response.Done(esNuevo, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<ElementoModel>> CargoEnLinea()
        {
            SingleResponse<IList<ElementoModel>> response = new SingleResponse<IList<ElementoModel>>();
            try
            {
                IList<ElementoModel> cargoLinea = iConfiguradorDataAcess.CargoEnLinea();
                response.Done(cargoLinea, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<ElementoModel>> ConsultaAseguradoras()
        {
            SingleResponse<IList<ElementoModel>> response = new SingleResponse<IList<ElementoModel>>();
            try
            {
                IList<ElementoModel> aseguradoras = iConfiguradorDataAcess.ConsultaAseguradoras();
                response.Done(aseguradoras, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<ElementoModel>> ConsultaEstatus() {

            SingleResponse<IList<ElementoModel>> response = new SingleResponse<IList<ElementoModel>>();
            try {
                IList<ElementoModel> estatus = iConfiguradorDataAcess.ConsultaEstatus();
                response.Done(estatus, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<ProductoFlexModel> GuardaProductoFlexible(ConfiguradorModel productoFlexModel)
        {
            var response = new SingleResponse<ProductoFlexModel>();
            string respuesta = string.Empty;
            try
            {
                var prodFlex = iConfiguradorDataAcess.GuardaProductoFlexible(productoFlexModel);
                respuesta = "Se agrego el producto de forma correcta";
                response.IsOk.Equals(true);
                ProductoFlexModel EmptyModel = new ProductoFlexModel();
                response.Done(EmptyModel, string.Empty);


            }
            catch (DomainValidationsException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (DalException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (Exception e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<ElementoModel>> ConsultaFormasPago()
        {

            SingleResponse<IList<ElementoModel>> response = new SingleResponse<IList<ElementoModel>>();
            try
            {
                IList<ElementoModel> formasPago = iConfiguradorDataAcess.ConsultaFormasPago();
                response.Done(formasPago, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<PerfilesUsuarioModel>> ConsultaPerfilesSistema()
        {
            SingleResponse<IList<PerfilesUsuarioModel>> response = new SingleResponse<IList<PerfilesUsuarioModel>>();

            try
            {
                IList<PerfilesUsuarioModel> perfiles = iConfiguradorDataAcess.ConsultaPerfilesSistema();
                response.Done(perfiles, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<UsuariosPerfil>> ConsultaUsuarioPorPerfil(ConfiguradorModel usuarioPerfilModel)
        {
            var response = new SingleResponse<IList<UsuariosPerfil>>();
            try
            {
                IList<UsuariosPerfil> usuarios = iConfiguradorDataAcess.ConsultaUsuarioPorPerfil(usuarioPerfilModel);
                response.Done(usuarios, string.Empty);
            }

            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<PerfilesFlexModel>> ConsultarUsuariosFlexibles()
        {
            var response = new SingleResponse<IList<PerfilesFlexModel>>();
            try
            {
                IList<PerfilesFlexModel> usuariosFlex = iConfiguradorDataAcess.ConsultarUsuariosFlexibles();
                response.Done(usuariosFlex, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<FormaPagoModel>> ConsultarFormasPagoLista()
        {
            var response = new SingleResponse<IList<FormaPagoModel>>();
            try
            {
                IList<FormaPagoModel> formasPago = iConfiguradorDataAcess.ConsultarFormasPagoLista();
                response.Done(formasPago, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<IList<FormasPagoProductoAseguradoraModel>> ConsultarFormasPagoAseguradoraLista()
        {
            var response = new SingleResponse<IList<FormasPagoProductoAseguradoraModel>>();
            try
            {
                IList<FormasPagoProductoAseguradoraModel> formasPagoAseguradora = iConfiguradorDataAcess.ConsultarFormasPagoAseguradoraLista();
                response.Done(formasPagoAseguradora, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }
        public SingleResponse<PerfilesFlexibleModel> GuardaUsuarioFlexible(ConfiguradorModel perfilesFlexibleModel)
        {
            var response = new SingleResponse<PerfilesFlexibleModel>();
            string respuesta = string.Empty;
            try
            {
                var perfilFlex = iConfiguradorDataAcess.GuardaUsuarioFlexible(perfilesFlexibleModel);
                response.IsOk.Equals(true);
                PerfilesFlexibleModel perfilesEmpty = new PerfilesFlexibleModel();
                response.Done(perfilesEmpty, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (DalException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (Exception e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<FormasPagoProductoModel> GrabarFormaPagoProducto(ConfiguradorModel formasPagoProductoModel)
        {
            var response = new SingleResponse<FormasPagoProductoModel>();
            string respuesta = string.Empty;
            try
            {
                var formasPago = iConfiguradorDataAcess.GrabarFormaPagoProducto(formasPagoProductoModel);
                response.IsOk.Equals(true);
                FormasPagoProductoModel empty = new FormasPagoProductoModel();
                response.Done(empty, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (DalException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (Exception e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }


        public SingleResponse<FormasPagoProductoAseguradora> GrabarFormasPagoProductoAseguradora(ConfiguradorModel formasPagoProductoAseguradora)
        {
            var response = new SingleResponse<FormasPagoProductoAseguradora>();
            string respuesta = string.Empty;
            try
            {
                var formasPagoAseguradora = iConfiguradorDataAcess.GrabarFormasPagoProductoAseguradora(formasPagoProductoAseguradora);
                response.IsOk.Equals(true);
                FormasPagoProductoAseguradora empty = new FormasPagoProductoAseguradora();
                response.Done(empty, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (DalException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (Exception e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;


        }

        public SingleResponse<PerfilesFlexibleModel> ActualizaStatusUdi(ConfiguradorModel perfilesFlexibleModel)
        {
            var response = new SingleResponse<PerfilesFlexibleModel>();
            string respuesta = string.Empty;
            try
            {
                var actualizaUdi = iConfiguradorDataAcess.ActualizaStatusUdi(perfilesFlexibleModel);
                response.IsOk.Equals(true);
                PerfilesFlexibleModel perfilesEmpty = new PerfilesFlexibleModel();
                response.Done(perfilesEmpty, string.Empty);
            }

            catch (DomainValidationsException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (DalException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (Exception e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<FormasPagoProductoModel> ActualizaPredeterminadoPago(ConfiguradorModel formasPagoProductoModel)
        {
            var response = new SingleResponse<FormasPagoProductoModel>();
            string respuesta = string.Empty;
            try
            {
                var actualizaPredeterminado = iConfiguradorDataAcess.ActualizaPredeterminadoPago(formasPagoProductoModel);
                response.IsOk.Equals(true);
                FormasPagoProductoModel empty = new FormasPagoProductoModel();
                response.Done(empty, string.Empty);
            }

            catch (DomainValidationsException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (DalException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (Exception e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<CoberModel> ActualizarHomologacionTooltip(ConfiguradorModel configuradorModel)
        {
            var response = new SingleResponse<CoberModel>();
            string respuesta = string.Empty;
            try
            {
                var actualizaHomoTooltip = iConfiguradorDataAcess.ActualizarHomologacionTooltip(configuradorModel);
                response.IsOk.Equals(true);
                CoberModel empty = new CoberModel();
                response.Done(empty, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (DalException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (Exception e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;


        }

        public SingleResponse<PerfilesFlexibleModel> EliminarUsuarioFlexible(ConfiguradorModel perfilesFlexibleModel)
        {
            var response = new SingleResponse<PerfilesFlexibleModel>();
            string respuesta = string.Empty;
            try
            {
                var eliminaUsuarioFlex = iConfiguradorDataAcess.EliminarUsuarioFlexible(perfilesFlexibleModel);
                response.IsOk.Equals(true);
                PerfilesFlexibleModel perfilesEmpty = new PerfilesFlexibleModel();
                response.Done(perfilesEmpty, string.Empty);
            }

            catch (DomainValidationsException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (DalException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (Exception e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<FormasPagoProductoModel> EliminarFormaDePago(ConfiguradorModel formasPagoProductoModel)
        {
            var response = new SingleResponse<FormasPagoProductoModel>();
            string respuesta = string.Empty;
            try
            {
                var eliminarFormaPago = iConfiguradorDataAcess.EliminarFormaDePago(formasPagoProductoModel);
                response.IsOk.Equals(true);
                FormasPagoProductoModel empty = new FormasPagoProductoModel();
                response.Done(empty, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (DalException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (Exception e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }


        public SingleResponse<FormasPagoProductoAseguradora> EliminarFormaPagoProductoAseguradora(ConfiguradorModel formasPagoProductoAseguradora)
        {
            var response = new SingleResponse<FormasPagoProductoAseguradora>();
            string respuesta = string.Empty;
            try
            {
                var eliminarFormaPagoAseguradora = iConfiguradorDataAcess.EliminarFormaPagoProductoAseguradora(formasPagoProductoAseguradora);
                response.IsOk.Equals(true);
                FormasPagoProductoAseguradora empty = new FormasPagoProductoAseguradora();
                response.Done(empty, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (DalException e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(e);
            }
            catch (Exception e)
            {
                respuesta = "No se agrego el producto de forma correcta";
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<ConfiguradorModel> ConsultaPanelConfiguradorFlex(ConfiguradorModel configuradorModel)
        {
            SingleResponse<ConfiguradorModel> response = new SingleResponse<ConfiguradorModel>();
            try
            {
                if (null == configuradorModel)
                {
                    throw new DomainException(Codes.ERR_00_06);
                }
                IList<Validation> validations = ValidatorZero.Validate(configuradorModel.PanelConfiguradorModel, new OptionsValidation()
                {
                    ValidateIntCero = true,
                    ExcludeOptionals = true
                });
                if (0 < validations.Count)
                {
                    throw new DomainValidationsException(validations);
                }
                ConfiguradorModel config = new ConfiguradorModel
                {
                    PanelConfiguradorModel = iConfiguradorDataAcess.ConsultaPanelConfiguradorFlex(configuradorModel)
                };

                response.Done(config, string.Empty);
            }

            catch (DalException e)
            {
                response.Error(e);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }
            return response;
        }

        public SingleResponse<RangosModel> ConsultaRangosSumasAseguradas(ConfiguradorModel configuradorModel)
        {
            var response = new SingleResponse<RangosModel>();
            try
            {
                var rangos = iConfiguradorDataAcess.ConsultaRangosSumasAseguradas(configuradorModel);
                response.Done(rangos, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<CoberModel> ActualizaRangosSumas(ConfiguradorModel configuradorModel)
        {
            var response = new SingleResponse<CoberModel>();
            try
            {
                var actualizaRangos = iConfiguradorDataAcess.ActualizaRangosSumas(configuradorModel);
                response.IsOk.Equals(true);
                CoberModel empty = new CoberModel();
                response.Done(empty, string.Empty);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<CoberModel> ActualizaRangosDeducibles(ConfiguradorModel configuradorModel)
        {
            var response = new SingleResponse<CoberModel>();
            try
            {
                var actualizaDeducibles = iConfiguradorDataAcess.ActualizaRangosDeducibles(configuradorModel);
                response.IsOk.Equals(true);
                CoberModel empty = new CoberModel();
                response.Done(empty, string.Empty);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<DocumentosPorCoberturaModel> GuardarDocumentoCobertura(ConfiguradorModel configuradorModel)
        {
            var response = new SingleResponse<DocumentosPorCoberturaModel>();
            try
            {
                var guardaDocumentoCobertura = iConfiguradorDataAcess.GuardarDocumentoCobertura(configuradorModel);
                response.IsOk.Equals(true);
                DocumentosPorCoberturaModel empty = new DocumentosPorCoberturaModel();
                response.Done(empty, string.Empty);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<TextoAuxiliarUsoVehiculoModel> GuardaTextoAuxiliarUso(ConfiguradorModel configuradorModel)
        {
            var response = new SingleResponse<TextoAuxiliarUsoVehiculoModel>();
            try
            {
                var guardaTextoAux = iConfiguradorDataAcess.GuardaTextoAuxiliarUso(configuradorModel);
                response.IsOk.Equals(true);
                TextoAuxiliarUsoVehiculoModel empty = new TextoAuxiliarUsoVehiculoModel();
                response.Done(empty, string.Empty);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }


        public SingleResponse<ConfiguradorModel> ConsultarEnmascaradoDeducibles(ConfiguradorModel configuradorModel)
        {
            var response = new SingleResponse<ConfiguradorModel>();
            try
            {
                ConfiguradorModel conf = new ConfiguradorModel
                {

                    CoberturaEnmascaramientoDeducible = iConfiguradorDataAcess.ConsultarEnmascaradoDeducibles(configuradorModel)
                };

                response.Done(conf, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<IList<DocumentosCoberModel>> ConsultaDocumentosPorCobertura(ConfiguradorModel configuradorModel)
        {
            var response = new SingleResponse<IList<DocumentosCoberModel>>();
            try
            {
                var selDocumentos = iConfiguradorDataAcess.ConsultaDocumentosPorCobertura(configuradorModel);
                response.Done(selDocumentos, string.Empty);
            }

            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        public SingleResponse<IList<DocumentosCoberModel>> ConsultaDocumentosTodos()
        {
            var response = new SingleResponse<IList<DocumentosCoberModel>>();
            try
            {
                var selDocumentos = iConfiguradorDataAcess.ConsultaDocumentosTodos();
                response.Done(selDocumentos, string.Empty);
            }
            catch (DomainValidationsException e)
            {
                response.SetValidations(e.Validations);
            }
            catch (DomainException e)
            {
                response.Error(e);
            }
            catch (DalException e)
            {
                response.Error(e);
            }
            catch (Exception e)
            {
                response.Error(new DomainException(Codes.ERR_00_01, e));
            }

            return response;
        }

        /// <summary>
        /// FJSQ///// implementacion
        /// </summary>
        /// <param name="directivasModel"></param>
        /// <returns></returns>
        public SingleResponse<IList<DirectivasModel>> RecuperaInfoDirectivas(DirectivasModel directivasModel)
        {
            throw new NotImplementedException();
        }

        public SingleResponse<IList<DirectivasModel>> RecuperaListaCoberturas(DirectivasModel directivasModel)
        {
            throw new NotImplementedException();
        }
    }
}
