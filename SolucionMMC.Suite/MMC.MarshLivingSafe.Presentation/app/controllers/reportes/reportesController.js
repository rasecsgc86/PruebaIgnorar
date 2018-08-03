define(['modules/app', 'services/requestService', 'services/mappingService'],
    function(app) {
        /**
         * DEFINIMOS Y REGISTRAMOS EL CONTROLLER homeController.
         * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
         * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
         * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
         * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
         *
         * @var tittle - TITULO DE LA PAGINA  
         */
        app.controller('ReportesController',
        [
            '$scope', '$rootScope', '$location', 'requestService', '$localStorage', 'mappingService',
            function($scope, $rootScope, $location, requestService, $localStorage, mappingService) {
                $rootScope.tittle = "Reportes tickets";
                $scope.abiertos = 0;
                $scope.Terminados = 0;
                $scope.espera = 0;
                $scope.total = 0;
                $scope.estatusTicket = {};
                $scope.ticketsReporte = [];
                $scope.estatusTickets = [];
                $scope.mensaje = "";
                var reporteModel = {
                
                };

                

                loadEstatusTickets();

                function loadEstatusTickets() {
                    requestService.post('tickets',
                        'reportes',
                        'consultarEstatusTickets',
                        null, //en caso de que no lleve parametros puse null
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.estatusTickets = response;
                            init();

                        },
                        function errorFunction() {},
                        function badRequestFunction() {});
                };

                //Set dafault value 'Todos'
                function init() {
                    var target = {
                        "IdEstatusTicket": "0",
                        "Descripcion": "",
                        "CveEstatus": "0"
                    };
                    for (var i = 0; i < $scope.estatusTickets.length; ++i) {
                        if ($scope.estatusTickets[i].Descripcion === target.Descripcion) {
                            $scope.estatusTickets[i] = target;
                            $scope.estatusTicket = $scope.estatusTickets[i];
                        }
                    }
                }

                $scope.valorSinDecimal = function(valor) {
                    var decimal = valor.toString();
                    decimal = decimal.split(".");
                    return decimal[0];
                }

                /**
                 * Nombre: contarEstatus.
                 * Objetivo : Contar los estatus
                 * 1	Registrado     ---- Abiertos
                 * 2	Proceso        ---- Abiertos
                 * 3	En tr�mite     ---- Abiertos
                 * 4	Incompleto     ---- En espera
                 * 5	Documentaci�n  ---- En espera
                 * 6	Cerrado        ---- Terminados
                 * 7	Cancelado      ---- Terminados
                 * @returns {} 
                 */
                function contarEstatus(ticketsReporte) {
                    for (var i = 0; i < ticketsReporte.length; ++i) {
                        if (ticketsReporte[i].CveEstatus === 1 ||
                            ticketsReporte[i].CveEstatus === 2 ||
                            ticketsReporte[i].CveEstatus === 3) {
                            $scope.abiertos++;
                        } else if (ticketsReporte[i].CveEstatus === 4 ||
                            ticketsReporte[i].CveEstatus === 5) {
                            $scope.espera++;
                        } else if (ticketsReporte[i].CveEstatus === 6 ||
                            ticketsReporte[i].CveEstatus === 7) {
                            $scope.Terminados++;
                        }
                    }
                }

                $scope.buscarTickets = function() {
                    $scope.camposValidar = {
                        FechaInicio: false,
                        FechaFin: false,
                        DescripcionEstatus: false
                    }

                    if ($scope.estatusTicket.Descripcion === "Todos") {
                        reporteModel = {
                            DescripcionEstatus: '',
                            FechaInicio: $scope.FechaInicio,
                            FechaFin: $scope.FechaFin
                        }
                    } else {
                        reporteModel = {
                            DescripcionEstatus: $scope.estatusTicket.Descripcion,
                            FechaInicio: $scope.FechaInicio,
                            FechaFin: $scope.FechaFin
                        };
                    }
                    if ($scope.FechaInicio != undefined &&
                        $scope.FechaFin != undefined &&
                        $scope.estatusTicket.Descripcion != undefined) {
                        requestService.post('tickets',
                            'reportes',
                            'consultarTicketsReporte',
                            reporteModel,
                            true,
                            true,
                            true,
                            function successFunction(response) {
                                $scope.abiertos = 0;
                                $scope.Terminados = 0;
                                $scope.espera = 0;
                                $scope.total = 0;
                                $scope.ticketsReporte = response;
                                if ($scope.ticketsReporte.length === 0) {
                                    $scope.abiertos = 0;
                                    $scope.Terminados = 0;
                                    $scope.espera = 0;
                                    $scope.total = 0;
                                    $scope.toggleModal();
                                    $scope.mensaje = "No hay tickets con el filtro seleccionado.";
                                } else {
                                    if ($scope.ticketsReporte.length > 50) {
                                        $scope.toggleModal();
                                        $scope
                                            .mensaje =
                                            "La b\u00FAsqueda sobrepasa el m\u00E1ximo de registros permitidos (50), si desea visualizar todos favor de realizar la exportaci\u00F3n.";
                                    }
                                    contarEstatus($scope.ticketsReporte);
                                }
                            },
                            function errorFunction() {},
                            function badRequestFunction() {});
                    } else {
                        $scope.camposValidar.FechaInicio = $scope.FechaInicio == undefined;
                        $scope.camposValidar.FechaFin = $scope.FechaFin == undefined;
                        $scope.camposValidar.DescripcionEstatus = $scope.estatusTicket.Descripcion == undefined;
                        $scope.toggleModalCampos();
                    }
                }
                $scope.showModal = false;
                $scope.toggleModal = function() {
                    $scope.showModal = !$scope.showModal;
                };
                $scope.toggleModalCampos = function() {
                    $scope.showModalCampos = !$scope.showModalCampos;
                };
                //#######################################################
                //#################   Calendario   ######################
                //#######################################################
                $scope.dateOptions = {
                    dateDisabled: disabled,
                    formatYear: 'yy'
                };
                $scope.toggleMin = function() {

                };
                $scope.toggleMin();

                // Disable weekend selection
                function disabled(data) {
                    var date = data.date,
                        mode = data.mode;
                    return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
                }

                $scope.open1 = function() {
                    $scope.popup1.opened = true;
                };

                $scope.open2 = function() {
                    $scope.popup2.opened = true;
                };

                $scope.popup1 = {
                    opened: false
                };

                $scope.popup2 = {
                    opened: false
                };
                /*************************************************************************
                 *Función para descargar el excel del reporte
                 */
                $scope.exportarExcel = function() {
                    reporteModel = {
                        DescripcionEstatus: '',
                        FechaInicio: '',
                        FechaFin: ''
                    }
                    if ($scope.estatusTicket.Descripcion === "Todos") {
                        reporteModel = {
                            DescripcionEstatus: '',
                            FechaInicio: $scope.FechaInicio,
                            FechaFin: $scope.FechaFin
                        }
                    } else {
                        reporteModel = {
                            DescripcionEstatus: $scope.estatusTicket.Descripcion,
                            FechaInicio: $scope.FechaInicio,
                            FechaFin: $scope.FechaFin
                        };
                    }

                    reporteModel.DescripcionEstatus = (reporteModel.DescripcionEstatus == undefined ||
                            reporteModel.DescripcionEstatus == null)
                        ? null
                        : $scope.estatusTicket.Descripcion;
                    reporteModel.FechaInicio = (reporteModel.FechaInicio == undefined ||
                            reporteModel.FechaInicio == null)
                        ? null
                        : $scope.FechaInicio.getFullYear() +
                        "-" +
                        ($scope.FechaInicio.getMonth() + 1) +
                        "-" +
                        $scope.FechaInicio.getDate();
                    reporteModel.FechaFin = (reporteModel.FechaFin == undefined || reporteModel.FechaFin == null)
                        ? null
                        : $scope.FechaFin.getFullYear() +
                        "-" +
                        ($scope.FechaFin.getMonth() + 1) +
                        "-" +
                        $scope.FechaFin.getDate();;
                    window.open(mappingService.services['tickets']['reportes']['consultarTicketsReporteExcel'] +
                        "?descripcionEstatus=" +
                        reporteModel.DescripcionEstatus +
                        "&fechaInicio=" +
                        reporteModel.FechaInicio +
                        "&fechaFin=" +
                        reporteModel.FechaFin,
                        '_blank',
                        '');
                }
            }
        ]);

    });