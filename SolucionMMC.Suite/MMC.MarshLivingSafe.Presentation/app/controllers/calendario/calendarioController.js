define(['modules/app', 'services/requestService'], function (app) {
    /**
	 * DEFINIMOS Y REGISTRAMOS EL CONTROLLER CalendarioController.
	 * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
	 * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
	 * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
	 * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
	 *
	 * @var tittle - TITULO DE LA PAGINA  
	 */
    app.controller('CalendarioController', ['$scope', '$rootScope', '$location', 'requestService', '$localStorage',
		function ($scope, $rootScope, $location, requestService, $localStorage) {
		    $rootScope.tittle = "Calendario";
		    $scope.calendario = [];
		    $scope.listaCalendario = [];
		    var calendarioModel = {

		    };
		    cargarCalendarios();
		    $scope.guardarCalendarioTickest= function () {
		        calendarioModel =  {
		            Dia : $scope.Dia
		        }
		        requestService.post('tickets',
		            'calendario',
		            'guardarCalendario',
		            calendarioModel ,
		            true,
		            true,
		            true,
		            function successFunction(response) {
		                $scope.calendario = response;
		                cargarCalendarios();
		                $scope.Dia = null;
		            },
		            function errorFunction() {
		                
		            },
		            function badRequestFunction(response) {
		            });
		    };

		    $scope.eliminarCalendario = function (id) {
                calendarioModel = {
                    IdDiaHabil: id
                }
                requestService.post('tickets',
		            'calendario',
		            'eliminarCalendario',
		            calendarioModel,
		            true,
		            true,
		            true,
		            function successFunction(response) {
		                //$scope.calendario = response;
		                cargarCalendarios();
		                $scope.Dia = null;
		            },
		            function errorFunction() { },
		            function badRequestFunction(response) { });
            }

		    function cargarCalendarios() {
		        requestService.post('tickets',
		            'calendario',
		            'consultarCalendario',
                    null,//en caso de que no lleve parametros puse null
		            true,
		            true,
		            true,
		            function successFunction(response) {
		                $scope.listaCalendario = response;
		            },
		            function errorFunction() { },
		            function badRequestFunction(response) { });
		    };


		    //#######################################################
		    //#################   Calendario   ######################
			//#######################################################
			var date = new Date();
			date.setDate(date.getDate() + 1);
		    $scope.dateOptions = {
		        dateDisabled: disabled,
				formatYear: 'yy',
				minDate: date,
				showTodayButton: true
		    };
		    $scope.toggleMin = function () {

		    };
		    $scope.toggleMin();
		    // Disable weekend selection
		    function disabled(data) {
		        var date = data.date,
                  mode = data.mode;
		        return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
		    }

		    $scope.open1 = function () {
		        $scope.popup1.opened = true;
		    };

		    $scope.popup1 = {
		        opened: false
		    };

		}
    ]);

});