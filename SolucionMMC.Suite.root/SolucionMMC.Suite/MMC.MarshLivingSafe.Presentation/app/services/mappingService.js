'use strict'
/**
 * @author <Josue Ramirez Davila>
 * @mail <josueramirezdavila@gmail.com>
 * @description Se definen las url del API-REST que alimentara la aplicacion.
 * * * ulmima modificacion 09/05/2018 INDRA 
 */

define(['modules/app'],
    function (app) {


        //app.service('sharedpkt', function () {
        //    var stringValue = 'test string value';
        //    var objectpkt = {
        //        data: 'test object value'
        //    };

        //    return {
        //        getString: function () {
        //            return stringValue;
        //        },
        //        setObjectpkt: function (value) {
        //            objectpkt = value;
        //        },
        //        getObjectpkt: function () {
        //            return objectpkt;
        //        }
        //    }
        //});


        app.service('mappingService',
        [
            function () {
                /* PREFIJO QUE SE DEBE CONCATENAR AL PRINCIPIO DE CADA MAPEO DE URL */

                var PATH_REST_SERVICES = "http://qa.automarshprueba.com.mx/AM45Secure.RESTService/api/";
                // var PATH_REST_SERVICES = "http://10.48.69.66/AM45Secure.RESTServices/api/";
                /*  ARREGLO QUE CONTENDRA EL MAPEO DE LAS URL A LOS REST-CONTROLLER*/

                var stringValue = 'test string value';
                var objectpkt = {
                    data: 'test object value'
                };

                return {
                    services: {
                        test: { // modulo|paquete en los controllers de java
                            test: { // nombre del controlador de java
                                upload: PATH_REST_SERVICES + 'Test/UploadFile'
                                // accion del controler java que se ejecutara
                            }
                        },
                        seguridad: {
                            login: {
                                login: "http://qa.automarshprueba.com.mx/AM45RESTService/api/Login",
                                //login: PATH_REST_SERVICES + "Login"
                                consultaMenus: PATH_REST_SERVICES + 'Seguridad/ConsultarMenus',
                                consultaVersionSistema: PATH_REST_SERVICES + 'Seguridad/ConsultaVesionSistema'

                            }
                        },
                        cotizador: {
                            cotizacion: {
                                consultarProductosCliente: PATH_REST_SERVICES + 'Cotizador/ConsultarProductosCliente',
                                consultarCliente: PATH_REST_SERVICES + 'Cotizador/ConsultarCliente',
                                consultarCodigoPostal: PATH_REST_SERVICES + 'Cotizador/ConsultarCodigoPostal',
                                consultarEstados: PATH_REST_SERVICES + 'Cotizador/ConsultarEstados',
                                ConsultaPanelCotizacionFlex:
                                    PATH_REST_SERVICES + 'Cotizador/ConsultaPanelCotizacionFlex',
                                FiltraPanelCotizacionFlex: PATH_REST_SERVICES + 'Cotizador/FiltraPanelCotizacionFlex',
                                consultaReglaNegocio: PATH_REST_SERVICES + 'Cotizador/ConsultaReglaNegocio',
                                cargaElementos: PATH_REST_SERVICES + 'Cotizador/CargaElementos',
                                consultaPasajeros: PATH_REST_SERVICES + 'Cotizador/ConsultaPasajeros',
                                consultarVersiones: PATH_REST_SERVICES + 'Cotizador/ConsultarVersiones',
                                consultaAgencias: PATH_REST_SERVICES + 'Cotizador/ConsultaAgencias',
                                consultaReglaUdi: PATH_REST_SERVICES + 'Cotizador/ConsultaReglaUdi',
                                calculaPlazos: PATH_REST_SERVICES + 'Cotizador/CalculaPlazos',
                                ejecutaGrabadoSolicitudCotizacion:
                                    PATH_REST_SERVICES + 'Cotizador/EjecutaGrabadoSolicitudCotizacion',
                                consultaFechaSistema: PATH_REST_SERVICES + 'Cotizador/ConsultaFechaSistema',
                                consultarCantCliente: PATH_REST_SERVICES + 'Cotizador/ConsultarCantidadCliente',
                                recargaInformacionCotizacion: PATH_REST_SERVICES + 'Cotizador/RecargaDatosCotizacion',
                                validaLimiteValorFactura: PATH_REST_SERVICES + 'Cotizador/ValidaLimiteValorFactura',
                                validaLimiteAdaptaciones: PATH_REST_SERVICES + 'Cotizador/ValidaLimiteAdaptaciones'
                            }
                        },
                        emitir: {
                            emitir: {
                                cargaInicial: PATH_REST_SERVICES + 'Emitir/CargaInicial',
                                crearEmision: PATH_REST_SERVICES + 'Emitir/CrearEmision',
                                consultaInformacionCliente: PATH_REST_SERVICES + 'Emitir/ConsultaInformacionCliente',
                                consultaSerie: PATH_REST_SERVICES + 'Emitir/ConsultaSerie',
                                crearEmisionMultiple: PATH_REST_SERVICES + 'Emitir/CrearEmisionMultiple',
                                consultaSerieGrab: PATH_REST_SERVICES + 'Emitir/ConsultaSerieGrab',
                                recuperaListaCoberturas: PATH_REST_SERVICES + 'Emitir/RecuperaListaCoberturas',
                                recuperaInfoCoberturas: PATH_REST_SERVICES + 'Emitir/RecuperaInfoCoberturas',
                                almacenaInfoCoberturas: PATH_REST_SERVICES + 'Emitir/AlmacenaInfoCoberturas',
                                filtrosConfigMultiple: PATH_REST_SERVICES + 'Emitir/FiltrosConfigMultiple',
                                usuarioAdministador: PATH_REST_SERVICES + 'Emitir/usuarioAdministador',
                                consultaConfigMultiple: PATH_REST_SERVICES + 'Emitir/ConsultarConfigMultiples',
                                guardarDatosConfigMultiple: PATH_REST_SERVICES + 'Emitir/GuardarDatosConfigMultiple',
                                elimarDatosConfigMultiple: PATH_REST_SERVICES + 'Emitir/EliminarDatosConfigMultiple',
                                permiteConfigMultiple: PATH_REST_SERVICES + 'Emitir/PermiteConfigMultiples'
                            },
                            imprimir: {
                                imprimir: 'http://qa.automarshprueba.com.mx/AM45RESTService/api/PrintPolicy'
                            },

                            Emisionmultiple: {
                                Register: PATH_REST_SERVICES + 'Emisionmultiple/Register',
                                getrecord: PATH_REST_SERVICES + 'Emisionmultiple/getrecord',
                                getrecordpoliza: PATH_REST_SERVICES + 'Emisionmultiple/getrecordpoliza',
                                getrecordsumary: PATH_REST_SERVICES + 'Emisionmultiple/getrecordsumary'

                            }
                        },
                        tickets: {
                            reportes: {
                                consultarTicketsReporte: PATH_REST_SERVICES + 'Reporte/ConsultarTicketsReporte',
                                consultarEstatusTickets: PATH_REST_SERVICES + 'Reporte/ConsultarEstatusTickets',
                                consultarTicketsReporteExcel:
                                    PATH_REST_SERVICES + 'Reporte/ConsultarTicketsReporteExcel'
                            },
                            gestionTickets: {
                                consultarTickest: PATH_REST_SERVICES + 'Gestion/ConsultarTickest',
                                consultarClientes: PATH_REST_SERVICES + 'Gestion/ConsultarClientes',
                                consultarSiEsClienteFlotillas:
                                    PATH_REST_SERVICES + 'Gestion/ConsultarSiEsClienteFlotillas',
                                consultarTiposTickets: PATH_REST_SERVICES + 'Gestion/ConsultarTiposTickets',
                                consultarResponsable: PATH_REST_SERVICES + 'Gestion/ConsultarResponsable',
                                consultarCaratula: PATH_REST_SERVICES + 'Gestion/ConsultarCaratula',
                                consultarReportaA: PATH_REST_SERVICES + 'Gestion/ConsultarReportaA',
                                consultaEstatusTickets: PATH_REST_SERVICES + 'Gestion/ConsultaEstatusTickets',
                                consultarAgencias: PATH_REST_SERVICES + 'Gestion/ConsultarAgencias',
                                guardarDatosContacto: PATH_REST_SERVICES + 'Gestion/GuardarDatosContacto',
                                guardarTicket: PATH_REST_SERVICES + 'Gestion/GuardarTicket',
                                cargarArchivo: PATH_REST_SERVICES + 'Gestion/CargarArchivo',
                                eliminarArchivo: PATH_REST_SERVICES + 'Gestion/EliminarArchivo',
                                consultarAseg: PATH_REST_SERVICES + 'Gestion/ConsultaAseguradoras'
                            },
                            calendario: {
                                guardarCalendario: PATH_REST_SERVICES + 'CalendarioTickets/GuardarCalendario',
                                eliminarCalendario: PATH_REST_SERVICES + 'CalendarioTickets/EliminarCalendario',
                                consultarCalendario: PATH_REST_SERVICES + 'CalendarioTickets/ConsultarCalendario'
                            },
                            configuracion: {
                                consultarConfigurarParametros:
                                    PATH_REST_SERVICES + 'ConfigurarParametrosTickets/ConsultarConfigurarParametros',
                                consultarClientesConfigurarParametros:
                                    PATH_REST_SERVICES +
                                        'ConfigurarParametrosTickets/ConsultarClientesConfigurarParametros',
                                buscarUsuarioResponsable: PATH_REST_SERVICES +
                                    'ConfigurarParametrosTickets/BuscarUsuarioResponsable',
                                guardarTiposTicketsClientes:
                                    PATH_REST_SERVICES + 'ConfigurarParametrosTickets/GuardarTiposTicketsClientes',
                                eliminarTipoTicketsCliente:
                                    PATH_REST_SERVICES + 'ConfigurarParametrosTickets/EliminarTipoTicketsCliente',
                                actulizarTiposTicketsClientes:
                                    PATH_REST_SERVICES + 'ConfigurarParametrosTickets/ActulizarTiposTicketsClientes'
                            },
                            Archivo: {
                                descargarArchivo: PATH_REST_SERVICES + 'Archivo/DescargarArchivo'
                            },
                            seguimiento: {
                                consultarInformacionTicket:
                                    PATH_REST_SERVICES + 'SeguimientoTickets/ConsultarInformacionTicket',
                                obetnerEstatusByUsuario: PATH_REST_SERVICES +
                                    'SeguimientoTickets/ObetnerEstatusByUsuario',
                                guardarComentariosTicket: PATH_REST_SERVICES +
                                    'SeguimientoTickets/GuardarComentariosTicket',
                                listarComentariosTicket: PATH_REST_SERVICES +
                                    'SeguimientoTickets/ListarComentariosTicket',
                                listaArchivosTickets: PATH_REST_SERVICES + 'SeguimientoTickets/ListaArchivosTickets',
                                eliminarArchivos: PATH_REST_SERVICES + 'SeguimientoTickets/EliminarArchivo',
                                cargarArchivoSeguimiento: PATH_REST_SERVICES +
                                    'SeguimientoTickets/CargarArchivoSeguimiento',
                                buscarUsuarioResponsableSeguimiento:
                                    PATH_REST_SERVICES + 'SeguimientoTickets/BuscarUsuarioResponsableSeguimiento',
                                reasignarResposnable: PATH_REST_SERVICES + 'SeguimientoTickets/ReasignarResposnable',
                                guardarArchivoSeguimientoCierre:
                                    PATH_REST_SERVICES + 'SeguimientoTickets/GuardarArchivoSeguimientoCierre',
                                guardarSeguimientoCierreSinArchivo:
                                    PATH_REST_SERVICES + 'SeguimientoTickets/GuardarSeguimientoCierreSinArchivo'
                            }
                        },
                        comparador: {
                            comparador: {
                                cargaInicial: PATH_REST_SERVICES + 'Comparador/CargaInicial',
                                cargaCotizacion: PATH_REST_SERVICES + 'Comparador/ConsultarCabeceraCotizacion',
                                cargaComparador: PATH_REST_SERVICES + 'Comparador/CargaComparador',
                                cargaReporte: PATH_REST_SERVICES + 'Comparador/ConsultarReporteCotizacion',
                                cargaCotizacionCot: PATH_REST_SERVICES +
                                    'Comparador/ConsultarCabeceraCotizacionCotizador',
                                enviorReporteCotEmail: PATH_REST_SERVICES + 'Comparador/EnviarMailReporteCotizacion',
                                descargaReporteCotEmail: PATH_REST_SERVICES + 'Comparador/descargaReporteEmail'
                            }
                        },
                        imprimir: {
                            folletos: {
                                consultaFolletos: PATH_REST_SERVICES + 'Imprimir/ConsultarFolletos',
                                descargarArchivo: PATH_REST_SERVICES + 'Imprimir/DescargarArchivo',
                                consultarAsegPaquete: PATH_REST_SERVICES + 'Imprimir/ConsultaAsegPaquete',
                                ConsultaCondicionesGenerales: PATH_REST_SERVICES + 'Imprimir/ConsultaCondicionesGrales'
                            }
                        },
                        manuales: {
                            manuales: {
                                consultaManuales: PATH_REST_SERVICES + 'Manuales/ConsultarManuales',
                                consultarCategoria: PATH_REST_SERVICES + 'Manuales/ConsultarCategoria',
                                cargarArchivo: PATH_REST_SERVICES + 'Manuales/CargarArchivo',
                                guardarDatosDocumento: PATH_REST_SERVICES + 'Manuales/GuardarDatosDocumento',
                                descargarArchivo: PATH_REST_SERVICES + 'Manuales/DescargarArchivo',
                                elimarDocumento: PATH_REST_SERVICES + 'Manuales/ElimarDocumento',
                                filtrosDocumentos: PATH_REST_SERVICES + 'Manuales/FiltrosDocumentos',
                                usuarioAdministador: PATH_REST_SERVICES + 'Manuales/usuarioAdministador'
                            }
                        },
                        imagenCliente: {
                            imagenCliente: {
                              
                                cargarArchivo: PATH_REST_SERVICES + 'ImagenCliente/CargarArchivo',
                                guardarDatosImagenCliente: PATH_REST_SERVICES + 'ImagenCliente/GuardarDatosImagenCliente', 
                                obtenerImagenCliente: PATH_REST_SERVICES + 'ImagenCliente/ObtenerImagenCliente',
                                elimarDocumento: PATH_REST_SERVICES + 'ImagenCliente/ElimarDocumento'
                            }
                        },
                        configurador: {
                            configurador: {
                                consultaClientesConfigurador: PATH_REST_SERVICES + 'Configurador/ConsultarClientesConfigurador',
                                consultaProductosFlexibles: PATH_REST_SERVICES + 'Configurador/ConsultaProductosFlexibles',
                                consultaTipoAuto: PATH_REST_SERVICES + 'Configurador/ConsultaTipoAuto',
                                consultaTipoServicio: PATH_REST_SERVICES + 'Configurador/ConsultaTipoServicio',
                                consultaTipoSeguro: PATH_REST_SERVICES + 'Configurador/ConsultaTipoSeguro',
                                esNuevo: PATH_REST_SERVICES + 'Configurador/EsNuevo',
                                cargoEnLinea: PATH_REST_SERVICES + 'Configurador/CargoEnLinea',
                                consultaAseguradoras: PATH_REST_SERVICES + 'Configurador/ConsultaAseguradoras',
                                consultaEstatus: PATH_REST_SERVICES + 'Configurador/ConsultaEstatus',
                                guardaProductoFlexible: PATH_REST_SERVICES + 'Configurador/GuardaProductoFlexible',
                                consultaPerfilesSistema: PATH_REST_SERVICES + 'Configurador/ConsultaPerfilesSistema',
                                consultaUsuariosPorPerfil: PATH_REST_SERVICES + 'Configurador/ConsultaUsuarioPorPerfil',
                                guardaUsuarioFlexible: PATH_REST_SERVICES + 'Configurador/GuardaUsuarioFlexible',
                                consultarUsuariosFlexibles: PATH_REST_SERVICES + 'Configurador/ConsultarUsuariosFlexibles',
                                actualizaUdiUsuario: PATH_REST_SERVICES + 'Configurador/ActualizaStatusUdi',
                                eliminarUsuarioFlexible: PATH_REST_SERVICES + 'Configurador/EliminarUsuarioFlexible',
                                consultaFormasPago: PATH_REST_SERVICES + 'Configurador/ConsultaFormasPago',
                                guardarFormaPagoProducto: PATH_REST_SERVICES + "Configurador/GrabarFormaPagoProducto",
                                actualizaPredeterminadoProducto: PATH_REST_SERVICES + "Configurador/ActualizaPredeterminadoPago",
                                eliminarFormaDePago: PATH_REST_SERVICES + "Configurador/EliminarFormaDePago",
                                consultarFormasPagoLista: PATH_REST_SERVICES + "Configurador/ConsultarFormasPagoLista",
                                consultaPanelConfiguradorFlex: PATH_REST_SERVICES + "Configurador/ConsultaPanelConfiguradorFlex",
                                consultaRangosSumasAseguradas: PATH_REST_SERVICES + "Configurador/ConsultaRangosSumasAseguradas",
                                grabarFormasPagoProductoAseguradora: PATH_REST_SERVICES + "Configurador/GrabarFormasPagoProductoAseguradora",
                                consultarFormasPagoAseguradoraLista: PATH_REST_SERVICES + "Configurador/ConsultarFormasPagoAseguradoraLista",
                                eliminarFormaPagoProductoAseguradora: PATH_REST_SERVICES + "Configurador/EliminarFormaPagoProductoAseguradora",
                                actualizarHomologacionTooltip: PATH_REST_SERVICES + "Configurador/ActualizarHomologacionTooltip",
                                actualizaRangosDeducibles: PATH_REST_SERVICES + "Configurador/ActualizaRangosDeducibles",
                                actualizaRangosSumas: PATH_REST_SERVICES + "Configurador/ActualizaRangosSumas",
                                recuperaInfoDirectivas: PATH_REST_SERVICES + 'Configurador/RecuperaInfoDirectivas',
                                recuperaListaCoberturas: PATH_REST_SERVICES + 'Configurador/RecuperaListaCoberturas',
                                consultaEnmarcaradoDeducibles: PATH_REST_SERVICES + 'Configurador/ConsultarEnmascaradoDeducibles',
                                cargaArchivoCobertura: PATH_REST_SERVICES + 'Configurador/CargaArchivoCobertura',
                                guardaDocumentoCobertura: PATH_REST_SERVICES + 'Configurador/GuardaDocumentoCobertura',
                                consultaDocumentosCobertura: PATH_REST_SERVICES + 'Configurador/ConsultaDocumentosPorCobertura',
                                descargarArchivo: PATH_REST_SERVICES + 'Configurador/DescargarArchivo',
                                guardaTextoAuxiliarUso: PATH_REST_SERVICES + 'Configurador/GuardaTextoAuxiliarUso',
                                consularDocumentosTodos: PATH_REST_SERVICES + 'Configurador/ConsultaDocumentosTodos',
                                consultaRelacionCoberturas: PATH_REST_SERVICES + 'Configurador/ConsultaRelacionCoberturas',
                                consultarCoberturasPrincipales: PATH_REST_SERVICES + 'Configurador/ConsultarCoberturasPrincipales',
                                consultarCoberturasDependientes: PATH_REST_SERVICES + 'Configurador/ConsultarCoberturasDependientes',
                                guardarCoberturasDependientes: PATH_REST_SERVICES + 'Configurador/GuardarCoberturasDependientes'
                                
                            }

                        },

                        getString: function () {
                            return stringValue;
                        },
                        setString: function (value) {
                            stringValue = value;
                        },
                        getObjectpkt: function () {
                            return objectpkt;
                        }
                        ,
                        setObjectpkt: function (value) {
                            objectpkt = value;
                        }
                    }
                };
            }
        ]);

    });