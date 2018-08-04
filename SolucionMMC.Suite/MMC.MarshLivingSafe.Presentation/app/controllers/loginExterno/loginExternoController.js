define(['modules/app', 'services/requestService', 'services/mappingService'], function (app) {
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
    app.controller('LoginExternoController', ['$scope', '$rootScope', '$location', 'requestService', '$localStorage', 'vcRecaptchaService', '$sessionStorage', '$http', 'mappingService', '$base64', '$stateParams',
		function ($scope, $rootScope, $location, requestService, $localStorage, vcRecaptchaService, $sessionStorage, $http, mappingService, $base64, $stateParams) {
                $rootScope.tittle = "";
                $rootScope.bMenu = true;

                $rootScope.pageTitle = "Marsh";
                $rootScope.bMenu = false;
                $rootScope.tittle = "Bienvenido ";
                $rootScope.persona = ""; 
                $sessionStorage.version = '';
                $rootScope.bLogOut = false; 

                //Se limpia el tkn para un nuevo inicio 
                if ($sessionStorage.tkn != undefined) {
                    $sessionStorage.tkn = undefined;
                    $sessionStorage.menusDisponibles = undefined;
                    $sessionStorage.version = '';
                    limpiarMenus();
                    $rootScope.initClavesMenu();
                    $rootScope.persona = ""; /* INDRA FJQP Nombre de Usuario en Pantalla de Inicio */
                    $rootScope.bLogOut = false; /* INDRA FJQP Nombre de Usuario en Pantalla de Inicio */
                }

            
		    //Cuando termina de cargar la pagina de login externo
            // Se ejecuta este codigo
                angular.element(document).ready(function () {
                    $scope.startloginExterno();
                });

            //Toma el token que se paso como parametro, para iniciar una sesion
                $scope.startloginExterno = function () {
                    var a = $stateParams.tkn;
                    var data =
                        {
                            token_id: $stateParams.tkn,
                            profile_id: ''

                        }

                    processLogin(true, data);

                }


                function processLogin(requestSuccess, data) {
                    if (requestSuccess) {

                        $sessionStorage.tkn = $base64.encode(JSON.stringify(data.token_id));
                        $rootScope.bMenu = true;
                        //Se consultan los menus disponibles de acuerdo al Token
                        // Se agrega el elemento NombreUsu
                        // INDRA FJQP
                        var solicitud = {
                            menuModel: {
                                PerfilId: '',
                                PersonaId: '',
                                NombreUsuario: ''  // INDRA  FJQP Se agrega el elemento NombreUsu
                            }
                        }



                        requestService.post('seguridad', // Modulo
                            'login', // Controlador
                            'consultaMenus', // Accion
                            solicitud, // Parametros
                            true, // Bloquear Interfaz/Vista
                            true, // Mostrar errores
                            true, // es "SingleResponse"
                            function successFunction(response) {
                                $sessionStorage.menusDisponibles = response;
                                $rootScope.cargarMenus();
                                // Llamado al método que obtiene la última versión en BD para compararla con la versión guardada en cache
                                consultaVersionSistema();
                                $rootScope.bLogOut = true; /* FJQP Nombre de Usuario en Pantalla de Inicio */
                            },
                            function errorFunction() { },
                            function badRequestFunction() { });
                        $location.url('/blank');

                    } else {
                        $rootScope.toggleModalErrors('Ocurrio un error al intentar iniciar sesion.');
                    }
                }


                $scope.user = {
                    Username: '',
                    Password: ''
                }

                function limpiarMenus() {
                    $rootScope.menusLimpios = {
                        General: {
                            Nombre: 'General',
                            Clave: '',
                            Mostrar: false
                        },
                        Reporte: {
                            Nombre: 'Reporte',
                            Clave: '',
                            Mostrar: false
                        },
                        Registro: {
                            Nombre: 'Registro',
                            Clave: '',
                            Mostrar: false
                        },
                        Calendario: {
                            Nombre: 'Calendario',
                            Clave: '',
                            Mostrar: false
                        },
                        Parametros: {
                            Nombre: 'Parametros',
                            Clave: '',
                            Mostrar: false
                        }
                    }
                    $rootScope.menus = $rootScope.menusLimpios;
                }
              

             



                /********************************************************************************************************************************
         **** Funcion para consultar la última versión del sistema y en caso de desplegar una versión nueva borrar cache del sistema ****
         ********************************************************************************************************************************/
                function consultaVersionSistema() {
                    requestService.post('seguridad',
                        'login',
                        'consultaVersionSistema',
                        null,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $sessionStorage.version = response.Version;

                            if ($sessionStorage.version !== $rootScope.versionSistema) {
                                this.$get = [
                                    '$cacheFactory', function ($cacheFactory) {
                                        return $cacheFactory('templates').removeAll();
                                    }
                                ];
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { });
                }

                $scope.response = null;
                $scope.widgetId = null;
                $scope.model = {
                    key: '6Ld8DxcUAAAAAP47Qhf4YagIY71f0XZir7o0gsOv'
                };
                $scope.setResponse = function (response) {
                    $scope.response = response;
                };
                $scope.setWidgetId = function (widgetId) {
                    $scope.widgetId = widgetId;
                };
                $scope.cbExpiration = function () {
                    vcRecaptchaService.reload($scope.widgetId);
                    $scope.response = null;
                };

             
              

              


            



            


                //function esUsuarioAdmin() {


                //    var request = {
                //        Tkn: JSON.parse($base64.decode($sessionStorage.tkn))

                //    }
                //    var url = mappingService.services['manuales']['manuales']['usuarioAdministador'];
                //    $http.post(url, request)
                //         .then(function success(dataee) {


                //             $scope.UsuarioAdmin = dataee.data.Response;


                //         },
                //             function error(data) {

                //             });



                //}



                //eND cONTRLLER

            }
        ]);

    });