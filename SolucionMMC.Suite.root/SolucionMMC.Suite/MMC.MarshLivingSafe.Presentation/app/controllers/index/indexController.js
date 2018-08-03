define(['modules/app', 'services/requestService'], function (app) {
    /**
	 * DEFINIMOS Y REGISTRAMOS EL CONTROLLER indexController.
	 * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
	 * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
	 * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
	 * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
	 *
	 * Var
	 * pageTitle = Título del sitio Web
	 * tittle = Titulo de la página  
	 * 
	 * Modifiacion INDRA FJQP 08/05/2018
	 * 
	 */
    app.controller('IndexController', ['$scope', '$rootScope', '$location', 'requestService', '$sessionStorage',
		function ($scope, $rootScope, $location, requestService, $sessionStorage) {
		    $scope.pageTitle = "Marsh";
		    $rootScope.bMenu = false;
		    $rootScope.titulo = "BIENVENIDO ";
		    $rootScope.tittle = "BIENVENIDO ";
		    $rootScope.persona = " "; /* INDRA FJQP Nombre de Usuario en Pantalla de Inicio */
		    $scope.showModalErrors = false;
		    $scope.modalErrorsType = "info";
		    $rootScope.versionSistema = "1.0";
		    $rootScope.bLogOut = false; /* INDRA FJQP Nombre de Usuario en Pantalla de Inicio */

		    $rootScope.menus = {
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

		    $scope.jsonListaMenus = {
		        Nombre: '',
		        Clave: ''
		    }
		    validaVersionSistema();

		    $rootScope.initClavesMenu = function () {
		        var count = Object.keys($rootScope.menus).length;
		        for (var j = 0; j < jsonListaMenus.length; j++) {
		            for (var i in $rootScope.menus) {
		                if ($rootScope.menus.hasOwnProperty(i)) {
		                    if (
								jsonListaMenus[j].Nombre === i) {
		                        $rootScope.menus[i].Clave = jsonListaMenus[j].Clave;
		                    }
		                }
		            }
		        }
		    };
		    $rootScope.initClavesMenu();

		    $rootScope.bLogOut = true; /* INDRA FJQP Nombre de Usuario en Pantalla de Inicio */

		    $rootScope.cargarMenus = function () {
		        var response = $sessionStorage.menusDisponibles;

		        for (var i in $rootScope.menus) {
		            if ($rootScope.menus.hasOwnProperty(i)) {
		                for (var r in response) {
		                    $rootScope.tittle = "BIENVENIDO ";
		                    $rootScope.persona = " " + response[r].NombreUsuario; /* INDRA FJQP Nombre de Usuario en Pantalla de Inicio */
		                    $sessionStorage.sessionperfilId = response[0].PerfilId;
		                    $sessionStorage.sessionpersonaid = response[0].PersonaId;
		                    $sessionStorage.sessionManejaUDI = response[0].ManejaUDI;
		                    $rootScope.manejaudi = response[0].ManejaUDI;
		                    if (response[0].PerfilId == 47 || response[0].PerfilId == "47") {
		                        $rootScope.isAdmin = true;
		                    } else {
		                        $rootScope.isAdmin = false;
		                    }

		                    if (response.hasOwnProperty(r)) {
		                        if (response[r].ClaveMenu === $rootScope.menus[i].Clave) {
		                            $rootScope.menus[i].Mostrar = true;
		                        }
		                    }
		                }
		            }
		        }
		    }

		    $rootScope.cargarMenus();


		    $scope.goHome = function () {
		        $location.url("/blank");
		    }

		    $rootScope.toggleModalErrors = function (msg, modalErrorsType) {
		        if (modalErrorsType) {
		            $scope.modalErrorsType = modalErrorsType;
		        }
		        if (!$scope.showModalErrors) {
		            $("#msgModalErrors").html(msg);
		        } else {
		            $("#msgModalErrors").empty();
		        }
		        $scope.showModalErrors = !$scope.showModalErrors;
		    }

		    $rootScope.$watch(function () {
		        return $sessionStorage.tkn;
		    }, function (newVal, oldVal) {
		        if (oldVal !== newVal && newVal === undefined) {
		            $scope.showModalErrors = true;
		            $scope.modalErrorsType = "info"; /* INDRA FJQP Nombre de Usuario en Pantalla de Inicio */
		            $("#msgModalErrors").html('Sesión finalizada con éxito');
		            $rootScope.bMenu = false;
		            $location.url("/login");
		        } else {
		            $rootScope.bMenu = true;
		        }
		    });

		    function validaVersionSistema() {
		        if ($sessionStorage.version !== undefined && $sessionStorage.version !== "") {
		            if ($sessionStorage.version !== $rootScope.versionSistema) {
		                this.$get = [
							'$cacheFactory', function ($cacheFactory) {
							    $cacheFactory('templates').destroy();
							    return $cacheFactory('$templates').removeAll();
							}
		                ];
		            }
		        }
		    }
		}
    ]);

});