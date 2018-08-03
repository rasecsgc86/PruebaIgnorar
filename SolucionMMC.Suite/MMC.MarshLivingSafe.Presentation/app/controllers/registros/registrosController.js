define(['modules/app', 'services/requestService'], function(app)
{
	/**
	 * DEFINIMOS Y REGISTRAMOS EL CONTROLLER homeController.
	 * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
	 * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
	 * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
	 * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
	 *
	 * @var tittle - TITULO DE LA PAGINA  
	 */
	app.controller('RegistrosController', ['$scope', '$rootScope','$location', 'requestService', '$localStorage', 'Notification',
		function($scope,$rootScope, $location, requestService, $localStorage, Notification)
    	{
		 	$rootScope.tittle = "Registros de tickets";
    	}
    ]);

});