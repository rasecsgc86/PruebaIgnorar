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
         * @var bMenu - BANDERA PARA MOSTRAR EL MENU O NO DEPENDIENDO SI ESTÁ LOGEADO
         * 
         *
         *******************************************************************************************
         *****************VARIABLES PARA EL DISEÑO**************************************************
         * @var rama - VARAIABLE PARA DETERMINAR UN ESTILO DEPENDIENDO DEL ESTADO DONDE SE ENCUENTRE
         */
        app.controller('CotizadorGenController',
        [
            '$scope', '$rootScope', '$location', 'requestService', '$localStorage', 'Notification', 'mappingService', '$sessionStorage', '$base64', '$http',
            function ($scope, $rootScope, $location, requestService, $localStorage, Notification, mappingService, $sessionStorage, $base64, $http) {
                $rootScope.tittle = "Cotizador-Gen";
                $rootScope.bMenu = true;
                $scope.rama = 'cot';
                $scope.bramaCot = true;
                $scope.bramaComp = false;
                $scope.bramaEmit = false;
                $scope.bramaPag = false;
                $scope.bramaImp = false;
                $scope.styleModalInfo = '';
                $scope.hayDatosPanel = false;
                $scope.paquete = '';
                $scope.idSolicitudR = '';
                $scope.PAQUETEFLEX = "Personalizado";
                $scope.listaAdaptaciones = [];
                $scope.bndFactura = false;
                $scope.valorp = "";
                $scope.Informacion = {
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
                        Producto: {
                            ProductoId: 0,
                            NombreProducto: "",
                            Flexible: true,
                            Cp: false
                        },
                        TipoArrendamiento: '',
                        Agencia: '',
                        TipoArrendamientoRegla: ''
                    },
                    Vehiculo: {
                        TipoUnidad: '',
                        Antiguedad: '',
                        ClaveMarsh: '',
                        Servicio: '',
                        Valor: 00,
                        Modelo: '',
                        SubMarca: '',
                        Armadora: '',
                        Carga: '',
                        Pasajero: '',
                        Version: '',
                        Remolque: '',
                        LoJack: '',
                        ShowCargas: false,
                        ShowRemolques: false
                    },
                    Cotizacion: {
                        Plazo: '',
                        Paquete: '',
                        Udi: '',
                        InicioVigencia: '',
                        FinVigencia: '',
                        Udis: '',
                        bndUDI: false,
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

                $scope.Panel = {};
                $rootScope.AuxAseguradoras = [
                    {
                        IdAseguradora: '',
                        NombreCobertura: '',
                        Img: ''
                    }
                ];

                $scope.enviarDatosDibujarPanel = function () {
                    var tipoCarga = ($scope.Informacion.Vehiculo.Carga != undefined &&
                        $scope.Informacion.Vehiculo.Carga.Valor != "" &&
                        $scope.Informacion.Vehiculo.Carga.Valor != null);


                    var datosVehiculo = {
                        panelCotizadorModel: {
                            IdProducto: $scope.Informacion.Cliente.Producto.ProductoId,
                            IdTipoVehiculo: $scope.Informacion.Vehiculo.TipoUnidad.ValorId,
                            IdCondicionVehiculo: $scope.Informacion.Vehiculo.Antiguedad.ValorId,
                            IdTipoServicioVehiculo: $scope.Informacion.Vehiculo.Servicio.ValorId,
                            SubMarca: $scope.Informacion.Vehiculo.SubMarca.Valor,
                            UDI: $scope.Informacion.Cotizacion.Udi.Valor,
                            TipoCarga: tipoCarga ? $scope.Informacion.Vehiculo.Carga.Valor.substr(0, 1) : null
                        },
                        SolicitudRegla: {
                            IdRegla: 1156,
                            ProductoFlex: $scope.Informacion.Cliente.ProductoFlex,
                            IdProducto: $scope.Informacion.Cliente.Producto.ProductoId,
                            IdCliente: $scope.Informacion.Cliente.Cliente.ClienteId,
                            EstadoVehiculo: $scope.Informacion.Vehiculo.Antiguedad.Valor,
                            Servicio: $scope.Informacion.Vehiculo.Servicio.ValorId
                        }
                    }

                    requestService.post('cotizador',
                        'cotizacion',
                        'ConsultaPanelCotizacionFlex',
                        datosVehiculo, // Se indica que no hay parametros que pasar
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.Panel = response.PanelCotizadorModel;
                            if ($scope.Panel != null && $scope.Panel.Aseguradoras != null) {
                                inicializaImgAseg();
                                $scope.hayDatosPanel = true;
                            } else
                                $scope.hayDatosPanel = false;
                        },
                        function errorFunction() { $scope.hayDatosPanel = false; },
                        function badRequestFunction() { $scope.hayDatosPanel = false; });
                };

                function inicializaImgAseg() {
                    for (var j = 0; j < jsonAseguradorasImg.length; j++)
                        for (var i = 0; i < $scope.Panel.Aseguradoras.length; i++)
                            if (jsonAseguradorasImg[j]
                                .aseguradoraId ===
                                $scope.Panel.Aseguradoras[i].IdAseguradora) {
                                $scope.AuxAseguradoras[i] = $scope.Panel.Aseguradoras[i];
                                $scope.AuxAseguradoras[i].Img = jsonAseguradorasImg[j].img;
                            }
                };


                $scope.showMe = false;
                $scope.enviarCotEmail = function (pktr) {

                    mappingService.services.setObjectpkt(pktr);
                    $scope.showMe = !$scope.showMe;

                }

                $scope.showDestinararios = false;
                $scope.AgregarDestinatarios = function () {


                    $scope.showDestinararios = !$scope.showDestinararios;
                    $scope.destinatariosModel = "";
                }



                $scope.EnviarPDFMail = function () {

                    var  destinatarios = '';
                    //Se toman los destinatarios del control
                    if ($scope.Informacion.Cliente.Cotizante != null)
                    {
                        destinatarios = $scope.Informacion.Cliente.Cotizante.CorreoElectronico;
                    }
                    if ($scope.destinatariosModel != undefined) {
                        destinatarios = destinatarios + ',' + $scope.destinatariosModel;
                    }
                    // se obtien el objeto pkt del servicio (Se habia enviado el set anteriormente)
                    var pkt = mappingService.services.getObjectpkt();
                    // Objeto del servicio
                    var datosEmail = {
                        cotizacionId: pkt.CotizacionId,
                        flexible: pkt.Flexible,
                        paqueteId: pkt.ElementoId,
                        solicitudId: $scope.idSolicitudR,
                        numero: pkt.Numero,
                        tkn: JSON.parse($base64.decode($sessionStorage.tkn)),
                        destinatarios: destinatarios
                    }

                    // Se invoca al servicio
                    var url = mappingService.services['comparador']['comparador']['enviorReporteCotEmail'];
                    $http.post(url, datosEmail)
                        .then(function success(data) {
                            var resp = JSON.stringify(data.data);

                            if (resp=='true') {
                                alert("Cotización Enviada!")
                            }
                            else {
                                alert("Ocurrió un error en el envió!")
                            }



                        },
                            function error(data) {

                            });

                    // Se oculta el menu de Envio de correo (Div)
                    $scope.showMe = !$scope.showMe;
                }


                /**********************************************************
                * Funcion para convertir a moneda
                ***********************************************************/
                $scope.numberFormat = function (amount) {
                    if (isNaN(amount))
                        return amount;
                    else {
                        var decimals = 2;
                        amount += ''; // por si pasan un numero en vez de un string
                        amount = parseFloat(amount
                            .replace(/[^0-9\.]/g, '')); // elimino cualquier cosa que no sea numero o punto
                        decimals = decimals || 0; // por si la variable no fue fue pasada
                        // si no es un numero o es igual a cero retorno el mismo cero
                        if (isNaN(amount) || amount === 0)
                            return parseFloat(0).toFixed(decimals);
                        // si es mayor o menor que cero retorno el valor formateado como numero
                        amount = '' + amount.toFixed(decimals);
                        var amount_parts = amount.split('.'),
                            regexp = /(\d+)(\d{3})/;
                        while (regexp.test(amount_parts[0]))
                            amount_parts[0] = amount_parts[0].replace(regexp, '$1' + ',' + '$2');
                        return '$' + amount_parts.join('.');
                    }
                }

                $scope.$watch("Panel.Coberturas",
                    function (newValue, oldValue) {
                        $scope.listaAdaptaciones = [];
                        $.each(newValue,
                            function (item, value) {
                                if (value.IsSeleccionada && value.IsEspecial) {
                                    $scope.listaAdaptaciones.push(value);
                                }
                            });
                    });

                $scope.calculaPosicionPixel = function (lengthText) {
                    $scope.style = {
                        "left": "0px",
                        "top": "0px"
                    }

                    if (lengthText <= 84) {
                        $scope.style.left = (lengthText * -7.53) + "px";
                    } else {
                        $scope.style.left = "-600px";
                        $scope.style.top = "-" + ((lengthText / 79) * 16.15) + "px";
                    }

                    return $scope.style;
                }
            }
        ]);

    });