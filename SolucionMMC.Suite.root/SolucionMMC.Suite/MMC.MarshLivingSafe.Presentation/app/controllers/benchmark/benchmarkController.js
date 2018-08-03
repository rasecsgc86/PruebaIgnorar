define(['modules/app', 'services/requestService', 'services/mappingService'],
    function (app) {
        /**
         * DEFINIMOS Y REGISTRAMOS EL CONTROLLER configController(Configurador).
         * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
         * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
         * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
         * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
         *
         * @var tittle - TITULO DE LA PAGINA
         * 
         * Modificacion INDRA FJQP - Notas Importantes, Tooltips ---
         * 
         */
        app.controller('BenchmarkController',
        [
            '$scope', '$stateParams', '$state', '$rootScope', '$location', '$sce', 'requestService', '$localStorage',
            'Notification', 'mappingService', '$sessionStorage', '$base64', '$http',
            function ($scope,
                $rootScope,
                $stateParams,
                $state,
                $location,
                $sce,
                requestService,
                $localStorage,
                Notification,
                mappingService,
                $sessionStorage,
                $base64, $http) {
                InicializaVariables();



                function InicializaVariables() {
                    $scope.$parent.idSolicitudR = $stateParams.params.idSolicitud;
                    /*Variables para los diseños*/
                    $rootScope.tittle = "Cotizador - Comparador de seguros";
                    $rootScope.bMenu = true;
                    $scope.$parent.rama = 'comp';
                    $scope.$parent.bramaCot = false;
                    $scope.$parent.bramaComp = true;
                    $scope.$parent.bramaEmit = false;
                    $scope.$parent.bramaPag = false;
                    $scope.$parent.bramaImp = false;
                    $scope.$parent.bPanelCoberturas = false;
                    $scope.notasImportantes = "";   /* INDRA FJQP Notas Importantes, tooltips */
                    $scope.$parent.paquete = '';
                    /*Template para mostrar las coberturas*/
                    $scope.coberturasPopover = {
                        content: 'Hello, World!',
                        templateUrl: './views/coberturas/coberturas.html',
                        title: 'coberturas'
                    };
                    /*Template para mostrar las leyendas de los nombres de las coberturas*/
                    $scope.leyendaCobPopover = {
                        templateUrl: './views/coberturas/leyendas.html',
                        trigger: 'focus'
                    };
                    /*Template para mostrar las acciones de los nombres de las coberturas*/
                    $scope.accionesPopover = {
                        templateUrl: './views/coberturas/acciones.html',
                        trigger: 'focus'
                    };
                    /* INDRA FJQP Notas Importantes, tooltips y Emsión Multiple */
                    $scope.$parent.CabeceraCotizacionHererada = $sessionStorage.CabeceraCotSession;
                    $scope.$parent.CotizarModelHeredada = $sessionStorage.CotizarModelHeredada;
                    /* INDRA FJQP Notas Importantes, tooltips y Emsión Multiple */

                    $scope.showErroresCot = false;

                    $scope.lFormasPago = [];
                    CargaInicial();
                    CargarCotizacion();
                }

                /**********************************************************
                * Funcion para cargar los datos iniciales de la cotización 
                * Formas de pago, Datos Solicitud
                ***********************************************************/
                function CargaInicial() {
                    var comparadorModel = {
                        datosCotizacionModel: {
                            SolicitudId: parseInt($stateParams.params.idSolicitud),
                            FormaPagoId: 0
                        }
                    }
                    $scope.$parent.CotizarModelHeredada = comparadorModel;
                    $sessionStorage.CotizarModelHeredada = comparadorModel;
                    requestService.post('comparador',
                        'comparador',
                        'cargaInicial',
                        comparadorModel,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.notasImportantes = response.NotasImportantes;
                            if ($scope.notasImportantes != 'NULL' &&
                                $scope.notasImportantes != '' &&
                                $scope.notasImportantes != null)
                                document.getElementById('notas').innerHTML = $scope.notasImportantes;
                            $scope.idSolicitud = response.SolicitudCotizacion.SolicitudId;
                            $scope.lFormasPago = response.FormasPagoProducto;
                            var ejecutoComparador = false;
                            for (i = 0; i < $scope.lFormasPago.length; i++) {
                                if ($scope.lFormasPago[i].Predeterminado) {
                                    var comparadorModel = {
                                        datosCotizacionModel: {
                                            SolicitudId: parseInt($stateParams.params.idSolicitud),
                                            FormaPagoId: $scope.lFormasPago[i].FormaPagoId
                                        }
                                    }

                                    $scope.$parent.CotizarModelHeredada = comparadorModel;
                                    $sessionStorage.CotizarModelHeredada = comparadorModel;

                                    CargaComparador(comparadorModel);
                                    ejecutoComparador = true;
                                    break;
                                }
                            }
                            if (!ejecutoComparador && $scope.lFormasPago.length > 0) {
                                var comparadorModel = {
                                    datosCotizacionModel: {
                                        SolicitudId: parseInt($stateParams.params.idSolicitud),
                                        FormaPagoId: $scope.lFormasPago[0].FormaPagoId
                                    }
                                }
                                $scope.$parent.CotizarModelHeredada = comparadorModel;
                                $sessionStorage.CotizarModelHeredada = comparadorModel;
                                CargaComparador(comparadorModel);
                            }

                        },
                        function errorFunction(response) {
                            console.log("Hubo un error" + response);
                        },
                        function badRequestFunction() { });
                };

                function CargarCotizacion() {
                    if ($stateParams.params.idSolicitud != undefined || $stateParams.params.idSolicitud != '') {
                        var comparadorModel = {
                            datosCotizacionModel: {
                                SolicitudId: $stateParams.params.idSolicitud,
                                FormaPagoId: 0
                            }
                        };
                        /* INDRA FJQP Notas Importantes, tooltips y Emsión Multiple */
                        $scope.$parent.CotizarModelHeredada = comparadorModel;
                        $sessionStorage.CotizarModelHeredada = comparadorModel;
                        /* INDRA FJQP Notas Importantes, tooltips y Emsión Multiple */
                        requestService.post('comparador',
                            'comparador',
                            'cargaCotizacion',
                            comparadorModel,
                            true,
                            true,
                            true,
                            function successFunction(response) {
                                $scope.$parent.hayDatosPanel = response.Panel.Coberturas.length > 0;
                                $scope.$parent.Informacion = response;
                                $scope.$parent.listaAdaptaciones = response.Panel.Coberturas;


                                if ($scope.$parent.Informacion.Cliente.Producto.Flexible == 1) {
                                    $scope.$parent.paquete = $scope.$parent.PAQUETEFLEX;
                                }
                            },
                            function errorFunction(response) {
                                console.log("Hubo un error" + response);
                            },
                            function badRequestFunction() { });
                    }
                };

                /**********************************************************
              * Funcion para cargar los datos iniciales de la cotización 
              * por cada forma de pago
              * Formas de pago, Datos Solicitud
              ***********************************************************/
                function CargaComparador(comparadorModel) {
                    requestService.post('comparador',
                        'comparador',
                        'cargaComparador',
                        comparadorModel,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.datosComparador = response;
                            $scope.CotId = $scope.datosComparador.ListNeCotizacion.CotizacionId;
                            console.log($scope.CotId);
                            InicializaImgAseg();
                            DisenioFormaPago(comparadorModel.datosCotizacionModel.FormaPagoId);
                            cargarCoberturasDocumentos($scope.CotId);
                            //<!-- INDRA FJQP SUMARY */-->
                            if (response.Errores != " Cotizacion generada con exito  Error Cotizacion QLT Deducible La conversión del tipo 'DBNull' en el tipo 'String' no es válida.") {
                                $scope.showErroresCot = true;
                                $scope.ErroresCot = response.Errores;
                            }
                            else {
                                $scope.showErroresCot = false;
                            }
                            //<!-- INDRA FJQP SUMARY */-->
                            return;
                        },
                        function errorFunction(response) {
                            console.log("Hubo un error" + response);
                        },
                        function badRequestFunction() { });
                };


                /**********************************************************
                * Funcion para inicializar las imagenes de las asgeuradoras 
                ***********************************************************/
                function InicializaImgAseg() {
                    for (j = 0; j < jsonAseguradorasImg.length; j++)
                        for (i = 0; i < $scope.datosComparador.AseguradorasProducto.length; i++)
                            if (jsonAseguradorasImg[j].aseguradoraId ==
                                $scope.datosComparador.AseguradorasProducto[i].AseguradoraId) {
                                $scope.datosComparador.AseguradorasProducto[i].Img = jsonAseguradorasImg[j].img;
                            }


                }

                /**********************************************************
                * Funcion para cambiar la comparacion por la forma de pago
                ***********************************************************/
                $scope.CambiarFormaPago = function (FormaPago) {
                    var comparadorModel = {
                        datosCotizacionModel: {
                            SolicitudId: parseInt($stateParams.params.idSolicitud),
                            FormaPagoId: FormaPago.FormaPagoId
                        }
                    };
                    $scope.$parent.CotizarModelHeredada = comparadorModel;
                    $sessionStorage.CotizarModelHeredada = comparadorModel;

                    CargaComparador(comparadorModel);
                }

                /**********************************************************
                * Funcion para indicar en que forma de pago esta, style
                ***********************************************************/
                function DisenioFormaPago(Id) {
                    document.getElementById(Id).style.backgroundColor = "rgb(167,88,32)";
                    for (i = 0; i < $scope.lFormasPago.length; i++) {
                        if ($scope.lFormasPago[i].FormaPagoId != Id)
                            document.getElementById($scope.lFormasPago[i].FormaPagoId).style
                                .backgroundColor = "rgb(244,129,50)";
                    }
                }

                /**********************************************************
                * Funcion para descargar la funcion de PDF
                ***********************************************************/
                $scope.descargarPDF = function (pkt) {
                    window.open(mappingService.services['comparador']['comparador']['cargaReporte'] +
                        "?cotizacionId=" +
                        pkt.CotizacionId +
                        "&flexible=" +
                        pkt.Flexible +
                        "&paqueteId=" +
                        pkt.ElementoId +
                        "&solicitudId=" +
                        $scope.idSolicitud +
                        "&numero=" +
                        pkt.Numero +
                        "&tkn=" +
                        JSON.parse($base64.decode($sessionStorage.tkn)),
                        '_blank',
                        '');
                }
                $scope.IniNomPkt = function (pkt) {
                    $scope.$parent.paquete = '';
                    $scope.$parent.paquete = pkt;
                }

                function cargarCoberturasDocumentos(cotId) {
                    console.log("entro a carga de documentos");


                    console.log($scope.CotId);

                    var solicitud = {
                        cotizacionModel: {
                            CotizacionId: cotId
                        }
                    }

                    requestService.post('configurador',
                    'configurador',
                    'consultaDocumentosCobertura',
                    solicitud,
                    true,
                    true,
                    true,
                    function successFunction(response) {
                        $scope.CoberturasDocumentos = response;
                    },
                    function errorFunction(response) {
                        console.log("Hubo un error" + response);
                    },
                    function badRequestFunction() { });

                }
                $scope.descargarArchivo = function (downloadPath) {
                    window.open(mappingService.services['configurador']['configurador']['descargarArchivo'] +
                        "?rutaArchivo=" +
                        downloadPath,
                        '_blank',
                        '');
                }

            }
        ]);
    });