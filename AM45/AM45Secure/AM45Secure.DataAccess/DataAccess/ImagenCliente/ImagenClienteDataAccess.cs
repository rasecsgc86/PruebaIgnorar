using AM45Secure.DataAccess.IDataAccess.IimagenCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.Commons.Modelos.ImagenCliente;
using AM45Secure.DataAccess.IDataAccess.IGeneric;
using System.Data.SqlClient;
using AM45Secure.Commons.Constantes.Comunes;
using System.Data;
using Zero.Exceptions;
using AM45Secure.Commons.Recursos;

namespace AM45Secure.DataAccess.DataAccess.ImagenCliente
{
    public class ImagenClienteDataAccess : IimagenClienteDataAccess
    {

        private readonly IGenericDataAccess iGenericDataAccess;


        public ImagenClienteDataAccess(IGenericDataAccess iGenericDataAccess)
        {
            this.iGenericDataAccess = iGenericDataAccess;
        }

        public bool ElimarDocumento(int Id)
        {
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpDeleteImagenClienteFlex
                };

                command.Parameters.Add("@Id", SqlDbType.Int).Value = Id;


                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);

                iGenericDataAccess.CloseConnection();

            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);

            }


            return true;
        }

        public bool GuardarDatosIMagen(ImageClienteModel requestModel)
        {
          
            try
            {
                SqlCommand command = new SqlCommand
                {
                    CommandText = (requestModel.IsUpdate == 0) ? ConstStoredProcedures.SpInsertImagenClienteFlex : ConstStoredProcedures.SpUpdateImagenClienteFlex
                };
                if (requestModel.IsUpdate == 1)// Solo Para Updates
                    command.Parameters.Add("@Id", SqlDbType.VarChar).Value = requestModel.Id;

               
                command.Parameters.Add("@URl", SqlDbType.VarChar).Value = requestModel.Url;

               if( requestModel.IsUpdate==0)
                command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = requestModel.IdCliente;
          


                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);

                iGenericDataAccess.CloseConnection();

            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);

            }


            return true;
        }

        public ImageClienteModel seleDatosImagen(ImageClienteModel requestModel)
        {

            var respuesta = new ImageClienteModel();
            try
            {
           
                SqlCommand command = new SqlCommand
                {
                    CommandText = ConstStoredProcedures.SpSelImagenClienteFlex
                };

                command.Parameters.Add("@IdCliente", SqlDbType.Int).Value = requestModel.IdCliente;
                command.Parameters.Add("@IdSolicitud", SqlDbType.Int).Value = requestModel.IdSolictud;


                SqlDataReader datosStored = iGenericDataAccess.StoredProcedure(command);
                if (datosStored.HasRows)
                {
                    while (datosStored.Read())
                    {

                        respuesta.Id = Convert.ToInt32(datosStored["Id"]);
                        respuesta.Fecha = Convert.ToDateTime(datosStored["Fecha"]);
                        respuesta.Url = Convert.ToString(datosStored["Url"]);
                        respuesta.IdCliente = Convert.ToInt32(datosStored["IdCliente"]);




                    }
                    iGenericDataAccess.CloseConnection();

                }
            }
            catch (Exception e)
            {
                iGenericDataAccess.CloseConnection();
                throw new DalException(Codes.ERR_00_01, e);

            }


            return respuesta;
        }
    }
}
