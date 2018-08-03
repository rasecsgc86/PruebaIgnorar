define(['modules/app', 'services/requestService', 'services/mappingService'], function (app) {
    /**
	 * DEFINIMOS Y REGISTRAMOS EL CONTROLLER indexController.
	 * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
	 * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
	 * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
	 * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
	 *
	 * 
	 * @var pageTitle - TITULO DEL SITIO WEB
	 * @var tittle - TITULO DE LA PAGINA  
	 * @bMenu - BANDERA PARA MOSTRAR EL MENU
	 * 
	 * Modificación INDRA FJQP 08/05/2018
	 */
    app.controller('LoginController', ['$scope', '$rootScope', '$location', 'requestService', '$localStorage', 'vcRecaptchaService', '$sessionStorage', '$http', 'mappingService', '$base64',
		function ($scope, $rootScope, $location, requestService, $localStorage, vcRecaptchaService, $sessionStorage, $http, mappingService, $base64) {
		    $rootScope.pageTitle = "Marsh";
		    $rootScope.bMenu = false;
		    $rootScope.tittle = "Bienvenido ";
		    $rootScope.persona = ""; /* INDRA FJQP Nombre de Usuario en Pantalla de Inicio */
		    $sessionStorage.version = '';
		    $rootScope.bLogOut = false; /* INDRA FJQP Nombre de Usuario en Pantalla de Inicio */

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
		    $scope.login = function () {
		        var url = mappingService.services['seguridad']['login']['login'];
		        $http.post(url, $.param($scope.user))
					.then(function success(data) {
					    var resp = JSON.stringify(data.data);
					    processLogin(true, resp);
					},
						function error(data) {
						    processLogin(false);
						});
		        //processLogin(true, JSON.stringify('{"token_id": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluaXYzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy91c2VyZGF0YSI6IkNhcmxvcyBNZW5kZXogUmFtaXJleiIsIm5hbWVpZCI6IjQ0MDc5NyIsInJvbGUiOiI0NyIsIm5iZiI6MTQ5MjUzOTExOCwiZXhwIjoxNDkyNTQyNzE4LCJpYXQiOjE0OTI1MzkxMTh9.zkqobApGHRB5VO4KsA_YNivP4DKxol7lgzDILUaDmx8"}'));
		        /*$sessionStorage.tkn = $base64.encode(JSON.stringify("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluaXYzIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy91c2VyZGF0YSI6IkNhcmxvcyBNZW5kZXogUmFtaXJleiIsIm5hbWVpZCI6IjQ0MDc5NyIsInJvbGUiOiI0NyIsIm5iZiI6MTQ5MjUzOTExOCwiZXhwIjoxNDkyNTQyNzE4LCJpYXQiOjE0OTI1MzkxMTh9.zkqobApGHRB5VO4KsA_YNivP4DKxol7lgzDILUaDmx8"));
				$rootScope.bMenu = true;
				$location.url('/blank');*/
		    }

		    function processLogin(requestSuccess, data) {
		        if (requestSuccess) {
		            if (data.indexOf("token_id") > 0) {
		                $sessionStorage.tkn = $base64.encode(JSON.stringify(JSON.parse(data).token_id));
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
							function errorFunction() {},
							function badRequestFunction() {});
		                $location.url('/blank');
		            } else if (data.indexOf("error") > 0) {
		                $rootScope.toggleModalErrors(JSON.parse(data).error);
		            }
		        } else {
		            $rootScope.toggleModalErrors('Ocurrio un error al intentar iniciar sesion.');
		        }
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
					function errorFunction() {},
					function badRequestFunction() {});
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
		}
    ]);

});