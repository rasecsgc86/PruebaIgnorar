define(['modules/app', 'services/requestService', 'services/mappingService', 'services/uploadFilesService'], function (app)
{
        /**
        * DEFINIMOS Y REGISTRAMOS EL CONTROLLER configController(Configurador).
        * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
        * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
        * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
        * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
        *
        * @var tittle - TITULO DE LA PAGINA
        * 
        *  Modificacion INDRA FJQP -- Configuracion de Directivas
        */
        app.controller('ConfiguradorController',
        [
            '$scope', '$stateParams', '$state', '$rootScope', '$location', '$sce', 'requestService', '$localStorage',
            'Notification', 'mappingService', '$sessionStorage', '$base64', '$http','uploadFilesService',
		function ($scope, $stateParams, $state, $rootScope, $location, $sce, requestService, $localStorage,
            Notification, mappingService, $sessionStorage, $base64, $http, uploadFilesService) {

		    inicializa();
		    var arrayDatos = [];
		  
		    $scope.cob;
		    var clienteSel = false;
		    function inicializa(){
		        $rootScope.tittle = "Configurador";
		        $rootScope.bMenu = true;
		        //$scope.showModalErrors = false;
		        $scope.isCollapsed = false;
		        var solicitud;

		        $scope.Panel = {};
		        $scope.PanelDeducibles = {};
		        $scope.ED = {};
		      
		        $scope.Produtos = {
		            ProductoID: '',
		            NombreProducto: '',
		            Fexible: '',
		            Cp: ''
		        };

		       

		        /*  Modificacion INDRA FJQP -- Configuracion de Directivas */
		        LimpiaCarga();
		        /*  Modificacion INDRA FJQP -- Configuracion de Directivas */

		        $(document).ready(function () {
		            $("#editarDirectiva").on('hidden.bs.modal', function () {
		                LimpiaCarga();
		                inicialPanel();
		            });
                
		            $("#TipoCoberturaCombo").change(function () {
		                var selectedText = $(this).find("option:selected").text();
		                var selectedValue = $(this).val();

		                if (selectedValue == "0") {
		                    $scope.confirmedFija = true;
		                }
		                else {
		                    $scope.confirmedFija = false;
		                }

		            });

		            $("#RangosDeducibleCombo").change(function () {
		                var selectedText = $(this).find("option:selected").text();
		                var selectedValue = $(this).val();

		                if (selectedValue == "0") {
		                    $scope.confirmedDeducibles = false;
		                }
		                else {
		                    $scope.confirmedDeducibles = true;
		                }

		                $scope.DefaultDeducibles = selectedText;

		            });

		        });
		        /*  Modificacion INDRA FJQP -- Configuracion de Directivas */

		        $scope.CobSelect = true;
		        $scope.CoberturaSelecciona = false;
		        $scope.ValidaCoberturaSel = true;

		        $scope.CoberturasCombo = {
		            iAccion: 0,
		            IdCobertura: 0,
		            Nombre: "",
		            idProductoFlex: 0,
		            idProductoFlexAseguradora: 0,
		            CoberturaFija: 0,
		            PerfilCoberturaFija: 0,
		            SumaAseguradaDefault: "",
		            PerfilSumaAsegurada: 0,
		            DeducibleDefault: "",
		            PerfilDeducible: 0,
		            ToolTipCobertura: "",
		            isEspecial: 0,
		            ParametroDeducible: "",
		            ParametroSA: "",
		            RangosModel: {},
		            idProducto: 0,
		            idTipoVehiculo: 0,
		            idTipoServicioVehiculo : 0
		        }

		        $scope.DeduciblesCombo = {};
		        $scope.SumaAseguradaCombo = {};
		        $scope.TipoCoberturaCombo = {};
		        $scope.RangosModel = {}


		        $scope.TipoCoberturaCombo = [{
		            idTipoCob: 0,
		            TipoCob: "BASICA",
		        }, {
		            idTipoCob: 1,
		            TipoCob: "OPCIONAL",
		        }];

		        $scope.CoberturasComboSel = {
		            iAccion: 0,
		            IdCobertura: 0,
		            Nombre: "",
		            idProductoFlex: 0,
		            idProductoFlexAseguradora: 0,
		            CoberturaFija: 0,
		            PerfilCoberturaFija: 0,
		            SumaAseguradaDefault: "",
		            PerfilSumaAsegurada: 0,
		            DeducibleDefault: "",
		            PerfilDeducible: 0,
		            ToolTipCobertura: "",
		            isEspecial: 0,
		            ParametroDeducible: "",
		            ParametroSA: "",
		            RangosModel: {
		                RangosDeducibles: [],
		                RangosSumas: []
		            },
		            idProducto: 0,
		            idTipoVehiculo: 0,
		            idTipoServicioVehiculo: 0
		        }
		        $scope.CoberturaSeleccionadaCombo = null
		        $scope.TipoCoberturaSeleccionadaCombo = null


		        $scope.CoberturaSeleccionadaCombo = {
		            IdCobertura: 0,
		            Nombre: "BASICA"
		        }

		        $scope.RangoSeleccionadaCombo = {
		        }

		        $scope.CobSelect = true;
		      //  CargaCoberturasCombo();

		        /**********************************************************
              * Funcion para cargar los datos iniciales de la cotización 
              ***********************************************************/

		        /* Modificacion INDRA JJHC metodo que carga la configuracion inicial del panel de coberturas */
		       
		     
		        cargaAseguradoras();
		        cargaTipoAuto();
		        cargaTipoServicio();
		        cargaEsNuevo();
		    }

		    $scope.mostrarErrorModal = function (mensajeError) {
		        $scope.mensajeError = mensajeError;
		        $("#errorModal").fadeIn();
		    }

		    $scope.cerrarErrorModal = function () {
		        $("#errorModal").fadeOut();

		    }

		    $scope.mostrarExitoModal = function (mensajeExito) {
		        $scope.mensajeExito = mensajeExito;
		        $("#exitoModal").fadeIn();
		    }

		    $scope.cerrarExitoModal = function () {
		        $("#exitoModal").fadeOut();

		    }


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

		            Cliente: {
		                Cliente: '',
		                ProductoFlex: '',
		                Producto: '',
		                TipoArrendamiento: '',
		                Agencia: ''
		            }
		        }

		    };

		    $scope.solicitud = {
		        clienteModel: {
		            Cliente: '',
		            ClienteId: ''
		        }
		    };
		    $scope.isCombo = 0;
		    cargarClientes();
		    limpiarC();
		    
            function InicializaImgAseg() {
                for (j = 0; j < jsonAseguradorasImg.length; j++)
                    for (i = 0; i < $scope.Panel.Aseguradoras.length; i++)
                        if (jsonAseguradorasImg[j].aseguradoraId == $scope.Panel.Aseguradoras[i].IdAseguradora) {
                            $scope.Panel.Aseguradoras[i].Img = jsonAseguradorasImg[j].img;
                        }
            }


		    /* Modificacion INDRA JJHC metodos para las cargas de configurador */


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

            $scope.buscarCoberturas = function () {

                inicialPanel();
            }
            function inicialPanel() {

                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idConficionVehiculo = $("#nuevoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();
                var aseguradoraId = $("#AseguradoraP option:selected").val();
                solicitud = {
                    panelConfiguradorModel: {
                        IdProducto: idProducto,
                        IdTipoVehiculo: idTipoVehiculo,
                        IdCondicionVehiculo: idConficionVehiculo,
                        IdTipoServicioVehiculo: idTipoServicioVehiculo,
                        SubMarca: "null",
                        TipoCarga: '',
                        AseguradoraId: aseguradoraId

                    }

                }
                requestService.post('configurador',
               'configurador',
               'consultaPanelConfiguradorFlex',
                solicitud, // Se indica que no hay parametros que pasar
                true,
                true,
                true,
                function successFunction(response) {

                    $scope.Panel = response.PanelConfiguradorModel;

                    if ($scope.Panel != null && $scope.Panel.Aseguradoras != null) {
                     
                        $scope.hayDatosPanel = true;
                        cargaEnmascaradoDeducibles($scope.Panel.Coberturas);
                    } else
                        $scope.hayDatosPanel = false;
                },
            function errorFunction() { $scope.hayDatosPanel = false; },
            function badRequestFunction() { $scope.hayDatosPanel = false; });


            }

            $(function () {
                $('.validanumericos').keypress(function (e) {
                    if (isNaN(this.value + String.fromCharCode(e.charCode)))
                        return false;
                })
                .on("cut copy paste", function (e) {
                    e.preventDefault();
                });
            });

            $scope.ElementosCargoLinea = {
                CatalogoId: '',
                ElementoId: '',
                Nombre: '',
                IdInterno: '',
                Comodin: ''
            };
            $scope.$parent.enviarPanel = function (cobertura) {
                /*Limpiar modelos*/
                if (!cobertura.IsSeleccionada) {
                    cobertura.FiltroValorRangoSuma = "";
                    cobertura.FiltroValorRangoDeducible = "";
                } else {
                    for (var l in $scope.Panel.Coberturas) {
                        if ($scope.Panel.Coberturas.hasOwnProperty(l)) {
                            if ($scope.Panel.Coberturas[l].IsSeleccionada &&
                                $scope.Panel.Coberturas[l].IsEspecial) {
                                var valorDeducible = $scope.Panel.Coberturas[l].FiltroValorRangoDeducible
                                    .replace(/[^\d|\-+|\.+]/g, '');
                                $scope.Panel.Coberturas[l].FiltroValorRangoDeducible = valorDeducible;
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
            function cargaEnmascaradoDeducibles() {

                var idCliente = $scope.$parent.Informacion.Cliente.Cliente.ClienteId
                var idCobertura = "";
                $scope.EnmascaradoDeducibles = [];
                solicitud = {
                    coberturaEnmascaramientoDeducible: {
                        IdCliente: idCliente
                    }
                }
                requestService.post('configurador',
                       'configurador',
                       'consultaEnmarcaradoDeducibles',
                       solicitud,
                       true,
                       true,
                       true,
                       function successFunction(response) {
                           $scope.PanelDeducibles = response.CoberturaEnmascaramientoDeducible;
                           console.log($scope.PanelDeducibles);
                       },
                       function errorFunction() { },
                       function badRequestFunction() { }
                       );

            }

            $scope.actualizarSumasAseguradas = function (cobertura) {
           
                $scope.cob = cobertura;
                $scope.RangosSumasAseguradas = [];
                var idCobertura = $scope.cob.IdCobertura;
                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();
                solicitud = {
                    coberModel: {
                        IdCobertura: idCobertura,
                        IdProducto: idProducto,
                        IdTipoVehiculo: idTipoVehiculo,
                        IdTipoServicioVehiculo: idTipoServicioVehiculo
                    }
                }
                requestService.post('configurador',
                       'configurador',
                       'consultaRangosSumasAseguradas',
                       solicitud,
                       true,
                       true,
                       true,
                       function successFunction(response) {
                           $scope.RangosSumasAseguradas = response.RangosSumas;
                           if ($scope.RangosSumasAseguradas.length === 0) {
                               console.log("hay datos sumas");
                               $("#chkTodosSumasAseguradas").attr("disabled", true);
                               $("#btnBorrarSumas").attr("disabled", true);
                           }
                           else {
                               console.log("no hay datos sumas");
                               $("#chkTodosSumasAseguradas").attr("disabled", false);
                               $("#btnBorrarSumas").attr("disabled", false);
                           }
                       },
                       function errorFunction() { },
                       function badRequestFunction() { }
                       );

             

            };

            $scope.agregarSumas = function () {
                console.log("Click agregar sumas " + $scope.cob.IdCobertura);
                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();
                var rangoInicial = $("#txtRangoInicial").val();
                var rangoFinal = $("#txtRangoFinal").val();
                var saltos = $("#txtSaltos").val();
                var comp = parseInt(rangoInicial) >= parseInt(rangoFinal);
                var comp2 = parseInt(saltos) >= parseInt(rangoFinal);
                if (rangoInicial != "" && rangoFinal != "" && saltos != "") {
                    if ($scope.RangosSumasAseguradas.length === 0) {
                        if (!comp) {

                            if (!comp2) {

                                solicitud = {
                                    coberModel: {
                                        IdCobertura: $scope.cob.IdCobertura,
                                        IdProducto: idProducto,
                                        IdTipoVehiculo: idTipoVehiculo,
                                        IdTipoServicioVehiculo: idTipoServicioVehiculo,
                                        RangoInicial: rangoInicial,
                                        RangoFinal: rangoFinal,
                                        Saltos: saltos
                                    }
                                }

                                requestService.post('configurador',
                                'configurador',
                                'actualizaRangosSumas',
                                solicitud,
                                true,
                                true,
                                false,
                                function successFunction(response) {

                                    recargarSumasAseguradas();
                                    $("#txtRangoInicial").val("");
                                    $("#txtRangoFinal").val("");
                                    $("#txtSaltos").val("");
                                    inicialPanel();
                                    $("#modalAgregarRangos1").modal('show');
                                },
                                function errorFunction() { },
                                function badRequestFunction() { }

                                );

                            }
                            else {
                                $("#mRangosSumasAgregarError4").modal('show');
                            }


                        }

                        else {
                            $("#mRangosSumasAgregarError2").modal('show');

                        }
                    }
                    else {
                        $("#mRangosSumasAgregarError3").modal('show');
                    }

                }

                else {

                    $("#mRangosSumasAgregarError").modal('show');
                }
            }
            $scope.borrarSumas = function () {
                console.log("Click borrar sumas " + $scope.cob.IdCobertura);
                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();
                if ($("#chkTodosSumasAseguradas").prop('checked')) {
                    console.log("Si el checkbox esta encendido");
                    
                    if (!$scope.cob.Enmascaramiento) {
                        console.log("Si no hayt enmascaramiento");
                        var bool = confirm("¿Desea eliminar los rangos de sumas aseguradas?");
                        if (bool) {

                            solicitud = {
                                coberModel: {
                                    IdCobertura: $scope.cob.IdCobertura,
                                    IdProducto: idProducto,
                                    IdTipoVehiculo: idTipoVehiculo,
                                    IdTipoServicioVehiculo: idTipoServicioVehiculo
                                    //RangoInicial: rangoInicial,
                                    //RangoFinal: rangoFinal,
                                    //Saltos: saltos
                                }
                            }

                            requestService.post('configurador',
                            'configurador',
                            'actualizaRangosSumas',
                            solicitud,
                            true,
                            true,
                            false,
                            function successFunction(response) {

                                recargarSumasAseguradas();
                                $("#chkTodosSumasAseguradas").prop('checked', false);
                                inicialPanel();
                                //$scope.RangosSumasAseguradas = response.RangosSumas;
                                //console.log($scope.RangosSumasAseguradas);

                            },
                            function errorFunction() { },
                            function badRequestFunction() { }

                            );

                            $("#modalBorrarRangos2").modal('show');
                        }
                    }

                    else {
                       
                        $("#modalAgregarDedEnmascarados").modal('show');
                    }
                }
                else {
                    $("#mRangosSumasBorrarError").modal('show');
                  

                }
            }
            $scope.deshabilitarCheckRangosSumas = function () {
                if ($scope.RangosSumasAseguradas.length > 0) {
                    $('.checar').attr("disabled", true);
                    //$("input:checkbox").attr("disabled", true);
                }
                $("#chkTodosSumasAseguradas").attr("disabled", false);
                $("#btnBorrarSumas").attr("disabled", false);
            }
            $scope.deshabilitarChekDeducibles = function () {

                if ($scope.RangosDeduciblesM.length > 0) {
                    $("input:checkbox").attr("disabled", false);
                }
              
            }
            function recargarSumasAseguradas() {

                $scope.RangosSumasAseguradas = [];
                var idCobertura = $scope.cob.IdCobertura;
                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();
                solicitud = {
                    coberModel: {
                        IdCobertura: idCobertura,
                        IdProducto: idProducto,
                        IdTipoVehiculo: idTipoVehiculo,
                        IdTipoServicioVehiculo: idTipoServicioVehiculo
                    }
                }

                requestService.post('configurador',
                       'configurador',
                       'consultaRangosSumasAseguradas',
                       solicitud,
                       true,
                       true,
                       true,
                       function successFunction(response) {

                           $scope.RangosSumasAseguradas = response.RangosSumas;
                           if ($scope.RangosSumasAseguradas.length === 0) {
                               $("#chkTodosSumasAseguradas").prop('checked', false);
                               $("#chkTodosSumasAseguradas").attr("disabled", true);
                               $("#btnBorrarSumas").attr("disabled", true);
                           }
                           else {
                               $("#chkTodosSumasAseguradas").prop('checked', false);
                               $("#chkTodosSumasAseguradas").attr("disabled", false);
                               $("#btnBorrarSumas").attr("disabled", false);
                           }
                           //console.log($scope.RangosSumasAseguradas);

                       },
                       function errorFunction() { },
                       function badRequestFunction() { }

                       );

             
            }
            $scope.actualizarRangosDeducibles = function (cobertura) {
                $scope.cob = cobertura;
                $scope.RangosDeduciblesM = [];

                var idCobertura = $scope.cob.IdCobertura;
                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();
                solicitud = {
                    coberModel: {
                        IdCobertura: idCobertura,
                        IdProducto: idProducto,
                        IdTipoVehiculo: idTipoVehiculo,
                        IdTipoServicioVehiculo: idTipoServicioVehiculo
                    }
                }

                requestService.post('configurador',
                       'configurador',
                       'consultaRangosSumasAseguradas',
                       solicitud,
                       true,
                       true,
                       true,
                       function successFunction(response) {
                           $scope.RangosDeduciblesM = response.RangosDeducibles;

                           if ($scope.RangosDeduciblesM.length === 0) {
                               console.log("No hay rangos deducibles");
                               $("#chkDeducibles").attr("disabled", true);
                               $("#btnBorrarDeducibles").attr("disabled", true);
                           }
                           else {
                               console.log("Hay rangos deducibles");
                               $("#chkDeducibles").attr("disabled", false);
                               $("#btnBorrarDeducibles").attr("disabled", false);
                           }

                       },
                       function errorFunction() { },
                       function badRequestFunction() { }

                       );
                console.log($scope.RangosDeduciblesM.length);
            };


            $scope.agregarDeducibles = function () {
                console.log("click en boton agregar " + $scope.cob.IdCobertura);
                var rango = $("#txtRangoDeducible").val();
                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();
                if (rango !== null && rango !== "") {

                    arrayDatos.push(rango);

                    for (var i = 0; i < $scope.RangosDeduciblesM.length; i++) {

                        if ($scope.RangosDeduciblesM[i] === arrayDatos[0]) {
                                $("#modalDeducibleRepetido").modal('show');
                                arrayDatos.length = 0;
                                $("#txtRangoDeducible").val("");
                        }

                    }

                    

                    if (arrayDatos.length > 0) {

                        solicitud = {
                            coberModel: {
                                IdCobertura: $scope.cob.IdCobertura,
                                IdProducto: idProducto,
                                IdTipoVehiculo: idTipoVehiculo,
                                IdTipoServicioVehiculo: idTipoServicioVehiculo,
                                ListaDeducibles: arrayDatos,
                                EliminarTodosDeducibles: 0,
                                AgregarOeliminarElemento: 1

                            }
                        }

                        requestService.post('configurador',
                        'configurador',
                        'actualizaRangosDeducibles',
                        solicitud,
                        true,
                        true,
                        false,
                        function successFunction(response) {

                            recargarRangosDeducibles();
                            $("#txtRangoDeducible").val("");
                            arrayDatos.length = 0;
                            inicialPanel();
                        },
                        function errorFunction() { },
                        function badRequestFunction() { }

                        );

                        $("#modalAgregarDeducible").modal('show');

                    }
                }
                else {

                    $("#modalAgregarDeduciblesVacios").modal('show');
                }



            }

            $scope.borrarDeducibles = function () {
                console.log("click en boton agregar " + $scope.cob.IdCobertura);
                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();
                if ($("#chkDeducibles").prop('checked')) {

                    var bool = confirm("¿Desea eliminar los rangos deducibles?");
                    if (bool) {
                        solicitud = {
                            coberModel: {
                                IdCobertura: $scope.cob.IdCobertura,
                                IdProducto: idProducto,
                                IdTipoVehiculo: idTipoVehiculo,
                                IdTipoServicioVehiculo: idTipoServicioVehiculo,
                                EliminarTodosDeducibles: "1"

                            }
                        }

                        requestService.post('configurador',
                        'configurador',
                        'actualizaRangosDeducibles',
                        solicitud,
                        true,
                        true,
                        false,
                        function successFunction(response) {

                            recargarRangosDeducibles();
                            $("#chkDeducibles").prop('checked', false);
                            inicialPanel();
                        },
                        function errorFunction() { },
                        function badRequestFunction() { }

                        );

                        $("#modalBorrarDeducibles2").modal('show');

                    }


                }
                else {
                    $("#modalBorrarDeduciblesTodos").modal('show');

                }
            }

            function recargarRangosDeducibles() {
                $scope.RangosDeduciblesM = [];

                var idCobertura = $scope.cob.IdCobertura;
                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();
                solicitud = {
                    coberModel: {
                        IdCobertura: idCobertura,
                        IdProducto: idProducto,
                        IdTipoVehiculo: idTipoVehiculo,
                        IdTipoServicioVehiculo: idTipoServicioVehiculo
                    }
                }

                requestService.post('configurador',
                       'configurador',
                       'consultaRangosSumasAseguradas',
                       solicitud,
                       true,
                       true,
                       true,
                       function successFunction(response) {
                           $scope.RangosDeduciblesM = response.RangosDeducibles;
                           if ($scope.RangosDeduciblesM.length === 0) {

                               $("#chkDeducibles").attr("disabled", true);
                               $("#btnBorrarDeducibles").attr("disabled", true);
                           }
                           else {
                               $("#chkDeducibles").attr("disabled", false);
                               $("#btnBorrarDeducibles").attr("disabled", false);
                           }
                       },
                       function errorFunction() { },
                       function badRequestFunction() { }

                       );

            }

            $scope.selRango = function (dato) {
                arrayDatos.push(dato);
                console.log(arrayDatos);
            }

            $scope.borrarDeduciblesSeleccionados = function () {
                var idCobertura = $scope.cob.IdCobertura;
                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();
                if (arrayDatos.length > 0) {

                    var bool = confirm("¿Desea eliminar los rangos seleccionados?");
                    if (bool) {
                        solicitud = {
                            coberModel: {
                                IdCobertura: idCobertura,
                                IdProducto: idProducto,
                                IdTipoVehiculo: idTipoVehiculo,
                                IdTipoServicioVehiculo: idTipoServicioVehiculo,
                                ListaDeducibles: arrayDatos,
                                EliminarTodosDeducibles: 0,
                                AgregarOeliminarElemento: 0

                            }
                        }

                        requestService.post('configurador',
                        'configurador',
                        'actualizaRangosDeducibles',
                        solicitud,
                        true,
                        true,
                        false,
                        function successFunction(response) {


                            recargarRangosDeducibles();
                            arrayDatos.length = 0;
                            inicialPanel();
                        },
                        function errorFunction() { },
                        function badRequestFunction() { }

                        );

                        $("#modalBorrarDeducibles2").modal('show');

                    }

                }
                else {

                    $("#modalBorrarDeduciblesTodos").modal('show');
                }


            }


            $scope.borrarRangosDeducibles = function () {

                console.log("boton borrar");
                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();

                solicitud = {
                    coberModel: {
                        IdCobertura: 244,
                        IdProducto: idProducto,
                        IdTipoVehiculo: idTipoVehiculo,
                        IdTipoServicioVehiculo: idTipoServicioVehiculo,
                        ListaDeducibles: arrayDatos
                    }
                }

                requestService.post('configurador',
                       'configurador',
                       'actualizaRangosDeducibles',
                       solicitud,
                       true,
                       true,
                       false,
                       function successFunction(response) {


                       },
                       function errorFunction() { },
                       function badRequestFunction() { }

                       );


            }
            $scope.actualizarHomologacionTooltip = function (cobertura) {
                $scope.cob = cobertura;
                var idCobertura = $scope.cob.IdCobertura;
                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idConficionVehiculo = $("#nuevoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();

                $("#btnEnviar").on("click", function () {
                    var homologacion = $("#txtHomologacion").val();
                    var tooltip = $("#txtTooltip").val();

                    if (homologacion != "" && tooltip != "") {

                        solicitud = {
                            coberModel: {
                                IdCobertura: idCobertura,
                                IdProducto: idProducto,
                                IdTipoVehiculo: idTipoVehiculo,
                                IdTipoServicioVehiculo: idTipoServicioVehiculo,
                                Homologacion: homologacion,
                                Tooltip: tooltip
                            }
                        }

                        requestService.post('configurador',
                            'configurador',
                            'actualizarHomologacionTooltip',
                            solicitud,
                            true,
                            true,
                            true,
                            function successFunction(response) {


                                $("#txtHomologacion").val("");
                                $("#txtTooltip").val("");
                                $("#modalTooltipC").modal('show');
                          

                            },
                            function errorFunction() { },
                            function badRequestFunction() { }

                            );
                    }
                    else {
                        alert("Los campos homologacion y tooltip son obligatorios. Favor de revisar");
                    }

                });

            }

            $scope.cerrarModalDeducibles = function () {
                $scope.cob = null;
                $("#txtRangoDeducible").val("");
                $("#chkDeducibles").prop('checked', false);
            }

            $scope.cerrarModalHT = function () {
                $("#modalTooltipC").modal('hide');
            }
            $scope.cerraModalSumasError = function () {
                $("#mRangosSumasAgregarError").modal('hide');
            }
            $scope.cerralModalBorrarSumasError = function () {
                $("#mRangosSumasBorrarError").modal('hide');
            }
            $scope.cerralModalAgregarSumas = function () {
                $("#mRangosSumasAgregarError2").modal('hide');
                var rangoInicial = $("#txtRangoInicial").val("");
                var rangoFinal = $("#txtRangoFinal").val("");
                var saltos = $("#txtSaltos").val("");
            }
            $scope.cerrarModalExistenRegistros = function () {
                $("#mRangosSumasAgregarError3").modal('hide');
            }
            $scope.cerrarModalSaltos = function () {
                $("#mRangosSumasAgregarError4").modal('hide');
            }
            $scope.cerrarModalAgregarRangos = function () {
                $("#modalAgregarRangos1").modal('hide');
            }
            $scope.cerrarModalBorrarRangos = function () {
                $("#modalBorrarRangos2").modal('hide');
            }
            $scope.cerrarModalSumasAseguradas = function () {
                $("#txtRangoInicial").val("");
                $("#txtRangoFinal").val("");
                $("#txtSaltos").val("");
                $scope.cob = null;
                $("#chkTodosSumasAseguradas").prop('checked', false);
            }
            $scope.cerrarModalAgregarRangoDeducible = function () {

                $("#modalAgregarDeducible").modal('hide');
            }

            $scope.cerrarModalDeducibleRepetido = function () {
                $("#modalDeducibleRepetido").modal('hide');
            }

            $scope.cerrarModalBorrarDeduciblePorCheck = function () {
                $("#modalBorrarDeducibles2").modal('hide');
            }
            $scope.cerraModalBorrarDeduciblesTodos = function () {
                $("#modalBorrarDeduciblesTodos").modal('hide');
            }

            $scope.cerrarModalDeducibleVacio = function () {
                $("#modalAgregarDeduciblesVacios").modal('hide');
            }

            $scope.cerrarModarBorrarDedEnmascarado = function () {
                $("#modalAgregarDedEnmascarados").modal('hide');
            }

            $scope.cerrarModalDocsRepetidos = function () {
                $("#modalDocumentosRepetidos").modal('hide');
            }

		    ///// funcion para checkBox sumas aseguradas
            $scope.checkAll = function () {
                if ($("#chkTodosSumasAseguradas").prop('checked')) {
                    $('.checar').prop('checked', true);
                }
                else {
                    $('.checar').prop('checked', false);
                }
            }
            $scope.checkAllDeducibles = function () {

                if ($("#chkDeducibles").prop('checked')) {
                    $('.checkDed').prop('checked', true);
                }
                else {
                    $('.checkDed').prop('checked', false);
                }
             
            }
            function cargarDatosHomo(idcob, isprodFlex) {
                console.log(idcob);
                console.log(idprodFlex);
            }
            $scope.enviaDatosTooltip = function () {

                var idCob = $.session.get('idCobertura');
                var idpF = $.session.get('idProdFlex');

                console.log(idCob);
                console.log(idpF);


            }
            $scope.limpiarCajasTooltip = function () {
                $("#txtHomologacion").val("");
                $("#txtTooltip").val("");
                $scope.cob = null;
            }
           

            function cargarClientes() {

                

                solicitud = {
                    clienteModel: {
                        Cliente: '',
                        ClienteId: ''
                    }
                }

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
                solicitud = {
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
            function cargarProductos() {

               
                solicitud = {
                    clienteModel: {
                        ClienteId: $scope.$parent.Informacion.Cliente.Cliente.ClienteId,
                        ProductoFlex: $scope.$parent.Informacion.Cliente.Cliente.ProductoFlex
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
                        
                    },
                    function errorFunction() { },
                    function badRequestFunction() { });
               


            }
            function cargarProductosModalProductos() {

                $scope.ProductosNuevo = {
                    ProductoID: '',
                    NombreProducto: '',
                    Fexible: '',
                    Cp: ''
                }
                $scope.productosNuevo = [];
                solicitud = {

                }

                requestService.post('configurador',
                        'configurador',
                        'consultaProductosFlexibles',
                        solicitud,
                        true,
                        true,
                        true,
                        function successFunction(response) {

                            if (response != null && response.length === 1) {
                                $scope.productosNuevo = response;
                                $scope.ProductosNuevo = $scope.productosNuevo[0];
                            }
                            else {
                                $scope.productosNuevo = response;
                            }



                        },
                        function errorFunction() { },
                        function badRequestFunction() { }

                        );



            }
            function cargarProductosModalCargoEnLinea() {

                $scope.ProductosCargoLinea = {
                    ProductoID: '',
                    NombreProducto: '',
                    Fexible: '',
                    Cp: ''
                }

                $scope.productosCargoLinea = [];
                solicitud = {

                }

                requestService.post('configurador',
                        'configurador',
                        'consultaProductosFlexibles',
                        solicitud,
                        true,
                        true,
                        true,
                        function successFunction(response) {

                            if (response != null && response.length === 1) {
                                $scope.productosCargoLinea = response;
                                $scope.ProductosCargoLinea = $scope.productosNuevo[0];
                            }
                            else {
                                $scope.productosCargoLinea = response;
                            }



                        },
                        function errorFunction() { },
                        function badRequestFunction() { }

                        );

            }
            $scope.cargarDatosModal = function () {
                cargarProductosModalProductos();
               
            }
            $scope.guardaProductosFlexibles = function () {

                var selProducto = $("#productoModal option:selected").val();
                var selTipoAuto = $("#tipoAuto option:selected").val();
                var selNuevo = $("#nuevo option:selected").val();
                var seltipoServicio = $("#tipoServicio option:selected").val();
                var textSubmarca = $("#txtProducto").val();
                var aseguradoraId = $("#AseguradoraM option:selected").val();

                if (selProducto != "" && selTipoAuto != "" && selNuevo != "" && seltipoServicio != "") {
                    solicitud = {

                        productoFlexModel: {
                            ProductoID: selProducto,
                            IdTipoVehiculo: selTipoAuto,
                            IdCondicionVehiculo: selNuevo,
                            IdTipoServicioVehiculo: seltipoServicio,
                            NotasImportantes: '',
                            Submarca: textSubmarca,
                            AseguradoraId: aseguradoraId

                        }

                    }

                    requestService.post('configurador',
                        'configurador',
                        'guardaProductoFlexible',
                        solicitud,
                        true,
                        true,
                        false,
                        function successFunction(response) {


                            alert("Se guardo el producto de forma correcta");
                           
                            Limpiar();
                            recargarDatosProductoModal();

                        },
                         function errorFunction(response) {
                             console.log("No se guardo el producto de forma correcta");
                         },
                            function badRequestFunction() { }



                        );
                }
                else {
                    alert("Todos los campos son obligatorios. Favor de verificar");
                }




            }
            $scope.cargarDatosModalCargoEnLinea = function () {
                cargarProductosModalCargoEnLinea();
              
            }
            $scope.cerrarModal = function () {
                console.log("Cerrar Modal");
                console.log($scope.elementosSeguro);
                Limpiar();
              
                $scope.elementosSeguro = [];
                
                cargaAseguradoras();
                cargaTipoAuto();
                cargaTipoServicio();
                cargaEsNuevo();
               
            }
            function cerrarModalInsertProducto() {
                Limpiar();
            }
            function cargaTipoAuto() {
                $scope.elementos = [];

                $scope.Elementos = {
                    CatalogoId: '',
                    ElementoId: '',
                    Nombre: '',
                    IdInterno: '',
                    Comodin: ''


                };

                solicitud = {}

                requestService.post('configurador',
                    'configurador',
                    'consultaTipoAuto',
                    solicitud,
                    true,
                    true,
                    true,
                    function successFunction(response) {

                        if (response != null && response.length === 1) {

                            $scope.elementos = response;
                            $scope.Elementos = $scope.elementos[0];

                        }
                        else {
                            $scope.elementos = response;
                        }



                    },

                      function errorFunction() { },
                        function badRequestFunction() { }
                    );
            }
            function cargaTipoServicio() {

                $scope.ElementosServicio = {
                    CatalogoId: '',
                    ElementoId: '',
                    Nombre: '',
                    IdInterno: '',
                    Comodin: ''


                };

                $scope.elementosServicio = [];

                solicitud = {}
                requestService.post('configurador',
               'configurador',
               'consultaTipoServicio',
               solicitud,
               true,
               true,
               true,
               function successFunction(response) {

                   if (response != null && response.length === 1) {

                       $scope.elementosServicio = response;
                       $scope.ElementosServicio = $scope.elementosServicio[0];
                   }
                   else {
                       $scope.elementosServicio = response;
                   }



               },

              function errorFunction() { },
                function badRequestFunction() { }
            );
            }
            function cargaTipoSeguro() {

                $scope.ElementosSeguro = {
                    CatalogoId: '',
                    ElementoId: '',
                    Nombre: '',
                    IdInterno: '',
                    Comodin: ''
                }

                $scope.elementosSeguro = [];
                $scope.Comodin = [];
                var id;
                solicitud = {}
                requestService.post('configurador',
               'configurador',
               'consultaTipoSeguro',
               solicitud,
               true,
               true,
               true,
               function successFunction(response) {


                   if (response != null && response.length === 1) {

                       $scope.Comodin = response;
                       $scope.elementosSeguro = $scope.Comodin;

                       $scope.ElementosSeguro = $scope.elementosSeguro[0];
                   }
                   else {
                       $scope.elementosSeguro = response;

                   }

               },

              function errorFunction() { },
                function badRequestFunction() { }
            );
            }
            function cargaEsNuevo() {

                $scope.ElementosNuevo = {

                    CatalogoId: '',
                    ElementoId: '',
                    Nombre: '',
                    IdInterno: '',
                    Comodin: ''

                };

                $scope.elementosNuevo = [];

                solicitud = {}
                requestService.post('configurador',
              'configurador',
              'esNuevo',
              solicitud,
              true,
              true,
              true,
              function successFunction(response) {

                  if (response != null && response.length === 1) {

                      $scope.elementosNuevo = response
                      $scope.ElementosNuevo = $scope.elementosNuevo[0];

                  }
                  else {
                      $scope.elementosNuevo = response;
                  }



              },

             function errorFunction() { },
                function badRequestFunction() { }
                );
            }
            function cargaEnLinea() {

                $scope.elementosCargoLinea = [];
                $scope.Comodin = [];
                solicitud = {}
                requestService.post('configurador',
             'configurador',
             'cargoEnLinea',
             solicitud,
             true,
             true,
             true,
             function successFunction(response) {
                 if (response != null && response.length === 1) {
                     $scope.Comodin = response;
                     $scope.elementosCargoLinea = $scope.Comodin;
                     $scope.ElementosCargoLinea = $scope.elementosCargoLinea[0];

                 }
                 else {
                     $scope.elementosCargoLinea = response;

                 }


             },

            function errorFunction() { },
               function badRequestFunction() { }
               );
            }
            function cargaAseguradoras() {

                $scope.ElementosAseguradora = {

                    CatalogoId: '',
                    ElementoId: '',
                    Nombre: '',
                    IdInterno: '',
                    Comodin: ''

                };

                $scope.elementosAsegura = [];
                $scope.Comodin = [];
                solicitud = {}
                requestService.post('configurador',
                'configurador',
                'consultaAseguradoras',
                solicitud,
                true,
                true,
                true,
               function successFunction(response) {
                   if (response != null && response.length === 1) {
                       $scope.Comodin = response;
                       $scope.elementosAsegura = $scope.Comodin;
                       $scope.ElementosAseguradora = $scope.elementosAsegura[0];
                   }
                   else {
                       $scope.elementosAsegura = response;
                   }


               },

              function errorFunction() { },
               function badRequestFunction() { }
               );
            }
            function cargaEstatus() {
                console.log("cargaEstatus Inicio...");
                $scope.ElementosEstatus = {
                    CatalogoId: '',
                    ElementoId: '',
                    Nombre: '',
                    IdInterno: '',
                    Comodin: ''
                }

                $scope.elementosEstatus = [];
                solicitud = {}
                requestService.post('configurador', 'configurador', 'consultaEstatus',
                    solicitud, true, true, true,
                    function successFunction(response) {

                        if (response != null && response.length === 1) {
                            console.log("entro a if estatus");
                            $scope.elementosEstatus = response;
                            $scope.ElementosEstatus = $scope.elementosEstatus[0];

                        }
                        else {
                            console.log("entro a else estatus")
                            $scope.elementosEstatus = response;
                        }

                    },
                     function errorFunction() { },
            function badRequestFunction() { }

                    );

            }
            $scope.clienteSel = function (combo) {
                if (combo !== 1)
                    $scope.$parent.Informacion.Cliente.Cliente = $scope.targetModelo;
                $scope.ProductoFlex = true;
                $scope.productoFlex = true;
                $scope.productos = [];
                $scope.productoFlexSel();
            
                if ($scope.Informacion.Cliente.Producto !== '') {
                    clienteSel = true;
                    limpiarC();
                }
              
            }
            function limpiarC() {
                if (clienteSel === true) {
                    $scope.$parent.Informacion.Cliente.Cliente.Agencia = '';
                    $scope.$parent.Informacion.Cliente.Cliente.Producto = '';
                    $scope.$parent.Informacion.Cliente.Cliente.ProductoFlex = 1;


                    clienteSel = false;

                }

                if ($scope.$parent.Informacion.Cliente.Cliente === '') {
                    $scope.$parent.Informacion.Cliente.Cliente.ProductoFlex = 1;
                    $scope.$parent.Informacion.Cliente.Cliente.Producto = '';
                    $scope.$parent.Informacion.Cliente.Cliente = '';
                    $scope.$parent.Informacion.Cliente.Cliente.Agencia = '';


                    $scope.productos = [];
                    $scope.clientes = [];
                    $scope.ProductoFlex = true;
                    $scope.productoFlex = true;

                }






            }
            $scope.productoFlexSel = function () {
                $scope.$parent.Informacion.Cliente.Cliente.ProductoFlex = ($scope.ProductoFlex === true) ? 1 : 0;
               
                $scope.$parent.Informacion.Cliente.Cliente.Agencia = '';

                clienteSel = false;
                limpiarC();
                cargarProductos();
               

            }

            $scope.productoSel = function () {
                clienteSel = false;
                if ($scope.$parent.Informacion.Cliente.Cliente.Producto !== undefined &&
                       $scope.$parent.Informacion.Cliente.Cliente.Producto !== null &&
                       $scope.$parent.Informacion.Cliente.Cliente.Producto !== '') {

                    limpiarC();
                }
            }
            $scope.productosModal1Sel = function (combo) {

               
                var sel = $("#productoModal option:selected").html();

                
                if (sel !== "Seleccionar") {
                   
                    cargaTipoAuto();
                    cargaTipoServicio();
                    cargaEsNuevo();

                    $("#txtProducto").val("");
                }
                else {
                    
                    Limpiar();
                    recargarDatosProductoModal();
                    $("#txtProducto").val("");
                }






            }
            $scope.productosSubModalSel = function (combo) {
                var sel = $("#productoSubModal option:selected").html();
                if (sel != "Seleccionar") {
                    cargaEstatus();
                    cargaAseguradoras();
                    $("#txtUrl").val("");
                    $("#txtLeyenda").val("");
                    $("#txtToken").val("");
                }
                else {
                    LimpiarSubModal();
                    $("#txtUrl").val("");
                    $("#txtLeyenda").val("");
                    $("#txtToken").val("");
                }


            }
            $scope.tipoAutosSel = function (combo) { }
            $scope.tipoServicioSel = function (combo) { }
            $scope.esNuevoSel = function (combo) { }
            $scope.cargoLineaSel = function (combo) { }
            $scope.aseguradorasPrincipal = function (combo) { }
            $scope.AseguradorasSel = function (combo) { }
            $scope.EstatusSel = function (combo) { }
            function Limpiar() {
                $scope.productosNuevo = [];
                $scope.elementos = [];
                $scope.elementosNuevo = [];
                $scope.elementosServicio = [];
                $("#txtProducto").val("");
            }
            function LimpiarSubModal() {
                $scope.elementosAsegura = [];
                $scope.elementosEstatus = [];
            }
            function recargarDatosProductoModal() {
                cargarProductosModalProductos();
            }
            function inicializarModalPerfiles() {
                cargarDatosPerfil();
                cargarUsuariosFlexibles();
            }
            $scope.cargaDatosModalPerfiles = function () {
                inicializarModalPerfiles();
                $("#chkUdi").prop('checked', false);
            }
            function cargarDatosPerfil() {

                $scope.PerfilesUsuario = {

                    PerfilUsuarioID: '',
                    Nombre: '',
                    PerfilPadreID: '',
                    Activo: '',
                    OpcionAcceso: '',
                    OpcionAccesoB: ''

                }
                $scope.perfilesUsuario = [];
                solicitud = {}

                requestService.post('configurador',
                    'configurador',
                    'consultaPerfilesSistema',
                    solicitud,
                    true,
                    true,
                    true,
                    function successFunction(response) {
                        if (response != null && response.length === 1) {
                            $scope.perfilesUsuario = response;
                            $scope.PerfilesUsuario = $scope.perfilesUsuario[0];

                        }

                        else {
                            $scope.perfilesUsuario = response;
                        }

                    },

                      function errorFunction() { },
                        function badRequestFunction() { }
                    );


            }
            $scope.selPerfil = function (combo) {

                var selProducto = $("#perfilUsuario option:selected").val();

                cargarUsuariosPorPerfil(selProducto);

            }
            $scope.selUsuario = function (combo) { }
            function cargarUsuariosPorPerfil(val) {

                $scope.UsuariosPorPerfil = {

                    PersonaID: '',
                    PerfilUsuarioID: '',
                    Nombre: ''

                }

                $scope.usuariosPorPerfil = [];
                solicitud = {
                    usuariosPerfilModel: {
                        PerfilUsuarioID: val
                    }
                }

                requestService.post('configurador',
            'configurador',
            'consultaUsuariosPorPerfil',
            solicitud,
            true,
            true,
            true,
            function successFunction(response) {

                if (response != null && response.length === 1) {
                    $scope.usuariosPorPerfil = response;
                    $scope.UsuariosPorPerfil = $scope.usuariosPorPerfil[0];
                }
                else {
                    $scope.usuariosPorPerfil = response;
                }



            },
            function errorFunction() { },
            function badRequestFunction() { }

            );


            }
		    //// funcion de agregar
            $scope.guardarUsuarioFlexible = function () {
                var selPerfilUsuario = $("#perfilUsuario option:selected").val();
                var selUsuarioPersona = $("#nombreUsuario option:selected").val();
                var selNombrePersona = $("#nombreUsuario option:selected").html();
                var Udi;

                if ($("#chkUdi").prop('checked')) {
                    Udi = 1;
                }
                else {
                    Udi = 0;
                }

                if (selPerfilUsuario != "" && selUsuarioPersona != "") {
                    solicitud = {
                        perfilesFlexibleModel: {

                            PerfilId: selPerfilUsuario,
                            PersonaId: selUsuarioPersona,
                            Comentario: selNombrePersona,
                            manejaUdi: Udi

                        }

                    }

                    requestService.post('configurador',
                        'configurador',
                        'guardaUsuarioFlexible',
                        solicitud,
                        true,
                        true,
                        false,
                        function successFunction(response) {

                            cargarUsuariosFlexibles();
                            $scope.perfilesUsuario = [];
                            $scope.usuariosPorPerfil = [];
                            if ($("#chkUdi").prop('checked')) {

                                $("#chkUdi").prop('checked', false);
                            }

                            inicializarModalPerfiles();
                        },
                         function errorFunction(response) {
                             console.log("No se guardo el producto de forma correcta");
                         },
                            function badRequestFunction() { }



                        );
                }
                else {
                    alert("El campo Perfil y Usuario son obligatorios. Favor de verificar");
                }





            }
            function cargarUsuariosFlexibles() {
                $scope.UsuariosFlexibles = {

                    PerfilFlexibleId: '',
                    PerfilId: '',
                    PersonaId: '',
                    Comentario: '',
                    maneja_udi: ''

                }

                $scope.usuariosFlexibles = [];
                solicitud = {};
                requestService.post('configurador',
                                    'configurador',
                                    'consultarUsuariosFlexibles',
                                    solicitud,
                                    true,
                                    true,
                                    true,
                                    function successFunction(response) {

                                        $scope.usuariosFlexibles = response;
                                        

                                    },
                                       function errorFunction() { },
                                       function badRequestFunction() { }

                                      );

            }
            function LimpiarModalUsuariosFlexible() {
                $scope.perfilesUsuario = []
                $scope.usuariosPorPerfil = [];
                $("#chkUdi").prop('checked', false);

            }
            $scope.init = function () {
                for (var i = 0; i < $scope.usuariosFlexibles.length; i++) {
                    $scope.status = $scope.usuariosFlexibles[i].maneja_udi;
                    console.log
                }
            }
            $scope.changeStatus = function (usr, idUser) {
                var res = usr.maneja_udi = !usr.maneja_udi
                actualizaStatusUdi(idUser, res);

            }
            $scope.eliminarUsuario = function (valor) {




                $("#mi-modal").modal('show');


                $("#modal-btn-si").on("click", function () {
                    solicitud = {
                        perfilesFlexibleModel: {

                            PerfilFlexibleId: valor

                        }

                    }

                    requestService.post('configurador',
                    'configurador',
                    'eliminarUsuarioFlexible',
                    solicitud,
                    true,
                    true,
                    false,
                    function successFunction(response) {
                        $("#mi-modal").modal('hide');
                        $scope.usuariosPorPerfil = [];
                        inicializarModalPerfiles();

                    },
                     function errorFunction(response) {
                         console.log("No se guardo el producto de forma correcta");
                     },
                     function badRequestFunction() { }

                    );

                });

                $("#modal-btn-no").on("click", function () {
                    $("#mi-modal").modal('hide');
                });

            }
            function actualizaStatusUdi(idPerfil, status) {
                var res = '';
                if (status == true)
                    res = 1;
                else {
                    res = 0;
                }
                solicitud = {
                    perfilesFlexibleModel: {

                        PerfilFlexibleId: idPerfil,
                        manejaUdi: res

                    }

                }


                requestService.post('configurador',
                    'configurador',
                    'actualizaUdiUsuario',
                    solicitud,
                    true,
                    true,
                    false,
                    function successFunction(response) {
                        cargarUsuariosFlexibles();

                    },
                     function errorFunction(response) {
                         console.log("No se guardo el producto de forma correcta");
                     },
                        function badRequestFunction() { }



                    );

            }
		    ////// funciones para formas de pago
            $scope.modalFormasPago = function () {
                inicializarModalFormasPago();

            }
            function inicializarModalFormasPago() {
                cargarFormasPago();
                cargarProductosModalProductos();
                cargarFormasPagoLista();
            }
            function cargarFormasPago() {

                $scope.elementosFormasPago = [];

                $scope.ElementosFormasPago = {

                    CatalogoId: '',
                    ElementoId: '',
                    Nombre: '',
                    IdInterno: '',
                    Comodin: ''

                }

                solicitud = {}
                requestService.post('configurador',
                 'configurador',
                 'consultaFormasPago',
                 solicitud,
                 true,
                 true,
                 true,
                 function successFunction(response) {

                     if (response != null && response.length === 1) {

                         $scope.elementosFormasPago = response;
                         $scope.ElementosFormasPago = $scope.elementosFormasPago[0];

                     }
                     else {
                         $scope.elementosFormasPago = response;
                     }



                 },

                   function errorFunction() { },
                     function badRequestFunction() { }
                 );



            }
            $scope.initPagos = function () {

                for (var i = 0; i < $scope.formasPagoLista.length; i++) {
                    $scope.status = $scope.formasPagoLista[i].Predeterminado;
                    console.log
                }

            }
            function cargarFormasPagoLista() {

                $scope.formasPagoLista = [];

                $scope.currentPage = 0;
                $scope.pageSize = 6;
                $scope.pages = [];


                
                $scope.FormasPagoLista = {
                    FormaPagoID: '',
                    ProductoID: '',
                    FormaPago: '',
                    Producto: '',
                    Predeterminado: ''
                }

                solicitud = {}
                requestService.post('configurador',
                            'configurador',
                            'consultarFormasPagoLista',
                            solicitud,
                            true,
                            true,
                            true,
                            function successFunction(response) {

                                $scope.formasPagoLista = response;
                                
                            },
                               function errorFunction() { },
                               function badRequestFunction() { }

                              );

            }
            $scope.grabarFormaPagoProducto = function () {
                var formaPago = $("#formaPago option:selected").val();
                var productoPago = $("#productoPago option:selected").val();
                var pred;

                if ($("#chkPred").prop('checked')) {
                    pred = 1;
                }
                else {
                    pred = 0;
                }

                if (formaPago != "" && productoPago != "") {
                    solicitud = {
                        formasPagoProductoModel: {
                            FormaPagoID: formaPago,
                            ProductoID: productoPago,
                            Predeterminado: pred

                        }

                    }

                    requestService.post('configurador',
                 'configurador',
                 'guardarFormaPagoProducto',
                 solicitud,
                 true,
                 true,
                 false,
                 function successFunction(response) {
                     $scope.elementosFormasPago = [];
                     $scope.productosNuevo = [];

                     if ($("#chkPred").prop('checked')) {

                         $("#chkPred").prop('checked', false);
                     }
                     alert("Forma de pago agregada de forma correcta");
                     inicializarModalFormasPago();

                 },
                  function errorFunction(response) {
                      console.log("No se guardo el producto de forma correcta");
                      alert("Ha ocurrido un error. Consultar con el administrador del sistema");
                  },
                     function badRequestFunction() { }



                 );


                }
                else {
                    alert("Los campos forma de pago, producto son obligatorios. Favor de revisar.");

                }

            }
            $scope.actualizarPredeterminadoProducto = function (pagos, productoId, formaPagoId) {
                var res = pagos.Predeterminado = !pagos.Predeterminado;
                actualizarPredPago(res, productoId, formaPagoId);
            }
            function actualizarPredPago(status, productoId, formaPagoId) {
                var res = '';
                if (status == true)
                    res = 1;
                else {
                    res = 0;
                }
                solicitud = {
                    formasPagoProductoModel: {

                        FormaPagoID: formaPagoId,
                        ProductoID: productoId,
                        Predeterminado: res

                    }

                }

                requestService.post('configurador',
                                    'configurador',
                                    'actualizaPredeterminadoProducto',
                                     solicitud,
                                     true,
                                     true,
                                     false,
                                     function successFunction(response) {
                                       

                                     },
                  function errorFunction(response) {
                      console.log("No se guardo el producto de forma correcta");
                  },
                  function badRequestFunction() { }



    );



            }
            $scope.eliminarFormaPago = function (valor1, valor2) {

               
                var bool = confirm("Seguro de eliminar el dato?");
                if (bool) {
                    solicitud = {
                        formasPagoProductoModel: {

                            FormaPagoID: valor1,
                            ProductoID: valor2
                        }

                    }

                    requestService.post('configurador',
                    'configurador',
                    'eliminarFormaDePago',
                    solicitud,
                    true,
                    true,
                    false,
                    function successFunction(response) {

                        $scope.elementosFormasPago = [];
                        $scope.productosNuevo = [];
                        
                        if ($("#chkPred").prop('checked')) {

                            $("#chkPred").prop('checked', false);
                        }
                        
                        $("#modalConfirmPago").modal('hide');
                        inicializarModalFormasPago();

                    },
                     function errorFunction(response) {
                         console.log("No se guardo el producto de forma correcta");
                     },
                     function badRequestFunction() { }

                    );

                    alert("se elimino correctamente");
                } else {
                    alert("cancelo la solicitud");
                }

            }
            function eliminarFormaPago(valor1, valor2) {

            }
            $scope.formaPagoSel = function (combobox) {
            }
            $scope.productoModalPagoSel = function (combobox) {
            }
		    ////// funciones para forma de pago por aseguradora
            $scope.inicializaModalPagoAseguradora = function () {
                inicializaPagosAseguradora();
            }
            function inicializaPagosAseguradora() {
                $scope.plazoPorProducto = [];
                cargaAseguradoras();
                cargarFormasPago();
                cargarProductosModalProductos();
                cargarFormasPagoAseguradoraLista();
                $("#txtCodigo").val("");
            }
            $scope.productosAseguradoraSel = function (combo) {
                var selProducto = $("#productoPagoAseguradora option:selected").val();
                cargarPlazosPorProducto(selProducto);
            }
            function cargarFormasPagoAseguradoraLista() {

                $scope.formasPagoAseguradoraLista = [];

               
                $scope.FormasPagoAseguradoraLista = {
                    Id: '',
                    AseguradoraID: '',
                    FormaPagoID: '',
                    Plazo: '',
                    Codigo: ''
                  
                }

                solicitud = {};
                requestService.post('configurador',
                            'configurador',
                            'consultarFormasPagoAseguradoraLista',
                            solicitud,
                            true,
                            true,
                            true,
                            function successFunction(response) {

                                $scope.formasPagoAseguradoraLista = response;
                                
                            },
                               function errorFunction() { },
                               function badRequestFunction() { }

                              );

            }
            function cargarPlazosPorProducto(producto) {
                $scope.PlazoPorProducto = {
                    ValorId: '',
                    Valor: ''
                }

                $scope.plazoPorProducto = [];
                solicitud = {
                    solicitudRegla: {
                        IdRegla: 1009,
                        IdProducto: producto
                    }
                }
                requestService.post('cotizador',
                'cotizacion',
                'consultaReglaNegocio',
                 solicitud,
                 true,
                 true,
                 true,
                function successFunction(response) {

                    if (response != null && response.length === 1) {
                        $scope.plazoPorProducto = response;
                        $scope.PlazoPorProducto = $scope.plazoPorProducto[0];
                    }
                    else {
                        $scope.plazoPorProducto = response;
                    }



                },
                function errorFunction() { },
                function badRequestFunction() { }

                );


            }
            $scope.grabarProductoAseguradora = function () {
                console.log("entra a metodo grabar");
                var aseguradora = $("#aseguradoraFormaPago option:selected").val();
                var producto = $("#productoPagoAseguradora option:selected").val();
                var formaPago = $("#formaPagoAseguradora option:selected").val();
                var plazo = $("#plazoAseguradora option:selected").val();
                var codigo = $("#txtCodigo").val();

                if (aseguradora != "" && producto != "" && formaPago != "" && plazo != "" && codigo != "") {

                    solicitud = {
                        formasPagoProductoAseguradora: {
                            AseguradoraID: aseguradora,
                            FormaPagoID: formaPago,
                            Plazo: plazo,
                            Codigo: codigo
                        }
                    }

                    requestService.post('configurador',
                    'configurador',
                    'grabarFormasPagoProductoAseguradora',
                    solicitud,
                     true,
                     true,
                     false,
                     function successFunction(response) {

                         inicializaPagosAseguradora();
                         $("#txtCodigo").val("");
                         $scope.plazoPorProducto = [];
                     },
                    function errorFunction(response) {
                        console.log("No se guardo el producto de forma correcta");
                        alert("Ha ocurrido un error. Consultar con el administrador del sistema");
                    },
                    function badRequestFunction() { }



                  );
                }
                else {
                    alert("Todos los datos son obligatorios. Favor de revisar");
                }

            }
            $scope.eliminarFormaPagoAseguradora = function (id) {
                console.log(id);

                $("#modalConfirmPagoAseguradora").modal('show');

                $("#btn-siA").on("click", function () {

                    solicitud = {
                        formasPagoProductoAseguradora: {
                            Id: id
                        }
                    }

                    requestService.post('configurador',
                    'configurador',
                    'eliminarFormaPagoProductoAseguradora',
                    solicitud,
                     true,
                     true,
                     false,
                     function successFunction(response) {

                         $("#modalConfirmPagoAseguradora").modal('hide');
                         inicializaPagosAseguradora();
                         $("#txtCodigo").val("");
                         $scope.plazoPorProducto = [];
                     },
                    function errorFunction(response) {
                        console.log("No se guardo el producto de forma correcta");
                        alert("Ha ocurrido un error. Consultar con el administrador del sistema");
                    },
                    function badRequestFunction() { }



                  );


                });
                $("#btn-noA").on("click", function () {
                    $("#modalConfirmPagoAseguradora").modal('hide');
                });


            }

            $scope.GuardaDocumentoCobertura = function () {

                $scope.uploader.uploadAll();

            }

            var urlUpload = {
                modulo: 'configurador',
                controller: 'configurador',
                action: 'cargaArchivoCobertura'
            }
            var uploader = $scope.uploader = uploadFilesService.__getInstance(urlUpload);

		    //Filtros Podemos validar el tamano del archivo, entre otras validaciones
            uploader.filters.push({
                name: 'sizeFilter',
                fn: function (item) {
                    if ((item.size / 1024 / 1024) > 10) {
                        $scope.mostrarErrorModal("El tama\u00F1o del archivo no debe exceder los 10 MB.");
                        return false;
                    }
                    return true;
                }
            });
            uploader.filters.push({
                name: 'quantityFilter',
                fn: function () {
                    if (uploader.queue.length > 0) {
                        uploader.clearQueue();
                    }
                    return true;
                }
            });

            uploader.filters.push({
                name: 'extensionfilter',
                fn: function (item) {
                    var extensionesPermitidas = new
                    Array("xls",
                        "xlsx",
                        "doc",
                        "docx",
                        "zip",
                        "gif",
                        "jpg",
                        "jpeg",
                        "png",
                        "xml",
                        "pdf",
                        "msg",
                        "XLS",
                        "XLSX",
                        "DOC",
                        "DOCX",
                        "ZIP",
                        "GIF",
                        "JPG",
                        "JPEG",
                        "PNG",
                        "XML",
                        "MSG",
                        "PDF",
                        "mp4");
                    var extension = item.name.split(".").pop();
                    var permitida = false;
                    for (var i = 0; i < extensionesPermitidas.length; i++) {
                        if (extensionesPermitidas[i] === extension) {
                            permitida = true;
                            break;
                        }
                    }
                    if (!permitida) {
                        $scope.mostrarErrorModal("La extensi\u00F3n del archivo no es permitida");
                        return false;
                    } else {
                        return true;
                    }

                }
            });

            uploader.onSuccessItem = function (fileItem, response) {

                var input = $("input[name=file]");
                var fileName = input.val();
                if (fileName) { // returns true if the string is not empty
                    input.val('');
                }
                $scope.uploader.clearQueue();
                if (response.IsOk) {
                    var nombreArchivo = response.Response.NombreArchivo;

                    $scope.Archivos = {

                        NombreArchivo: response.Response.NombreArchivo,
                        RutaArchivo: response.Response.Ruta,

                    };
                    consultarDocsCobertura();
                   
                }
                else {
                    if (response.Message) {
                        $scope.mostrarErrorModal(response.Message);
                    }
                }
                $scope.isArchivoRequired = true;
               
            };

            function consultarDocsCobertura() {
               
                $scope.DocsCoberturas = [];

                solicitud = {}

                requestService.post('configurador',
                         'configurador',
                         'consularDocumentosTodos',
               solicitud,
               true,
               true,
               true,
             function successFunction(response) {

                 $scope.DocsCoberturas = response;
                 console.log("Documentos por coberturas " + $scope.DocsCoberturas.length);

                 guardarDocumentoCobertura();
                
             },
             function errorFunction(response) {

             },
              function badRequestFunction() { }



          );
       }

      function guardarDocumentoCobertura() {
                var bandera = true;
              
                var idProducto = $("#productoP option:selected").val();
                var idCobertura = $scope.cob.IdCobertura;
                var rutaFile = "";
                if ($scope.Archivos != undefined) {
                    rutaFile = $scope.Archivos.RutaArchivo;
                }

                
           for (var i = 0; i < $scope.DocsCoberturas.length; i++) {

               if ($scope.DocsCoberturas[i].IdCobertura === idCobertura) {

                 
                   bandera = false;
               }
               

           }
           if (bandera) {
               solicitud = {
                   documentoPorCobertura: {
                       IdProducto: idProducto,
                       IdCobertura: idCobertura,
                       UrlArchivoCobertura: rutaFile
                   }
               }
               requestService.post('configurador',
                                  'configurador',
                                  'guardaDocumentoCobertura',
                        solicitud,
                        true,
                        true,
                        false,
                      function successFunction(response) {
                          console.log("Se guardo el documento de forma satisfactoria");
                          alert("El documento se guardo de forma satisfactoria.");
                      },
                      function errorFunction(response) {
                          alert("el documento no se guardo de forma satisfactoria");
                      },
                       function badRequestFunction() { }



                   );

           }
           else {
               alert("La cobertura ya tiene un documento asociado. Favor de verificar");

           }

         }

		


		    /*******************************************************************/
		    /*  Modificacion INDRA FJQP -- Configuracion de Directivas */

            $scope.PasaParametroModal = function (coberturaparam) {
                //cob = coberturaparam;
                $scope.cob = coberturaparam;
                console.log(coberturaparam);
                $scope.NumeroCobertura = coberturaparam.IdCobertura;
                $scope.NombreCobertura = coberturaparam.NombreCobertura;
                CargaCoberturasCombo(coberturaparam.IdCobertura, coberturaparam.NombreCobertura);
            };

            function LimpiaCarga() {
                $scope.CobSelect = true;
                $scope.CoberturaSelecciona = false;
                $scope.ValidaCoberturaSel = true;

                $scope.CoberturasCombo = {
                    iAccion: 0,
                    IdCobertura: 0,
                    Nombre: "",
                    idProductoFlex: 0,
                    idProductoFlexAseguradora: 0,
                    CoberturaFija: 0,
                    PerfilCoberturaFija: 0,
                    SumaAseguradaDefault: "",
                    PerfilSumaAsegurada: 0,
                    DeducibleDefault: "",
                    PerfilDeducible: 0,
                    ToolTipCobertura: "",
                    isEspecial: 0,
                    ParametroDeducible: "",
                    ParametroSA: "",
                    RangosModel: {},
                    idProducto: 0,
                    idTipoVehiculo: 0,
                    idTipoServicioVehiculo: 0
                }

                $scope.DeduciblesCombo = {};
                $scope.SumaAseguradaCombo = {};
                $scope.TipoCoberturasCombo = {};


                $scope.TipoCoberturasCombo = [{
                    idTipoCob: 0,
                    TipoCob: "BASICA",
                }, {
                    idTipoCob: 1,
                    TipoCob: "OPCIONAL",
                }];

                $scope.CoberturasComboSel = {
                    iAccion: 0,
                    IdCobertura: 0,
                    Nombre: "",
                    idProductoFlex: 0,
                    idProductoFlexAseguradora: 0,
                    CoberturaFija: 0,
                    PerfilCoberturaFija: 0,
                    SumaAseguradaDefault: "",
                    PerfilSumaAsegurada: 0,
                    DeducibleDefault: "",
                    PerfilDeducible: 0,
                    ToolTipCobertura: "",
                    isEspecial: 0,
                    ParametroDeducible: "",
                    ParametroSA: "",
                    RangosModel: {
                        RangosDeducibles: [],
                        RangosSumas: []
                    },
                    idProducto: 0,
                    idTipoVehiculo: 0,
                    idTipoServicioVehiculo : 0
                }

                $scope.CoberturaSeleccionadaCombo = null
                $scope.TipoCoberturaSeleccionadaCombo = null


                $scope.CoberturaSeleccionadaCombo = {
                    IdCobertura: 0,
                    Nombre: "BASICA"
                }

                $scope.RangoSeleccionadaCombo = {
                }

                $scope.CobSelect = false;
                $scope.CoberturaSelecciona = false;
                $scope.ValidaCoberturaSel = true;

     
               

            }

		    /**********************************************************
    		* Funcion para cargar los datos de la cobertura
    		* Modificacion INDRA FJQP -- Configuracion de Directivas 
    		***********************************************************/
            function CargaCoberturasCombo(CoberturaParam, NombreCoberturaParam) {

                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();

                var directivasModel = {
                    iAccion: 0,
                    IdCobertura: CoberturaParam,
                    Nombre: NombreCoberturaParam,
                    idProductoFlex: 0,
                    idProductoFlexAseguradora: 0,
                    CoberturaFija: 0,
                    PerfilCoberturaFija: 0,
                    SumaAseguradaDefault: "",
                    PerfilSumaAsegurada: 0,
                    DeducibleDefault: "",
                    PerfilDeducible: 0,
                    ToolTipCobertura: "",
                    isEspecial: 0,
                    ParametroDeducible: "",
                    ParametroSA: "",
                    RangosModel: {},
                    idProducto: idProducto,
                    idTipoVehiculo: idTipoVehiculo,
                    idTipoServicioVehiculo : idTipoServicioVehiculo 
                };



                requestService.post('emitir', // Modulo
                        'emitir', // Controlador
                        'recuperaListaCoberturas', // Accion
                        directivasModel, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            $scope.CobSelect = false;
                            $scope.CoberturaSelecciona = false;
                            $scope.ValidaCoberturaSel = true;
                            $scope.CoberturasCombo = response;
                            $scope.CobSelect = true;
                            $scope.OcultaCobSelect = false;
                            CoberturaSel(CoberturaParam);
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
            }

            $scope.tipoCoberturaSel = function () { }

		    /********************************************************************************************************
            ****                          Funcion para la seleccion de la cobertura                           ****
            ********************************************************************************************************/
		    /*  Modificacion INDRA FJQP -- Configuracion de Directivas */
            function CoberturaSel(CobSel) {

                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();

                var directivasModel = {
                    iAccion: 1,
                    IdCobertura: CobSel,
                    Nombre: "",
                    idProductoFlex: 0,
                    idProductoFlexAseguradora: 0,
                    CoberturaFija: 0,
                    PerfilCoberturaFija: 0,
                    SumaAseguradaDefault: "",
                    PerfilSumaAsegurada: 0,
                    DeducibleDefault: "",
                    PerfilDeducible: 0,
                    ToolTipCobertura: "",
                    isEspecial: 0,
                    ParametroDeducible: "",
                    ParametroSA: "",
                    RangosModel: {},
                    idProducto: idProducto,
                    idTipoVehiculo: idTipoVehiculo,
                    idTipoServicioVehiculo : idTipoServicioVehiculo
                };

                /*  Modificacion INDRA FJQP -- Configuracion de Directivas */
                function limpia_deduciblescombo(Default) {
                    var regions = {};
                    var idregions = {};
                    var select = document.getElementById("RangosDeducibleCombo");
                    for (var i = 0; i < regions.length; i++) {
                        select.options[i] = new Option(regions[i], idregions[i]);
                    }

                    if (regions.length == 0 || regions.length === undefined) {
                        document.getElementById("RangosDeducibleCombo").innerHTML = "";
                    }

                    var select = document.getElementById("RangosDeducibleCombo");
                    select.value = Default;
                };

                /*  Modificacion INDRA FJQP -- Configuracion de Directivas */
                function limpia_sumaaseguradacombo(Default) {
                    var regionsa = {};
                    var idregionsa = {};
                    var select = document.getElementById("SumaAseguradaCombo");
                    for (var i = 0; i < regionsa.length; i++) {
                        select.options[i] = new Option(regionsa[i], idregionsa[i]);
                    }

                    if (regionsa.length == 0 || regionsa.length === undefined) {
                        document.getElementById("SumaAseguradaCombo").innerHTML = "";
                    }

                    var select = document.getElementById("SumaAseguradaCombo");
                    select.value = Default;
                };

                /*  Modificacion INDRA FJQP -- Configuracion de Directivas */
                function cargar_deduciblescombo(array, Default) {

                    if (array[0] == "S/D") {

                        var select = document.getElementById("RangosDeducibleCombo");
                        select.value = "S/D";
                        $scope.CoberturasComboSel[0].DeducibleDefault = "S/D";

                        var ValoresSAA = "S/D";
                        var arregloSAA = ValoresSAA.split('|');

                        var regions = arregloSAA;
                        var idregions = arregloSAA;

                        Default = "S/D";

                        var select = document.getElementById("RangosDeducibleCombo");
                        for (var i = 0; i < regions.length; i++) {
                            select.options[i] = new Option(regions[i], idregions[i]);
                        }

                        if (i <= 1) {
                            var select = document.getElementById("RangosDeducibleCombo");
                            select.value = regions[0];
                            $scope.CoberturasComboSel[0].DeducibleDefault = regions[0];
                        } else {
                            var select = document.getElementById("RangosDeducibleCombo");
                            select.value = Default;
                            $scope.CoberturasComboSel[0].DeducibleDefault = Default;
                        }



                    } else
                    {
                        var regions = array;
                        var idregions = array;
                        var select = document.getElementById("RangosDeducibleCombo");
                        for (var i = 0; i < regions.length; i++) {
                            select.options[i] = new Option(regions[i], idregions[i]);
                        }

                        if (i <= 1) {
                            var select = document.getElementById("RangosDeducibleCombo");
                            select.value = regions[0];
                            $scope.CoberturasComboSel[0].DeducibleDefault = regions[0];
                        } else {
                            var select = document.getElementById("RangosDeducibleCombo");
                            select.value = Default;
                            $scope.CoberturasComboSel[0].DeducibleDefault = Default;
                        }
                    }
                }

                /*  Modificacion INDRA FJQP -- Configuracion de Directivas */
                function Cargar_SumaAsegurdacombo(array, Default) {

                    var regionsa = array;
                    var idregionsa = array;
                    var select = document.getElementById("SumaAseguradaCombo");
                    for (var i = 0; i < regionsa.length; i++) {
                        select.options[i] = new Option(regionsa[i], idregionsa[i]);
                    }

                    if (i <= 1) {
                        var select = document.getElementById("SumaAseguradaCombo");
                        select.value = regionsa[0];
                        $scope.CoberturasComboSel[0].SumaAseguradaDefault = regionsa[0];
                    }
                    else {
                        var select = document.getElementById("SumaAseguradaCombo");
                        select.value = Default;
                        $scope.CoberturasComboSel[0].SumaAseguradaDefault = Default;
                    }

                }

                /*  Modificacion INDRA FJQP -- Configuracion de Directivas */
                function Cargar_TipoCobertura(array, array2, Default) {

                    var regionsaa = array;
                    var idregionsaa = array2;
                    var select = document.getElementById("TipoCoberturaCombo");
                    for (var i = 0; i < regionsaa.length; i++) {
                        select.options[i] = new Option(regionsaa[i], idregionsaa[i]);
                    }

                    var select = document.getElementById("TipoCoberturaCombo");
                    select.value = Default;
                }



                /*  Modificacion INDRA FJQP -- Configuracion de Directivas */


                requestService.post('emitir', // Modulo
                        'emitir', // Controlador
                        'recuperaInfoCoberturas', // Accion
                        directivasModel, // Parametros
                        true, // Bloquear Interfaz/Vista
                        true, // Mostrar errores
                        true, // es "SingleResponse"
                        function successFunction(response) {
                            $scope.CoberturasComboSel = response;
                            $scope.OcultaCobSelect = false;
                            limpia_deduciblescombo("");
                            limpia_sumaaseguradacombo("");
                            $scope.RangosDeducibleCombo = "";
                            $scope.SumaAseguradaCombo = "";

                            $scope.CoberturaSelecciona = true;
                            $scope.ValidaCoberturaSel = false;
                            $scope.CobSelect = true;

                            
                            var ValoresDeducibles = $scope.CoberturasComboSel[0].rangosModel.RangosDeducibles;
                            var defaulDeducibles = $scope.CoberturasComboSel[0].DeducibleDefault;
                            cargar_deduciblescombo(ValoresDeducibles, defaulDeducibles);

                            var ValoresSA = $scope.CoberturasComboSel[0].rangosModel.RangosSumas;
                            var defaulSA = $scope.CoberturasComboSel[0].SumaAseguradaDefault;
                            Cargar_SumaAsegurdacombo(ValoresSA, defaulSA);

                            var ValoresSAA = "BASICA|OPCIONAL";
                            var ValoresSAA2 = "0|1";
                            var arregloSAA = ValoresSAA.split('|');
                            var arregloSAA2 = ValoresSAA2.split('|');

                            if ($scope.CoberturasComboSel[0].CoberturaFija == 1) {
                                var defaulSAA = "0";
                                $scope.confirmedFija = true;
                            } else {
                                var defaulSAA = "1";
                                $scope.confirmedFija = false;
                            };
                            Cargar_TipoCobertura(arregloSAA, arregloSAA2, defaulSAA);

                            if ($scope.CoberturasComboSel[0].SumaAseguradaDefault == "") {
                                $scope.confirmedRangos = false;
                            }
                            else {
                                $scope.confirmedRangos = true;
                                $scope.SumaAseguradaDefault = document.getElementById("SumaAseguradaCombo").value;
                            }

                            if ($scope.CoberturasComboSel[0].DeducibleDefault == "") {
                                $scope.confirmedDeducibles = false;
                                $scope.DefaultDeducibles = "";
                            }
                            else {
                                $scope.confirmedDeducibles = true;
                                $scope.DefaultDeducibles = document.getElementById("RangosDeducibleCombo").value;
                            }


                            if ($scope.CoberturasComboSel[0].CoberturaFija == 1) {
                                $scope.confirmedFija = true;
                            }
                            else {
                                $scope.confirmedFija = false;
                            }
                            if ($scope.CoberturasComboSel[0].PerfilCoberturaFija == 1) {
                                $scope.confirmedAdmin = true;
                            }
                            else {
                                $scope.confirmedAdmin = false;
                            }
                            if ($scope.CoberturasComboSel[0].PerfilSumaAsegurada == 1) {
                                $scope.confirmedRangosAdmin = true;

                            }
                            else {
                                $scope.confirmedRangosAdmin = false;
                            }
                            if ($scope.CoberturasComboSel[0].PerfilDeducible == 1) {
                                $scope.confirmedDeduciblesAdmin = true;
                            }
                            else {
                                $scope.confirmedDeduciblesAdmin = false;
                            }

                            $scope.ToolTipCobertura = $scope.CoberturasComboSel[0].ToolTipCobertura;
                            $scope.IsEspecial = $scope.CoberturasComboSel[0].isEspecial;
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
            }

		    /*  Modificacion INDRA FJQP -- Configuracion de Directivas */

            $scope.changeFija = function () {

                if ($scope.confirmedFija) {
                    document.getElementById("TipoCoberturaCombo").value = "BASICA"
                }
                else {
                    document.getElementById("TipoCoberturaCombo").value = "OPCIONAL"
                }
            };
            $scope.changeFijaAdmin = function () {

                if ($scope.confirmedAdmin) {
                    $scope.iCoberturafija = 1;
                }
                else {
                    $scope.iCoberturafija = 0;
                }
            };
            $scope.changeRangosAdmin = function () {

                if ($scope.confirmedRangosAdmin) {

                }
                else {

                }
            };
            $scope.changeDeduciblesAdmin = function () {

                if ($scope.confirmedDeduciblesAdmin) {

                }
                else {

                }
            };
            $scope.changeRangosSumas = function () {

                if ($scope.confirmedRangos) {

                    $scope.DefaultSumasAseguradas = document.getElementById("SumaAseguradaCombo").value;

                }
                else {

                    $scope.DefaultSumasAseguradas = "";
                }
            };
            $scope.changeRangosDeducibles = function () {

                if ($scope.confirmedDeducibles) {

                    $scope.DefaultDeducibles = document.getElementById("RangosDeducibleCombo").value;

                }
                else {

                    $scope.DefaultDeducibles = "";
                }
            };

		    /*  Modificacion INDRA FJQP -- Configuracion de Directivas */

            $scope.GrabarConfiguracion = function (CoberturaId) {
             
                var r = confirm("Esta usted seguro de almacenar la configuración con los valores seleccionados.?");
                if (r != true) {
                    return;
                }

                var directivasGrabadoModel = {}

                if (CoberturaId == undefined) {
                    CoberturaId = $scope.NumeroCobertura;
                }

                var idProducto = $("#productoP option:selected").val();
                var idTipoVehiculo = $("#tipoAutoP option:selected").val();
                var idTipoServicioVehiculo = $("#tipoServicioP option:selected").val();

                directivasGrabadoModel.iAccion = 2;

                
                directivasGrabadoModel.IdCobertura = CoberturaId;
                directivasGrabadoModel.Nombre = "";
                directivasGrabadoModel.idProductoFlex = 0;
                directivasGrabadoModel.idProductoFlexAseguradora = 0;

                if ($scope.confirmedFija)
                { directivasGrabadoModel.CoberturaFija = 1 }
                else
                { directivasGrabadoModel.CoberturaFija = 0 };

                if ($scope.confirmedAdmin)
                { directivasGrabadoModel.PerfilCoberturaFija = 1 }
                else
                { directivasGrabadoModel.PerfilCoberturaFija = 0 }

                if ($scope.confirmedRangos) {
                    directivasGrabadoModel.SumaAseguradaDefault = document.getElementById("SumaAseguradaCombo").value;
                } else {
                    directivasGrabadoModel.SumaAseguradaDefault = "";
                }

                if ($scope.confirmedRangosAdmin)
                { directivasGrabadoModel.PerfilSumaAsegurada = 1 }
                else
                { directivasGrabadoModel.PerfilSumaAsegurada = 0 }

                if ($scope.confirmedDeducibles) {
                    directivasGrabadoModel.DeducibleDefault = $scope.DefaultDeducibles;
                } else {

                    directivasGrabadoModel.DeducibleDefault = "";
                }

                if ($scope.confirmedDeduciblesAdmin)
                { directivasGrabadoModel.PerfilDeducible = 1 }
                else
                { directivasGrabadoModel.PerfilDeducible = 0 }

                if ($scope.confirmedFija) {
                    document.getElementById("TipoCoberturaCombo").value = "BASICA"
                }
                else {
                    document.getElementById("TipoCoberturaCombo").value = "OPCIONAL"
                }

                directivasGrabadoModel.ToolTipCobertura = $scope.ToolTipCobertura;
                directivasGrabadoModel.isEspecial = $scope.CoberturaSeleccionadaCombo.isEspecial;
                directivasGrabadoModel.ParametroDeducible = $scope.CoberturaSeleccionadaCombo.ParametroDeducible;
                directivasGrabadoModel.ParametroSA = $scope.CoberturaSeleccionadaCombo.ParametroSA;

                directivasGrabadoModel.idProducto = idProducto;
                directivasGrabadoModel.idTipoVehiculo = idTipoVehiculo;
                directivasGrabadoModel.idTipoServicioVehiculo = idTipoServicioVehiculo;

                 
                requestService.post('emitir', // Modulo
                    'emitir', // Controlador
                    'almacenaInfoCoberturas', // Accion
                    directivasGrabadoModel, // Parametros
                    true, // Bloquear Interfaz/Vista
                    true, // Mostrar errores
                    true, // es "SingleResponse"
                    function successFunction(response) {
                        $scope.CoberturasComboSel = response;
                        alert('Las directivas de la aseguradora y cobertura seleccionada han sido almacebadas con éxito.');
                        inicialPanel();
                    },
                    function errorFunction() {
                        alert('Las directivas de la aseguradora y cobertura seleccionada no se almacenaron, favor de verificar.');
                    },
                    function badRequestFunction() {
                        alert('Las directivas de la aseguradora y cobertura seleccionada no se almacenaron, favor de verificar.');
                    }
                   );

        }

		    }

        ]);

    });
