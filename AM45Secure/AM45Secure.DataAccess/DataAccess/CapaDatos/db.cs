using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AM45Secure.DataAccess.Entidades;


namespace AM45Secure.DataAccess.DataAccess.CapaDatos
{
    /* INDRA FJQP Emisión Multiple */
    public class db
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString);

        public void Add_VehiculoDB(VehiculosCapturaEmision rs)

        {

            SqlCommand com = new SqlCommand("sp_Insert_tblTempEmisionMasiva", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@idNoCotizacion", rs.idNoCotizacion);

            com.Parameters.AddWithValue("@idNoConsec", rs.idNoConsec);

            com.Parameters.AddWithValue("@sNoSerie", rs.sNoSerie);

            com.Parameters.AddWithValue("@sNoMotor", rs.sNoMotor);

            com.Parameters.AddWithValue("@sPlacas", rs.sPlacas);

            com.Parameters.AddWithValue("@sContrato", rs.sContrato);

            com.Parameters.AddWithValue("@iEstatusReg", rs.iEstatusReg);

            com.Parameters.AddWithValue("@sConductor", rs.sConductor);

            com.Parameters.AddWithValue("@sSolicitud", rs.sSolicitud);

            com.Parameters.AddWithValue("@sQLTS", rs.sQLTS);

            com.Parameters.AddWithValue("@sCotizacion", rs.sCotizacion);

            com.Parameters.AddWithValue("@sPolizaQLT", rs.sPolizaQLT);

            com.Parameters.AddWithValue("@iInciso", rs.iInciso);

            com.Parameters.AddWithValue("@iEndoso", rs.iEndoso);

            try
            {
                con.Open();

                com.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception e)
            {
                throw;
            }



        }
        /* INDRA FJQP Emisión Multiple */
        public void Update_VehiculoDB(VehiculosCapturaEmision rs)

        {

            SqlCommand com = new SqlCommand("sp_Update_tblTempEmisionMasiva", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@idNoCotizacion", rs.idNoCotizacion);

            com.Parameters.AddWithValue("@idNoConsec", rs.idNoConsec);

            com.Parameters.AddWithValue("@sNoSerie", rs.sNoSerie);

            com.Parameters.AddWithValue("@sNoMotor", rs.sNoMotor);

            com.Parameters.AddWithValue("@sPlacas", rs.sPlacas);

            com.Parameters.AddWithValue("@sContrato", rs.sContrato);

            com.Parameters.AddWithValue("@iEstatusReg", rs.iEstatusReg);

            com.Parameters.AddWithValue("@sConductor", rs.sConductor);

            com.Parameters.AddWithValue("@sSolicitud", rs.sSolicitud);

            com.Parameters.AddWithValue("@sQLTS", rs.sQLTS);

            com.Parameters.AddWithValue("@sCotizacion", rs.sCotizacion);

            com.Parameters.AddWithValue("@sPolizaQLT", rs.sPolizaQLT);

            com.Parameters.AddWithValue("@iInciso", rs.iInciso);
            com.Parameters.AddWithValue("@iEndoso", rs.iEndoso);

            try
            {
                con.Open();

                com.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        /* INDRA FJQP Emisión Multiple */
        public void Update_VehiculoDBNuevo(int NoCot, int idNoConsec, String sSolicitud, String sQLTS, String sCotizacion, String sPolizaQLT, int iEstatusReg, int iInciso, int iEndoso)

        {

            SqlCommand com = new SqlCommand("sp_Update_tblTempEmisionMasiva", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@idNoCotizacion", NoCot);

            com.Parameters.AddWithValue("@idNoConsec", idNoConsec);

            com.Parameters.AddWithValue("@sSolicitud", sSolicitud);

            com.Parameters.AddWithValue("@iEstatusReg", iEstatusReg);

            com.Parameters.AddWithValue("@sQLTS", sQLTS);

            com.Parameters.AddWithValue("@sCotizacion", sCotizacion);

            com.Parameters.AddWithValue("@sPolizaQLT", sPolizaQLT);

            com.Parameters.AddWithValue("@iInciso", iInciso);

            com.Parameters.AddWithValue("@iEndoso", iEndoso);

            try
            {
                con.Open();

                com.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        /* INDRA FJQP Emisión Multiple */

        /* INDRA FJQP Encontrack */
        public void InsertEncontrack(int NoCot, int idNoConsec, String sSolicitud, String sQLTS, String sCotizacion, String sPolizaQLT, int iInciso, int iEndoso, int Encontrack)

        {

            SqlCommand com = new SqlCommand("sp_Insert_tblTempEmisionMasivaEncontrack", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@idNoCotizacion", NoCot);

            com.Parameters.AddWithValue("@idNoConsec", idNoConsec);

            com.Parameters.AddWithValue("@sSolicitud", sSolicitud);

            com.Parameters.AddWithValue("@sQLTS", sQLTS);

            com.Parameters.AddWithValue("@sCotizacion", sCotizacion);

            com.Parameters.AddWithValue("@sPolizaQLT", sPolizaQLT);

            com.Parameters.AddWithValue("@iInciso", iInciso);

            com.Parameters.AddWithValue("@iEndoso", iEndoso);

            com.Parameters.AddWithValue("@Encontrack", Encontrack);

            try
            {
                con.Open();

                com.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception e)
            {
                throw;
            }

        }
        /* INDRA FJQP Encontrack */

        public DataSet Getrecord(int NoCot, int iStatus)

        {

            SqlCommand com = new SqlCommand("sp_Get_tblTempEmisionMasiva", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idNoCotizacion", NoCot);
            com.Parameters.AddWithValue("@iEstatusReg", iStatus);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }
        /* INDRA FJQP Emisión Multiple */
        public DataSet GetrecordPoliza(int NoCot, int iStatus)

        {

            SqlCommand com = new SqlCommand("sp_Get_tblTempEmisionMasivaPoliza", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idNoCotizacion", NoCot);
            com.Parameters.AddWithValue("@iEstatusReg", iStatus);
            com.Parameters.AddWithValue("@stringJSON", "");


            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }
        /* INDRA FJQP Emisión Multiple */
        public DataSet GetrecordCotiza(int NoCot)

        {

            SqlCommand com = new SqlCommand("sp_Get_InfoCotizacionMultiple", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idNoCotizacion", NoCot);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }
        /* INDRA FJQP Emisión Multiple */
        public DataSet GetCoberturas(int Accion, int NoCob, int NoProd, int Producto, int TipoVehiculo, int TipoServicioVehiculo)

        {

            SqlCommand com = new SqlCommand("sp_Get_DistinctCoberturasProductos", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@iAccion", Accion);
            com.Parameters.AddWithValue("@IdCobertura", NoCob);
            com.Parameters.AddWithValue("@Nombre", "");
            com.Parameters.AddWithValue("@idProductoFlex", NoProd);
            com.Parameters.AddWithValue("@idProductoFlexAseguradora", 0);
            com.Parameters.AddWithValue("@CoberturaFija", 0);
            com.Parameters.AddWithValue("@PerfilCoberturaFija", 0);
            com.Parameters.AddWithValue("@SumaAseguradaDefault", 0);
            com.Parameters.AddWithValue("@PerfilSumaAsegurada", 0);
            com.Parameters.AddWithValue("@DeducibleDefault", 0);
            com.Parameters.AddWithValue("@PerfilDeducible", 0);
            com.Parameters.AddWithValue("@ToolTipCobertura", "");
            com.Parameters.AddWithValue("@isEspecial", 0);
            com.Parameters.AddWithValue("@ParametroDeducible", "");
            com.Parameters.AddWithValue("@ParametroSA", "");
            com.Parameters.AddWithValue("@Rangos", "");
            com.Parameters.AddWithValue("@Producto", Producto);
            com.Parameters.AddWithValue("@TipoVehiculo", TipoVehiculo);
            com.Parameters.AddWithValue("@TipoServicioVehiculo", TipoServicioVehiculo);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }

        /* INDRA FJQP Emisión Multiple */
        public DataSet UpdateCoberturas(int Accion, int NoCob, int NoProd, int NoProdFlexAseg, int CoberturaFija,
                                            int PerfilCoberturaFija, string SumaAseguradaDefault,
                                            int PerfilSumaAsegurada, string DeducibleDefault,
                                            int PerfilDeducible, string ToolTipCobertura, int isEspecial,
                                            int Producto, int TipoVehiculo, int TipoServicioVehiculo)

        {

            SqlCommand com = new SqlCommand("sp_Get_DistinctCoberturasProductos", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@iAccion", Accion);
            com.Parameters.AddWithValue("@IdCobertura", NoCob);
            com.Parameters.AddWithValue("@Nombre", "");
            com.Parameters.AddWithValue("@idProductoFlex", NoProd);
            com.Parameters.AddWithValue("@idProductoFlexAseguradora", NoProdFlexAseg);
            com.Parameters.AddWithValue("@CoberturaFija", CoberturaFija);
            com.Parameters.AddWithValue("@PerfilCoberturaFija", PerfilCoberturaFija);
            com.Parameters.AddWithValue("@SumaAseguradaDefault", SumaAseguradaDefault);
            com.Parameters.AddWithValue("@PerfilSumaAsegurada", PerfilSumaAsegurada);
            com.Parameters.AddWithValue("@DeducibleDefault", DeducibleDefault);
            com.Parameters.AddWithValue("@PerfilDeducible", PerfilDeducible);
            com.Parameters.AddWithValue("@ToolTipCobertura", ToolTipCobertura);
            com.Parameters.AddWithValue("@isEspecial", isEspecial);
            com.Parameters.AddWithValue("@ParametroDeducible", "");
            com.Parameters.AddWithValue("@ParametroSA", "");
            com.Parameters.AddWithValue("@Rangos", "");
            com.Parameters.AddWithValue("@Producto", Producto);
            com.Parameters.AddWithValue("@TipoVehiculo", TipoVehiculo);
            com.Parameters.AddWithValue("@TipoServicioVehiculo", TipoServicioVehiculo);

            try
            {
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /* INDRA FJQP SUMARY */
        public DataSet GetrecordSumary(int NoCot)

        {

            SqlCommand com = new SqlCommand("sp_ObtieneSumario_tblTempEmisionMasiva", con);

            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@idNoCotizacion", NoCot);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }
    }

}
