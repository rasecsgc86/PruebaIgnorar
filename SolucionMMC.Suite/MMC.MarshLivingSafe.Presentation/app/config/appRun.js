/**
 * @author <Josue Ramirez Davila>
 * @mail <josueramirezdavila@gmail.com>
 * @description SE REALIZAN LAS CONFIGURACIONES QUE SE EJECUTARAN CON EL METODO RUN DE ANGULAR
 *
 */

define(['modules/app'], function(app)
{
    app.run(function ($rootScope, $location, blockUI) {

		/* Bloqueamos la aplicacion en lo que se carga la pagina con $routeChangeStart */
	  $rootScope.$on('$routeChangeStart', function(next, current)
	  {
			/*$rootScope.startBlockUI();*/
		});

	    /* Desbloqueamos la aplicaicon $routeChangeSuccess */
		$rootScope.$on('$routeChangeSuccess', function(next, current)
		{
			$rootScope.stopBlockUI();
		});

		/* Desbloqueamos la aplicaicon $routeChangeError */
		$rootScope.$on('$routeChangeError', function(next, current)
		{
			$rootScope.stopBlockUI();
		});

		/* Desbloqueamos la aplicaicon $routeUpdate */
		$rootScope.$on('$routeUpdate ', function(next, current)
		{
			$rootScope.stopBlockUI();
		});

		/* Bloqueamos la aplicacion en lo que se carga la pagina con $locationChangeStart */
		$rootScope.$on("$locationChangeStart",function(event, next, current)
		{
        	$rootScope.startBlockUI();
    	});

		/* Desbloqueamos la aplicaicon $locationChangeSuccess */
		$rootScope.$on('$locationChangeSuccess', function(next, current)
		{
			$rootScope.stopBlockUI();
		});

		/* Desbloqueamos la aplicaicon $locationChangeError */
		$rootScope.$on('$locationChangeError', function(next, current)
		{
			$rootScope.stopBlockUI();
		});

		/* Desbloqueamos la aplicaicon $locationUpdate */
		$rootScope.$on('$locationUpdate ', function(next, current)
		{
			$rootScope.stopBlockUI();
		});

		/* Funcion para bloquear la aplicacion */
		$rootScope.startBlockUI = function()
		{
			blockUI.start('Cargando, por favor espere...');
		}

		/* Funcion para desbloquear la aplicacion */
		$rootScope.stopBlockUI = function()
		{
			blockUI.stop();
		}

	
	});
});
