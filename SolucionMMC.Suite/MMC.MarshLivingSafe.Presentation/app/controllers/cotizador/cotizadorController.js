/// <reference path="../reportes/reportesController.js" />
define(['modules/app', 'services/requestService', 'services/mappingService'],
    function (app) {
        /**
         * DEFINIMOS Y REGISTRAMOS EL CONTROLLER homeController.
         * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
         * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
         * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
         * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
         *
         * @var tittle - TITULO DE LA PAGINA  
         * 
         * Modoficacion INDRA FJQP --- Encontrack, Emisi�n Multiple
         * 
         */
        app.controller('CotizadorController',
        [
            '$scope', '$stateParams', '$state', '$rootScope', '$location', 'requestService', '$base64',
            'mappingService', '$localStorage', 'Notification', '$sessionStorage',
            function ($scope,
                $stateParams,
                $state,
                $rootScope,
                $location,
                requestService,
                $base64,
                mappingService,
                $localStorage,
                Notification,
                $sessionStorage) {
                /******************************************************************************* 
                 ****       Instrucciones de inicio de nuestro template o pagina html       ****
                 *******************************************************************************/
                $rootScope.tittle = 'Cotizador'; // Titulo del template
                $rootScope.bMenu = true; // Muestra Menu
                $scope.$parent.rama = 'cot';
                $scope.$parent.bramaCot = true;
                $scope.$parent.bramaComp = false;
                $scope.$parent.bramaEmit = false;
                $scope.$parent.bramaPag = false;
                $scope.$parent.bramaImp = false;
                $scope.optPersonaFisica = true; // Muestra campos relacionados a "Personas Fisicas"
                $scope.optPersonaMoral = false; // Muestra campos relacionados a "Personas Morales"
                $scope.modalBuscar = false; // Inicializa en false la modal donde obtiene informacion consultada en BD
                $scope.modalWarning = false; // Inicializa en false la modal "Warning"
                $scope.modalRequeridos = false; // Inicializa en false la modal de "Campos Requeridos"
                $scope.modalSinResultados = false; // Inicializa en false la modal "Sin Resultados"
                $scope.modalRequeridosList = false; // Inicializa en false la modal "Lista de Requeridos"
                $scope.$parent.paquete = '';
                $scope.$parent.CabeceraCotizacionHeredada = ''; /* Modificacion INDRA FJQP --- Encontrack, Emisi�n Multiple*/
                $scope.$parent.CotizarModelHeredada = ''; /* Modificacion INDRA FJQP --- Encontrack, Emisi�n Multiple*/
                var clienteSel = false;
                var fechaRetroactiva = 0;
                var fechaRetroactividad = '';
                var solicitud;
                var bndAntiguedad = false;
                var bndTipoUnidad = false;
                var bndCategoria = false;
                var bndProducto = false;
                var bndServicio = false;
                var bndUdi = false;
                var bndPlazoSel = false; //Bandera para validar si hace el remplace de la fecha
                var valorFacturaValidada = 0; // Variable espejo que obtiene el valor factura

                $scope.requeridosPanelList = [];
                $scope.requeridosClienteList = []; // Inicializa el valor del cuerpo del mensaje   
                $scope.requeridosVehiculoList = [];
                $scope.requeridosCotizacionList = [];
                $scope.$parent.paquete = $scope.PAQUETEFLEX;

                $scope.confirmedEncontrack = false; /* Modificacion INDRA FJQP --- Encontrack, Emisi�n Multiple*/
                $scope.DisclaimerEcontrak = false;  /* INDRA FJQP Proceso de Encontrack */
                $scope.modalDisclaimer = false;     /* INDRA FJQP Proceso de Encontrack */

                $scope.DisclaimerEE = false;        /* Modificacion INDRA FJQP --- Disclaimer EE y Adaptaciones*/
                $scope.DisclaimerAdapta = false;    /* Modificacion INDRA FJQP --- Disclaimer EE y Adaptaciones*/
                $scope.DisclaimerEETexto = false;        /* Modificacion INDRA FJQP --- Disclaimer EE y Adaptaciones*/
                $scope.DisclaimerAdaptaTexto = false;    /* Modificacion INDRA FJQP --- Disclaimer EE y Adaptaciones*/

                $scope.$parent.Informacion = {
                    Cliente: {
                        Cotizante: {
                            Nombre: '',
                            ApellidoP: '',
                            ApellidoM: '',
                            RazonSocial: '',
                            TipoPersona: 'Fisica',
                            Telefono: '',
                            CorreoElectronico: ''
                        },
                        Cliente: '',
                        ProductoFlex: '',
                        Producto: '',
                        TipoArrendamiento: '',
                        Agencia: ''
                    },
                    Vehiculo: {
                        TipoUnidad: '',
                        Antiguedad: '',
                        ClaveMarsh: '',
                        Servicio: '',
                        Valor: 0,
                        Modelo: '',
                        SubMarca: '',
                        Armadora: '',
                        Carga: '',
                        Pasajero: '',
                        Version: '',
                        Remolque: '',
                        LoJack: ''
                    },
                    Cotizacion: {
                        Plazo: '',
                        Paquete: '',
                        Udi: '',
                        InicioVigencia: '',
                        FinVigencia: '',
                        Udis: '',
                        CP: {
                            CodigoPostal: '',
                            Colonia: '',
                            ColoniaId: '',
                            Pais: '',
                            Estado: '',
                            EstadoId: '',
                            Delegacion: '',
                            DelegacionId: ''
                        },
                        Estado: ''
                    },
                    Panel: {}
                };
                $scope.$parent.Panel = {};
                $scope.$parent.hayDatosPanel = false;
                $scope.solicitud = {
                    clienteModel: {
                        Cliente: '',
                        ClienteId: ''
                    }
                };
                $scope.isCombo = 0;
                $scope.IsSessionAg = false;
                cargarClientes();
                limpiar(); // Llama funcion para limpiar objetos de la pantalla cotizacion
                cargaElementos(); // Llama funcion para llenado de combos [Arrendamiento, LoJack, remolques]
                inicializaInformacionGral();

                if ($scope.$parent.idSolicitudR !== '') {
                    recargaInformacionCotizacion();
                }

                // Llama funcion para inicializar JSON usado para validar campos requeridos            
                $scope.minlength = 5;
                $scope.itemValueAutocomplete = "Cliente";
                $scope.itemLabelAutocomplete = "Cliente";
                /************************************************************************************************************
                 **** Funcion para cargar combo Clientes consultando la vista de acuerdo al usuario logeado seleccionado ****
                 ************************************************************************************************************/
                $scope.urlautocomplete = $base64.encode(JSON.stringify([
                    'cotizador',
                    'cotizacion',
                    'consultarCliente'
                ]));
                /************************************************************************************ 
                 ****        Funcion para el calendario usando las directivas de angular         ****
                 ************************************************************************************/
                $scope.today = function () {
                    $scope.dt = null;
                    $scope.dt2 = '';
                };

                $scope.today();

                $scope.clear = function () {
                    $scope.dt = null;
                };

                $scope.inlineOptions = {
                    customClass: getDayClass,
                    minDate: new Date(),
                    showWeeks: true
                };

                $scope.dateOptions = {
                    dateDisabled: disabled,
                    formatYear: 'yy',
                    maxDate: new Date(2020, 5, 22),
                    minDate: new Date(),
                    startingDay: 1
                };

                function disabled(data) {
                    var date = data.date,
                        mode = data.mode;
                    return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
                }

                $scope.toggleMin = function () {
                    $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
                    $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
                };

                $scope.toggleMin();

                $scope.open1 = function () {
                    $scope.popup1.opened = true;
                };

                $scope.setDate = function (year, month, day) {
                    $scope.dt = new Date(year, month, day);
                };

                $scope.formats = ['dd-MMMM-yyyy', 'dd/MM/yyyy', 'MM-dd-yyyy', 'dd.MM.yyyy', 'shortDate'];
                $scope.format = $scope.formats[1];
                $scope.altInputFormats = ['M!/d!/yyyy'];

                $scope.popup1 = {
                    opened: false
                };

                var tomorrow = new Date();
                tomorrow.setDate(tomorrow.getDate() + 1);
                var afterTomorrow = new Date();
                afterTomorrow.setDate(tomorrow.getDate() + 1);
                $scope.events = [
                    {
                        date: tomorrow,
                        status: 'full'
                    },
                    {
                        date: afterTomorrow,
                        status: 'partially'
                    }
                ];

                function getDayClass(data) {
                    var date = data.date,
                        mode = data.mode;
                    if (mode === 'day') {
                        var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                        for (var i = 0; i < $scope.events.length; i++) {
                            var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);
                            if (dayToCheck === currentDay) {
                                return $scope.events[i].status;
                            }
                        }
                    }

                    return '';
                }

                /******************************************************************************************************** 
                 ****                          Funcion para la seleccion de Tipo de Persona                          ****
                 ********************************************************************************************************/
                $scope.showPersonaFisica = function () {
                    $scope.optPersonaFisica = true; // Muestra campos relacionados a "Personas Fisicas"
                    $scope.optPersonaMoral = false; // Oculta campos relacionados a "Personas Morales"
                    $scope.$parent.Informacion.Cliente.Cotizante.Nombre = '';
                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP = '';
                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM = '';
                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono = '';
                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico = '';
                }

                $scope.showPersonaMoral = function () {
                    $scope.optPersonaFisica = false; // Oculta campos relacionados a "Personas Fisicas"
                    $scope.optPersonaMoral = true; // Muestra campos relacionados a "Personas Morales"
                    $scope.$parent.Informacion.Cliente.Cotizante.RazonSocial = '';
                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono = '';
                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico = '';
                }

                /********************************************************************************************************
                 ****                              Funcion para la seleccion de Cliente                              ****
                 ********************************************************************************************************/
                $scope.clienteSel = function (combo) {
                    if (combo !== 1)
                        $scope.$parent.Informacion.Cliente.Cliente = $scope.targetModelo;
                    $scope.ProductoFlex = true;
                    $scope.productoFlex = true;
                    $scope.productos = [];
                    $scope.productoFlexSel();
                    cargaTipoArrendamiento();

                    if ($scope.$parent.Informacion.Cliente.Producto !== '') {
                        clienteSel = true;
                        limpiar();
                    }

                    cargarDatosImagen($scope.$parent.Informacion.Cliente.Cliente.ClienteId)
                };
                function cargarDatosImagen(idClient) {

                    requestModel = {
                        IdCliente: idClient,
                        IdSolictud: 0

                    }


                    requestService.post('imagenCliente', // Modulo
                           'imagenCliente', // Controlador
                           'obtenerImagenCliente', // Accion
                           requestModel, // Parametros
                           true, // Bloquear Interfaz/Vista
                           true, // Mostrar errores
                           true, // es "SingleResponse"
                           function successFunction(response) {
                               var x = document.getElementById("ImagenCliente");
                               x.setAttribute('src', "data:image/png;base64," + response.imagen64);
                           },
                           function errorFunction() { },
                           function badRequestFunction() { });




                }

                $scope.productoFlexSel = function () {
                    $scope.$parent.Informacion.Cliente.ProductoFlex = ($scope.ProductoFlex === true) ? 1 : 0;
                    $scope.$parent.hayDatosPanel = false;
                    $scope.$parent.Informacion.Cliente.Agencia = '';

                    clienteSel = false;
                    limpiar();
                    cargaProductos();
                }

                /********************************************************************************************************
                 ****                             Funcion para la seleccion de Producto                              ****
                 ********************************************************************************************************/
                $scope.productoSel = function () {
                    $scope.btnCalendario = true;
                    $scope.deshabilitaUdi = $scope.bndCarga;
                    $scope.plazo = true;
                    clienteSel = false;
                    bndProducto = true;
                    bndUdi = false;

                    $scope.Udis = [];
                    $scope.Plazo = [];

                    $scope.$parent.hayDatosPanel = false;

                    if ($scope.$parent.Informacion.Cliente.Producto !== undefined &&
                        $scope.$parent.Informacion.Cliente.Producto !== null &&
                        $scope.$parent.Informacion.Cliente.Producto !== '') {
                        limpiar();
                        $scope.showHideEstados = !$scope.$parent.Informacion.Cliente.Producto.Cp;
                        $scope.showHideBuscar = true;
                        cargaEstados();
                        verificarArrendamiento();

                        if ($scope.$parent.Informacion.Cliente.ProductoFlex === 0) {
                            verificarLoJack();
                        }

                        verificaAgencias();
                        cargaFechaSistema();
                    }
                };

                /********************************************************************************************************
                 ****                          Funcion para la seleccion de Tipo de Unidad                           ****
                 ********************************************************************************************************/
                $scope.tipoUnidadSel = function () {
                    $scope.Udis = [];
                    $scope.Plazo = [];

                    $scope.plazo = true;
                    $scope.bndCarga = false;
                    $scope.btnCalendario = true;
                    $scope.bndPasajeros = false;
                    $scope.$parent.bndUDI = false;
                    $scope.$parent.bndFactura = false;
                    $scope.deshabilitaUdi = $scope.bndCarga;
                    $scope.$parent.hayDatosPanel = false;
                    $scope.$parent.Informacion.Vehiculo.ShowCargas = false;

                    $scope.dt2 = '';
                    $scope.$parent.Informacion.Cotizacion.FinVigencia = '';

                    if ($scope.$parent.Informacion.Vehiculo.TipoUnidad !== undefined) {
                        bndTipoUnidad = true;
                        cargaAntiguedadVehiculo();
                    }
                }

                /********************************************************************************************************
                 ****                            Funcion para la seleccion de Antiguedad                             ****
                 ********************************************************************************************************/
                $scope.antiguedadSel = function () {
                    $scope.Udis = [];
                    $scope.Plazo = [];

                    $scope.plazo = true;
                    $scope.bndCarga = false;
                    $scope.bndPasajeros = false;
                    $scope.btnCalendario = true;
                    $scope.$parent.bndUDI = false;
                    $scope.$parent.bndFactura = false;
                    $scope.$parent.hayDatosPanel = false;
                    $scope.deshabilitaUdi = $scope.bndCarga;
                    $scope.$parent.Informacion.Vehiculo.ShowCargas = false;

                    $scope.dt2 = '';
                    $scope.$parent.Informacion.Cotizacion.FinVigencia = '';

                    if ($scope.$parent.Informacion.Vehiculo.Antiguedad !== undefined) {
                        bndAntiguedad = true;
                        cargaServicio();
                    }
                }

                /********************************************************************************************************
                 ****                            Funcion para la seleccion de Servicio                               ****
                 ********************************************************************************************************/
                $scope.servicioSel = function () {
                    $scope.Udis = [];
                    $scope.Plazo = [];

                    $scope.plazo = true;
                    $scope.bndCarga = false;
                    $scope.bndPasajeros = false;
                    $scope.btnCalendario = true;
                    $scope.$parent.bndUDI = false;
                    $scope.$parent.bndFactura = false;
                    $scope.$parent.hayDatosPanel = false;
                    $scope.deshabilitaUdi = $scope.bndCarga;
                    $scope.$parent.Informacion.Vehiculo.ShowCargas = false;

                    $scope.dt2 = '';
                    $scope.$parent.Informacion.Cotizacion.FinVigencia = '';

                    if (($scope.$parent.Informacion.Vehiculo.TipoUnidad.ValorId === "755" ||
                            $scope.$parent.Informacion.Vehiculo.TipoUnidad.ValorId === "5565") &&
                        $scope.$parent.Informacion.Vehiculo.Servicio.ServicioId === "2757") {
                        cargarCargas();
                    }

                    if ($scope.$parent.Informacion.Vehiculo.Servicio !== undefined) {
                        bndServicio = true;
                        cargaModelo();
                    }

                }


                /********************************************************************************************************
                 ****                            Funcion para la seleccion de Modelo                              ****
                 ********************************************************************************************************/
                $scope.modeloSel = function () {
                    $scope.Udis = [];
                    $scope.Plazo = [];

                    $scope.deshabilitaUdi = $scope.bndCarga;
                    $scope.$parent.hayDatosPanel = false;
                    $scope.$parent.bndFactura = false;
                    $scope.$parent.bndUDI = false;
                    $scope.btnCalendario = true;
                    $scope.bndPasajeros = false;
                    $scope.plazo = true;

                    $scope.dt2 = '';
                    $scope.$parent.Informacion.Cotizacion.FinVigencia = '';

                    if ($scope.$parent.Informacion.Vehiculo.Modelo !== undefined) {
                        cargarArmadoras();
                    }
                }

                /********************************************************************************************************
                 ****                        Funcion para la seleccion de Armadora o Agencia                         ****
                 ********************************************************************************************************/
                $scope.armadoraSel = function () {
                    $scope.Udis = [];
                    $scope.Plazo = [];

                    $scope.deshabilitaUdi = $scope.bndCarga;
                    $scope.$parent.hayDatosPanel = false;
                    $scope.$parent.bndFactura = false;
                    $scope.$parent.bndUDI = false;
                    $scope.btnCalendario = true;
                    $scope.bndPasajeros = false;
                    $scope.plazo = true;

                    $scope.dt2 = '';
                    $scope.$parent.Informacion.Cotizacion.FinVigencia = '';

                    if ($scope.$parent.Informacion.Vehiculo.Armadora !== undefined) {
                        cargarSubMarcas();
                    }
                };

                /********************************************************************************************************
                 ****                            Funcion para la seleccion de Submarca                               ****
                 ********************************************************************************************************/
                $scope.submarcaSel = function () {
                    $scope.Udis = [];
                    $scope.Plazo = [];

                    $scope.$parent.Informacion.Cotizacion.Udi = null;
                    $scope.$parent.hayDatosPanel = false;
                    $scope.$parent.bndFactura = false;
                    $scope.$parent.bndUDI = false;
                    $scope.deshabilitaUdi = false;
                    $scope.btnCalendario = false;
                    $scope.bndPasajeros = false;
                    $scope.plazo = false;
                    bndCategoria = true;

                    $scope.dt2 = '';
                    $scope.$parent.Informacion.Cotizacion.FinVigencia = '';

                    if ($scope.$parent.Informacion.Vehiculo.SubMarca !== undefined) {
                        cargarVersiones();

                        if ($scope.bndCarga &&
                            $scope.$parent.Informacion.Vehiculo.Carga === null) {
                            $scope.deshabilitaUdi = true;
                        }
                    }
                };

                /********************************************************************************************************
                 ****                    Funcion que se dispara al seleccionar el Tipo de Carga                      ****
                 ********************************************************************************************************/
                $scope.cargaSel = function () {
                    $scope.$parent.hayDatosPanel = false;
                    $scope.deshabilitaUdi = false;

                    if ($scope.$parent.Informacion.Cotizacion.Udi !== null) {
                        $scope.$parent.enviarDatosDibujarPanel();
                    }
                }

                /********************************************************************************************************
                 ****                            Funcion para la seleccion de fechas                                 ****
                 ********************************************************************************************************/
                $scope.PlazoSel = function () {
                    if ($scope.$parent.Informacion.Cotizacion.Plazo != null) {
                        var meses = parseInt($scope.$parent.Informacion.Cotizacion.Plazo.Valor, 10);
                        calculaFechaVsPlazo(meses);
                        bndPlazoSel = true;
                        if ($scope.ProductoFlex) {
                            if (!$scope.$parent.bndUDI) {
                                if (bndProducto && bndTipoUnidad && bndAntiguedad && bndServicio && bndCategoria) {
                                    $scope.$parent.enviarDatosDibujarPanel();
                                }
                            }
                        }
                    }
                }

                /********************************************************************************************************
                 ****                            Funcion para la seleccion de UDI                                    ****
                 ********************************************************************************************************/
                $scope.udiSel = function () {
                    $scope.$parent.hayDatosPanel = false;

                    if ($scope.$parent.Informacion.Cotizacion.Udi !== undefined) {
                        bndUdi = true;

                        if ($scope.ProductoFlex === true) {
                            if (bndProducto && bndTipoUnidad && bndAntiguedad && bndServicio && bndCategoria) {
                                $scope.$parent.enviarDatosDibujarPanel();
                            }
                        }
                    }
                }

                /******************************************************************************************************
                 ****                            Funcion para la seleccion de fechas                               ****
                 ******************************************************************************************************/
                $scope.fechaSel = function () {
                    $scope.$parent.Informacion.Cotizacion.InicioVigencia = $scope.dt;

                    if (fechaRetroactividad !== 0) {
                        fechaRetroactiva = new Date(fechaRetroactividad[0].FechaRetro);
                        if (comparaFechas($scope.dt.toLocaleDateString(), fechaRetroactiva.toLocaleDateString()) ===
                            false) {
                            $scope.warning = 'La retroactividad solo permite seleccionar ' +
                                fechaRetroactividad[0].Valor +
                                ' dias atras';
                            $scope.modalWarning = true;
                            $scope.plazo = true;
                            $scope.dt = null;
                            $scope.dt2 = '';
                            $scope.$parent.Informacion.Cotizacion.FinVigencia = '';
                        } else {
                            $scope.plazo = false;

                            if ($scope.$parent.Informacion.Cotizacion.Plazo !== null) {
                                calculaPlazo();
                            }
                        }
                    } else {
                        fechaRetroactiva = new Date();
                        if (comparaFechas($scope.dt.toLocaleDateString(), fechaRetroactiva.toLocaleDateString()) ===
                            false) {
                            $scope.warning = 'Solo permite seleccionar fecha actual o posteriores';
                            $scope.modalWarning = true;
                            $scope.plazo = true;
                            $scope.dt = null;
                            $scope.dt2 = '';
                            $scope.$parent.Informacion.Cotizacion.FinVigencia = '';
                        } else {
                            $scope.plazo = false;

                            if ($scope.$parent.Informacion.Cotizacion.Plazo !== null) {
                                calculaPlazo();
                            }
                        }
                    }
                }

                /******************************************************************************************************** 
                 ***            Funcion para validar "Campos Requeridos" y guardar cabecera de la cotizacion         ****
                 ********************************************************************************************************/
                $rootScope.submitCot = function () {
                    validaInfoCliente();
                    validaInfoVehiculos();
                    validaDatosCotizacion();                    
                    $scope.showHidePanel = false;
                    $scope.showHidePanelList = false;
                    $scope.showHideClienteList = false;
                    $scope.showHideVehiculoList = false;
                    $scope.showHideCotizacionList = false;
                    $scope.requeridosPanelList = [];

                    if ($scope.ProductoFlex && $scope.$parent.hayDatosPanel) {
                        validaPanel();
                        if ($scope.requeridosPanelList.length !== 0) {
                            $scope.showHidePanelList = true;
                        }
                    } else {
                        if ($scope.ProductoFlex && !$scope.$parent.hayDatosPanel) {
                            $scope.showHidePanel = true;
                        }
                    }

                    if ($scope.requeridosVehiculoList.length !== 0 ||
                        $scope.requeridosClienteList.length !== 0 ||
                        $scope.requeridosCotizacionList.length !== 0 ||
                        $scope.requeridosPanelList.length !== 0) {

                        if ($scope.requeridosVehiculoList.length !== 0) {
                            $scope.showHideVehiculoList = true;
                        }

                        if ($scope.requeridosClienteList.length !== 0) {
                            $scope.showHideClienteList = true;
                        }

                        if ($scope.requeridosCotizacionList.length !== 0) {
                            $scope.showHideCotizacionList = true;
                        }

                        $scope.modalRequeridosList = true;
                    } else {
                        if ($scope.ProductoFlex && !$scope.$parent.hayDatosPanel) {
                            $scope.modalRequeridosList = true;
                        } else {
                            $scope.Informacion.Vehiculo.ShowCargas = $scope.bndCarga;
                            $scope.$parent.Informacion.Panel = $scope.$parent.Panel;

                            try {
                                var tipoArrendamiento = {
                                    ElementoId: $scope.Informacion.Cliente.TipoArrendamiento.ValorId,
                                    Nombre: $scope.Informacion.Cliente.TipoArrendamiento.Valor
                                }
                            }
                            catch (err) {
                                var tipoArrendamiento = {
                                    ElementoId: 0,
                                    Nombre: ""
                                }
                            }

                            var codigoPostalCot = $scope.$parent.Informacion.Cotizacion.CP;
                            var iniVig = $scope.$parent.Informacion.Cotizacion.InicioVigencia;
                            var finVig;

                            if ($scope.$parent.idSolicitudR && !bndPlazoSel) {
                                finVig = new Date($scope.$parent.Informacion.Cotizacion.FinVigencia);
                            } else {
                                finVig = new Date($scope.$parent.Informacion.Cotizacion.FinVigencia.replace(/\-/g, "/"));
                            }

                            var arrendamientoCot = $scope.Informacion.Cliente.TipoArrendamiento;

                            $scope.$parent.Informacion.Cliente.TipoArrendamientoRegla = arrendamientoCot;
                            $scope.$parent.Informacion.Cliente.TipoArrendamiento = tipoArrendamiento;

                            try {
                                $scope.$parent.Informacion.Cotizacion.InicioVigencia = iniVig.toDateString("yyyy/MM/dd");
                            }
                            catch (err) {
                                finVig = new Date($scope.$parent.Informacion.Cotizacion.FinVigencia.replace(/\-/g, "/"));
                            }

                            try {
                                $scope.$parent.Informacion.Cotizacion.FinVigencia = finVig.toDateString("yyyy/MM/dd");
                            }
                            catch (err) {
                                finVig = new Date($scope.$parent.Informacion.Cotizacion.FinVigencia.replace(/\-/g, "/"));
                            }


                            $scope.$parent.Informacion.Cotizacion.CP = $scope.showHideEstados ? null : codigoPostalCot;

                            for (var l in $scope.$parent.Panel.Coberturas) {
                                if ($scope.$parent.Panel.Coberturas.hasOwnProperty(l)) {
                                    if ($scope.$parent.Panel.Coberturas[l].NombreCobertura.indexOf("Pasajero") > -1) {
                                        var auxFiltroSuma = $scope.$parent.Panel.Coberturas[l].FiltroValorRangoSuma;
                                        var auxFiltroDeducible = $scope.$parent.Panel.Coberturas[l]
                                            .FiltroValorRangoDeducible;
                                        $scope.$parent.Panel.Coberturas[l].FiltroValorRangoSuma = auxFiltroDeducible;
                                        $scope.$parent.Panel.Coberturas[l].FiltroValorRangoDeducible = auxFiltroSuma;
                                    }
                                }
                            }

                            /* Modificacion INDRA FJQP --- Encontrack, Emisi�n Multiple*/
                            var cabecera = JSON.parse(angular.toJson($scope.$parent.Informacion));

                            $scope.$parent.CabeceraCotHeredada = cabecera;
                            $sessionStorage.CabeceraCotSession = cabecera;
                            $sessionStorage.CotizarModelHeredada = "";

                            if ($scope.DisclaimerEcontrak == true) {
                                $scope.modalDisclaimer = true;
                                $sessionStorage.DisclaimerEE = $scope.DisclaimerEE;
                                $sessionStorage.DisclaimerAdapta = $scope.DisclaimerAdapta;
                                $sessionStorage.DisclaimerEETexto = $scope.DisclaimerEETexto;
                                $sessionStorage.DisclaimerAdaptaTexto = $scope.DisclaimerAdaptaTexto;
                            }
                            else {

                                $sessionStorage.confirmedEncontrack = false;
                                $sessionStorage.DisclaimerEE = $scope.DisclaimerEE;
                                $sessionStorage.DisclaimerAdapta = $scope.DisclaimerAdapta;
                                $sessionStorage.DisclaimerEETexto = $scope.DisclaimerEETexto;
                                $sessionStorage.DisclaimerAdaptaTexto = $scope.DisclaimerAdaptaTexto;
                                enviaCabeceraCot(cabecera);



                            }
                            /* Modificacion INDRA FJQP --- Encontrack, Emisi�n Multiple*/
                        }
                    }
                }


                /*****************************************************************************************************
                 **** Funcion para cargar combo Productos consultando la vista de acuerdo al cliente seleccionado ****
                 *****************************************************************************************************/
                function cargaProductos() {
                    // Declaracion del JSON que se usara para consultar por Cliente seleciconado y obtener lista [Combo productos]
                    solicitud = {
                        clienteModel: {
                            ClienteId: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                            ProductoFlex: $scope.$parent.Informacion.Cliente.ProductoFlex
                        }
                    }

                    requestService.post('cotizador',
                        'cotizacion',
                        'consultarProductosCliente',
                        solicitud,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.productos = [];
                            $scope.productos = response;
                            cargaAgencias();
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /****************************************************************************************************
                 **** Funcion para cargar combo Agencias consultando la vista de acuerdo al cliente seleccionado ****
                 ****************************************************************************************************/
                function cargaFechaSistema() {
                    requestService.post('cotizador',
                        'cotizacion',
                        'consultaFechaSistema',
                        null,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            if (response != null) {
                                $scope.dt = new Date(response[0].FechaSistema);
                                $scope.$parent.Informacion.Cotizacion.InicioVigencia = $scope.dt;
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /****************************************************************************************************
                 **** Funcion para cargar combo Agencias consultando la vista de acuerdo al cliente seleccionado ****
                 ****************************************************************************************************/
                function cargaAgencias() {
                    solicitud = {
                        clientProdAgenAseg: {
                            ClienteId: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                            Tkn: JSON.parse($base64.decode($sessionStorage.tkn))
                        }
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaAgencias', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            $scope.Agencias = response; // Obtiene datos consultados
                            if ($scope.Agencias != null && $scope.Agencias.length > 0) {
                                $scope.IsSessionAg = $scope.Agencias[0].IsSession;
                                if ($scope.IsSessionAg === true)
                                    $scope.$parent.Informacion.Cliente.Agencia = $scope.Agencias[0];
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /***********************************************************************
                 **** Funcion para cargar combo Estados consultando la regla [1155] ****
                 ***********************************************************************/
                function cargaEstados() {
                    if (!$scope.$parent.Informacion.Cliente.Producto.Cp) {
                        solicitud = {
                            solicitudRegla: {
                                IdRegla: 3261,
                                IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId
                            }
                        }
                        requestService.post('cotizador', // Modulo
                            'cotizacion', // Controlador
                            'consultaReglaNegocio', // Accion
                            solicitud, // Parametros
                            true, // Bloquear Interfaz/Vista
                            true, // Mostrar errores
                            true, // es "SingleResponse"
                            function successFunction(response) {
                                if (response.length !== 0) {
                                    $scope.Estados = response;
                                }
                            },
                            function errorFunction() { },
                            function badRequestFunction() { });
                    }
                    cargaTipoUnidad();

                };

                /***************************************************************************
                 **** Funcion para cargar combo Tipo Unidad consultando la regla [2064] ****
                 ***************************************************************************/
                function cargaTipoUnidad() {
                    $scope.Udis = [];
                    $scope.Modelos = [];
                    $scope.Servicios = [];
                    $scope.Armadoras = [];
                    $scope.SubMarcas = [];
                    $scope.Pasajeros = [];
                    $scope.Versiones = [];
                    $scope.Antiguedad = [];
                    $scope.TiposUnidad = [];

                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 2064,
                            IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                            ProductoFlex: $scope.$parent.Informacion.Cliente.ProductoFlex
                        }
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaReglaNegocio', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            $scope.TiposUnidad = response;
                        }, 
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /***********************************************************************
                 **** Funcion para cargar combo Modelos consultando la regla [1156] ****
                 ***********************************************************************/
                function cargaUdi() {
                    if ($scope.ProductoFlex) {
                        solicitud = {
                            solicitudRegla: {
                                IdRegla: 1156,
                                ProductoFlex: $scope.$parent.Informacion.Cliente.ProductoFlex,
                                IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                                EstadoVehiculo: $scope.$parent.Informacion.Vehiculo.Antiguedad.Valor,
                                Servicio: $scope.$parent.Informacion.Vehiculo.Servicio.ValorId
                            }
                        }

                        requestService.post('cotizador', // Modulo
                            'cotizacion', // Controlador
                            'consultaReglaUdi', // Accion
                            solicitud, // Parametros
                            true, // Bloquear Interfaz/Vista
                            true, // Mostrar errores
                            true, // es "SingleResponse"
                            function successFunction(response) {
                                if (response[0].ValorId !== '0') {
                                    if ($sessionStorage.sessionManejaUDI == 1) {    // FJQP INDRA ManejoUDI 
                                        $scope.$parent.bndUDI = true;               // FJQP INDRA ManejoUDI 
                                        $scope.Udis = response;                     // FJQP INDRA ManejoUDI 
                                    }                                               // FJQP INDRA ManejoUDI
                                    else {                                          // FJQP INDRA ManejoUDI
                                        $scope.$parent.bndUDI = false;              // FJQP INDRA ManejoUDI 
                                        $scope.Udis = response;  // ANTES
                                        $scope.$parent.Informacion.Cotizacion.Udi = response[0];
                                        $scope.$parent.enviarDatosDibujarPanel();

                                        // FJQP INDRA ManejoUDI 
                                    }                                               // FJQP INDRA ManejoUDI
                                } else {                                            // FJQP INDRA ManejoUDI
                                    if ($sessionStorage.sessionManejaUDI == 1) {    // FJQP INDRA ManejoUDI 
                                        $scope.$parent.Informacion.Cotizacion.Udi = response[0];    // FJQP INDRA ManejoUDI 
                                    }                                               // FJQP INDRA ManejoUDI 
                                    else {                                          // FJQP INDRA ManejoUDI 
                                        $scope.$parent.bndUDI = false;              // FJQP INDRA ManejoUDI
                                    }                                               // FJQP INDRA ManejoUDI
                                }
                            },
                            function errorFunction() {
                                $scope.$parent.bndUDI = false;
                            },
                            function badRequestFunction() { });
                    }
                }

                /*********************************************************************
                 **** Funcion para cargar combo Nuevo consultando la regla [2063] ****
                 *********************************************************************/
                function cargaAntiguedadVehiculo() {
                    $scope.Udis = [];
                    $scope.Modelos = [];
                    $scope.Servicios = [];
                    $scope.Armadoras = [];
                    $scope.SubMarcas = [];
                    $scope.Pasajeros = [];
                    $scope.Versiones = [];
                    $scope.Antiguedad = [];

                    if ($scope.$parent.Informacion.Vehiculo.TipoUnidad != null) {
                        solicitud = {
                            solicitudRegla: {
                                IdRegla: 2063,
                                IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                ProductoFlex: $scope.$parent.Informacion.Cliente.ProductoFlex,
                                IdTipoVehiculo: $scope.$parent.Informacion.Vehiculo.TipoUnidad.ValorId
                            }
                        }

                        requestService.post('cotizador', // Modulo
                            'cotizacion', // Controlador
                            'consultaReglaNegocio', // Accion
                            solicitud, // Parametros
                            true, // Bloquear Interfaz/Vista
                            true, // Mostrar errores
                            true, // es "SingleResponse"
                            function successFunction(response) {
                                $scope.Antiguedad = response;
                            },
                            function errorFunction() { },
                            function badRequestFunction() { });
                    }
                }

                /*************************************************************************
                 **** Funcion para cargar combo Servicios consultando la regla [1155] ****
                 *************************************************************************/
                function cargaServicio() {
                    $scope.Udis = [];
                    $scope.Modelos = [];
                    $scope.Servicios = [];
                    $scope.Armadoras = [];
                    $scope.SubMarcas = [];
                    $scope.Pasajeros = [];
                    $scope.Versiones = [];

                    if ($scope.$parent.Informacion.Vehiculo.Antiguedad != null) {
                        solicitud = {
                            solicitudRegla: {
                                IdRegla: 1155,
                                IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                                ProductoFlex: $scope.$parent.Informacion.Cliente.ProductoFlex,
                                IdTipoVehiculo: $scope.$parent.Informacion.Vehiculo.TipoUnidad.ValorId,
                                EstadoVehiculo: $scope.$parent.Informacion.Vehiculo.Antiguedad.Valor
                            }
                        }

                        requestService.post('cotizador', // Modulo
                            'cotizacion', // Controlador
                            'consultaReglaNegocio', // Accion
                            solicitud, // Parametros
                            true, // Bloquear Interfaz/Vista
                            true, // Mostrar errores
                            true, // es "SingleResponse"
                            function successFunction(response) {
                                $scope.Servicios = response;

                            },
                            function errorFunction() { },
                            function badRequestFunction() { });
                    }
                }


                function guardarTextoAuxiliar() {



                }

                /***********************************************************************
                 **** Funcion para cargar combo Modelos consultando la regla [1046] ****
                 ***********************************************************************/
                function cargaModelo() {
                    $scope.Modelos = [];
                    $scope.Armadoras = [];
                    $scope.SubMarcas = [];
                    $scope.Pasajeros = [];
                    $scope.Versiones = [];

                    if ($scope.$parent.Informacion.Vehiculo.Servicio != null) {
                        solicitud = {
                            solicitudRegla: {
                                IdRegla: 1046,
                                ProductoFlex: $scope.$parent.Informacion.Cliente.ProductoFlex,
                                IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                                IdTipoVehiculo: $scope.$parent.Informacion.Vehiculo.TipoUnidad.ValorId,
                                EstadoVehiculo: $scope.$parent.Informacion.Vehiculo.Antiguedad.Valor,
                                Servicio: $scope.$parent.Informacion.Vehiculo.Servicio.ValorId
                            }
                        }

                        requestService.post('cotizador', // Modulo
                            'cotizacion', // Controlador
                            'consultaReglaNegocio', // Accion
                            solicitud, // Parametros
                            true, // Bloquear Interfaz/Vista
                            true, // Mostrar errores
                            true, // es "SingleResponse"
                            function successFunction(response) {
                                $scope.Modelos = response;
                            },
                            function errorFunction() { },
                            function badRequestFunction() { });
                    }
                }

                /*************************************************************************
                 **** Funcion para cargar combo Armadoras consultando la regla [1042] ****
                 *************************************************************************/
                function cargarArmadoras() {
                    $scope.Armadoras = [];
                    $scope.SubMarcas = [];
                    $scope.Pasajeros = [];
                    $scope.Versiones = [];

                    if ($scope.$parent.Informacion.Vehiculo.Modelo != null) {
                        solicitud = {
                            solicitudRegla: {
                                IdRegla: 1042,
                                IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                TipoVehiculo: $scope.$parent.Informacion.Vehiculo.TipoUnidad.ValorId,
                                Servicio: $scope.$parent.Informacion.Vehiculo.Servicio.ValorId,
                                Modelo: $scope.$parent.Informacion.Vehiculo.Modelo.Valor
                            }
                        }

                        requestService.post('cotizador',
                            'cotizacion', // Controlador
                            'consultaReglaNegocio', // Accion
                            solicitud, // Parametros
                            true, // Bloquear Interfaz/Vista
                            true, // Mostrar errores
                            true, // es "SingleResponse"
                            function successFunction(response) {
                                $scope.Armadoras = response;
                            },
                            function errorFunction() { },
                            function badRequestFunction() { });
                    }
                }

                /************************************************************************
                 **** Funcion para cargar combo Submarca consultando la regla [5613] ****
                 ************************************************************************/
                function cargarSubMarcas() {
                    $scope.SubMarcas = [];
                    $scope.Pasajeros = [];
                    $scope.Versiones = [];

                    if ($scope.$parent.Informacion.Vehiculo.Armadora != null) {
                        solicitud = {
                            solicitudRegla: {
                                IdRegla: 5613,
                                IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                AseguradoraId: 222,
                                TipoVehiculo: $scope.$parent.Informacion.Vehiculo.TipoUnidad.ValorId,
                                Servicio: $scope.$parent.Informacion.Vehiculo.Servicio.ValorId,
                                Modelo: $scope.$parent.Informacion.Vehiculo.Modelo.Valor,
                                Marca: $scope.$parent.Informacion.Vehiculo.Armadora.Valor
                            }
                        }

                        requestService.post('cotizador', // Modulo
                            'cotizacion', // Controlador
                            'consultaReglaNegocio', // Accion
                            solicitud, // Parametros
                            true, // Bloquear Interfaz/Vista
                            true, // Mostrar errores
                            true, // es "SingleResponse"
                            function successFunction(response) {
                                $scope.SubMarcas = response;
                            },
                            function errorFunction() { },
                            function badRequestFunction() { });
                    }
                }

                /***********************************************************************************************************************
                 **** Funcion para cargar combo Versiones segun Marca, Submarva y tipo de servicio consultando la tabla [Vehiculos] ****
                 ***********************************************************************************************************************/
                function cargarVersiones() {
                    $scope.Versiones = [];

                    if ($scope.$parent.Informacion.Vehiculo.SubMarca != null) {
                        solicitud = {
                            solicitudVersiones: {
                                Tipo: $scope.$parent.Informacion.Vehiculo.TipoUnidad.ValorId,
                                Servicio: $scope.$parent.Informacion.Vehiculo.Servicio.ValorId,
                                Modelo: $scope.$parent.Informacion.Vehiculo.Modelo.Valor,
                                Marca: $scope.$parent.Informacion.Vehiculo.Armadora.Valor,
                                Submarca: $scope.$parent.Informacion.Vehiculo.SubMarca.Valor,
                                Filtro: $scope.$parent.Informacion.Vehiculo.Armadora.ValorId
                            }
                        }

                        requestService.post('cotizador', // Modulo
                            'cotizacion', // Controlador
                            'consultarVersiones', // Accion
                            solicitud, // Parametros
                            true, // Bloquear Interfaz/Vista
                            true, // Mostrar errores
                            true, // es "SingleResponse"
                            function successFunction(response) {
                                $scope.Versiones = response;
                                cargaUdi();
                                cargarPlazos();
                            },
                            function errorFunction() {
                                cargaUdi();
                                cargarPlazos();
                            },
                            function badRequestFunction() { });
                    }
                }

                /******************************************************************************
                 **** Funcion para cargar combo Pasajeros consultando la tabla [Vehiculos] ****
                 ******************************************************************************/
                $scope.cargarPasajeros = function cargarPasajeros() {
                    if ($scope.$parent.Informacion.Vehiculo.Version != null) {
                        solicitud = {
                            solicitudPasajeros: {
                                Tipo: $scope.$parent.Informacion.Vehiculo.TipoUnidad.Valor,
                                TipoId: $scope.$parent.Informacion.Vehiculo.TipoUnidad.ValorId,
                                Producto: $scope.$parent.Informacion.Cliente.Producto.NombreProducto,
                                EsFlexible: $scope.$parent.Informacion.Cliente.ProductoFlex,
                                Servicio: $scope.$parent.Informacion.Vehiculo.Servicio.ValorId,
                                Modelo: $scope.$parent.Informacion.Vehiculo.Modelo.Valor,
                                Marca: $scope.$parent.Informacion.Vehiculo.Armadora.Valor,
                                Submarca: $scope.$parent.Informacion.Vehiculo.SubMarca.Valor,
                                Version: $scope.$parent.Informacion.Vehiculo.Version.Descripcion
                            }
                        }

                        requestService.post('cotizador', // Modulo
                            'cotizacion', // Controlador
                            'consultaPasajeros', // Accion
                            solicitud, // Parametros
                            true, // Bloquear Interfaz/Vista
                            true, // Mostrar errores
                            true, // es "SingleResponse"
                            function successFunction(response) {
                                if (response.length > 0) {
                                    $scope.Pasajeros = response;
                                    $scope.bndPasajeros = true;
                                } else {
                                    $scope.bndPasajeros = false;
                                }

                                if ($scope.$parent.Informacion.Vehiculo.Antiguedad.Valor === 'NUEVO') {
                                    $scope.$parent.bndFactura = true;
                                    cargarLimiteFactura();
                                } else {
                                    cargarComboFactura();
                                }
                            },
                            function errorFunction() {
                                $scope.$parent.Informacion.Vehiculo.Pasajero = null;
                                $scope.bndPasajeros = false;
                                $scope.Pasajeros = [];
                            },
                            function badRequestFunction() { });
                    }

                }

                /*****************************************************************************
                 **** Funcion para cargar combo tipo de carga consultando la regla [6596] ****
                 *****************************************************************************/
                function cargarCargas() {
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 6596,
                            IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId
                        }
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaReglaNegocio', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            if (response.length !== 0) {
                                $scope.Cargas = response;
                                if ($scope.Cargas[0].ValorId === '1') {
                                    $scope.bndCarga = true;
                                    $scope.$parent.Informacion.Vehiculo.ShowCargas = true;
                                    $scope.$parent.Informacion.Vehiculo.ShowRemolques = $scope.Cargas[0]
                                        .HabilitaRemolques;
                                } else {
                                    $scope.bndCarga = false;
                                    $scope.$parent.Informacion.Vehiculo.ShowCargas = false;
                                }
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /********************************************************************************
                 **** Funcion para cargar los Plazos mediante la consulta de la regla [1009] ****
                 ********************************************************************************/
                function cargarPlazos() {
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 1009,
                            IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId
                        }
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaReglaNegocio', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {

                            $scope.Plazo = response;
                            cargaRetroactividad();
                        },
                        function errorFunction() {
                            cargaRetroactividad();
                        },
                        function badRequestFunction() { });
                }

                /********************************************************************************************
                 **** Funcion para cargar la regla [3306] y validar si se habilita o no el combo Lo Jack ****
                 ********************************************************************************************/
                function verificarLoJack() {
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 3306,
                            IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                            IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                            AseguradoraId: 222
                        }
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaReglaNegocio', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            if (response.length !== 0) {
                                $scope.LoJack = response;

                                if ($scope.LoJack === null) {
                                    $scope.bndLoJack = false;
                                } else {
                                    $scope.bndLoJack = true;
                                }
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });

                }

                /****************************************************************************************
                 **** Funcion para cargar la regla [6623] y obtener si se muestra Tipo Arrentamiento ****
                 ****************************************************************************************/
                function verificarArrendamiento() {
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 6623,
                            IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                            IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId.toString()
                        }
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaReglaNegocio', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            if (response.length !== 0) {
                                $scope.Arrendamiento = response;

                                if ($scope.Arrendamiento[0].Valor === 0) {
                                    $scope.bndArrendamiento = false;
                                } else {
                                    $scope.bndArrendamiento = true;
                                }
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /****************************************************************************************
                 ****    Funcion para cargar la regla [1132] y obtener si se muestra o no Agencia    ****
                 ****************************************************************************************/
                function verificaAgencias() {
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 1132,
                            IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId
                        }
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaReglaNegocio', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            if (response.length !== 0) {
                                var muestraAgencia = response[0].Valor;

                                if (muestraAgencia === 0) {
                                    $scope.bndAgencia = false;
                                } else {
                                    $scope.bndAgencia = true;
                                }
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /******************************************************************************************************
                 **** Funcion para cargar la regla [3253] y obtener los limites de Valor Factura para autos usados ****
                 ******************************************************************************************************/
                function cargarComboFactura() {
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 3253,
                            IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                            IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                            AseguradoraId: 222
                        }
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaReglaNegocio', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            if (response.length === 0) {
                                $scope.$parent.bndFactura = false;
                                $scope.$parent.Informacion.Vehiculo.Valor = null;
                            } else {
                                $scope.$parent.bndFactura = true;
                                cargarLimiteFactura();
                                $scope.$parent.Informacion.Vehiculo.Valor = null;
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /***************************************************************
                 * Peticion al servicio REST para obtener el limite de factura *
                 ***************************************************************/
                function cargarLimiteFactura() {
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 5612,
                            //IdProducto: '314',
                            IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                            ProductoFlex: $scope.$parent.Informacion.Cliente.ProductoFlex,
                            Submarca: $scope.$parent.Informacion.Vehiculo.SubMarca.Valor
                        }
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaReglaNegocio', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            $scope.LimiteFactura = response[0].Valor; // Obtiene datos consultados
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /************************************************************************************* 
                 ****  Funcion para "Buscar" por CP Vs Colonia usando las directivas de angular   ****
                 *************************************************************************************/
                $scope.obtenerRegion = function () {
                    // Valida si el campo CodigoPostal es de tipo indefinido Inicializa CodigoPostal en vacio
                    if ($scope.$parent.Informacion.Cotizacion.CP.CodigoPostal === undefined) {
                        $scope.$parent.Informacion.Cotizacion.CP.CodigoPostal = '';
                    }

                    // Valida si el campo Colonia es de tipo indefinido Inicializa Colonia en vacio
                    if ($scope.$parent.Informacion.Cotizacion.CP.Colonia === undefined) {
                        $scope.$parent.Informacion.Cotizacion.CP.Colonia = '';
                    }

                    // Valida si los campos [Codigo Postal y Colonia] no fueron capturados:
                    if (($scope.$parent.Informacion.Cotizacion.CP.CodigoPostal === '' &&
                            $scope.$parent.Informacion.Cotizacion.CP.Colonia === '') ||
                        ($scope.$parent.Informacion.Cotizacion.CP.CodigoPostal === null &&
                            $scope.$parent.Informacion.Cotizacion.CP.Colonia === null)) {
                        $scope.requeridos = 'C\u00F3digo Postal o Colonia'; // Indica leyenda de campos requeridos
                        $scope.modalRequeridos = true; // Muestra modal "Requeridos"
                    } else { // De lo contrario:
                        // Valida si la pantalla modal esta oculta [False], con la finalidad de no estar haciendo llamados inecesarios al Back
                        if ($scope.modalBuscar === false) {
                            solicitud = {
                                codigoPostalModel: {
                                    CodigoPostal: $scope.$parent.Informacion.Cotizacion.CP.CodigoPostal,
                                    Colonia: $scope.$parent.Informacion.Cotizacion.CP.Colonia
                                }
                            }

                            requestService.post('cotizador',
                                'cotizacion',
                                'consultarCodigoPostal',
                                solicitud,
                                true,
                                true,
                                true,
                                function successFunction(response) {
                                    // Valida si obtuvo registros
                                    $scope.modalBuscar = true; // Mustra la modal "Busqueda por C.P y Colonia"
                                    $scope.regiones = response;
                                },
                                function errorFunction() { },
                                function badRequestFunction() { });
                        } else { // De lo contrario:
                            $scope.modalBuscar = false; // Oculta pantalla modal "Busqueda por C.P. y  Colonia"
                        }
                    }
                };

                /*********************************************************************************************
                 **** Funcion que carga los Tipo de Arrendamiento de acuerdo a la Regla de Negocio [6645] ****
                 *********************************************************************************************/
                function cargaTipoArrendamiento() {
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 12276,
                            IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId
                        }
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaReglaNegocio', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            $scope.TipoArrendamiento = response; // Obtiene datos consultados
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /*******************************************************************
                 **** Funcion para cargar los elementos Arrentamiento y Lo Jack ****
                 *******************************************************************/
                function cargaElementos() {
                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'cargaElementos', // Accion
                        null, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            if (response.length !== 0) {
                                //$scope.TipoArrendamiento = response.Arrendamiento;
                                $scope.elementosLoJack = response.LoJack;
                                $scope.Remolques = response.Remolque;
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                };

                /******************************************************************************************************
                 ****          Funcion para cargar la regla [2394] y obtener los rangos de retroactividad          ****
                 ******************************************************************************************************/
                function cargaRetroactividad() {
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 2394,
                            IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId
                        }
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaReglaNegocio', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            if (response.length === 0) {
                                fechaRetroactividad = 0;
                            } else { // De lo contrario:
                                fechaRetroactividad = response;
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /***************************************************************************************
                 ****                Funcion para calcular la fecha fin de vigencia                 ****
                 ***************************************************************************************/
                function calculaFechaVsPlazo(meses) {
                    solicitud = {
                        plazos: {
                            FechaIniVigencia: $scope.dt.toDateString("dd/MM/yyyy"),
                            Plazos: meses
                        }
                    }

                    requestService.post('cotizador',
                        'cotizacion', // Controlador
                        'calculaPlazos', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            if (response.length !== 0) {
                                var fechaFinVigencia = response.FechaFinVigencia.split('T');
                                fechaFinVigencia = fechaFinVigencia[0].split('-');
                                var finVigencia = fechaFinVigencia[2] +
                                    '/' +
                                    fechaFinVigencia[1] +
                                    '/' +
                                    fechaFinVigencia[0];
                                $scope.dt2 = finVigencia;
                                $scope.$parent.Informacion.Cotizacion
                                    .FinVigencia = fechaFinVigencia[1] +
                                    '-' +
                                    fechaFinVigencia[2] +
                                    '-' +
                                    fechaFinVigencia[0];
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /***************************************************************************************
                 ****              Funcion enviar generar cabecera de la cotizacion                 ****
                 ***************************************************************************************/
                function enviaCabeceraCot(parametros) {
                    requestService.post('cotizador',
                        'cotizacion', // Controlador
                        'ejecutaGrabadoSolicitudCotizacion', // Accion
                        parametros, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            $location.url('/cotizadorgen/comparador/' + response.IdSolicitud);
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /******************************************************************************************* 
                 ****               Funcion para "eMail" usando las expresiones regulares               ****
                 *******************************************************************************************/
                $scope.validarCorreo = function () {
                    var email = $scope.CorreoElectronico;

                    if (email != null && email !== "") {
                        if (validateEmail(email)) {
                            $scope.resultadoValidaEmail = null;
                            $scope.isValidoEmail = true;
                        } else {
                            $scope.resultadoValidaEmail = email + " no es valido";
                            $scope.isValidoEmail = false;
                        }

                        return false;
                    } else {

                        return false;
                    }
                }

                function validateEmail(email) {
                    var re =
                        /^((([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,})))$/;
                    return re.test(email);
                }

                /******************************************************************************************* 
                 ****                     Funcion para dar formato "$ ###,###.##"                       ****
                 *******************************************************************************************/
                function formatoMoneda(valor, moneda) {
                    var cadena = "";
                    var aux;
                    var cont = 1,
                        m;

                    if (valor < 0) {
                        aux = 1;
                    } else {
                        aux = 0;
                    }

                    valor = valor.toString();

                    for (m = valor.length - 1; m >= 0; m--) {
                        cadena = valor.charAt(m) + cadena;

                        if (cont % 3 === 0 && m > aux) {
                            cadena = "," + cadena;
                        }

                        if (cont === 3) {
                            cont = 1;
                        } else {
                            cont++;
                        }
                    }

                    cadena = cadena.replace(/,/, ",");

                    return moneda + cadena;

                }

                /******************************************************************************************* 
                 *                  Funcion para validar "Factura"                                         *
                 *******************************************************************************************/
                $scope.validarFactura = function () {
                    for (var l in $scope.$parent.Panel.Coberturas) {
                        if ($scope.$parent.Panel.Coberturas.hasOwnProperty(l)) {
                            if ($scope.$parent.Panel.Coberturas[l].IsSeleccionada &&
                                $scope.$parent.Panel.Coberturas[l].IsEspecial) {
                                $scope.$parent.Panel.Coberturas[l].FiltroValorRangoDeducible = "";
                            }
                        }
                    }

                    if ($scope.$parent.Informacion.Vehiculo.Valor <= $scope.LimiteFactura) {

                        valorFacturaValidada = $scope.$parent.Informacion.Vehiculo.Valor;
                    }
                    else {                    

                            message = "El valor capturado &nbsp;" +  $scope.$parent.Informacion.Vehiculo.Valor + "&nbsp; sobrepasa el monto permitido \$5,000,000.00\.<br>";
                            $rootScope.toggleModalErrors(message);
                            valorFacturaValidada = 0;
                            $scope.$parent.Informacion.Vehiculo.Valor = null;                      
                    }
                    return;

                }

                /******************************************************************************************* 
                 *                  Funcion para ejecutar el "FiltraPanelCotizacionFlex"                   *
                 *******************************************************************************************/
                $scope.$parent.enviarPanel = function (cobertura) {
                    /*Limpiar modelos*/
                    if (!cobertura.IsSeleccionada) {
                        cobertura.FiltroValorRangoSuma = "";
                        cobertura.FiltroValorRangoDeducible = "";
                    } else {
                        for (var l in $scope.$parent.Panel.Coberturas) {
                            if ($scope.$parent.Panel.Coberturas.hasOwnProperty(l)) {
                                if ($scope.$parent.Panel.Coberturas[l].IsSeleccionada &&
                                    $scope.$parent.Panel.Coberturas[l].IsEspecial) {
                                    var valorDeducible = $scope.$parent.Panel.Coberturas[l].FiltroValorRangoDeducible
                                        .replace(/[^\d|\-+|\.+]/g, '');
                                    $scope.$parent.Panel.Coberturas[l].FiltroValorRangoDeducible = valorDeducible;
                                }
                            }
                        }
                    }

                    solicitud = {
                        PanelCotizadorModel: JSON.parse(angular.toJson($scope.$parent.Panel))
                    }

                    solicitud.PanelCotizadorModel.IdProducto = $scope.$parent.Informacion.Cliente.Producto.ProductoId;
                    solicitud.PanelCotizadorModel.IdTipoVehiculo = $scope.$parent.Informacion.Vehiculo.TipoUnidad
                        .ValorId;
                    solicitud.PanelCotizadorModel.IdCondicionVehiculo = $scope.$parent.Informacion.Vehiculo.Antiguedad
                        .ValorId;
                    solicitud.PanelCotizadorModel.IdTipoServicioVehiculo = $scope.$parent.Informacion.Vehiculo.Servicio
                        .ValorId;
                    solicitud.PanelCotizadorModel.UDI = $scope.$parent.Informacion.Cotizacion.Udi.Valor;

                    requestService.post('cotizador',
                        'cotizacion',
                        'FiltraPanelCotizacionFlex',
                        solicitud, // Se indica que no hay parametros que pasar
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.$parent.Panel = response.PanelCotizadorModel;
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /******************************************************************************************* 
                 **       Funcion para Limpiar el JSON CP para evitar que manden algun dato en vacio      **
                 *******************************************************************************************/
                $scope.chgCodigoPostal = function () {
                    if ($scope.$parent.Informacion.Cotizacion.CP.CodigoPostal === '' ||
                        $scope.$parent.Informacion.Cotizacion.CP.CodigoPostal === undefined ||
                        $scope.$parent.Informacion.Cotizacion.CP.Colonia === '' ||
                        $scope.$parent.Informacion.Cotizacion.CP.Colonia === undefined) {
                        $scope.$parent.Informacion.Cotizacion.CP.Estado = '';
                        $scope.$parent.Informacion.Cotizacion.CP.Pais = '';
                        $scope.$parent.Informacion.Cotizacion.CP.Delegacion = '';
                    }
                }

                /*********************************************************************************************
                 ****           Funcion validar campos "Requeridos" - Informacion del Cliente             ****
                 *********************************************************************************************/
                function validaInfoCliente() {
                    if ($scope.optPersonaFisica === true) {
                        if ((($scope.$parent.Informacion.Cliente.Cotizante.ApellidoP === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === undefined) &&
                                ($scope.$parent.Informacion.Cliente.Cotizante.Nombre !== '' &&
                                    $scope.$parent.Informacion.Cliente.Cotizante.Nombre !== undefined)) ||
                            (($scope.$parent.Informacion.Cliente.Cotizante.Nombre === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Nombre === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === undefined) &&
                                ($scope.$parent.Informacion.Cliente.Cotizante.ApellidoP !== '' &&
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP !== undefined)) ||
                            (($scope.$parent.Informacion.Cliente.Cotizante.Nombre === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Nombre === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === undefined) &&
                                ($scope.$parent.Informacion.Cliente.Cotizante.ApellidoM !== '' &&
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM !== undefined)) ||
                            (($scope.$parent.Informacion.Cliente.Cotizante.Nombre === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Nombre === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === undefined) &&
                                ($scope.$parent.Informacion.Cliente.Cotizante.ApellidoM !== '' &&
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM !== undefined)) ||
                            (($scope.$parent.Informacion.Cliente.Cotizante.Nombre === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Nombre === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === undefined) &&
                                ($scope.$parent.Informacion.Cliente.Cotizante.Telefono !== '' &&
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono !== undefined)) ||
                            (($scope.$parent.Informacion.Cliente.Cotizante.Nombre === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Nombre === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === undefined) &&
                                ($scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico !== '' &&
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico !== undefined))) {
                            $scope.validacionInformacion.Cliente.RazonSocial.Activo = false;
                            $scope.validacionInformacion.Cliente.Nombre.Activo = true;
                        }
                    }

                    if ($scope.optPersonaMoral === true) {
                        if ((($scope.$parent.Informacion.Cliente.Cotizante.Telefono === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === undefined) &&
                                $scope.$parent.Informacion.Cliente.Cotizante.RazonSocial !== '' &&
                                $scope.$parent.Informacion.Cliente.Cotizante.RazonSocial !== undefined) ||
                            (($scope.$parent.Informacion.Cliente.Cotizante.RazonSocial === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.RazonSocial === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico === undefined) &&
                                $scope.$parent.Informacion.Cliente.Cotizante.Telefono !== '' &&
                                $scope.$parent.Informacion.Cliente.Cotizante.Telefono !== undefined) ||
                            (($scope.$parent.Informacion.Cliente.Cotizante.RazonSocial === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.RazonSocial === undefined ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === '' ||
                                    $scope.$parent.Informacion.Cliente.Cotizante.Telefono === undefined) &&
                                $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico !== '' &&
                                $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico !== undefined)
                        ) {
                            $scope.validacionInformacion.Cliente.RazonSocial.Activo = true;
                            $scope.validacionInformacion.Cliente.Nombre.Activo = false;
                        }
                    }
                    if ($scope.$parent.Informacion.Cliente.Cliente.Cliente === '' ||
                        $scope.$parent.Informacion.Cliente.Cliente.Cliente === undefined) {
                        $scope.validacionInformacion.Cliente.Cliente.Activo = true;
                        $scope.validacionInformacion.Cliente.Producto.Activo = true;
                    } else {
                        if ($scope.$parent.Informacion.Cliente.Producto === '' ||
                            $scope.$parent.Informacion.Cliente.Producto === null ||
                            $scope.$parent.Informacion.Cliente.Producto === undefined) {
                            $scope.validacionInformacion.Cliente.Producto.Activo = true;
                        }
                    }

                    if ($scope.bndArrendamiento) {
                        if ($scope.$parent.Informacion.Cliente.TipoArrendamiento === null ||
                            $scope.$parent.Informacion.Cliente.TipoArrendamiento === '' ||
                            $scope.$parent.Informacion.Cliente.TipoArrendamiento === undefined) {
                            $scope.validacionInformacion.Cliente.Arrendamiento.Activo = true;
                        }
                    }

                    if ($scope.bndAgencia) {
                        if ($scope.$parent.Informacion.Cliente.Agencia === null ||
                            $scope.$parent.Informacion.Cliente.Agencia === '' ||
                            $scope.$parent.Informacion.Cliente.Agencia === undefined) {
                            $scope.validacionInformacion.Cliente.Agencias.Activo = true;
                        }
                    }

                    for (var i in $scope.validacionInformacion.Cliente) {
                        if ($scope.validacionInformacion.Cliente.hasOwnProperty(i)) {
                            if ($scope.validacionInformacion.Cliente[i].Activo === true) {
                                $scope.requeridosClienteList.push($scope.validacionInformacion.Cliente[i].Label);
                            }
                        }
                    }
                }

                /**********************************************************************************************
                 ****           Funcion validar campos "Requeridos" - Informacion del Vehiculo             ****
                 **********************************************************************************************/
                function validaInfoVehiculos() {
                    if ($scope.$parent.Informacion.Cliente.Producto === '' ||
                        $scope.$parent.Informacion.Cliente.Producto === null ||
                        $scope.$parent.Informacion.Cliente.Producto === undefined) {
                        $scope.validacionInformacion.Vehiculo.TipoUnidad.Activo = true;
                        $scope.validacionInformacion.Vehiculo.Antiguedad.Activo = true;
                        $scope.validacionInformacion.Vehiculo.Servicio.Activo = true;
                        $scope.validacionInformacion.Vehiculo.Modelo.Activo = true;
                        $scope.validacionInformacion.Vehiculo.Armadora.Activo = true;
                        $scope.validacionInformacion.Vehiculo.SubMarca.Activo = true;
                        $scope.validacionInformacion.Vehiculo.Version.Activo = true;
                    } else {
                        if ($scope.$parent.Informacion.Vehiculo.TipoUnidad === null) {
                            $scope.validacionInformacion.Vehiculo.TipoUnidad.Activo = true;
                            $scope.validacionInformacion.Vehiculo.Antiguedad.Activo = true;
                            $scope.validacionInformacion.Vehiculo.Servicio.Activo = true;
                            $scope.validacionInformacion.Vehiculo.Modelo.Activo = true;
                            $scope.validacionInformacion.Vehiculo.Armadora.Activo = true;
                            $scope.validacionInformacion.Vehiculo.SubMarca.Activo = true;
                            $scope.validacionInformacion.Vehiculo.Version.Activo = true;
                        } else {
                            if ($scope.bndPasajeros) {
                                if ($scope.$parent.Informacion.Vehiculo.Pasajero === null) {
                                    $scope.validacionInformacion.Vehiculo.Pasajero.Activo = true;
                                }
                            }
                            if ($scope.$parent.Informacion.Vehiculo.Antiguedad === null) {
                                $scope.validacionInformacion.Vehiculo.Antiguedad.Activo = true;
                                $scope.validacionInformacion.Vehiculo.Servicio.Activo = true;
                                $scope.validacionInformacion.Vehiculo.Modelo.Activo = true;
                                $scope.validacionInformacion.Vehiculo.Armadora.Activo = true;
                                $scope.validacionInformacion.Vehiculo.SubMarca.Activo = true;
                                $scope.validacionInformacion.Vehiculo.Version.Activo = true;
                            } else {
                                if ($scope.$parent.Informacion.Vehiculo.Servicio === null) {
                                    $scope.validacionInformacion.Vehiculo.Servicio.Activo = true;
                                    $scope.validacionInformacion.Vehiculo.Modelo.Activo = true;
                                    $scope.validacionInformacion.Vehiculo.Armadora.Activo = true;
                                    $scope.validacionInformacion.Vehiculo.SubMarca.Activo = true;
                                    $scope.validacionInformacion.Vehiculo.Version.Activo = true;
                                } else {
                                    if ($scope.$parent.Informacion.Vehiculo.Modelo === null) {
                                        $scope.validacionInformacion.Vehiculo.Modelo.Activo = true;
                                        $scope.validacionInformacion.Vehiculo.Armadora.Activo = true;
                                        $scope.validacionInformacion.Vehiculo.SubMarca.Activo = true;
                                        $scope.validacionInformacion.Vehiculo.Version.Activo = true;
                                    } else {
                                        if ($scope.$parent.Informacion.Vehiculo.Armadora === null) {
                                            $scope.validacionInformacion.Vehiculo.Armadora.Activo = true;
                                            $scope.validacionInformacion.Vehiculo.SubMarca.Activo = true;
                                            $scope.validacionInformacion.Vehiculo.Version.Activo = true;
                                        } else {
                                            if ($scope.$parent.Informacion.Vehiculo.SubMarca === null) {
                                                $scope.validacionInformacion.Vehiculo.SubMarca.Activo = true;
                                                $scope.validacionInformacion.Vehiculo.Version.Activo = true;
                                            } else {
                                                if ($scope.$parent.Informacion.Vehiculo.Version === null) {
                                                    $scope.validacionInformacion.Vehiculo.Version.Activo = true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if ($scope.bndCarga) {
                        if (($scope.$parent.Informacion.Vehiculo.Carga === null) ||
                            ($scope.$parent.Informacion.Vehiculo.Carga === '')) {
                            $scope.validacionInformacion.Vehiculo.Carga.Activo = true;
                        }

                        if ($scope.Cargas[0].HabilitaRemolques) {
                            if (($scope.$parent.Informacion.Vehiculo.TipoUnidad.ValorId === "5565") &&
                                $scope.$parent.Informacion.Vehiculo.Servicio.ServicioId === "2757") {
                                if ($scope.$parent.Informacion.Vehiculo.Remolque === null) {
                                    $scope.validacionInformacion.Vehiculo.Remolques.Activo = true;
                                }
                            }

                        }
                    }

                    if ($scope.$parent.bndFactura) {
                        if (($scope.$parent.Informacion.Vehiculo.Valor === null) ||
                            ($scope.$parent.Informacion.Vehiculo.Valor === 0) ||
                            ($scope.$parent.Informacion.Vehiculo.Valor === '')) {
                            $scope.validacionInformacion.Vehiculo.ValorFactura.Activo = true;
                        }
                        else {
                            $scope.validacionInformacion.Vehiculo.ValorFactura.Activo = false;
                        }
                    }

                    if ($scope.bndLoJack) {
                        if (($scope.$parent.Informacion.Vehiculo.LoJack === null) ||
                            ($scope.$parent.Informacion.Vehiculo.LoJack === '')) {
                            $scope.validacionInformacion.Vehiculo.LoJack.Activo = true;
                        }
                    }

                    for (var i in $scope.validacionInformacion.Vehiculo) {
                        if ($scope.validacionInformacion.Vehiculo.hasOwnProperty(i)) {
                            if ($scope.validacionInformacion.Vehiculo[i].Activo === true) {
                                $scope.requeridosVehiculoList.push($scope.validacionInformacion.Vehiculo[i].Label);
                            }
                        }
                    }
                }

                /*********************************************************************************************
                 ****             Funcion validar campos "Requeridos" - Datos de Cotizaci�n               ****
                 *********************************************************************************************/
                function validaDatosCotizacion() {
                    for (var i in $scope.validacionInformacion.Cotizacion) {
                        if ($scope.validacionInformacion.Cotizacion.hasOwnProperty(i)) {
                            if ($scope.Informacion.Cotizacion[i] === null ||
                                $scope.Informacion.Cotizacion[i] === undefined ||
                                $scope.Informacion.Cotizacion[i] === "") {
                                if (i === 'CodigoPostal' || i === 'Estado') {
                                    if ($scope.showHideBuscar) {
                                        if ($scope.showHideEstados &&
                                            i === 'Estado' &&
                                            $scope.$parent.Informacion.Cotizacion.Estado === null) {
                                            $scope.validacionInformacion.Cotizacion[i].Activo = true;
                                        } else {
                                            if (!$scope.showHideEstados &&
                                                i === 'CodigoPostal' &&
                                                $scope.$parent.Informacion.Cotizacion.CP === null) {
                                                $scope.validacionInformacion.Cotizacion[i].Activo = true;
                                            } else {
                                                if (!$scope.showHideEstados &&
                                                    i === 'CodigoPostal' &&
                                                    $scope.$parent.Informacion.Cotizacion.CP !== null) {
                                                    $scope.validacionInformacion.Cotizacion[i].Activo = false;
                                                    $scope.$parent.Informacion.Cotizacion.Estado = null;
                                                }
                                            }
                                            if ($scope.showHideEstados &&
                                                i === 'Estado' &&
                                                $scope.$parent.Informacion.Cotizacion.Estado !== null) {
                                                $scope.validacionInformacion.Cotizacion[i].Activo = false;
                                                $scope.$parent.Informacion.Cotizacion.CP = null;
                                            }
                                        }
                                    }
                                } else {
                                    if (i === 'Udi') {
                                        if ($scope.$parent
                                            .bndUDI &&
                                            $scope.$parent.Informacion.Cotizacion.Udi != null) {
                                            $scope.validacionInformacion.Cotizacion[i].Activo = false;
                                        } else {
                                            if ($scope.$parent.bndUDI) {
                                                $scope.validacionInformacion.Cotizacion[i].Activo = true;
                                            } else {
                                                $scope.validacionInformacion.Cotizacion[i].Activo = false;
                                            }
                                        }
                                    } else {
                                        $scope.validacionInformacion.Cotizacion[i].Activo = true;
                                    }
                                }

                                if ($scope.validacionInformacion.Cotizacion[i].Activo) {
                                    if ($scope.validacionInformacion.Cotizacion.hasOwnProperty(i)) {
                                        $scope.requeridosCotizacionList
                                            .push($scope.validacionInformacion.Cotizacion[i].Label);
                                    }
                                }
                            } else {
                                $scope.validacionInformacion.Cotizacion[i].Activo = false;
                            }
                        }
                    }
                }

                /***************************************************************************************************
                 ****           Funcion validar campos "Requeridos" - Panel Coberturas/Aseguradoras             ****
                 ***************************************************************************************************/
                function validaPanel() {
                    $scope.DisclaimerEcontrak = false;
                    $scope.DisclaimerEE = false;
                    $scope.DisclaimerAdapta = false;
                    $scope.DisclaimerEETexto = "";
                    $scope.DisclaimerAdaptaTexto = "";

                    if ($scope.$parent.hayDatosPanel) {
                        for (var l in $scope.$parent.Panel.Coberturas) {
                            if ($scope.$parent.Panel.Coberturas.hasOwnProperty(l)) {
                                if ($scope.$parent.Panel.Coberturas[l].IsSeleccionada) {
                                    $scope.validacionInformacion.Panel.Coberturas.Activo = false;

                                    if ($scope.$parent.Panel.Coberturas[l].FiltroValorRangoSuma === null ||
                                        $scope.$parent.Panel.Coberturas[l].FiltroValorRangoSuma === undefined ||
                                        $scope.$parent.Panel.Coberturas[l].FiltroValorRangoSuma === '') {
                                        $scope.validacionInformacion.Panel.FiltroSumaAseg.Activo = true;
                                    }
                                    if ($scope.$parent.Panel.Coberturas[l].FiltroValorRangoDeducible === null ||
                                        $scope.$parent.Panel.Coberturas[l].FiltroValorRangoDeducible === undefined ||
                                        $scope.$parent.Panel.Coberturas[l].FiltroValorRangoDeducible === '') {
                                        $scope.validacionInformacion.Panel.FiltroDeducible.Activo = true;
                                    }

                                    if ($scope.$parent.Panel.Coberturas[l].NombreCobertura === 'Asistencia Satelital' || $scope.$parent.Panel.Coberturas[l].NombreCobertura === 'ASISTENCIA SATELITAL') {
                                        $scope.DisclaimerEcontrak = true;
                                    }

                                    if ($scope.$parent.Panel.Coberturas[l].NombreCobertura === 'Equipo Especial' || $scope.$parent.Panel.Coberturas[l].NombreCobertura === 'EQUIPO ESPECIAL') {
                                        $scope.DisclaimerEE = true;
                                        $scope.DisclaimerEETexto = $scope.$parent.Panel.Coberturas[l].Descripcion;

                                    }

                                    if ($scope.$parent.Panel.Coberturas[l].NombreCobertura === 'Adaptaciones' || $scope.$parent.Panel.Coberturas[l].NombreCobertura === 'ADAPTACIONES') {
                                        $scope.DisclaimerAdapta = true;
                                        $scope.DisclaimerAdaptaTexto = $scope.$parent.Panel.Coberturas[l].Descripcion;
                                    }


                                } else {
                                    if ($scope.$parent.Panel.Coberturas[l].IsSeleccionada == false) {
                                        if ($scope.$parent.Panel.Coberturas[l].NombreCobertura === 'Asistencia Satelital' || $scope.$parent.Panel.Coberturas[l].NombreCobertura === 'ASISTENCIA SATELITAL') {
                                            $scope.DisclaimerEcontrak = false;
                                        }
                                        if ($scope.$parent.Panel.Coberturas[l].NombreCobertura === 'Equipo Especial' || $scope.$parent.Panel.Coberturas[l].NombreCobertura === 'EQUIPO ESPECIAL') {
                                            $scope.DisclaimerEE = false;
                                        }

                                        if ($scope.$parent.Panel.Coberturas[l].NombreCobertura === 'Adaptaciones' || $scope.$parent.Panel.Coberturas[l].NombreCobertura === 'ADAPTACIONES') {
                                            $scope.DisclaimerAdapta = false;
                                        }
                                    }


                                }
                            }
                        }

                        for (var k in $scope.validacionInformacion.Panel) {
                            if ($scope.validacionInformacion.Panel.hasOwnProperty(k)) {
                                if ($scope.validacionInformacion.Panel[k].Activo === true) {
                                    $scope.requeridosPanelList.push($scope.validacionInformacion.Panel[k].Label);
                                }

                            }
                        }
                    }
                }

                /***************************************************************************************************
                 ****                Funcion Calcula plazo de acuerdo a la fecha seleccionada                   ****
                 ***************************************************************************************************/
                function calculaPlazo() {
                    var meses = parseInt($scope.$parent.Informacion.Cotizacion.Plazo.Valor, 10);
                    calculaFechaVsPlazo(meses);
                }

                /*********************************************************************************************
                 **** Funcion para "Cerrar" modal de campos "Requeridos" usando las directivas de angular ****
                 *********************************************************************************************/
                $scope.hideWarning = function () {
                    $scope.modalWarning = !$scope.modalWarning; // Ocultamos la modal "Warning"
                    $scope.warning = ''; // Inicializa el valor del cuerpo del mensaje  
                }

                /* INDRA FJQP Encontrack y Emisi�n Multiple */
                $scope.AceptaEncontrack = function () {
                    $sessionStorage.confirmedEncontrack = true;
                    $scope.modalDisclaimer = !$scope.modalDisclaimer; // Ocultamos la modal "Warning"
                    var cabecera = JSON.parse(angular.toJson($scope.$parent.Informacion));

                    $scope.$parent.CabeceraCotHeredada = cabecera;
                    $sessionStorage.CabeceraCotSession = cabecera;
                    $sessionStorage.CotizarModelHeredada = "";
                    $sessionStorage.DisclaimerEE = $scope.DisclaimerEE;
                    $sessionStorage.DisclaimerAdapta = $scope.DisclaimerAdapta;
                    enviaCabeceraCot(cabecera);
                }

                /* INDRA FJQP Encontrack y Emisi�n Multiple */
                $scope.RechazaEncontrack = function () {
                    $sessionStorage.confirmedEncontrack = false;
                    $scope.modalDisclaimer = !$scope.modalDisclaimer; // Ocultamos la modal "Warning"
                    $scope.DisclaimerEcontrak == true;
                }

                /*********************************************************************************************
                 **** Funcion para "Cerrar" modal de campos "Requeridos" usando las directivas de angular ****
                 *********************************************************************************************/
                $scope.hideRequeridos = function () {
                    $scope.modalRequeridos = !$scope.modalRequeridos; // Ocultamos la modal de campos "Requeridos"
                    $scope.requeridos = ''; // Inicializa el valor del cuerpo del mensaje               
                }

                /*********************************************************************************************
                 **** Funcion para "Cerrar" modal de campos "Requeridos" usando las directivas de angular ****
                 *********************************************************************************************/
                $scope.hideRequeridosList = function () {
                    $scope.modalRequeridosList = !$scope
                        .modalRequeridosList; // Ocultamos la modal de campos "Requeridos"
                    $scope.requeridosPanelList = [];
                    $scope.requeridosClienteList = []; // Inicializa el valor del cuerpo del mensaje   
                    $scope.requeridosVehiculoList = [];
                    $scope.requeridosCotizacionList = [];
                    inicializaInformacionGral();
                }

                /*************************************************************************************** 
                 **** Funcion para "Cerrar" modal "Sin resultados" usando las directivas de angular ****
                 ***************************************************************************************/
                $scope.hideResultados = function () {
                    $scope.modalSinResultados = !$scope
                        .modalSinResultados; // Ocultamos la modal de campos "Sin Resultados"
                    $scope.resultado = ''; // Inicializa el valor del cuerpo del mensaje
                }

                /*************************************************************************************** 
                 **** Funcion para "Cerrar" modal "Sin resultados" usando las directivas de angular ****
                 ***************************************************************************************/
                $scope.selectRegion = function (buscar) {
                    $scope.$parent.Informacion.Cotizacion.CP = buscar;
                    $scope.modalBuscar = false; // Oculta pantalla modal "Busqueda por C.P. y  Colonia"
                    $scope.Informacion.Cotizacion.Estado = buscar;
                }

                /***************************************************************************************
                 ****                   Funcion para validar la retroactividad                      ****
                 ***************************************************************************************/
                function comparaFechas(calendario, reglaRetroactiva) {
                    var fCalendario = calendario.split('/');
                    var fRetroactiva = reglaRetroactiva.split('/');
                    var xDia = fCalendario[0];
                    var xMes = fCalendario[1];
                    var xAnio = fCalendario[2];
                    var yDia = fRetroactiva[0];
                    var yMes = fRetroactiva[1];
                    var yAnio = fRetroactiva[2];

                    // Si anio de la fecha seleccionada en el calendario es mayor a la retroactiva, entonces...
                    if (xAnio > yAnio) {
                        return (true); // Retorna un verdadero
                        // De lo contrario; anio seleccionado en calendario es menor o igual a anio de la retroactiva
                    } else {
                        // Si anio de la fecha seleccionada en el calendario es igual a la retroactiva, entonces...
                        if (xAnio === yAnio) {
                            // Si mes de la fecha seleccionada en el calendario es mayor a la retroactiva, entonces...
                            if (xMes > yMes) {
                                return (true); // Retorna un verdadero
                                // de lo contrario; mes seleccionado en el calendario es igual o menor de la retroactiva
                            } else {
                                // Si mes seleccionado en el calendario es igual al mes de la retroactiva, entonces...
                                if (xMes === yMes) {
                                    // si dia seleccionado en calendario es igual o mayor al dia de la retroactiva, entonces...
                                    if (xDia >= yDia) {
                                        return (true); // Retorna un verdadero
                                        // De lo contrario; dia seleccionado en calendario es menor al dia de la retroactiva
                                    } else {
                                        return (false); // Retorna un falso
                                    }
                                    // De lo contrario; mes seleccionado en el calendario es menor al mes de la retroactiva 
                                } else {
                                    return (false);
                                }
                            }
                            // De lo contrario: anio seleccionado en calendario es menor al anio de la retroactiva 
                        } else {
                            return (false); // Retorna un falso
                        }
                    }
                }

                /***************************************************************************************
                 ****                         Funcion para limpiar Campos                           ****
                 ***************************************************************************************/
                function limpiar() {
                    if (clienteSel === true) {
                        $scope.$parent.Informacion.Cliente.Agencia = '';
                        $scope.$parent.Informacion.Cliente.Producto = '';
                        $scope.$parent.Informacion.Cliente.ProductoFlex = 1;

                        $scope.bRegresar = false;
                        $scope.deshabilitaUdi = true;
                        $scope.ProductoFlex = true;
                        $scope.btnCalendario = true;
                        clienteSel = false;
                        bndProducto = false;
                    }

                    if ($scope.$parent.Informacion.Cliente.Cliente === '') {
                        $scope.$parent.Informacion.Cliente.ProductoFlex = 1;
                        $scope.$parent.Informacion.Cliente.Producto = '';
                        $scope.$parent.Informacion.Cliente.Cliente = '';
                        $scope.$parent.Informacion.Cliente.Agencia = '';

                        $scope.$parent.listaAdaptaciones = [];
                        $scope.requeridosCotizacionList = [];
                        $scope.requeridosVehiculoList = [];
                        $scope.requeridosClienteList = [];
                        $scope.requeridosPanelList = [];
                        $scope.TipoArrendamiento = [];
                        $scope.Remolques = [];
                        $scope.elementosLoJack = [];
                        $scope.productos = [];
                        $scope.clientes = [];

                        $scope.$parent.bndUDI = false;
                        $scope.deshabilitaUdi = true;
                        $scope.ProductoFlex = true;
                        $scope.productoFlex = true;
                        $scope.bRegresar = false;
                        bndProducto = false;
                    }
                    bndPlazoSel = false;
                    $scope.$parent.Informacion.Cotizacion.FinVigencia = '';
                    $scope.dt2 = '';
                    $scope.TiposUnidad = [];
                    $scope.Antiguedad = [];
                    $scope.Servicios = [];
                    $scope.Armadoras = [];
                    $scope.SubMarcas = [];
                    $scope.Versiones = [];
                    $scope.Pasajeros = [];
                    $scope.regiones = [];
                    $scope.Estados = [];
                    $scope.Modelos = [];
                    $scope.Cargas = [];
                    $scope.Plazo = [];
                    $scope.Udis = [];

                    $scope.$parent.bndFactura = false;
                    $scope.bndArrendamiento = false;
                    $scope.showHideEstados = false;
                    $scope.showHideBuscar = false;
                    $scope.$parent.bndUDI = false;
                    $scope.bndPasajeros = false;
                    $scope.btnCalendario = true;
                    $scope.deshabilitaUdi = true;
                    $scope.bndAgencia = false;
                    $scope.bndLoJack = false;
                    $scope.bndCarga = false;
                    bndAntiguedad = false;
                    bndTipoUnidad = false;
                    bndCategoria = false;
                    bndServicio = false;
                    $scope.plazo = true;
                    bndUdi = false;

                    $scope.$parent.Informacion.Cotizacion.InicioVigencia = null;
                    $scope.$parent.Informacion.Vehiculo.Arrendamiento = null;
                    $scope.$parent.Informacion.Vehiculo.TipoUnidad = null;
                    $scope.$parent.Informacion.Vehiculo.Antiguedad = null;
                    $scope.$parent.Informacion.Vehiculo.Remolque = null;
                    $scope.$parent.Informacion.Cotizacion.Estado = null;
                    $scope.$parent.Informacion.Vehiculo.Servicio = null;
                    $scope.$parent.Informacion.Vehiculo.Armadora = null;
                    $scope.$parent.Informacion.Vehiculo.SubMarca = null;
                    $scope.$parent.Informacion.Vehiculo.Pasajero = null;
                    $scope.$parent.Informacion.Vehiculo.Version = null;
                    $scope.$parent.Informacion.Cotizacion.Plazo = null;
                    $scope.$parent.Informacion.Vehiculo.Modelo = null;
                    $scope.$parent.Informacion.Vehiculo.LoJack = null;
                    $scope.$parent.Informacion.Cotizacion.Udi = null;
                    $scope.$parent.Informacion.Vehiculo.Carga = null;
                    $scope.$parent.Informacion.Vehiculo.Valor = null;
                    $scope.$parent.Informacion.Cotizacion.CP = null;
                    $scope.LimiteFactura = null;
                    fechaRetroactividad = null;
                    $scope.$parent.Informacion.Vehiculo.ShowCargas = false;
                    $scope.$parent.Informacion.Vehiculo.ShowRemolques = false;

                }

                function inicializaInformacionGral() {
                    $scope.validacionInformacion = {
                        Cliente: {
                            Nombre: {
                                Label:
                                    "Si, ingresa cualquier campo del �rea de cotizante debe ingresar los dem�s datos",
                                Activo: false
                            },
                            RazonSocial: {
                                Label:
                                    "Si, ingresa cualquier campo del �rea de cotizante debe ingresar los dem�s datos ",
                                Activo: false
                            },
                            Cliente: {
                                Label: "Cliente",
                                Activo: false
                            },
                            Producto: {
                                Label: "Producto",
                                Activo: false
                            },
                            Arrendamiento: {
                                Label: "Tipo de Arrendamiento",
                                Activo: false
                            },
                            Agencias: {
                                Label: "Agencia",
                                Activo: false
                            }
                        },
                        Vehiculo: {
                            TipoUnidad: {
                                Label: "Tipo de Unidad",
                                Activo: false
                            },
                            Antiguedad: {
                                Label: "Antig�edad",
                                Activo: false
                            },
                            Servicio: {
                                Label: "Servicio",
                                Activo: false
                            },
                            Modelo: {
                                Label: "Modelo",
                                Activo: false
                            },
                            Armadora: {
                                Label: "Armadora",
                                Activo: false
                            },
                            SubMarca: {
                                Label: "Submarca",
                                Activo: false
                            },
                            Pasajero: {
                                Label: "Pasajero",
                                Activo: false
                            },
                            Version: {
                                Label: "Versi�n",
                                Activo: false
                            },
                            LoJack: {
                                Label: "LoJack",
                                Activo: false
                            },
                            Carga: {
                                Label: "Carga",
                                Activo: false
                            },
                            Remolques: {
                                Label: "No. Remolques",
                                Activo: false
                            },
                            ValorFactura: {
                                Label: "Valor Factura",
                                Activo: false
                            }
                        },
                        Cotizacion: {
                            InicioVigencia: {
                                Label: "Fecha Inicio de Vigencia",
                                Activo: false
                            },
                            Plazo: {
                                Label: "Plazo",
                                Activo: false
                            },
                            Udi: {
                                Label: "UDI",
                                Activo: false
                            },
                            CodigoPostal: {
                                Label: 'B�squeda por "C�digo Postal" o "Colonia"',
                                Activo: false
                            },
                            Estado: {
                                Label: "Estado",
                                Activo: false
                            }
                        },
                        Panel: {
                            FiltroDeducible: {
                                Label: 'Deducibles "Favor de validar"',
                                Activo: false
                            },
                            FiltroSumaAseg: {
                                Label: 'Sumas Aseguradas "Favor de validar"',
                                Activo: false
                            },
                            Coberturas: {
                                Label: 'Coberturas "Favor de Validar"',
                                Activo: true
                            }
                        }
                    };
                }

                function cargarClientes() {
                    var solicitud = {
                        clienteModel: {
                            Cliente: '',
                            ClienteId: ''
                        }
                    };
                    requestService.post('cotizador',
                        'cotizacion',
                        'consultarCantCliente',
                        solicitud,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            if (response != null) {
                                $scope.isCombo = response;
                                if (response <= 20)
                                    cargaClienteCombo();
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                function cargaClienteCombo() {
                    var solicitud = {
                        clienteModel: {
                            Cliente: '',
                            ClienteId: ''
                        }
                    };
                    requestService.post('cotizador',
                        'cotizacion',
                        'consultarCliente',
                        solicitud,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            if (response != null)
                                $scope.Clientes = response;
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                function recargaInformacionCotizacion() {
                    limpiar();
                    $scope.bRegresar = true;

                    var solicitud = {
                        SolicitudId: $scope.$parent.idSolicitudR
                    };

                    requestService.post('cotizador',
                        'cotizacion',
                        'recargaInformacionCotizacion',
                        solicitud,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            var infCotizacion = response.PreCargaCotizador;
                            var infCabecera = response.CabeceraCotizacionModel;
                            var esFlexible, fechaVig, dia, mes, anio;

                            if (infCabecera.Cliente.Cotizante != null) {
                                if (infCabecera.Cliente.Cotizante.TipoPersona === "Fisica") {
                                    document.getElementsByName("TipoPersona")[0].checked = true;
                                } else {
                                    document.getElementsByName("TipoPersona")[1].checked = true;
                                }
                            } else {
                                var cotizante =
                                {
                                    Nombre: '',
                                    ApellidoP: '',
                                    ApellidoM: '',
                                    RazonSocial: '',
                                    TipoPersona: 'Fisica',
                                    Telefono: '',
                                    CorreoElectronico: ''
                                };

                                infCabecera.Cliente.Cotizante = cotizante;
                                document.getElementsByName("TipoPersona")[0].checked = true;
                            }

                            $scope.$parent.Panel = {};

                            bndCategoria = true;
                            bndTipoUnidad = true;
                            bndAntiguedad = true;
                            bndProducto = true;
                            bndServicio = true;
                            $scope.showHideBuscar = true;
                            $scope.btnCalendario = false;
                            $scope.hayDatosPanel = false;
                            $scope.bndAgencia = true;
                            $scope.plazo = false;

                            $scope.dt = new Date(infCabecera.Cotizacion.InicioVigencia);

                            fechaVig = infCabecera.Cotizacion.FinVigencia.split('T')[0];
                            infCabecera.Cotizacion
                                .InicioVigencia = new Date(infCabecera.Cotizacion.InicioVigencia);
                            dia = fechaVig.split('-')[2];
                            mes = fechaVig.split('-')[1];
                            anio = fechaVig.split('-')[0];

                            $scope.productos = infCotizacion.Productos;
                            $scope.dt2 = dia + '/' + mes + '/' + anio;
                            $scope.Agencias = infCotizacion.Agencias;

                            if (infCotizacion.TipoArrendamiento.length > 0) {
                                $scope.TipoArrendamiento = infCotizacion.TipoArrendamiento;
                                $scope.bndArrendamiento = true;
                            }

                            if ($scope.$parent.Informacion.Cliente.ProductoFlex === 0) {
                                $scope.bndLoJack = (infCabecera.Vehiculo.LoJackModel != null);
                            } else {
                                $scope.bndLoJack = false;
                            }

                            $scope.$parent.Informacion.Vehiculo
                                .ShowRemolques = ($scope.Cargas.length === 0)
                                ? false
                                : $scope.Cargas[0].HabilitaRemolques;
                            $scope.$parent.Informacion.Vehiculo.ShowCargas = !(infCabecera.Vehiculo.Carga == null);
                            $scope.bRemolques = !(infCabecera.Vehiculo.Remolque.ElementoId === 0);
                            $scope.bndPasajeros = !(infCabecera.Vehiculo.Pasajero.Pasajeros === 0);
                            $scope.$parent.bndFactura = !(infCabecera.Vehiculo.Valor === 0);
                            $scope.bndCarga = !(infCabecera.Vehiculo.Carga == null);
                            $scope.TiposUnidad = infCotizacion.TipoUnidad;
                            $scope.Antiguedad = infCotizacion.Antiguedad;
                            $scope.Servicios = infCotizacion.Servicio;
                            $scope.Modelos = infCotizacion.Modelos;
                            $scope.Armadoras = infCotizacion.Armadoras;
                            $scope.SubMarcas = infCotizacion.Submarcas;
                            $scope.Versiones = infCotizacion.Versiones;
                            $scope.Pasajeros = infCotizacion.Pasajeros;
                            $scope.Cargas = infCotizacion.Cargas;
                            $scope.Plazo = infCotizacion.Plazo;

                            if (infCabecera.Cotizacion.CP.CodigoPostal !== null) {
                                $scope.showHideEstados = false;
                                $scope.$parent.Informacion.Cotizacion.Estado = null;
                                $scope.$parent.Informacion.Cotizacion.CP = infCabecera.Cotizacion.CP;
                            } else {
                                $scope.showHideEstados = true;
                                $scope.Estados = infCotizacion.Estados;
                                $scope.$parent.Informacion.Cotizacion.CP = null;
                            }

                            infCabecera.Vehiculo.Valor = "";
                            $scope.$parent.Informacion.Cliente = infCabecera.Cliente;
                            $scope.$parent.Informacion.Cliente.TipoArrendamiento = infCabecera.Cliente
                                .TipoArrendamientoRegla;
                            $scope.$parent.Informacion.Cotizacion = infCabecera.Cotizacion;
                            $scope.$parent.Informacion.Vehiculo = infCabecera.Vehiculo;
                            $scope.$parent.Informacion.Vehiculo.LoJack = infCabecera.Vehiculo.LoJackModel;

                            $scope.solicitud.clienteModel.Cliente = infCabecera.Cliente.Cliente.Cliente;
                            $scope.ProductoFlex = (infCabecera.Cliente.Producto.Flexible === 1) ? true : false;
                            $scope.$parent.listaAdaptaciones = infCabecera.Panel.Coberturas;
                            esFlexible = $scope.$parent.Informacion.Cliente.Producto.Flexible;

                            cargarLimiteFactura();
                            cargaRetroactividad();

                            if (esFlexible === 1) {
                                $scope.$parent.bndUDI = (infCotizacion.Udi[0].ValorId === '0') ? false : true;
                                $scope.deshabilitaUdi = (infCotizacion.Udi[0].ValorId === '0') ? true : false;
                                $scope.$parent.hayDatosPanel = true;
                                $scope.$parent.Panel = infCotizacion.PanelCotizadorModel;
                                $scope.Udis = infCotizacion.Udi;
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                $scope.$parent.validaSuma = function (sumaAsegurada, cobertura) {
                    if (valorFacturaValidada == 0) { valorFacturaValidada = $scope.$parent.Informacion.Vehiculo.Valor };
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 6597,
                            IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                            IdCobertura: cobertura
                        },
                        limiteValorFactura: {
                            LimiteValorFactura: parseFloat(valorFacturaValidada),
                            ValorFactura: sumaAsegurada
                        }
                    }

                    requestService.post('cotizador',
                        'cotizacion',
                        'validaLimiteAdaptaciones',
                        solicitud, // Se indica que no hay parametros que pasar
                        true,
                        true,
                        true,
                        function successFunction() { },
                        function errorFunction() {
                            for (var l in $scope.$parent.Panel.Coberturas) {
                                if ($scope.$parent.Panel.Coberturas.hasOwnProperty(l)) {
                                    if ($scope.$parent.Panel.Coberturas[l].IsSeleccionada &&
                                        $scope.$parent.Panel.Coberturas[l].IsEspecial &&
                                        $scope.$parent.Panel.Coberturas[l].IdCobertura === cobertura) {
                                        $scope.$parent.Panel.Coberturas[l].FiltroValorRangoDeducible = "";
                                    }
                                }
                            }
                        },
                        function badRequestFunction() { });
                }
            }
        ]);
    });