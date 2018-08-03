using System;
using System.Web.Http;
using System.Collections.Generic;
using Zero.Handlers.Response;
using System.Data;
using AM45Secure.Business.IBusiness.IComparador;
using AM45Secure.Business.IBusiness.ICotizador;
using AM45Secure.DataAccess.IDataAccess.ICotizador;
using AM45Secure.DataAccess.DataAccess.CapaDatos;


namespace AM45Secure.RESTServices.Controllers.Emisionmultiple
{
    //[Authorize]
    public class EmisionmultipleController : ApiController
    {
        private readonly ICotizadorDataAccess iCotizadorDataAccess;
        private readonly IComparadorBusiness iComparadorBusiness;
        private readonly ICotizadorBusiness iCotizadorBusiness;

        public EmisionmultipleController(ICotizadorDataAccess iCotizadorDataAccess, IComparadorBusiness iComparadorBusiness, ICotizadorBusiness iCotizadorBusiness)
        {
            this.iCotizadorDataAccess = iCotizadorDataAccess;
            this.iComparadorBusiness = iComparadorBusiness;
            this.iCotizadorBusiness = iCotizadorBusiness;
        }

        /* INDRA FJQP -- Modificaciones Emision Multiple */
        // GET: Vehiculos
        DataAccess.DataAccess.CapaDatos.db dblayer = new DataAccess.DataAccess.CapaDatos.db();


        /* INDRA FJQP -- Modificaciones Emision Multiple */
        [HttpPost]
        public SingleResponse<VehiculosCapturaEmision> Register(List<VehiculosCapturaEmision> ListJson)
        {
            SingleResponse<VehiculosCapturaEmision> response = new SingleResponse<VehiculosCapturaEmision>();
            string res = string.Empty;
            try
            {
                foreach (var item in ListJson)
                {
                    VehiculosCapturaEmision rs = new VehiculosCapturaEmision();
                    rs.idNoCotizacion = item.idNoCotizacion;
                    rs.idNoConsec = item.idNoConsec;
                    rs.sNoSerie = item.sNoSerie;
                    rs.sNoMotor = item.sNoMotor;
                    rs.sPlacas = item.sPlacas;
                    rs.sContrato = item.sContrato;
                    rs.sConductor = item.sConductor;
                    rs.iEstatusReg = item.iEstatusReg;
                    rs.sSolicitud = item.sSolicitud;
                    rs.sQLTS = item.sQLTS;
                    rs.sCotizacion = item.sCotizacion;
                    rs.sPolizaQLT = item.sPolizaQLT;
                    rs.iInciso = item.iInciso;
                    rs.iEndoso = item.iEndoso;

                    dblayer.Add_VehiculoDB(rs);


                    res = "Se insertaron los registros...!";

                }

            }
            catch (Exception)
            {
                res = "Fallo al Realizar inserción de Registro";
            }

            response.IsOk.Equals(true);
            //** response.Message.Equals(res);

            VehiculosCapturaEmision datosCliente = new VehiculosCapturaEmision();
            response.Done(datosCliente, string.Empty);

            return response;

        }

        /* INDRA FJQP -- Modificaciones Emision Multiple */
        [HttpPost]
        public SingleResponse<List<VehiculosCapturaEmision>> getrecord(VehiculosCapturaEmision ListParam)
        {
            SingleResponse<List<VehiculosCapturaEmision>> response = new SingleResponse<List<VehiculosCapturaEmision>>();

            int iNoCot = 0;
            int iEstatus = 0;

            iNoCot = ListParam.idNoCotizacion;
            iEstatus = ListParam.iEstatusReg;


            DataSet ds = dblayer.Getrecord(iNoCot, iEstatus);

            List<VehiculosCapturaEmision> listreg = new List<VehiculosCapturaEmision>();

            List<VehiculosCapturaEmision> datosCliente = new List<VehiculosCapturaEmision>();


            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                listreg.Add(new VehiculosCapturaEmision

                {
                    idNoCotizacion = Convert.ToInt32(dr["idNoCotizacion"]),
                    idNoConsec = Convert.ToInt32(dr["idNoConsec"]),
                    sNoSerie = Convert.ToString(dr["sNoSerie"]),
                    sNoMotor = Convert.ToString(dr["sNoMotor"]),
                    sPlacas = Convert.ToString(dr["sPlacas"]),
                    sContrato = Convert.ToString(dr["sContrato"]),
                    iEstatusReg = Convert.ToInt32(dr["iEstatusReg"]),
                    sDescEstatus = Convert.ToString(dr["sDescEstatus"]),
                    sConductor = Convert.ToString(dr["sConductor"]),
                    sSolicitud = Convert.ToString(dr["sSolicitud"]),
                    sQLTS = Convert.ToString(dr["sQLTS"]),
                    sCotizacion = Convert.ToString(dr["sCotizacion"]),
                    sPolizaQLT = Convert.ToString(dr["sPolizaQLT"]),
                    iInciso = Convert.ToInt32(dr["iInciso"]),
                    iEndoso = Convert.ToInt32(dr["iEndoso"]),

                });
            }

            datosCliente = listreg;

            response.IsOk.Equals(true);

            response.Done(datosCliente, string.Empty);

            return response;

        }

        /* INDRA FJQP -- Modificaciones Emision Multiple */
        [HttpPost]
        public SingleResponse<List<VehiculosCapturaEmision>> getrecordpoliza(VehiculosCapturaEmision ListParam)
        {
            SingleResponse<List<VehiculosCapturaEmision>> response = new SingleResponse<List<VehiculosCapturaEmision>>();

            int iNoCot = 0;
            int iEstatus = 0;

            iNoCot = ListParam.idNoCotizacion;
            iEstatus = ListParam.iEstatusReg;


            DataSet ds = dblayer.GetrecordPoliza(iNoCot, iEstatus);

            List<VehiculosCapturaEmision> listreg = new List<VehiculosCapturaEmision>();

            List<VehiculosCapturaEmision> datosCliente = new List<VehiculosCapturaEmision>();


            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                listreg.Add(new VehiculosCapturaEmision

                {

                    idNoCotizacion = Convert.ToInt32(dr["idNoCotizacion"]),
                    idNoConsec = Convert.ToInt32(dr["idNoConsec"]),
                    sNoSerie = Convert.ToString(dr["sNoSerie"]),
                    sNoMotor = Convert.ToString(dr["sNoMotor"]),
                    sPlacas = Convert.ToString(dr["sPlacas"]),
                    sContrato = Convert.ToString(dr["sContrato"]),
                    iEstatusReg = Convert.ToInt32(dr["iEstatusReg"]),
                    sDescEstatus = Convert.ToString(dr["sDescEstatus"]),
                    sConductor = Convert.ToString(dr["sConductor"]),
                    sSolicitud = Convert.ToString(dr["sSolicitud"]),
                    sQLTS = Convert.ToString(dr["sQLTS"]),
                    sCotizacion = Convert.ToString(dr["sCotizacion"]),
                    sPolizaQLT = Convert.ToString(dr["sPolizaQLT"]),
                    iInciso = Convert.ToInt32(dr["iInciso"]),
                    iEndoso = Convert.ToInt32(dr["iEndoso"]),
                    sJSON = Convert.ToString(dr["sJSON"]),

                });
            }

            datosCliente = listreg;

            response.IsOk.Equals(true);

            response.Done(datosCliente, string.Empty);

            return response;

        }

        /* INDRA FJQP SUMARY */
        [HttpPost]
        public SingleResponse<List<Sumario>> getrecordsumary(Sumario ListParam)
        {
            SingleResponse<List<Sumario>> response = new SingleResponse<List<Sumario>>();

            int iNoCot = 0;

            iNoCot = ListParam.idNoCotizacion;


            DataSet ds = dblayer.GetrecordSumary(iNoCot);

            List<Sumario> listreg = new List<Sumario>();

            List<Sumario> datosCliente = new List<Sumario>();


            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                listreg.Add(new Sumario

                {

                    idNoCotizacion = Convert.ToInt32(dr["idNoCotizacion"]),
                    intCotGen = Convert.ToInt32(dr["CotGen"]),
                    intPolEmi = Convert.ToInt32(dr["PolEmi"]),
                    PolNoEmi = Convert.ToInt32(dr["PolNoEmi"]),

                });
            }

            datosCliente = listreg;

            response.IsOk.Equals(true);

            response.Done(datosCliente, string.Empty);

            return response;

        }

    }
}