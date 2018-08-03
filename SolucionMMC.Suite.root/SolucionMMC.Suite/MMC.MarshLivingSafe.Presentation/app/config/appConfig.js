/**
 * @author <Josue Ramirez Davila>
 * @mail <josueramirezdavila@gmail.com>
 * @description SE REALIZAN LAS CONFIGURACIONES DE DIFERENTES COMPONENTES
 *
 */

define(['modules/app'], function (app)
{

    app.config(function (NotificationProvider, $httpProvider) {

		/* Valores configurados por default al componente de Notifications */
        NotificationProvider.setOptions({
            delay: 3000,
            startTop: getDocHeight() / 2,
            startRight: 10,
            verticalSpacing: 20,
            horizontalSpacing: 20,
            positionX: 'center',
            positionY: 'top'
        });

		/* Obtiene el height de la pagina */
        function getDocHeight() {
    		var D = document;
    		return Math.max(
		    	D.body.scrollHeight, D.documentElement.scrollHeight,
		    	D.body.offsetHeight, D.documentElement.offsetHeight,
		    	D.body.clientHeight, D.documentElement.clientHeight
   			);
        }

		/* Registramos el interceptor de $http */
        $httpProvider.interceptors.push('requestInterceptor');

    });
});
