define(['modules/app', 'services/requestService', 'services/mappingService'],
    function (app) {
        /**
         * @author <Israel Borja Ruiz>
         * DEFINIMOS Y REGISTRAMOS EL CONTROLLER emitirController.
         * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
         * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
         * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
         * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
         *
         * @var tittle - TITULO DE LA PAGINA  
         * @var rama - AUXILIAR PARA EL DISEÑO, POSICIONA LA IMAGEN AZUL DEPENDIENDO DE QUEestado estamos
         * 
         * Modifiación INDRA FJQP **** EMISION MULTIPLE *****
         * 
         * 
         */
        app.controller('EmitirController',
        [
            '$scope', '$stateParams', '$state', '$rootScope', '$location', 'requestService', '$localStorage',
            'Notification', '$sessionStorage', 'mappingService', '$http',
            function ($scope, $stateParams, $state, $rootScope, $location, requestService, $localStorage, Notification, $sessionStorage, mappingService, $http) {
                $rootScope.tittle = "Emitir";
                $scope.$parent.rama = 'emit';
                $scope.$parent.bramaCot = false;
                $scope.$parent.bramaComp = false;
                $scope.$parent.bramaEmit = true;
                $scope.$parent.bramaPag = false;
                $scope.$parent.bramaImp = false;
                $scope.$parent.bPanelCoberturas = false;
                $scope.$parent.idSolicitudR = $stateParams.idSolicitud;
                $scope.modalBuscar = false; // Inicializa en false la modal donde obtiene informaci�n consultada en BD
                $scope.modalRequeridos = false; // Inicializa en false la modal de "Campos Requeridos"
                $scope.modalSinResultados = false; // Inicializa en false la modal "Sin Resultados"
                $scope.modalInvalido = false;
                $scope.styleModalInfo = ''; // Inicializa estilo de modal que obtiene informaci�n consultada en BD
                $scope.ModalRequeridos = 'ModalRequeridos'; // Inicializa estilo dl titulo para la modal de requeridos
                var codigoPostal = {};
                $scope.validacionElemento = [];
                $scope.requeridosContratanteList = [];
                $scope.requeridosComplementariaList = [];
                $scope.requeridosValidosList = [];
                $scope.requeridosDireccionList = [];
                $scope.requeridosAgenciaList = [];
                $scope.requeridosVehiculoList = [];
                $scope.IdCliente = '';
                $scope.IdProducto = '';
                $scope.modeloGenero = '';
                var solicitud;
                var serieRequerida = false;

                /* Modifiación INDRA FJQP **** EMISION MULTIPLE ******/
                $scope.CabeceraCotHeredada = $sessionStorage.CabeceraCotSession;
                $scope.CotizarModelHeredada = $sessionStorage.CotizarModelHeredada;

                /* Modifiación INDRA FJQP Disclimer EE y Adaptaciones */

                $scope.DisclaimerEE = $sessionStorage.DisclaimerEE;
                $scope.DisclaimerAdapta = $sessionStorage.DisclaimerAdapta;

                /** Modificacion INDRA FJQP -- Emisión Múltiple ---*/
                $scope.confirmedEncontrack = $sessionStorage.confirmedEncontrack;
                $scope.isOpenMarkt = false;
                var Encontrack = 0;
                if ($sessionStorage.confirmedEncontrack == true) {
                    Encontrack = 1;
                }
                else {
                    Encontrack = 0;
                }
                /**
                 * Banderas para determinar las reglas de negocio por campos y secciones  
                 */
                $scope.optPersonaFisica = true;
                $scope.optCargoGobierno = true;
                $scope.RFCinhabilitado = false;
                $scope.Generoinhabilitado = false;
                $scope.optSDoctoFederal = true;
                $scope.optEntidad = true;
                $scope.optInfoComplementaria = false;
                $scope.optVendedorObligatorio = false;
                //$scope.optVendedorNoObligatorio = true;
                $scope.optVendedorNoObligatorio = false;
                $scope.optConductorObligatorio = false;
                $scope.optConductorNoObligatorio = true;
                $scope.bndDatosRegion = true;
                $scope.doSubmit = false;
                $scope.bndBeneficiario = false;
                $scope.ExistePrecarga = false;
                $scope.BeneficiarioPreferente = '';

                /** Emision Múltiple */
                $scope.PermiteEmisionMultiple = false;

                /**

                 * Se ejecuta un submit al formulario prueba para realizar validaciones
                 */
                $scope.ValidacionInformacion = {
                    Contratante: {
                        Nombre: {
                            Etiqueta: 'Nombre',
                            Requerido: false
                        },
                        Paterno: {
                            Etiqueta: 'Paterno',
                            Requerido: false
                        },
                        FechaNacimiento: {
                            Etiqueta: 'Fecha de Nacimiento',
                            Requerido: false
                        },
                        RFC: {
                            Etiqueta: 'RFC',
                            Requerido: false
                        },
                        Nacionalidad: {
                            Etiqueta: 'Nacionalidad',
                            Requerido: false
                        },
                        Genero: {
                            Etiqueta: 'Género',
                            Requerido: false
                        },
                        CorreoElectronico: {
                            Etiqueta: 'Correo Electrónico',
                            Requerido: false
                        },
                        Telefono: {
                            Etiqueta: 'Teléfono',
                            Requerido: false
                        }
                    },
                    Validos: {
                        CorreoElectronico: {
                            Etiqueta: 'Correo Electrónico',
                            Requerido: false
                        },
                        RFC: {
                            Etiqueta: 'RFC',
                            Requerido: false
                        },
                        RFCBeneficiario: {
                            Etiqueta: 'RFC Beneficiario',
                            Requerido: false
                        },
                        CURP: {
                            Etiqueta: 'CURP',
                            Requerido: false
                        },
                        Serie: {
                            Etiqueta:
                                'Serie ( No debe incluir caracteres especiales y las letras: I, O, Q y Ñ. Debe contener 17 caracteres)',
                            Requerido: false
                        }
                    },
                    Complementaria: {
                        PaisNacimiento: {
                            Etiqueta: 'Pais de Nacimiento',
                            Requerido: false
                        },
                        EntidadNacimiento: {
                            Etiqueta: 'Entidad Federativa de Nacimiento',
                            Requerido: false
                        },
                        DoctoIdentificacion: {
                            Etiqueta: 'Docto de Identificación',
                            Requerido: false
                        },
                        NumDocto: {
                            Etiqueta: 'No. de Docto',
                            Requerido: false
                        },
                        OficinaExpide: {
                            Etiqueta: 'Oficina',
                            Requerido: false
                        },
                        Profesion: {
                            Etiqueta: 'Profesión',
                            Requerido: false
                        },
                        Ocupacion: {
                            Etiqueta: 'Ocupación',
                            Requerido: false
                        },
                        GiroNegocio: {
                            Etiqueta: 'Actividad o giro de negocio',
                            Requerido: false
                        },
                        MandoGobierno: {
                            Etiqueta: 'Mando en Gobierno',
                            Requerido: false
                        },
                        DescripcionCargo: {
                            Etiqueta: 'Descripción del cargo',
                            Requerido: false
                        }
                    },
                    ComplementariaMoral: {
                        ActividadPrincipal: {
                            Etiqueta: 'Actividad Principal',
                            Requerido: false
                        },
                        DoctoIdentificacionMoral: {
                            Etiqueta: 'Docto Identificación',
                            Requerido: false
                        },
                        NumDoctoMoral: {
                            Etiqueta: 'Num. de Docto',
                            Requerido: false
                        },
                        GiroNegocioMoral: {
                            Etiqueta: 'Actividad o Giro de Negocio',
                            Requerido: false
                        },
                        ObjetivoSocial: {
                            Etiqueta: 'Objectivo Social',
                            Requerido: false
                        },
                        MandoGobiernoMoral: {
                            Etiqueta: 'Mando de gobierno o Administrador',
                            Requerido: false
                        },
                        NombreFuncionario: {
                            Etiqueta: 'Nombre Funcionario',
                            Requerido: false
                        },
                        FechaNacimientoApoderado: {
                            Etiqueta: 'Fecha de Nacimiento del Apoderado',
                            Requerido: false
                        },
                        PaisNacimientoApoderado: {
                            Etiqueta: 'Pais de Nacimiento del Apoderado',
                            Requerido: false
                        },
                        EntidadApoderado: {
                            Etiqueta: 'Entidad de Nacimiento del Apoderado',
                            Requerido: false
                        }
                    },
                    Direccion: {
                        Pais: {
                            Etiqueta: 'Pais',
                            Requerido: false
                        },
                        Estado: {
                            Etiqueta: 'Estado',
                            Requerido: false
                        },
                        Delegacion: {
                            Etiqueta: 'Delegación',
                            Requerido: false
                        },
                        Calle: {
                            Etiqueta: 'Calle',
                            Requerido: false
                        },
                        Numero: {
                            Etiqueta: 'Número',
                            Requerido: false
                        },
                        CodigoPostal: {
                            Etiqueta: 'Código Postal',
                            Requerido: false
                        },
                        Colonia: {
                            Etiqueta: 'Colonia',
                            Requerido: false
                        }
                    },
                    Vehiculo: {
                        Motor: {
                            Etiqueta: 'Motor',
                            Requerido: false
                        },
                        Serie: {
                            Etiqueta: 'Serie',
                            Requerido: false
                        },
                        Conductor: {
                            Etiqueta: 'Conductor',
                            Requerido: false
                        }
                    },
                    Agencia: {
                        /*Vendedor: {
                            Etiqueta: 'Vendedor',
                            Requerido: false
                        },*/
                        NumContrato: {
                            Etiqueta: 'Num. de Contrato',
                            Requerido: false
                        },
                        InicioContrato: {
                            Etiqueta: 'Inicio de Contrato',
                            Requerido: false
                        }
                    }
                }

                $scope.InformacionEmitir = {
                    Solicitud: {
                        SolicitudId: $stateParams.idSolicitud,
                        Numero: $stateParams.numero,
                        CotizacionId: $stateParams.idCotizacion,
                        InfoComplementaria: $scope.optInfoComplementaria,
                        TipoArrendamiento: ''
                    },
                    Contratante: {
                        PersonaId: '',
                        TipoPersona: 'Fisica',
                        Nombre: '',
                        Paterno: '',
                        Materno: '',
                        FechaNacimiento: '',
                        RFC: '',
                        Nacionalidad: '',
                        Genero: '',
                        CorreoElectronico: '',
                        Telefono: '',
                        Telefono2: ''
                    },
                    DatosComplementarios: {},
                    Complementaria: {
                        PaisNacimiento: '',
                        EntidadNacimiento: '',
                        DoctoIdentificacion: '',
                        NumDocto: '',
                        OficinaExpide: '',
                        CURP: '',
                        SerieFiel: '',
                        Profesion: '',
                        Ocupacion: '',
                        GiroNegocio: '',
                        MandoGobierno: '',
                        DescripcionCargo: '',
                        RegimenFiscal: ''
                    },
                    ComplementariaMoral: {
                        ActividadPrincipal: '',
                        SerieFIELMoral: '',
                        DoctoIdentificacionMoral: '',
                        NumDoctoMoral: '',
                        ApoderadoLegal: '',
                        FolioMercatil: '',
                        EstructuraCoorporativa: '',
                        InformacionAccionistas: '',
                        GiroNegocioMoral: '',
                        ObjetivoSocial: '',
                        MandoGobiernoMoral: '',
                        NombreFuncionario: '',
                        FechaNacimientoApoderado: '',
                        PaisNacimientoApoderado: '',
                        EntidadApoderado: ''
                    },
                    Direccion: {
                        Pais: '',
                        Estado: '',
                        EstadoId: '',
                        ValidaEstado: true,
                        Delegacion: '',
                        DelegacionId: '',
                        Calle: '',
                        Numero: '',
                        Colonia: '',
                        CodigoPostal: ''
                    },
                    Vehiculo: {
                        Motor: '',
                        Serie: '',
                        Placas: '',
                        BeneficiarioPreferente: '',
                        RFCBeneficiario: '',
                        AseguradoAdicional: '',
                        Conductor: '',
                        ContratoLoJack: ''
                    },
                    Agencia: {
                        Color: '',
                        ClaveVendedor: '',
                        Vendedor: '',
                        ClienteVip: false,
                        NumContrato: '',
                        InicioContrato: '',
                        FinContrato: '',
                        CorreoAgencia: '',
                        Grupo: '',
                        Distribuidora: '',
                        Integrante: ''
                    },
                    CabeceraCotHeredada: {},
                    CotizarModelHeredada: {},
                    ConfirmedEncontrack: Encontrack
                }


                /**
                         * Se ejecutan peticiones al servidor
                         */
                cargarCotizacion();
                loadComboBox();

                InicializaEmisionMultiple();


                if ($scope.$parent.Informacion.Cliente.Cliente.Cliente.indexOf('OPEN MARKET') > 0) {
                    $scope.isOpenMarkt = true;
                }

                /**
                 * Uso de directivas para seleccionar tipo de persona 
                 */
                $scope.showPersonaFisica = function () {
                    limpiarCampos();
                    $scope.modeloGenero = '';
                    $scope.optPersonaFisica = true;
                    $scope.optPersonaMoral = false;
                    $scope.Generoinhabilitado = false;
                    $scope.InformacionEmitir.Contratante.TipoPersona = "Fisica";
                    $scope.ValidacionInformacion.Contratante.Nombre.Etiqueta = 'Nombre';
                    $scope.ValidacionInformacion.Contratante.FechaNacimiento.Etiqueta = 'Fecha de Nacimiento';
                }
                $scope.showPersonaMoral = function () {
                    limpiarCampos();
                    $scope.modeloGenero = "Empresarial";
                    $scope.generoSel();
                    $scope.optPersonaFisica = false;
                    $scope.optPersonaMoral = true;
                    $scope.Generoinhabilitado = true;
                    $scope.InformacionEmitir.Contratante.TipoPersona = "Moral";
                    $scope.ValidacionInformacion.Contratante.Nombre.Etiqueta = 'Razón Social';
                    $scope.ValidacionInformacion.Contratante.FechaNacimiento.Etiqueta = 'Fecha de Constitución.';
                }

                /**
                 * Validacion y desactivacion de campos de acuerdo a la 
                 * Nacionalidad para definir el RFC
                 */

                $scope.showPersonaNacional = function () {
                    if ($scope.InformacionEmitir.Contratante.Nacionalidad === "5945") {
                        if ($scope.InformacionEmitir.Contratante.RFC === "XEXX010101000") {
                            $scope.InformacionEmitir.Contratante.RFC = "";
                            $scope.RFCinhabilitado = false;
                        }
                        // FJQP Informacion Complementaria $scope.optEntidad = false;
                    } else {
                        $scope.InformacionEmitir.Contratante.RFC = "XEXX010101000";
                        $scope.RFCinhabilitado = true;
                        // FJQP Informacion Complementaria $scope.optEntidad = true;
                        // FJQP Informacion Complementaria $scope.InformacionEmitir.Complementaria.EntidadNacimiento = "";
                    }
                }

                /**
                 * Uso de bandera para activar Descripcion del Cargo en gobierno 
                 */
                $scope.showCargoGobierno = function () {
                    if ($scope.InformacionEmitir.Complementaria.MandoGobierno === 33) {
                        $scope.InformacionEmitir.Complementaria.DescripcionCargo = "";
                        // FJQP Informacion Complementaria $scope.optCargoGobierno = false;
                    } else {
                        $scope.InformacionEmitir.Complementaria.DescripcionCargo = "";
                        // FJQP Informacion Complementaria $scope.optCargoGobierno = true;
                    }

                    if ($scope.optPersonaMoral) {
                        if ($scope.InformacionEmitir.ComplementariaMoral.MandoGobiernoMoral === 33) {
                            $scope.InformacionEmitir.ComplementariaMoral.NombreFuncionario = "";
                            //FJQP Informacion Complementaria $scope.optCargoGobierno = false;
                        } else {
                            $scope.InformacionEmitir.ComplementariaMoral.NombreFuncionario = "";
                            //FJQP Informacion Complementaria $scope.optCargoGobierno = true;
                        }
                    }


                }

                /**
                 * Uso de bandera para activar el campo oficina si el docto es federal 
                 */
                $scope.showDoctoFederal = function () {
                    if ($scope.InformacionEmitir.Complementaria.DoctoIdentificacion === 5957 ||
                        $scope.InformacionEmitir.Complementaria.DoctoIdentificacion === 5958) {
                        $scope.optCDoctoFederal = true;
                        $scope.optSDoctoFederal = false;
                    } else {
                        $scope.optCDoctoFederal = false;
                        $scope.optSDoctoFederal = true;
                    }
                }

                $scope.showEntidad = function () {
                    if ($scope.InformacionEmitir.Complementaria.PaisNacimiento === 6115) {
                        // FJQP Informacion Complementaria $scope.optEntidad = false;
                    } else {
                        // FJQP Informacion Complementaria $scope.optEntidad = true;
                        $scope.InformacionEmitir.Complementaria.EntidadNacimiento = "";
                    }

                    if ($scope.optPersonaMoral) {
                        if ($scope.InformacionEmitir.ComplementariaMoral.PaisNacimientoApoderado !== 6115) {
                            // FJQP Informacion Complementaria $scope.optEntidad = true;
                            $scope.InformacionEmitir.ComplementariaMoral.EntidadApoderado = "";
                        } else {
                            // FJQP Informacion Complementaria $scope.optEntidad = false;
                        }
                    }
                }

                $scope.generoSel = function () {
                    $scope.InformacionEmitir.Contratante
                        .Genero = ($scope.modeloGenero === "Masculino")
                        ? 48
                        : ($scope.modeloGenero === "Femenino")
                        ? 49
                        : ($scope.modeloGenero === "Empresarial") ? 2840 : '';
                }

                $scope.motorSel = function () {
                    if ($scope.InformacionEmitir.Vehiculo.Motor === "") {
                        $scope.InformacionEmitir.Vehiculo.Motor = "S/M";
                    }
                }

                $scope.fechaNacimientoSel = function (dt) {
                    $scope.InformacionEmitir.Contratante.FechaNacimiento = dt;
                }

                $scope.inicioContratoSel = function (dt2) {
                    $scope.InformacionEmitir.Agencia.InicioContrato = dt2;
                }

                $scope.fechaApoderadoSel = function (dt3) {
                    $scope.InformacionEmitir.ComplementariaMoral.FechaNacimientoApoderado = dt3;
                }
                /**
                 * Calendario
                 */
                $scope.today = function () {
                    $scope.dt = new Date();
                };

                $scope.clear = function () {
                    $scope.dt = null;
                };

                $scope.inlineOptions = {
                    customClass: getDayClass,
                    minDate: new Date(),
                    showWeeks: true
                };

                $scope.dateOptions = {
                    formatYear: 'yy',
                    maxDate: new Date(2020, 5, 22),
                    minDate: new Date(),
                    startingDay: 1,
                    orientation: "bottom auto"
                };

                $scope.dateOptionsEndDate = {
                    formatYear: 'yy',
                    maxDate: new Date(),
                    minDate: 0,
                    startingDay: 1,
                    orientation: "bottom auto"
                };

                $('#datepicker').datepicker({
                    orientation: 'bottom'
                });

                // Disable weekend selection
                //function disabled(data) {
                //    var date = data.date,
                //        mode = data.mode;
                //    return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
                //}

                $scope.toggleMin = function () {
                    $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
                    $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
                };

                $scope.toggleMin();

                $scope.open1 = function () {
                    $scope.popup1.opened = true;
                };

                $scope.open2 = function () {
                    $scope.popup2.opened = true;
                };

                $scope.open3 = function () {
                    $scope.popup3.opened = true;
                };

                $scope.setDate = function (year, month, day) {
                    $scope.dt = new Date(year, month, day);
                };

                $scope.formats = ['dd/MM/yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
                $scope.format = $scope.formats[0];
                $scope.altInputFormats = ['M!/d!/yyyy'];

                $scope.popup1 = {
                    opened: false
                };

                $scope.popup2 = {
                    opened: false
                };

                $scope.popup3 = {
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



                /*
                 * INDRA - FJQP -- Emisión Múltiple --
                 *
                 * funcion para realizar la validacion de numeros de serie para la pantalla de
                 * captura de informacion variable para emisión multiple 
                 */
                function validaseries() {
                    var vseries = false;
                    var a = 1;
                    var Series = $scope.Vehiculos;

                    for (var i = 0; i < Series.length; i++) {

                        var char = Series[i].sNoSerie;

                        if (char != null && char !== "") {
                            a = a + i;
                            consultaSerieGraba(char, a);
                        } else {
                            $scope.resultadoValidaSerie = null;
                            $scope.ValidacionInformacion.Validos.Serie.Requerido = false;
                        }
                    }

                    return false;
                }

                /*
                 * INDRA - FJQP -- Emisión Múltiple --
                 *
                 * funcion para realizar la validacion de numeros campos vacios para la pantalla de
                 * captura de informacion variable para emisión multiple 
                 */

                function validavacios() {
                    var vacios = false;
                    var cadena = "";

                    var Series = $scope.Vehiculos;
                    var Motores = $scope.Vehiculos;
                    var Placas22 = $scope.Vehiculos;
                    var Contratos = $scope.Vehiculos;

                    var a;
                    var serievacia;
                    a = 0;
                    serievacia = 0;

                    for (var i = 0; i < Series.length; i++) {
                        if (Series[i].sNoSerie === "" || Series[i].sNoSerie === undefined) {
                            serievacia = serievacia + 1;
                        }
                    }

                    if (serievacia >= 1) {
                        cadena = 'Existen ' + serievacia + ' números de serie vacias. \n';
                    } else {
                        var vseries = false;
                        var a = 1;
                        var Series = $scope.Vehiculos;

                        for (var i = 0; i < Series.length; i++) {

                            var char = Series[i].sNoSerie;

                            if (char != null && char !== "") {
                                a = a + i;
                                if (consultaSerieGraba(char, a)) {
                                    cadena = cadena + 'Existe error en el número de serie ' + a + '. \n';
                                    serievacia = 1;
                                }
                                else {
                                }
                            } else {
                                $scope.resultadoValidaSerie = null;
                                $scope.ValidacionInformacion.Validos.Serie.Requerido = false;
                            }
                        }
                    }

                    var a;
                    var motorvacia;
                    a = 0;
                    motorvacia = 0;

                    for (var i = 0; i < Motores.length; i++) {
                        if (Motores[i].sNoMotor == "") {
                            motorvacia = motorvacia + 1;
                        }
                    }

                    if (motorvacia >= 1) {
                        cadena = cadena + 'Existen ' + motorvacia + ' números de motor vacios. \n';
                    }

                    var a;
                    var placavacia;
                    a = 0;
                    placavacia = 0;

                    for (var i = 0; i < Placas22.length; i++) {
                        if (Placas22[i].sPlacas == "") {
                            placavacia = placavacia + 1;
                        }

                    }

                    if (placavacia >= 1) {
                        cadena = cadena + 'Existen ' + placavacia + ' números de placa vacios. \n';
                    }

                    var a;
                    var contratovacia;
                    a = 0;
                    contrato = 0;

                    for (var i = 0; i < Contratos.length; i++) {
                        if (Contratos[i].sContrato == "") {
                            contrato = contrato + 1;
                        }
                    }

                    if (contrato >= 1) {
                        cadena = cadena + 'Existen ' + contrato + ' números de contrato vacios. \n';
                    }

                    if (cadena.length > 0) {

                        alert('Favor de capturar la información requerida : \n' + cadena);
                        vacios = true;
                    }
                    else {
                        vacios = false;

                    }

                    return vacios;

                }

                $scope.validarCorreo = function () {
                    var email = $scope.InformacionEmitir.Contratante.Correo;
                    if (email != null && email !== "") {
                        if (validateEmail(email)) {
                            $scope.resultadoValidaEmail = null;
                            $scope.ValidacionInformacion.Validos.Correo.Requerido = false;
                        } else {
                            $scope.resultadoValidaEmail = email + " no es valido";
                            $scope.ValidacionInformacion.Validos.Correo.Requerido = true;
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

                $scope.validarCURP = function () {
                    var curp = $scope.InformacionEmitir.Complementaria.CURP;
                    if (curp != null && curp !== "") {
                        if (validateCurp(curp)) {
                            $scope.resultadoValidaCURP = null;
                            $scope.ValidacionInformacion.Validos.CURP.Requerido = false;
                        } else {
                            $scope.resultadoValidaCURP = curp + " no es valido";
                            $scope.ValidacionInformacion.Validos.CURP.Requerido = true;
                        }

                        return false;
                    } else {
                        $scope.resultadoValidaCURP = null;
                        $scope.ValidacionInformacion.Validos.CURP.Requerido = false;
                        return false;
                    }
                }

                function validateCurp(curp) {
                    var re = /^.*(?=.{18})(?=.*[0-9])(?=.*[A-ZÑ]).*$/;
                    return re.test(curp);
                }

                function validarCadena(cadena) {
                    return (cadena != null && cadena !== "" && cadena.length > 0);
                }

                $("#txtRFC").keyup(function () {
                    this.value = this.value.toLocaleUpperCase();
                });

                function validarEstructuraRfc() {
                    var response = true;
                    var rfcValue = $scope.InformacionEmitir.Contratante.RFC;
                    var primerApellidoParticularValue = ($scope.InformacionEmitir.Contratante.Paterno).toUpperCase();
                    var segundoApellidoParticularValue = ($scope.InformacionEmitir.Contratante.Materno).toUpperCase();
                    var nombreParticularValue = ($scope.InformacionEmitir.Contratante.Nombre).toUpperCase();
                    var valoresFechaRfc, patronNumeros, soloNumeros, valoresHomoclave, patronLetras, soloLetras;

                    var denominacionValue = ($scope.InformacionEmitir.Contratante.Nombre).toUpperCase();
                    //Validar RFC si es persona fisica
                    if ($scope.optPersonaFisica === true &&
                        rfcValue.length === 13 &&
                        validarCadena(primerApellidoParticularValue) &&
                        validarCadena(nombreParticularValue) &&
                        primerApellidoParticularValue.length >= 2 &&
                        nombreParticularValue.length >= 1
                    ) {
                        var letrasPrimerApellido = primerApellidoParticularValue.substr(0, 1);
                        var letraSegundoApellido = "";
                        var letraNombreParticular = nombreParticularValue.substr(0, 1);

                        var i = 0;
                        for (i = 1; i < primerApellidoParticularValue.length; i++) {
                            var caracter = primerApellidoParticularValue.charAt(i);
                            var patronCaracter = /[AEIOUÄËÏÖÜÁÉÍÓÚ]/;
                            var caracterCorrecto = patronCaracter.test(caracter);
                            if (caracterCorrecto) {
                                letrasPrimerApellido = letrasPrimerApellido + caracter;
                                break;
                            }
                        }
                        var rfc4Letras = "";
                        var validoEnBaseCampos = "";
                        valoresFechaRfc = rfcValue.substr(4, 6);
                        patronNumeros = /[0-9]{6}/;
                        soloNumeros = patronNumeros.test(valoresFechaRfc);

                        valoresHomoclave = rfcValue.substr(10, 3);
                        patronLetras = /[A-Z0-9]{3}/;
                        soloLetras = patronLetras.test(valoresHomoclave);


                        if (validarCadena(segundoApellidoParticularValue) && segundoApellidoParticularValue.length >= 1
                        ) {
                            letraSegundoApellido = segundoApellidoParticularValue.substr(0, 1);
                            rfc4Letras = letrasPrimerApellido + letraSegundoApellido + letraNombreParticular;
                            validoEnBaseCampos = rfcValue.substr(0, 4);
                        } else {
                            rfc4Letras = letrasPrimerApellido + "X" + letraNombreParticular;
                            validoEnBaseCampos = rfcValue.substr(0, 2);
                            validoEnBaseCampos = validoEnBaseCampos + "X" + rfcValue.substr(3, 1);
                        }

                        if (validoEnBaseCampos !== rfc4Letras || !soloNumeros || !soloLetras) {
                            response = false;
                        }

                    } else if ($scope.optPersonaMoral === true &&
                        rfcValue.length === 12 &&
                        denominacionValue.length >= 3) {
                        // Validar RFC si es persona Moral
                        var letrasDenominacion = denominacionValue.substr(0, 3);
                        var letrasRfc = rfcValue.substr(0, 3);

                        valoresFechaRfc = rfcValue.substr(3, 6);
                        patronNumeros = /[0-9]{6}/;
                        soloNumeros = patronNumeros.test(valoresFechaRfc);

                        valoresHomoclave = rfcValue.substr(9, 3);
                        patronLetras = /[A-Z0-9]{3}/;
                        soloLetras = patronLetras.test(valoresHomoclave);


                        if (letrasDenominacion !== letrasRfc || !soloNumeros || !soloLetras) {
                            response = false;
                        }
                    } else {
                        response = false;
                    }
                    return response;
                }

                $scope.validarRFC = function () {
                    var rfc = $scope.InformacionEmitir.Contratante.RFC;
                    if (rfc != null && rfc !== "") {
                        if (validarEstructuraRfc()) {
                            $scope.resultadoValidaRFC = null;
                            $scope.ValidacionInformacion.Validos.RFC.Requerido = false;
                        } else {
                            $scope.resultadoValidaRFC = rfc + " no es valido";
                            $scope.ValidacionInformacion.Validos.RFC.Requerido = true;
                        }

                        return false;
                    } else {

                        return false;
                    }
                }

                $scope.validarRFCBeneficiario = function () {
                    var rfc = $scope.InformacionEmitir.Vehiculo.RFCBeneficiario;
                    if (rfc != null && rfc !== "") {
                        if (validateRfc(rfc)) {
                            $scope.resultadoValidaRFCBeneficiario = null;
                            $scope.ValidacionInformacion.Validos.RFCBeneficiario.Requerido = false;
                        } else {
                            $scope.resultadoValidaRFCBeneficiario = rfc + " no es valido";
                            $scope.ValidacionInformacion.Validos.RFCBeneficiario.Requerido = true;
                        }
                        return false;
                    } else {
                        $scope.resultadoValidaRFCBeneficiario = null;
                        $scope.ValidacionInformacion.Validos.RFCBeneficiario.Requerido = false;
                        return false;
                    }
                }

                function validateRfc(rfc) {
                    var re = /^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$/;
                    return re.test(rfc);
                }

                $scope.validaCaracteres = function () {
                    var char = $scope.InformacionEmitir.Vehiculo.Serie;

                    if (char != null && char !== "") {
                        consultaSerie(char);
                    } else {
                        $scope.resultadoValidaSerie = null;
                        $scope.ValidacionInformacion.Validos.Serie.Requerido = false;
                    }
                    return false;
                }

                $scope.validaCaracteresMultiple = function (index) {

                    var Vehiculo = $scope.Vehiculos;

                    var char = Vehiculo[index].sNoSerie;

                    if (char != null && char !== "") {
                        consultaSerie(char);
                    } else {
                        $scope.resultadoValidaSerie = null;
                        $scope.ValidacionInformacion.Validos.Serie.Requerido = false;
                        $scope.ValidaCapMultiple = false;
                    }
                    return false;

                }



                /**********************************************************
                 * Funcion para cargar los datos iniciales de la cotización 
                 * Con el formato de Scope.Information
                 ***********************************************************/
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
                                $scope.$parent.hayDatosPanel = response.Panel.Coberturas.length > 0;
                                $scope.$parent.Informacion = response;
                                $scope.$parent.listaAdaptaciones = response.Panel.Coberturas;
                                $scope.dt2 = new Date($scope.$parent.Informacion.Cotizacion.InicioVigencia.toString());
                                $scope.inicioContratoSel($scope.dt2);
                                precargaInformacionCliente();
                                cargaValidacionConductor();
                                cargaValidacionPanel();

                                if ($scope.$parent.Informacion.Cliente.TipoArrendamiento != null ||
                                    $scope.$parent.Informacion.Cliente.TipoArrendamiento !== '') {
                                    cargaValidacionBeneficiario();
                                }
                                cargarAsegPaquete();

                                $scope.PermiteEmisionMultiple = false;
                                $scope.PermiteContrato = false;

                                configRequestModel = {
                                    Aseguradora: "222",
                                    Producto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                    Perfil: $sessionStorage.sessionperfilId,
                                    Usuario: $sessionStorage.sessionpersonaid,
                                }

                                var url = mappingService.services['emitir']['emitir']['permiteConfigMultiple'];
                                $http.post(url, configRequestModel)
                                     .then(function success(dataee) {
                                         if (dataee.data.Response.length == 0) {
                                             $scope.PermiteEmisionMultiple = false;
                                             $scope.PermiteContrato = false;
                                         }
                                         if (dataee.data.Response.length > 0) {
                                             if (dataee.data.Response[0].iPermiteEmisionMultiple == 1) {
                                                 $scope.PermiteEmisionMultiple = true;
                                             }
                                             else {
                                                 $scope.PermiteEmisionMultiple = false;
                                             }
                                             if (dataee.data.Response[0].iPermiteContrato == 1) {
                                                 $scope.PermiteContrato = true;
                                             }
                                             else {
                                                 $scope.PermiteContrato = false;
                                             }

                                         }
                                     },
                                          function error(data) {
                                              $scope.PermiteEmisionMultiple = false;
                                              $scope.PermiteContrato = false;
                                          });
                            },
                            function errorFunction(response) {
                                console.log("Hubo un error" + response);
                                $scope.PermiteEmisionMultiple = false;
                            },
                            function badRequestFunction() { });
                    }

                };

                /**
                 * Funcion para hacer la carga inicial de todos los combo box
                 */

                function precargaInformacionCliente() {


                    try {
                        solicitud = {
                            solicitudRegla: {
                                IdRegla: 6622,
                                IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                                IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                TipoArrendamiento: $scope.$parent.Informacion.Cliente.TipoArrendamiento.Nombre
                            }
                        }
                    }
                    catch (err) {
                        solicitud = {
                            solicitudRegla: {
                                IdRegla: 6622,
                                IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                                IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                TipoArrendamiento: "ARRENDAMIENTO PURO"
                            }
                        }

                        if ($scope.InformacionEmitir.Contratante.TipoPersona === "MORAL") {
                            $scope.showPersonaMoral();
                        } else {
                            $scope.showPersonaFisica();
                        }

                        if ($scope.$parent.Informacion.Cliente.Cotizante != undefined) {
                            $scope.InformacionEmitir.Contratante.TipoPersona = $scope.$parent.Informacion.Cliente.Cotizante.TipoPersona;
                            $scope.InformacionEmitir.Contratante.Nombre = $scope.$parent.Informacion.Cliente.Cotizante.Nombre;
                            $scope.InformacionEmitir.Contratante.Paterno = $scope.$parent.Informacion.Cliente.Cotizante.ApellidoP;
                            $scope.InformacionEmitir.Contratante.Materno = $scope.$parent.Informacion.Cliente.Cotizante.ApellidoM;

                            $scope.InformacionEmitir.Contratante.CorreoElectronico = $scope.$parent.Informacion.Cliente.Cotizante.CorreoElectronico;
                            $scope.InformacionEmitir.Contratante.Telefono = $scope.$parent.Informacion.Cliente.Cotizante.Telefono;
                        }
                        $scope.ExistePrecarga = false;

                    }

                    requestService.post('emitir', // Modulo
                        'emitir', // Controlador
                        'consultaInformacionCliente', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            if (response.length > 0) {
                                if (response[0].Contratante.TipoPersona === "MORAL") {
                                    $scope.showPersonaMoral();
                                } else {
                                    $scope.showPersonaFisica();
                                }

                                response[0].Contratante.TipoPersona = $scope.InformacionEmitir.Contratante.TipoPersona;
                                $scope.InformacionEmitir.Contratante = response[0].Contratante;
                                $scope.InformacionEmitir.Direccion = response[0].Direccion;
                                $scope.dt = new Date($scope.InformacionEmitir.Contratante.FechaNacimiento);
                                $scope.Nacionalidad = $scope.InformacionEmitir.Contratante.Nacionalidad.Nombre;
                                $scope.InformacionEmitir.Contratante.Nacionalidad = $scope.InformacionEmitir.Contratante.Nacionalidad.ElementoId;
                                $scope.ExistePrecarga = true;
                                $scope.InformacionEmitir.Direccion.ValidaEstado = false;
                            } else {
                                if ($scope.$parent.Informacion.Cotizacion.CP.CodigoPostal !== null) {
                                    $scope.habilitaBusquedaCP = true;

                                    $scope.habilitaBusquedaCP = false;
                                    $scope.ExistePrecarga = false;

                                    var codigoPostal = $scope.$parent.Informacion.Cotizacion.CP;
                                    $scope.InformacionEmitir.Direccion.Pais = codigoPostal.Pais;
                                    $scope.InformacionEmitir.Direccion.Estado = codigoPostal.Estado;
                                    $scope.InformacionEmitir.Direccion.EstadoId = codigoPostal.EstadoId;
                                    $scope.InformacionEmitir.Direccion.Delegacion = codigoPostal.Delegacion;
                                    $scope.InformacionEmitir.Direccion.Colonia = codigoPostal.Colonia;
                                    $scope.InformacionEmitir.Direccion.CodigoPostal = codigoPostal.CodigoPostal;
                                    $scope.Colonia = codigoPostal.Colonia;
                                    $scope.CodigoPostal = codigoPostal.CodigoPostal;
                                    $scope.bndDatosRegion = false;
                                    $scope.InformacionEmitir.Direccion.ValidaEstado = true;
                                    $scope.habilitaBusquedaCP = true;
                                } else {
                                    $scope.habilitaBusquedaCP = false;
                                    $scope.bndDatosRegion = true;
                                }
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /**
                 * Funcion para hacer la carga inicial de todos los combo box
                 */
                function loadComboBox() {
                    requestService.post('emitir',
                        'emitir',
                        'cargaInicial',
                        null, // Se indica que no hay parametros que pasar
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.elementos = response;
                            $scope.NacionalidadList = response.NacionalidadList;
                            $scope.GeneroList = response.GeneroList;
                            $scope.PaisNacimientoList = response.PaisNacimientoList;
                            $scope.EntidadNacimientoList = response.EntidadNacimientoList;
                            $scope.DoctoIdentifiacionList = response.DoctoIdentifiacionList;
                            $scope.NumDoctoList = response.NumDoctoList;
                            $scope.ProfesionList = response.ProfesionList;
                            $scope.OcupacionList = response.OcupacionList;
                            $scope.GiroNegocioList = response.GiroNegocioList;
                            $scope.MandoEnGobiernoList = response.MandoEnGobiernoList;
                            $scope.RegimenFiscalEmpresarialList = response.RegimenFiscalEmpresarialList;
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                };

                $scope.obtenerRegion = function () {
                    // Valida si el campo CodigoPostal es de tipo indefinido
                    if ($scope.CodigoPostal === undefined) {
                        // Inicializa el valor en vacio
                        $scope.CodigoPostal = '';
                        // De lo contrario:
                    }

                    // Valida si el campo Colonia es de tipo indefinido
                    if ($scope.Colonia === undefined) {
                        // Inicializa el valor en vacio
                        $scope.Colonia = '';
                        // De lo contrario:
                    }

                    // Valida si los campos [Codigo Postal y Colonia] no fueron capturados:
                    if (($scope.CodigoPostal === '' && $scope.Colonia === '') ||
                        ($scope.CodigoPostal === null && $scope.Colonia === null)) {
                        $scope.styleModalInfo = ''; // Inicializa estilo de la modal de "Busqueda por C.P y Colonia"
                        $scope.requeridos = 'C\u00F3digo Postal o Colonia'; // Indica leyenda de campos requeridos
                        $scope.modalRequeridos = true; // Muestra modal "Requeridos"
                        // De lo contrario:
                    } else {
                        // Valida si la pantalla modal esta oculta [False], con la finalidad de no estar haciendo llamados inecesarios al Back
                        if ($scope.modalBuscar === false) {
                            $scope.styleModalInfo = 'Info'; // Asigna estilo a la modal de "Busqueda por C.P y Colonia"
                            // Mapea campo "Codigo Postal" Vs "CodigoPostal" del JSON 

                            codigoPostal = {
                                CodigoPostalModel: {
                                    CodigoPostal: $scope.CodigoPostal,
                                    Colonia: $scope.Colonia
                                }
                            }

                            //// Mapea campo "Colonia" Vs "Colonia" del JSON 
                            requestService.post('cotizador',
                                'cotizacion',
                                'consultarCodigoPostal',
                                codigoPostal,
                                true,
                                true,
                                true,
                                function successFunction(response) {
                                    // Valida si obtuvo registros
                                    if (response.length === 0) {
                                        $scope.styleModalInfo = '';
                                        // Indica leyenda sin resultados
                                        $scope.resultado = 'No se encontraron coincidencias en la b\u00FAsqueda';
                                        // Muestra modal "Sin Resultados"
                                        $scope.modalSinResultados = true;
                                        // De lo contrario:
                                    } else {
                                        $scope.modalBuscar = true; // Mustra la modal "Busqueda por C.P y Colonia"
                                        $scope.regiones = []; // Inicializa lista regiones
                                        // Obtiene lista de acuerdo al resultado de la consulta que se ejecuto
                                        $scope.regiones = response;
                                    }
                                },
                                function errorFunction() { },
                                function badRequestFunction() { });
                            // De lo contrario:
                        } else {
                            $scope.modalBuscar = false; // Oculta pantalla modal "Busqueda por C.P. y  Colonia"
                            $scope.styleModalInfo = ''; // Inicializa estilo de la modal de "Busqueda por C.P y Colonia"
                        }
                    }
                };

                /**
                 **  Funcion para cargar la regla [6213] y obtener la validacion de productos articulos 140
                 **/
                function cargaValidacionPanel() {
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 6213,
                            //IdProducto: 339
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
                            $scope.validacionInfoComplementaria = response;
                            if (response.length === 0) {
                                $scope.optInfoComplementaria = false;
                            } else {
                                if ($scope.validacionInfoComplementaria[0].Valor === 1 || $scope.validacionInfoComplementaria[0].Valor === "1") {
                                    $scope.optInfoComplementaria = true;
                                }
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /**
                 **  Funcion para cargar la regla [6539] y validar el campo vendedor
                 **/
                //function cargaValidacionVendedor() {
                //    solicitud = {
                //        solicitudRegla: {
                //            IdRegla: 6539,
                //            IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                //            AseguradoraId: 222
                //        }
                //    }

                //    requestService.post('cotizador', // Modulo
                //        'cotizacion', // Controlador
                //        'consultaReglaNegocio', // Accion
                //        solicitud, // Parametros
                //        true, // Bloquear Interfaz/Vista
                //        true, // Mostrar errores
                //        true, // es "SingleResponse"
                //        function successFunction(response) {
                //            $scope.validacionElemento = response;
                //            if ($scope.validacionElemento.length === 0) {
                //                $scope.optVendedorNoObligatorio = true;
                //                $scope.optVendedorObligatorio = false;
                //            } else {
                //                if ($scope.validacionElemento[0].Valor == 1) {
                //                    $scope.optVendedorObligatorio = true;
                //                    $scope.optVendedorNoObligatorio = false;
                //                }
                //            }
                //        },
                //        function errorFunction() {},
                //        function badRequestFunction() {});
                //}

                /**
                 **  Funcion para cargar la regla [6618] y validar el campo Conductor
                 **/
                function cargaValidacionConductor() {
                    solicitud = {
                        solicitudRegla: {
                            IdRegla: 6618,
                            IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
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
                            $scope.validacionElemento = response[0];
                            if ($scope.validacionElemento === "" ||
                                $scope.validacionElemento == null ||
                                $scope.validacionElemento == undefined) {
                                $scope.optConductorObligatorio = !$scope.optConductorObligatorio;
                                $scope.optConductorNoObligatorio = !$scope.optConductorNoObligatorio;
                            } else {
                                if ($scope.validacionElemento.Valor === 1) {
                                    $scope.optConductorObligatorio = !$scope.optConductorObligatorio;
                                    $scope.optConductorNoObligatorio = !$scope.optConductorNoObligatorio;
                                }
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /**
                 **  Funcion para cargar la regla [2630] para obtener beneficiario preferente de acuerdo al cliente y producto
                 **/
                function cargaValidacionBeneficiario() {

                    try {
                        solicitud = {
                            solicitudRegla: {
                                IdRegla: 2630,
                                IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                                IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                TipoArrendamiento: $scope.$parent.Informacion.Cliente.TipoArrendamiento.Nombre
                            }
                        }
                    }
                    catch (err) {
                        solicitud = {
                            solicitudRegla: {
                                IdRegla: 2630,
                                IdCliente: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                                IdProducto: $scope.$parent.Informacion.Cliente.Producto.ProductoId,
                                TipoArrendamiento: ""
                            }
                        }
                        $scope.bndBeneficiario = false;
                    }

                    requestService.post('cotizador', // Modulo
                        'cotizacion', // Controlador
                        'consultaReglaNegocio', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            var beneficiario = response[0];

                            if (response.length > 0) {
                                $scope.BeneficiarioPreferente = beneficiario.Valor;
                                if (beneficiario.ValorId === '1') {
                                    $scope.bndBeneficiario = true;
                                } else {
                                    $scope.bndBeneficiario = false;
                                }
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                function deleteItemList(array, item) {
                    var index = array.indexOf(item);
                    if (index !== -1)
                        array.splice(index, 1);
                }

                $rootScope.submitEmi = function () {
                    var i;
                    $rootScope.startBlockUI.start = 'Espere... Emitiendo Pólizas';
                    $scope.requeridosContratanteList = [];
                    $scope.requeridosComplementariaList = [];
                    $scope.requeridosComplementariaMoralList = [];
                    $scope.requeridosValidosList = [];
                    $scope.requeridosDireccionList = [];
                    $scope.requeridosAgenciaList = [];
                    $scope.requeridosVehiculoList = [];

                    for (i in $scope.ValidacionInformacion.Validos) {
                        if ($scope.ValidacionInformacion.Validos.hasOwnProperty(i)) {
                            if ($scope.ValidacionInformacion.Validos[i].Requerido === true) {
                                $scope.requeridosValidosList.push($scope.ValidacionInformacion.Validos[i].Etiqueta);
                                $scope.showValidosList = true;
                            } else {
                                $scope.ValidacionInformacion.Validos[i].Requerido = false;
                            }
                        }
                    }

                    for (i in $scope.ValidacionInformacion.Direccion) {
                        if ($scope.ValidacionInformacion.Direccion.hasOwnProperty(i)) {
                            if ($scope.InformacionEmitir.Direccion[i] == null ||
                                $scope.InformacionEmitir.Direccion[i] == undefined ||
                                $scope.InformacionEmitir.Direccion[i] === "") {
                                $scope.ValidacionInformacion.Direccion[i].Requerido = true;
                                $scope.requeridosDireccionList.push($scope.ValidacionInformacion.Direccion[i].Etiqueta);
                            } else {
                                $scope.ValidacionInformacion.Direccion[i].Requerido = false;
                            }
                        }
                    }

                    for (i in $scope.ValidacionInformacion.Contratante) {
                        if ($scope.ValidacionInformacion.Contratante.hasOwnProperty(i)) {
                            if ($scope.InformacionEmitir.Contratante[i] == null ||
                                $scope.InformacionEmitir.Contratante[i] == undefined ||
                                $scope.InformacionEmitir.Contratante[i] === "") {
                                $scope.ValidacionInformacion.Contratante[i].Requerido = true;
                                $scope.requeridosContratanteList
                                    .push($scope.ValidacionInformacion.Contratante[i].Etiqueta);
                            } else {
                                $scope.ValidacionInformacion.Contratante[i].Requerido = false;
                            }
                        }
                    }

                    for (i in $scope.ValidacionInformacion.Vehiculo) {
                        if ($scope.ValidacionInformacion.Vehiculo.hasOwnProperty(i)) {
                            if (i !== "Serie") {
                                if ($scope.InformacionEmitir.Vehiculo[i] == null ||
                                    $scope.InformacionEmitir.Vehiculo[i] == undefined ||
                                    $scope.InformacionEmitir.Vehiculo[i] === "") {
                                    $scope.ValidacionInformacion.Vehiculo[i].Requerido = true;
                                    $scope.requeridosVehiculoList
                                        .push($scope.ValidacionInformacion.Vehiculo[i].Etiqueta);
                                } else {
                                    $scope.ValidacionInformacion.Vehiculo[i].Requerido = false;
                                }
                            } else {
                                if ($scope.InformacionEmitir.Vehiculo[i] == null ||
                                    $scope.InformacionEmitir.Vehiculo[i] == undefined ||
                                    $scope.InformacionEmitir.Vehiculo[i] == "") {
                                    $scope.ValidacionInformacion.Vehiculo[i].Requerido = true;
                                    $scope.requeridosVehiculoList
                                        .push($scope.ValidacionInformacion.Vehiculo[i].Etiqueta);
                                } else {
                                    if (serieRequerida) {
                                        $scope.ValidacionInformacion.Vehiculo[i].Requerido = serieRequerida;
                                        $scope.requeridosVehiculoList
                                            .push($scope.ValidacionInformacion.Vehiculo[i].Etiqueta + " (Invalida)");
                                    } else {
                                        $scope.ValidacionInformacion.Vehiculo[i].Requerido = false;
                                    }
                                }

                            }
                        }
                    }

                    //* INDRA FJQP Contratos
                    if ($scope.PermiteContrato == true && $scope.isOpenMarkt == false) {
                        $scope.inicioContratoSel($scope.dt2);
                        for (i in $scope.ValidacionInformacion.Agencia) {
                            if ($scope.ValidacionInformacion.Agencia.hasOwnProperty(i)) {
                                if ($scope.InformacionEmitir.Agencia[i] == null ||
                                    $scope.InformacionEmitir.Agencia[i] == undefined ||
                                    $scope.InformacionEmitir.Agencia[i] === "") {
                                    $scope.ValidacionInformacion.Agencia[i].Requerido = true;
                                    $scope.requeridosAgenciaList.push($scope.ValidacionInformacion.Agencia[i].Etiqueta);
                                } else {
                                    $scope.ValidacionInformacion.Agencia[i].Requerido = false;
                                }
                            }
                        }
                    }


                    if ($scope.optInfoComplementaria && $scope.optPersonaFisica) {
                        for (i in $scope.ValidacionInformacion.Complementaria) {
                            if ($scope.ValidacionInformacion.Complementaria.hasOwnProperty(i)) {
                                if ($scope.InformacionEmitir.Complementaria[i] == null ||
                                    $scope.InformacionEmitir.Complementaria[i] == undefined ||
                                    $scope.InformacionEmitir.Complementaria[i] === "") {
                                    $scope.ValidacionInformacion.Complementaria[i].Requerido = true;
                                    $scope.requeridosComplementariaList
                                        .push($scope.ValidacionInformacion.Complementaria[i].Etiqueta);
                                } else {
                                    $scope.ValidacionInformacion.Complementaria[i].Requerido = false;
                                }
                            }
                        }
                    }

                    if ($scope.optInfoComplementaria && $scope.optPersonaMoral) {
                        for (i in $scope.ValidacionInformacion.ComplementariaMoral) {
                            if ($scope.ValidacionInformacion.ComplementariaMoral.hasOwnProperty(i)) {
                                if ($scope.InformacionEmitir.ComplementariaMoral[i] == null ||
                                    $scope.InformacionEmitir.ComplementariaMoral[i] == undefined ||
                                    $scope.InformacionEmitir.ComplementariaMoral[i] === "") {
                                    $scope.ValidacionInformacion.ComplementariaMoral[i].Requerido = true;
                                    $scope.requeridosComplementariaMoralList
                                        .push($scope.ValidacionInformacion.ComplementariaMoral[i].Etiqueta);
                                } else {
                                    $scope.ValidacionInformacion.ComplementariaMoral[i].Requerido = false;
                                }
                            }
                        }
                    }

                    if ($scope.optPersonaFisica !== true) {
                        $scope.ValidacionInformacion.Contratante.Paterno.Requerido = false;
                        $scope.ValidacionInformacion.Contratante.Genero.Requerido = false;
                        deleteItemList($scope.requeridosContratanteList, "Paterno");
                        deleteItemList($scope.requeridosContratanteList, "Materno");
                        $scope.InformacionEmitir.Complementaria.SerieFiel = $scope.InformacionEmitir.ComplementariaMoral
                            .SerieFIELMoral;
                        $scope.InformacionEmitir.Complementaria.DoctoIdentificacion = $scope.InformacionEmitir
                            .ComplementariaMoral.DoctoIdentificacionMoral;
                        $scope.InformacionEmitir.Complementaria.NumDocto = $scope.InformacionEmitir.ComplementariaMoral
                            .NumDoctoMoral;
                        $scope.InformacionEmitir.Complementaria.MandoGobierno = $scope.InformacionEmitir
                            .ComplementariaMoral
                            .MandoGobiernoMoral;
                    }
                    if ($scope.RFCinhabilitado) {
                        $scope.ValidacionInformacion.Contratante.RFC.Requerido = false;
                        deleteItemList($scope.requeridosContratanteList, "RFC");
                        deleteItemList($scope.requeridosValidosList, "RFC");
                    }

                    if ($scope.Generoinhabilitado) {
                        $scope.ValidacionInformacion.Contratante.Genero.Requerido = false;
                        deleteItemList($scope.requeridosContratanteList, "Género");
                    }
                    if ($scope.optCargoGobierno) {
                        $scope.ValidacionInformacion.Complementaria.DescripcionCargo.Requerido = false;
                        deleteItemList($scope.requeridosComplementariaList, "Descripción del cargo");

                        $scope.ValidacionInformacion.ComplementariaMoral.NombreFuncionario.Requerido = false;
                        deleteItemList($scope.requeridosComplementariaMoralList, "Nombre Funcionario");
                    }

                    if ($scope.optSDoctoFederal) {
                        $scope.ValidacionInformacion.Complementaria.OficinaExpide.Requerido = false;
                        deleteItemList($scope.requeridosComplementariaList, "Oficina");
                    }

                    if ($scope.optVendedorNoObligatorio) {
                        $scope.ValidacionInformacion.Agencia.Vendedor.Requerido = false;
                        deleteItemList($scope.requeridosAgenciaList, "Vendedor");
                    }

                    if ($scope.optConductorNoObligatorio) {
                        $scope.ValidacionInformacion.Vehiculo.Conductor.Requerido = false;
                        deleteItemList($scope.requeridosVehiculoList, "Conductor");
                    }

                    if ($scope.optEntidad) {
                        $scope.ValidacionInformacion.Complementaria.EntidadNacimiento.Requerido = false;
                        deleteItemList($scope.requeridosComplementariaList, "Entidad Federativa de Nacimiento");

                        $scope.ValidacionInformacion.ComplementariaMoral.EntidadApoderado.Requerido = false;
                        deleteItemList($scope.requeridosComplementariaMoralList, "Entidad de Nacimiento del Apoderado");
                    }

                    $scope.showContratanteList = ($scope.requeridosContratanteList != "");
                    $scope.showHComplementariaList = ($scope.requeridosComplementariaList != "");
                    $scope.showHComplementariaMoralList = ($scope.requeridosComplementariaMoralList != "");
                    $scope.showDireccionList = ($scope.requeridosDireccionList != "");
                    $scope.showValidosList = ($scope.requeridosValidosList != "");
                    $scope.showAgenciaList = ($scope.requeridosAgenciaList != "");
                    $scope.showVehiculoList = ($scope.requeridosVehiculoList != "");

                    if ($scope.showContratanteList !== false ||
                        $scope.showHComplementariaList !== false ||
                        $scope.showDireccionList !== false ||
                        $scope.showAgenciaList !== false ||
                        $scope.showVehiculoList !== false ||
                        $scope.showHComplementariaMoralList !== false) {
                        $scope.modalRequeridosList = true;
                    } else {
                        var key;

                        $scope.InformacionEmitir.DatosComplementarios = {};

                        for (key in $scope.InformacionEmitir.Complementaria)
                            if ($scope.InformacionEmitir.Complementaria.hasOwnProperty(key))
                                $scope.InformacionEmitir.DatosComplementarios[key] = $scope.InformacionEmitir
                                    .Complementaria[key];
                        for (key in $scope.InformacionEmitir.ComplementariaMoral)
                            if ($scope.InformacionEmitir.ComplementariaMoral.hasOwnProperty(key))
                                $scope.InformacionEmitir.DatosComplementarios[key] = $scope.InformacionEmitir
                                    .ComplementariaMoral[key];

                        /*
                        * INDRA - FJQP -- Emisión Múltiple --
                        *
                        * en base a la seleccion si es emisión normal o el cliente selecciono
                        * la opcion de captura de informacion emisión multiple 
                        */

                        if ($scope.EsNormal) {
                            $scope.saveData();
                        } else {
                            $scope.saveDataMultiple();
                            $rootScope.startBlockUI.start = 'Espere...';
                        }

                    }
                }

                function limpiarCampos() {
                    $scope.InformacionEmitirLimpiar = {
                        Solicitud: {
                            SolicitudId: $stateParams.idSolicitud,
                            Numero: $stateParams.numero,
                            CotizacionId: $stateParams.idCotizacion,
                            InfoComplementaria: $scope.optInfoComplementaria,
                            TipoArrendamiento: ''
                        },
                        Contratante: {
                            PersonaId: '',
                            TipoPersona: '',
                            Nombre: '',
                            Paterno: '',
                            Materno: '',
                            FechaNacimiento: '',
                            RFC: '',
                            Nacionalidad: '',
                            Genero: '',
                            CorreoElectronico: '',
                            Telefono: '',
                            Telefono2: ''
                        },
                        DatosComplementarios: {},
                        Complementaria: {
                            PaisNacimiento: '',
                            EntidadNacimiento: '',
                            DoctoIdentificacion: '',
                            NumDocto: '',
                            OficinaExpide: '',
                            CURP: '',
                            SerieFiel: '',
                            Profesion: '',
                            Ocupacion: '',
                            GiroNegocio: '',
                            MandoGobierno: '',
                            DescripcionCargo: '',
                            RegimenFiscal: ''
                        },
                        ComplementariaMoral: {
                            ActividadPrincipal: '',
                            SerieFIELMoral: '',
                            DoctoIdentificacionMoral: '',
                            NumDoctoMoral: '',
                            ApoderadoLegal: '',
                            FolioMercatil: '',
                            EstructuraCoorporativa: '',
                            InformacionAccionistas: '',
                            GiroNegocioMoral: '',
                            ObjetivoSocial: '',
                            MandoGobiernoMoral: '',
                            NombreFuncionario: '',
                            NombreApoderadoLegal: '',
                            FechaNacimientoApoderado: '',
                            EntidadApoderado: ''
                        },
                        Direccion: $scope.InformacionEmitir.Direccion,
                        Vehiculo: {
                            Motor: '',
                            Serie: '',
                            Placas: '',
                            BeneficiarioPreferente: '',
                            RFCBeneficiario: '',
                            AseguradoAdicional: '',
                            Conductor: '',
                            ContratoLoJack: ''
                        },
                        Agencia: {
                            Color: '',
                            ClaveVendedor: '',
                            Vendedor: '',
                            ClienteVip: false,
                            NumContrato: '',
                            InicioContrato: '',
                            FinContrato: '',
                            CorreoAgencia: '',
                            Grupo: '',
                            Distribuidora: '',
                            Integrante: ''
                        },
                        CabeceraCotHeredada: {},
                        CotizarModelHeredada: {},
                        ConfirmedEncontrack: Encontrack,
                    }
                    $scope.resultadoValidaEmail = null;
                    $scope.resultadoValidaCURP = null;
                    $scope.resultadoValidaRFC = null;
                    $scope.resultadoValidaRFCBeneficiario = null;
                    $scope.resultadoValidaSerie = null;
                    $scope.InformacionEmitir = $scope.InformacionEmitirLimpiar;
                }


                /*
                * INDRA - FJQP -- Emisión Múltiple --
                *
                * se agrega al final de de la llamada a la pantalla de impresión 
                * el valor de la variable que indica si el usuario selecciono 
                * la opcion de emisión multiple o emisión normal
                */

                $scope.saveData = function () {
                    $scope.generoSel();
                    $scope.InformacionEmitir.Vehiculo.BeneficiarioPreferente = $scope.BeneficiarioPreferente;
                    if ($scope.$parent.Informacion.Cliente.TipoArrendamiento != undefined) {
                        $scope.InformacionEmitir.Solicitud.TipoArrendamiento = $scope.$parent.Informacion.Cliente.TipoArrendamiento.IdInterno;
                    }
                    else {
                        $scope.InformacionEmitir.Solicitud.TipoArrendamiento = 0;
                    }
                    var informacion = JSON.parse(angular.toJson($scope.InformacionEmitir));

                    requestService.post('emitir', // Modulo
                        'emitir', // Controlador
                        'crearEmision', // Accion
                        informacion, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            if (response[0] != undefined &&
                                response[0] !== '' &&
                                $stateParams.numero != undefined &&
                                $stateParams.numero !== '') {
                                $location.url('/cotizadorgen/imprimir/' +
                                    response[0].NeIncisosEndoso.Poliza +
                                    ',' +
                                    response[0].NeIncisosEndoso.Inciso +
                                    ',' +
                                    response[0].NeIncisosEndoso.Endoso +
                                    ',' +
                                    $scope.$parent.idSolicitudR +
                                    ',' +
                                    $stateParams.numero +
                                    ',' +
                                    $scope.EsNormal); // INDRA FJQP Emision Multiple
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /*
              * INDRA - FJQP -- Emisión Múltiple --
              *
              * Funcion que implemewnto para el almacenamiento de la informacion
              * cuando el usuario selecciono la opción de emisión multiple 
              */
                $scope.saveDataMultiple = function () {
                    $scope.generoSel();
                    $scope.InformacionEmitir.Vehiculo.BeneficiarioPreferente = $scope.BeneficiarioPreferente;
                    $scope.InformacionEmitir.Solicitud.TipoArrendamiento = $scope.$parent.Informacion
                        .Cliente.TipoArrendamiento.IdInterno;

                    $scope.InformacionEmitir.CabeceraCotHeredada = $sessionStorage.CabeceraCotSession;
                    $scope.InformacionEmitir.CotizarModelHeredada = $sessionStorage.CotizarModelHeredada;

                    var informacion = JSON.parse(angular.toJson($scope.InformacionEmitir));

                    requestService.post('emitir', // Modulo
                        'emitir', // Controlador
                        'crearEmisionMultiple', // Accion
                        informacion, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {

                            var Datos = {
                                idNoCotizacion: $scope.InformacionEmitir.Solicitud.SolicitudId,
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
                                iEndoso: 0
                            };
                            var informacionD = JSON.parse(angular.toJson(Datos));
                            requestService.post('emitir', // Modulo
                                'Emisionmultiple', // Controlador
                                'getrecord', // Accion
                                informacionD, // Parametros
                              true, // Bloquear Interfaz/Vista
                              true, // Mostrar errores
                              true, // es "SingleResponse"
                              function successFunction(response) {

                                  $scope.regdataTermina2 = angular.copy(response);
                                  $scope.divTerminaProceso = false;
                                  $scope.divTerminaProceso2 = true;
                                  $scope.$parent.bramaEmit = false;

                                  if (response[0] != undefined &&
                                        response[0] !== '' &&
                                            $stateParams.numero != undefined &&
                                            $stateParams.numero !== '') {
                                      $location.url('/cotizadorgen/imprimir/' +
                                          $scope.regdataTermina2[0].sPolizaQLT +
                                    ',' +
                                    1 +
                                    ',' +
                                    0 +
                                    ',' +
                                    $scope.$parent.idSolicitudR +
                                    ',' +
                                    $stateParams.numero +
                                    ',' +
                                    $scope.EsNormal);
                                  }
                              },
                            function errorFunction(response) {
                                alert('failed' + error);
                            },
                            function badRequestFunction() { });

                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                /*************************************************************************************** 
                 * Funcion para "Cerrar" modal de campos "Requeridos" usando las directivas de angular *
                 ***************************************************************************************/
                $scope.hideRequeridos = function () {
                    //event.preventDefault();
                    $scope.modalRequeridos = !$scope.modalRequeridos; // Ocultamos la modal de campos "Requeridos"
                    $scope.requeridos = ''; // Inicializa el valor del cuerpo del mensaje
                }

                /*************************************************************************************** 
                 * Funcion para "Cerrar" modal de campos "Requeridos" usando las directivas de angular *
                 ***************************************************************************************/
                $scope.hideRequeridosList = function () {
                    //event.preventDefault();
                    $scope.modalRequeridosList = !$scope
                        .modalRequeridosList; // Ocultamos la modal de campos "Requeridos"
                    $scope.requeridos = ''; // Inicializa el valor del cuerpo del mensaje
                }

                /*************************************************************************************** 
                 **** Funcion para "Cerrar" modal "Sin resultados" usando las directivas de angular ****
                 ***************************************************************************************/
                $scope.hideResultados = function () {
                    //event.preventDefault();
                    $scope.modalSinResultados = !$scope
                        .modalSinResultados; // Ocultamos la modal de campos "Sin Resultados"
                    $scope.resultado = ''; // Inicializa el valor del cuerpo del mensaje
                }

                $scope.hideInvalido = function () {
                    //event.preventDefault();
                    $scope.modalInvalido = !$scope.modalInvalido; // Ocultamos la modal de campos "Sin Resultados"
                    $scope.resultado = ''; // Inicializa el valor del cuerpo del mensaje
                }

                /*************************************************************************************** 
                 **** Funcion para "Cerrar" modal "Sin resultados" usando las directivas de angular ****
                 ***************************************************************************************/
                $scope.selectRegion = function (buscar) {
                    if (buscar.Estado === $scope.$parent.Informacion.Cotizacion.Estado.Estado ||
                        buscar.CodigoPostal === $scope.$parent.Informacion.Cotizacion.Estado.Estado) {
                        $scope.bndDatosRegion = false;
                        $scope.InformacionEmitir.Direccion.Pais = buscar.Pais;
                        $scope.InformacionEmitir.Direccion.Estado = buscar.Estado;
                        $scope.InformacionEmitir.Direccion.EstadoId = buscar.EstadoId;
                        $scope.InformacionEmitir.Direccion.Delegacion = buscar.Delegacion;
                        $scope.InformacionEmitir.Direccion.DelegacionId = buscar.DelegacionId;
                        $scope.InformacionEmitir.Direccion.Colonia = buscar.Colonia;
                        $scope.InformacionEmitir.Direccion.CodigoPostal = buscar.CodigoPostal;
                        $scope.modalBuscar = false; // Oculta pantalla modal "Busqueda por C.P. y  Colonia"
                    } else {
                        $scope.modalInvalido = true;
                        $scope.modalBuscar = false;
                    }
                }

                function cargarAsegPaquete() {
                    if ($stateParams.idSolicitud != undefined ||
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

                function consultaSerie(char) {
                    solicitud = {
                        Serie: char
                    }
                    requestService.post('emitir', // Modulo
                        'emitir', // Controlador
                        'consultaSerie', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction() {
                            $scope.resultadoValidaSerie = null;
                            serieRequerida = false;
                            $scope.ValidaCapMultiple = false;
                        },
                        function errorFunction() {
                            $scope.ValidaCapMultiple = true;
                            serieRequerida = true;
                        },
                        function badRequestFunction() { });
                }

                /*
                * INDRA - FJQP -- Emisión Múltiple --
                *
                * Consulta el numero de serie antes de grabar
                * 
                */
                function consultaSerieGraba(char, indice) {
                    solicitud = {
                        Serie: char,
                        IndiceSerie: indice
                    }
                    requestService.post('emitir', // Modulo
                        'emitir', // Controlador
                        'consultaSerieGrab', // Accion
                        solicitud, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            return false;
                        },
                        function errorFunction() {
                            return true;

                        },
                        function badRequestFunction() { });
                }

                /** Emision Multiple **/
                /*
                * INDRA - FJQP -- Emisión Múltiple --
                *
                * para la pantalla de captura de emisión multiple
                * 
                */
                function setModalMaxHeight(element) {
                    this.$element = $(element);
                    this.$content = this.$element.find('.modal-content');
                    var borderWidth = this.$content.outerHeight() - this.$content.innerHeight();
                    var dialogMargin = $(window).width() < 768 ? 20 : 60;
                    var contentHeight = $(window).height() - (dialogMargin + borderWidth);
                    var headerHeight = this.$element.find('.modal-header').outerHeight() || 0;
                    var footerHeight = this.$element.find('.modal-footer').outerHeight() || 0;
                    var maxHeight = contentHeight - (headerHeight + footerHeight);

                    this.$content.css({
                        'overflow': 'hidden'
                    });

                    this.$element
                      .find('.modal-body').css({
                          'max-height': maxHeight,
                          'overflow-y': 'auto'
                      });
                }

                $('.modal').on('show.bs.modal', function () {
                    $(this).show();
                    setModalMaxHeight(this);
                });

                $(window).resize(function () {
                    if ($('.modal.in').length != 0) {
                        setModalMaxHeight($('.modal.in'));
                    }
                });

                /*
              * INDRA - FJQP -- Emisión Múltiple --
              *
              * inicializa informacion para Emisión Multiple
              * 
              */
                function InicializaEmisionMultiple() {


                    $scope.data = [{
                        idNoCotizacion: 0, idNoConsec: 0, sPlacas: "", sNoMotor: "", sNoSerie: "", sContrato: "", iEstatusReg: "", sDescEstatus: "", sCondunctor: "", sSolicitud: "", sQLTS: "", sCotizacion: "", sPolizaQLT: "", iInciso: 0, iEndoso: 0
                    }];

                    $scope.btntext = "Grabar";

                    $scope.Vehiculos = angular.copy($scope.data);
                    $scope.enabledEdit = [];
                    $scope.enabledEdit[$scope.Vehiculos.length - 1] = true;
                    $scope.ContaReg = 1;
                    $scope.Cotiza = $stateParams.idSolicitud;
                    $scope.CotizaQLTS = $stateParams.idCotizacion;
                    $scope.EsNormal = true;
                    $scope.CapNoVehi = false;
                    $scope.SelMultiple = false;
                    $scope.ValidaCapMultiple = false;

                    $scope.PatternValidaSerie = /^[^ioqñIOQÑ´°[<>{}()¡¿\\\/|;:.,\-_\+~!?@#$%^=&*'\""/;`%]*$/;


                    $scope.change = function () {
                        $scope.counter++;
                        if ($scope.confirmed) {
                            $scope.divVehiculo = true;
                            $scope.divVehiculoBtn = true;
                            $scope.requeridoCuantos = true;
                            $scope.Cotiza = $stateParams.idSolicitud;
                            $scope.CotizaQLTS = $stateParams.idCotizacion;
                            $scope.EsNormal = false;
                        }
                        else {
                            $scope.divVehiculo = false;
                            $scope.divVehiculoBtn = false;
                            $scope.requeridoCuantos = false;
                            $scope.Cotiza = $stateParams.idSolicitud;
                            $scope.CotizaQLTS = $stateParams.idCotizacion;
                            $scope.totalreg = "";
                            $stateParams.idCotizacion;
                            $scope.Cotiza = $stateParams.idSolicitud;
                            $scope.CotizaQLTS = $stateParams.idCotizacion;
                            $scope.EsNormal = true;
                        }
                    };

                    $scope.ValidaCuantos = function () {
                        if ($scope.totalreg <= 1 || $scope.totalreg == undefined)
                            $scope.divVehiculo = false;
                        else

                            $scope.divVehiculo = true;

                        var limpios = [];
                        var elementos = {
                            idNoCotizacion: 0, idNoConsec: 0, sPlacas: "", sNoMotor: "", sNoSerie: "", sContrato: "", iEstatusReg: 0, sDescEstatus: "", sCondunctor: "", sSolicitud: "", sQLTS: "", sCotizacion: "", sPolizaQLT: "", iInciso: 0, iEndoso: 0, disableEdit: false
                        };

                        for (var i = 0; i < $scope.totalreg; ++i) {
                            limpios.push(elementos);
                        }

                        var informacionVer = JSON.parse(angular.toJson(limpios));

                        $scope.data = angular.copy(informacionVer);


                        $scope.btntext = "Grabar";

                        $scope.Vehiculos = angular.copy($scope.data);
                        $scope.enabledEdit = [];
                        $scope.enabledEdit[$scope.Vehiculos.length - 1] = true;

                        for (var i = 0; i < $scope.totalreg; ++i) {
                            $scope.enabledEdit[i] = true;
                        }

                        $scope.ContaReg = $scope.totalreg;
                        if ($scope.Vehiculos.length == $scope.totalreg)
                            $scope.dsblBtn = true;

                        $scope.test2.$invalid = false;


                    };

                    $scope.agregaDatosVehiculo = function () {
                        if ($scope.Vehiculos.length > $scope.totalreg) {
                            $scope.dsblBtn = true;
                        }
                        else {

                            var Vehiculo = {
                                idNoCotizacion: 0, sPlacas: "", sNoMotor: "", sNoSerie: "", sContrato: "", iEstatusReg: 0, sDescEstatus: "", sCondunctor: "", sSolicitud: "", sQLTS: "", sCotizacion: "", sPolizaQLT: "", iInciso: 0, iEndoso: 0, disableEdit: false
                            };

                            $scope.Vehiculos.push(Vehiculo);
                            $scope.enabledEdit[$scope.Vehiculos.length - 1] = true;
                            $scope.ContaReg = $scope.Vehiculos.length;
                            if ($scope.Vehiculos.length == $scope.totalreg)
                                $scope.dsblBtn = true;

                        }
                    };

                    $scope.editRegistro = function (index) {
                        console.log("edit index" + index);
                        $scope.enabledEdit[index] = true;
                    };

                    $scope.deleteRegistro = function (index) {
                        $scope.Vehiculos.splice(index, 1);
                        $scope.ContaReg = $scope.Vehiculos.length;
                        if ($scope.Vehiculos.length < $scope.totalreg) {
                            $scope.dsblBtn = false;
                            if ($scope.Vehiculos.length == 0)
                                $scope.ContaReg = 1
                        }

                        var Vehiculo = {
                            idNoCotizacion: 0, sPlacas: "", sNoMotor: "", sNoSerie: "", sContrato: "", iEstatusReg: 0, sDescEstatus: "", sCondunctor: "", sSolicitud: "", sQLTS: "", sCotizacion: "", sPolizaQLT: "", iInciso: 0, iEndoso: 0, disableEdit: false
                        };

                        $scope.Vehiculos.push(Vehiculo);
                        $scope.enabledEdit[$scope.Vehiculos.length - 1] = true;
                        $scope.ContaReg = $scope.Vehiculos.length;
                        if ($scope.Vehiculos.length == $scope.totalreg)
                            $scope.dsblBtn = true;


                    };

                    $scope.btntext = "Grabar";

                    /*
                    * INDRA - FJQP -- Emisión Múltiple --
                    *
                    * validacion de informacion para Emisión Multiple
                    * 
                    */
                    $scope.ValidaCapturaGen = function () {
                        if ($scope.Vehiculos.length < $scope.totalreg) {
                            alert('No ha completado la captura de todos los campos, favor de completar la infornación solicitada.');
                        }
                        else {

                            var salval = true;

                            salval = validavacios();

                            if (salval == true) {

                            }
                            else {
                                $scope.btntext = "Grabar";
                                var Series = $scope.Vehiculos;

                                var Serie = {};
                                var unicosSerie = Series.filter(function (e) {
                                    return Serie[e.sNoSerie] ? false : (Serie[e.sNoSerie] = true);
                                });

                                if (Series.length != unicosSerie.length) {
                                    alert('No pueden existir números de serie repetidas, favor de corregir.');
                                }
                                else {
                                    $scope.btntext = "Grabar";
                                    var Motores = $scope.Vehiculos;

                                    var Motor = {};
                                    var unicosMotor = Motores.filter(function (e) {
                                        return Motor[e.sNoMotor] ? false : (Motor[e.sNoMotor] = true);
                                    });

                                    if (Motores.length != unicosMotor.length) {
                                        alert('No pueden existir números de motor repetidos, favor de corregir.');
                                    }
                                    else {
                                        $scope.btntext = "Grabar";
                                        var Placas22 = $scope.Vehiculos;

                                        var Placa2 = {};
                                        var unicosPlaca = Placas22.filter(function (e) {
                                            return Placa2[e.sPlacas] ? false : (Placa2[e.sPlacas] = true);
                                        });

                                        if (Placas22.length != unicosPlaca.length) {
                                            alert('No pueden existir placas repetidas, favor de corregir.');



                                        }
                                        else {
                                            $scope.btntext = "Grabar";
                                            var Contratos = $scope.Vehiculos;
                                            var Contrato = {};
                                            var unicosContrato = Contratos.filter(function (e) {
                                                return Contrato[e.sContrato] ? false : (Contrato[e.sContrato] = true);
                                            });

                                            if (Contratos.length != unicosContrato.length) {
                                                alert('No pueden existir contratos repetido, favor de corregir.');



                                            }
                                            else {


                                                $scope.btntext = "Grabar";

                                                alert('Datos validos \n  Se procede a realizar el grabado de la información de los Vehículos. \n\n');

                                                $scope.btntext = "Grabar";
                                                $scope.dsblBtnCancel = true;

                                                $('#myPleaseWait').modal('show');

                                                $scope.Cotiza = $scope.InformacionEmitir.Solicitud.SolicitudId;
                                                $scope.CotizaQLTS = $stateParams.idCotizacion;
                                                var Cotizacion = $scope.Cotiza;

                                                var Vehiculo = $scope.Vehiculos;
                                                var a;
                                                a = 0;
                                                for (var i = 0; i < Vehiculo.length; i++) {
                                                    if (Vehiculo[i].idNoCotizacion === 0 || Vehiculo[i].idNoCotizacion === undefined) {
                                                        Vehiculo[i].idNoCotizacion = Cotizacion;
                                                        Vehiculo[i].idNoConsec = a = i + 1;
                                                        Vehiculo[i].iEstatusReg = 1;
                                                        Vehiculo[i].sDescEstatus = "";
                                                        Vehiculo[0].sSolicitud = $scope.InformacionEmitir.Solicitud.SolicitudId;
                                                        Vehiculo[0].sQLTS = $scope.InformacionEmitir.Solicitud.Numero;
                                                        Vehiculo[0].sCotizacion = $scope.InformacionEmitir.Solicitud.CotizacionId;
                                                        Vehiculo[i].sPolizaQLT = "";
                                                        Vehiculo[i].iInciso = 0;
                                                        Vehiculo[i].iEndoso = 0;
                                                    }
                                                }




                                                var informacionV = JSON.parse(angular.toJson(Vehiculo));

                                                requestService.post('emitir', // Modulo
                                                            'Emisionmultiple', // Controlador
                                                            'Register', // Accion
                                                            informacionV, // Parametros
                                                        true, // Bloquear Interfaz/Vista
                                                        true, // Mostrar errores
                                                        false, // es "SingleResponse"
                                                        function successFunction(response) {
                                                            $scope.btntext = "Grabar";
                                                            $scope.registration = null;
                                                            $('#myPleaseWait').modal('hide');
                                                            alert('Los vehículos han sido almacenados.');
                                                            $scope.dsblBtnCancel = false;
                                                            $('#exampleModal').modal('hide');
                                                            //hide the modal
                                                            $('body').removeClass('modal-open');
                                                            //modal-open class is added on body so it has to be removed
                                                            $('.modal-backdrop').remove();
                                                            //need to remove div with modal-backdrop class
                                                            $('.modal').modal('hide').data('bs.modal', null);
                                                            $scope.divVehiculo = false;
                                                            $scope.divVehiculoBtn = false;
                                                            $scope.requeridoCuantos = false;
                                                            $scope.Cotiza = "";
                                                            $scope.Cotiza = $scope.InformacionEmitir.Solicitud.SolicitudId;
                                                            $scope.CotizaQLTS = $stateParams.idCotizacion;
                                                            $scope.divVehiculo = false;
                                                            $scope.divVehiculoBtn = false;
                                                            $scope.requeridoCuantos = false;
                                                            $scope.Cotiza = $scope.InformacionEmitir.Solicitud.SolicitudId;
                                                            $scope.CotizaQLTS = $stateParams.idCotizacion;
                                                            //*$scope.totalreg = "";
                                                            $scope.confirmed = false;

                                                            $scope.Vehiculos = angular.copy($scope.data);
                                                            $scope.enabledEdit = [];
                                                            $scope.enabledEdit[$scope.Vehiculos.length - 1] = true;
                                                            $scope.ContaReg = 1;

                                                            $scope.dsblBtn = false;



                                                            var Datos = {
                                                                idNoCotizacion: Cotizacion,
                                                                idNoConsec: 0,
                                                                sNoSerie: "",
                                                                sNoMotor: "",
                                                                sPlacas: "",
                                                                sContrato: "",
                                                                iEstatusReg: 1,
                                                                sDescEstatus: "",
                                                                sCondunctor: "",
                                                                sSolicitud: "",
                                                                sQLTS: "",
                                                                sCotizacion: "",
                                                                sPolizaQLT: "",
                                                                iInciso: 0,
                                                                iEndoso: 0

                                                            };



                                                            var informacionD = JSON.parse(angular.toJson(Datos));

                                                            requestService.post('emitir', // Modulo
                                                                'Emisionmultiple', // Controlador
                                                                'getrecord', // Accion
                                                                informacionD, // Parametros
                                                                true, // Bloquear Interfaz/Vista
                                                                true, // Mostrar errores
                                                                true, // es "SingleResponse"
                                                                function successFunction(response) {

                                                                    $scope.regdata = angular.copy(response);
                                                                    $scope.regdataTermina = angular.copy(response);
                                                                    $scope.divTerminaProceso = true;
                                                                    $scope.divTerminaProceso2 = false;
                                                                    $scope.confirmed = true;
                                                                    $scope.divVehiculo = true;
                                                                    $scope.divVehiculoBtn = false;
                                                                    $scope.InformacionEmitir.Vehiculo.Motor = $scope.regdataTermina[0].sNoMotor;
                                                                    $scope.InformacionEmitir.Vehiculo.Serie = $scope.regdataTermina[0].sNoSerie;
                                                                    $scope.InformacionEmitir.Vehiculo.Placas = $scope.regdataTermina[0].sPlacas;
                                                                    $scope.InformacionEmitir.Agencia.NumContrato = $scope.regdataTermina[0].sContrato;
                                                                    $scope.InformacionEmitir.Vehiculo.Conductor = $scope.regdataTermina[0].sConductor;
                                                                    $scope.CapNoVehi = true;
                                                                    $scope.SelMultiple = true;


                                                                },
                                                    function errorFunction(response) {
                                                        alert('failed' + error);
                                                    },
                                                    function badRequestFunction() { });

                                                        },
                                                        function errorFunction(response) {
                                                            alert('failed' + error);
                                                            alert('Error al Almacenar Información');
                                                        },
                                                        function badRequestFunction() { });

                                            }

                                        }

                                    }

                                }
                            }

                        }
                    }
                }

                /** Emision Múltiple **/

            }

        ]);

    });