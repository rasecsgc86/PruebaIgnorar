define(['modules/app', 'services/requestService', 'services/mappingService'], function (app) {
    /**
	 * DEFINIMOS Y REGISTRAMOS EL CONTROLLER printController.
	 * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
	 * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
	 * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
	 * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
	 *
	 * @var tittle - TITULO DE LA PAGINA  
	 * @bMenu - BANDERA PARA MOSTRAR EL MENU
	 * 
	 * Modificacion INDRA FJQP -- Emisión Múltiple ---
	 * 
	 * 
	 */
    app.controller('PrintController', ['$scope', '$rootScope', '$location', 'requestService', '$sessionStorage', '$http', '$base64', '$stateParams', 'mappingService',
    function ($scope, $rootScope, $location, requestService, $sessionStorage, $http, $base64, $stateParams, mappingService) {
        $rootScope.bMenu = true;
        $rootScope.tittle = "Cotizador - Impresión de Póliza";
        $scope.$parent.rama = 'imp';
        $scope.$parent.bramaCot = false;
        $scope.$parent.bramaComp = false;
        $scope.$parent.bramaEmit = false;
        $scope.$parent.bramaPag = false;
        $scope.$parent.bramaImp = true;
        $scope.$parent.bPanelCoberturas = false;

        /** Modificacion INDRA FJQP -- Emisión Múltiple ---*/
        $scope.confirmedEncontrack = $sessionStorage.confirmedEncontrack;

        if ($stateParams.esNormal != "false") {
            $scope.esNormal = true;
            $scope.esMultiple = false;
        } else {
            $scope.esNormal = false;
            $scope.esMultiple = true;
        }
        /** Modificacion INDRA FJQP -- Emisión Múltiple ---*/

        $scope.policy = $stateParams.policy;
        $scope.subsection = $stateParams.subsection;
        $scope.endorsement = $stateParams.endorsement;
        $scope.printHeader = '';
        $scope.printHeaderS = [];

        $scope.documents = {};
        $scope.documentsS = {
            Documentos: []
        };
        $scope.PrintPolicyModel = {
            Policy: $scope.policy,
            Subsection: $scope.subsection,
            Endorsement: $scope.endorsement
        };
        $scope.folletos = {
            FolletoMarsh: {
                Key: "Folleto Marsh",
                Value: "",
                Show: true
            },
            FolletoAseguradora: {
                Key: "Folleto Marsh",
                Value: "",
                Show: true
            },
            FolletoZurich: {
                Key: "Folleto Zurich",
                Value: "",
                Show: false
            },
            FolletoEncontrack: {
                Key: "Folleto Encontrack",
                Value: "",
                Show: false
            }
        }

        $scope.PolizasFolletos = {
            Polizas: []
        };

        $scope.folletosM = {
            Doc: [{
                FolletoMarsh: {},
                FolletoAseguradora: {},
                FolletoZurich: {},
                FolletoEncontrack: {},
                llave: {}
            }]
        }
        /** Modificacion INDRA FJQP -- Emisión Múltiple ---*/
        if ($scope.esNormal) {
            $scope.EsMultiple = false;
            $scope.esNormal = true;
            loadPrintData();
            cargarCotizacion();
            $scope.EsNormal = true;
        }
        else {
            $scope.EsMultiple = true;
            var Datos = {
                idNoCotizacion: $stateParams.idSolicitud,
                idNoConsec: 0,
                sNoSerie: "",
                sNoMotor: "",
                sPlacas: "",
                sContrato: "",
                iEstatusReg: 2,
                sDescEstatus: "",
                sCondunctor: "",
                sSolicitud: "",
                sQLTS: "",
                sCotizacion: "",
                sPolizaQLT: "",
                iInciso: 0,
                iEndoso: 0,
                sJSON: "",
                printHeader: ""
            };
            var informacionD = JSON.parse(angular.toJson(Datos));
            requestService.post('emitir', // Modulo
           'Emisionmultiple', // Controlador
           'getrecordpoliza', // Accion
           informacionD, // Parametros
           true, // Bloquear Interfaz/Vista
               true, // Mostrar errores
           true, // es "SingleResponse"
           function successFunction(response) {
               Datos = JSON.parse(angular.toJson(response));
               $scope.polizasEmitidas = [];
               $scope.polizasEmitidas = Datos;
               $scope.PolizasFolletos.Polizas = $scope.polizasEmitidas

               var Vehiculo = $scope.polizasEmitidas
               var a;
               a = 0;
               var vectorAux = new Array();
               for (var i = 0; i < Vehiculo.length; i++) {
                   if (Vehiculo[i].idNoCotizacion != 0) {
                       $scope.PrintPolicyModelS = {
                           Policy: Vehiculo[i].sPolizaQLT,
                           Subsection: Vehiculo[i].iInciso,
                           Endorsement: Vehiculo[i].iEndoso
                       };
                       $scope.folletosM.Doc[i] = {
                           FolletoMarsh: {
                               Key: "Folleto Marsh",
                               Value: "",
                               Show: true
                           },
                           FolletoAseguradora: {
                               Key: "Folleto Marsh",
                               Value: "",
                               Show: true
                           },
                           FolletoZurich: {
                               Key: "Folleto Zurich",
                               Value: "",
                               Show: false
                           },
                           FolletoEncontrack: {
                               Key: "Folleto Encontrack",
                               Value: "",
                               Show: false
                           },
                           llave: {}
                       };


                       var apiUrl = 'http://qa.automarshprueba.com.mx/AM45RESTService/api/PrintPolicy';
                       // var apiUrl = 'http://localhost/AM45RESTService/api/PrintPolicy';

                       $http({
                           url: apiUrl,
                           method: 'POST',
                           data: JSON.stringify($scope.PrintPolicyModelS),
                           headers: {
                               Authorization: "Bearer " + JSON.parse($base64.decode($sessionStorage.tkn)),
                               'Content-Type': 'application/json'
                           }
                       }).then(function successCallback(response) {
                           //console.log(response.data.Documents);
                           $scope.printHeaderS = '¡Póliza emitida correctamente!';
                           $scope.documentsS.Documentos[$scope.documentsS.Documentos.length] = response.data.Documents;
                           Datos[$scope.documentsS.Documentos.length - 1].printHeader = '¡Póliza emitida correctamente!';
                           $scope.polizasEmitidas[$scope.documentsS.Documentos.length - 1].printHeader = Datos[$scope.documentsS.Documentos.length - 1].printHeader;
                           $scope.startDate = response.data.StartDate;
                           cargarCotizacionM($scope.polizasEmitidas[$scope.documentsS.Documentos.length - 1].sSolicitud, $scope.polizasEmitidas[$scope.documentsS.Documentos.length - 1].sPolizaQLT, $scope.documentsS.Documentos.length - 1);
                           return true;
                       }, function errorCallback() {
                           $scope.printHeaderS = 'Ocurrio un error al imprimir la póliza.';
                           Datos[$scope.documentsS.Documentos.length - 1].printHeader = '¡Póliza emitida correctamente!';
                           $scope.polizasEmitidas[i].printHeader = Datos[$scope.documentsS.Documentos.length - 1].printHeader;
                           $rootScope.toggleModalErrors('Ocurrio un error al imprimir la póliza.');
                       });
                   } // fin del if
               } // fin de for
           },
           function errorFunction(response) {
               alert('failed' + error);
           },
           function badRequestFunction() { });
            // FJQP SUMARY
            CargaSumary();
        }


        function loadPrintData() {
            var apiUrl = 'http://qa.automarshprueba.com.mx/AM45RESTService/api/PrintPolicy';
            //var apiUrl = 'http://localhost/AM45RESTService/api/PrintPolicy';

            $http({
                url: apiUrl,
                method: 'POST',
                data: JSON.stringify($scope.PrintPolicyModel),
                headers: {
                    Authorization: "Bearer " + JSON.parse($base64.decode($sessionStorage.tkn)),
                    'Content-Type': 'application/json'
                }
            }).then(function successCallback(response) {
                //console.log(response.data.Documents);
                $scope.printHeader = '¡Póliza emitida correctamente!';
                $scope.documents = response.data.Documents;
                $scope.startDate = response.data.StartDate;
                //console.log($scope.documents);
                return true;
            }, function errorCallback(response) {
                //alert(response.data.error);
                console.log(JSON.stringify(response.data));
                //$rootScope.showModalErrors = true;
                //$("#msgModalErrors").text('Ocurrio un error al imprimir la póliza.');
                $scope.printHeader = 'Ocurrio un error al imprimir la póliza.';
                $rootScope.toggleModalErrors('Ocurrio un error al imprimir la póliza.');
            });

        }

        /** Modificacion INDRA FJQP -- Emisión Múltiple ---*/
        function cargarCotizacionM(Solicitud, Poliza, ConReg) {
            if (Solicitud != undefined || Solicitud !== '') {
                var comparadorModel = {
                    datosCotizacionModel: {
                        SolicitudId: Solicitud,
                        FormaPagoId: 0
                    }
                };
                requestService.post('comparador',
                    'comparador',
                    'cargaCotizacion',
                    comparadorModel,
                    true,
                    true,
                    true,
                    function successFunction(response) {
                        $scope.$parent.Informacion = response;
                        cargarFolletosM(Poliza, ConReg);
                        cargaCondicionesGrales();
                    },
                    function errorFunction(response) {
                        console.log("Hubo un error" + response);
                    },
                    function badRequestFunction() { });
            }
        };
        function cargarCotizacion() {
            if ($stateParams.idSolicitud != undefined || $stateParams.idSolicitud !== '') {
                var comparadorModel = {
                    datosCotizacionModel: {
                        SolicitudId: $stateParams.idSolicitud,
                        FormaPagoId: 0
                    }
                };
                requestService.post('comparador',
                    'comparador',
                    'cargaCotizacion',
                    comparadorModel,
                    true,
                    true,
                    true,
                    function successFunction(response) {
                        $scope.$parent.Informacion = response;
                        cargarFolletos();
                        cargaCondicionesGrales();
                    },
                    function errorFunction(response) {
                        console.log("Hubo un error" + response);
                    },
                    function badRequestFunction() { });
            }
        };
        $scope.descargarArchivo = function (downloadPath) {
            window.open(encodeURI(mappingService.services['imprimir']['folletos']['descargarArchivo'] +
                "?rutaArchivo=" +
                downloadPath,
                '_blank',
                ''));
        }

        /** Modificacion INDRA FJQP -- Emisión Múltiple ---*/
        function cargarFolletosM(Poliza, ConReg) {
            if ($stateParams.idSolicitud != undefined || $stateParams.idSolicitud !== '') {
                var solicitud = { Poliza: Poliza }
                requestService.post('imprimir',
                    'folletos',
                    'consultaFolletos',
                        solicitud,
                    true,
                    true,
                    true,
                    function successFunction(response) {
                        $scope.folletosM.Doc[ConReg].FolletoMarsh.Key = "Folleto Marsh"
                        $scope.folletosM.Doc[ConReg].FolletoMarsh.Value = response.FolletoMarsh;
                        $scope.folletosM.Doc[ConReg].FolletoMarsh.llave = Poliza;
                        $scope.folletosM.Doc[ConReg].FolletoAseguradora.Key = "Folleto Aseguradora"
                        $scope.folletosM.Doc[ConReg].FolletoAseguradora.Value = response.FolletoAseguradora;
                        $scope.folletosM.Doc[ConReg].FolletoAseguradora.llave = Poliza
                        $scope.folletosM.Doc[ConReg].FolletoZurich.Key = "Folleto Zurich"
                        $scope.folletosM.Doc[ConReg].FolletoZurich.Value = response.Zurich;
                        $scope.folletosM.Doc[ConReg].FolletoZurich.llave = Poliza
                        $scope.folletosM.Doc[ConReg].FolletoZurich.Show = response.MuestraZurich;


                        if ($scope.confirmedEncontrack) {
                            $scope.folletosM.Doc[ConReg].FolletoEncontrack.Key = "Folleto Encontrack"
                            $scope.folletosM.Doc[ConReg].FolletoEncontrack.Value = response.QLTSEncontrack;
                            $scope.folletosM.Doc[ConReg].FolletoEncontrack.llave = Poliza;
                            $scope.folletosM.Doc[ConReg].FolletoEncontrack.Show = true;
                        } else {
                            $scope.folletosM.Doc[ConReg].FolletoEncontrack.Key = "Folleto Encontrack"
                            $scope.folletosM.Doc[ConReg].FolletoEncontrack.Value = response.QLTSEncontrack;
                            $scope.folletosM.Doc[ConReg].FolletoEncontrack.llave = Poliza;
                            $scope.folletosM.Doc[ConReg].FolletoEncontrack.Show = false;
                        }

                        cargarAsegPaquete();
                    },
                    function errorFunction(response) {
                        console.log("Hubo un error" + response);
                    },
                    function badRequestFunction() { });
            }
        };

        /** Modificacion INDRA FJQP -- Emisión Múltiple ---*/
        function cargarFolletos() {
            if ($stateParams.idSolicitud != undefined || $stateParams.idSolicitud !== '') {
                var solicitud = { Poliza: $stateParams.policy }
                requestService.post('imprimir',
                    'folletos',
                    'consultaFolletos',
                        solicitud,
                    true,
                    true,
                    true,
                    function successFunction(response) {
                        $scope.folletos.FolletoMarsh.Value = response.FolletoMarsh;
                        $scope.folletos.FolletoAseguradora.Value = response.FolletoAseguradora;
                        $scope.folletos.FolletoZurich.Value = response.FolletoZurich;
                        $scope.folletos.FolletoZurich.Show = response.MuestraZurich;

                        if ($scope.confirmedEncontrack) {
                            $scope.folletos.FolletoEncontrack.Key = "Folleto Encontrack"
                            $scope.folletos.FolletoEncontrack.Value = response.QLTSEncontrack;
                            $scope.folletos.FolletoEncontrack.Show = true;
                        } else {
                            $scope.folletos.FolletoEncontrack.Key = "Folleto Encontrack"
                            $scope.folletos.FolletoEncontrack.Value = response.QLTSEncontrack;
                            $scope.folletos.FolletoEncontrack.Show = false;
                        }

                        cargarAsegPaquete();
                    },
                    function errorFunction(response) {
                        console.log("Hubo un error" + response);
                    },
                    function badRequestFunction() { });
            }
        };

        function cargarAsegPaquete() {
            if ($stateParams.idSolicitud !== undefined ||
                $stateParams.idSolicitud !== '' ||
                $stateParams.numero != undefined ||
                $stateParams.numero !== '') {
                var asegModel = {
                    Aseguradora: "",
                    Paquete: "",
                    Numero: $stateParams.numero,
                    SolicitudId: $stateParams.idSolicitud
                }
                requestService.post('imprimir',
                    'folletos',
                    'consultarAsegPaquete',
                    asegModel,
                    true,
                    true,
                    true,
                    function successFunction(response) {
                        $scope.folletos.FolletoAseguradora.Key = response.Aseguradora;
                        if ($scope.$parent.Informacion.Cliente.Producto.Flexible)
                            $scope.$parent.paquete = $scope.$parent.PAQUETEFLEX;
                        else
                            $scope.$parent.paquete = response.Paquete;
                    },
                    function errorFunction(response) {
                        console.log("Hubo un error" + response);
                    },
                    function badRequestFunction() { });
            }
        };

        function cargaCondicionesGrales() {
            if ($stateParams.idSolicitud != undefined || $stateParams.idSolicitud != '') {
                var solicitud = { VehiculoId: $scope.$parent.Informacion.Vehiculo.TipoUnidad.ValorId }

                requestService.post('imprimir',
                    'folletos',
                    'ConsultaCondicionesGenerales',
                    solicitud,
                    true,
                    true,
                    true,
                    function successFunction(response) {
                        if (response.MuestraCondicionesGralesAutos) {
                            $scope.condicionesGrales = {
                                "Condiciones Generales Qualitas": response.CondicionesGralesQlt,
                                "Condiciones Generales Autos": response.CondicionesGralesAutos
                            }
                        } else {
                            $scope.condicionesGrales = {
                                "Condiciones Generales Qualitas": response.CondicionesGralesQlt,
                                "Condiciones Generales Camiones": response.CondicionesGralesCamiones
                            }
                        }

                    },
                    function errorFunction() { },
                    function badRequestFunction() { });
            }
        };

        function CargaInicial() {
            var comparadorModel = {
                datosCotizacionModel: {
                    SolicitudId: parseInt($stateParams.idSolicitud),
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
                                    SolicitudId: parseInt($stateParams.idSolicitud),
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
                                SolicitudId: parseInt($stateParams.idSolicitud),
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
                    console.log($scope.datosComparador);
                    $scope.CotId = $scope.datosComparador.AseguradorasProducto[0].ListaPaquetes[0].CotizacionId;

                    cargarCoberturasDocumentos($scope.CotId);
                    return;
                },
                function errorFunction(response) {
                    console.log("Hubo un error" + response);
                },
                function badRequestFunction() { });
        };

        function cargarCoberturasDocumentos(cotId) {
            console.log("entro a carga de documentos");

            //var cotId = $scope.CotId;
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

        /* INDRA FJQP Sumary */
        $scope.AceptaSumary = function () {
            $scope.modalSumary = !$scope.modalSumary; // Ocultamos la modal "Warning"
        }

        function CargaSumary() {
            var Sumary = {
                idNoCotizacion: $stateParams.idSolicitud,
                CotGen: 0,
                PolEmi: 0,
                PolNoEmi: 0
            };
            var informacionSum = JSON.parse(angular.toJson(Sumary));
            requestService.post('emitir',
                'Emisionmultiple',
                'getrecordsumary',
                informacionSum,
                true,
                true,
                true,
                function successFunction(response) {
                    Sumary = JSON.parse(angular.toJson(response));
                    $scope.Sumario = [];
                    $scope.Sumario = Sumary;
                    $scope.Sumarios = Sumary;
                    $scope.modalSumary = true;
                    return;
                },
                function errorFunction(response) {
                    console.log("Hubo un error" + response);
                },
                function badRequestFunction() { });
        };




    }
    ]);

});

