/**
 * @author <Josue Ramirez Davila>
 * @mail <josueramirezdavila@gmail.com>
 * @description CONFIGURAMOS EL MODULO BREADCRUMBS DE NUESTRA APLICACION EN ANGULAR
 *
 */
define(['modules/app'], function(app)
{
	return app.config(['$stateProvider', function($stateProvider)
    {
    	/* PATHS DONDE ESTAN LOS CONTROLLERS Y LAS VISTAS */
			var PATHS = {
					controllers : ".app/controllers/",
					htmlTemplates : "./views/"
			};

			/* FUNCION QUE ARMA LA RUTA DEL TEMPLATE A RESOLVER */
			function templateUrlResolve(path) {
					return PATHS.htmlTemplates + path + '/' + path + SUFIJOS.html;
			}

			/* SUFIJOS PARA LOS CONTROLLERS Y LAS VISTAS */
			var SUFIJOS = {
					Controller : "Controller",
					html       : ".html"
			}

			/* RUTAS DE LA APLICACION */
    	/*$stateProvider
	      	.state('index', {
	        		url: '/',
	        		templateUrl: templateUrlResolve('index'),
	        		data: {
	                displayName: 'Marsh'
	            }
	      	})
	      	.state('index.home', {
	        		url: '^/home',
	        		templateUrl: templateUrlResolve('home'),
	        		data: {
	                displayName: 'Home'
	            }
	      	});*/
    }]);
});
