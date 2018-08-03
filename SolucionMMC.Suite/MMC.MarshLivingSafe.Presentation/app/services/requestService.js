/**
 * @author <Josue Ramirez Davila>
 * @mail <josueramirezdavila@gmail.com>
 *
 * SE DEFINE EL SERVICIO QUE EJECUTA
 * LAS PETICIONES AL API-REST
 *
 */

define(['modules/app', 'services/mappingService'], function(app)
{
    app.service('requestService', ['$rootScope', '$http', '$q', 'mappingService', '$sessionStorage', '$base64', function ($rootScope, $http, $q, mappingService, $sessionStorage, $base64)
	{

		/**
		 * @author <Josue Ramirez Davila>
 		 * @mail <josueramirezdavila@gmail.com>
		 * @description METODO PARA REALIZAR LAS PETICIONES POST AL REST-CONTROLLER
		 * @param module - Modulo|paquete en los RestController de JAVA
		 * @param controller - Controller de JAVA
		 * @param action - Accion del controller JAVA que se ejecutara
		 * @return promis - Se retorna la promesa generada por la petcion
		 *
		 */
		this.post = function (module, controller, action, params, isBlockUI, showErrors, isSingleResponse, successFunction, errorFunction, badRequestFunction, showSuccessMessage)
		{
            $rootScope.messages = "";
			/* OBTENEMOS LA URL DE ACUERDO A LOS PARAMETROS*/
			var url = mappingService.services[module][controller][action];

			/* VALIDAMOS SI SE REQUIERE BLOQUEAR UI */
			if (isBlockUI)
			{
				$rootScope.startBlockUI();
			}

            /**
             * Armamos el POST
             */
			if (params) {
			    params['Tkn'] = JSON.parse($base64.decode($sessionStorage.tkn));
			    params = JSON.stringify(params);
			} else {
			    params = {}
			    params['Tkn'] = JSON.parse($base64.decode($sessionStorage.tkn));
			}

            /**
             * Ejecutamos la peticion AJAX POST
             */
		    $http.post(url, params)
                .then(function success(data) {
		            data = data.data;
                    /* VERIFICAMOS SI LA RESPUESTA ES DE TIPO SingleResponse */
                    if (isSingleResponse) {
                        /* VERIFICAMOS EL CODIGO DE ERROR REGRASDO POR EL REST-CONTROLLER
                        * EN EL OBJETO SingleResponse<T> */
                        if (data.IsOk) {
                            /* SI EL CODIGO DE ERROR DEL REST-CONTROLLER
                            * ES EXITOSO, SI ES ASI EXECUTAMOS LA FUNCION SUCCES RECIBIDA. */

                            if (showSuccessMessage && data.Message && data.Message !== '') {
                                $rootScope.toggleModalErrors(data.Message, "success");
                            }
                            if (successFunction) {
                                successFunction(data.Response);
                            }
                        } else {
                            /* SI EL CODIGO DE ERROR DEL REST-CONTROLLER
                            * ES ERRONEO, RESOLVEMOS LA PROMESA CON ESTATUS ERRONEA . */

                            if (showErrors) {
                                var message = "";
                                if (data.Validations && data.Validations.length > 0) {
                                    $.each(data.Validations,
                                        function(item, i) {
                                            message += i.Message + "<br>";
                                        });
                                } else if (data.Message && data.Message !== '') {
                                    message = data.Message;
                                }
                                $rootScope.toggleModalErrors(message);
                            }

                            /* EJECUTAMOS FUNCION ERROR */
                            if (errorFunction) {
                                errorFunction(data.Response);
                            }
                        }
                    } else {
                        /* EJECUTAMOS FUNCION SUCCES PARA LAS PETICIONES NO SingleResponse */
                        if (successFunction) {
                            successFunction(data);
                    }
                }
            },
            function error(data) {
                /* SI OCURRE UN EROR EN LA PETICION AJAX AL REST-CONTROLLER
                * , EJECUTAMOS LA FUNCION BADREQUEST RECIBIDA . */

                if (badRequestFunction) {
		            badRequestFunction(data);
                }

		        $rootScope.toggleModalErrors("Error al hacer la peticion.", "danger");
            });
		    

			/* VALIDAMOS SI SE REQUIERE DESBLOQUEAR UI */
			if (isBlockUI)
			{
				$rootScope.stopBlockUI();
			}
		};

		/**
		 * @author <Josue Ramirez Davila>
 		 * @mail <josueramirezdavila@gmail.com>
		 * @module Autocomplete
		 * @description Funcion para realizar las busquedas sencitivas
		 *
		 * @param modulo - Modulo|paquete en los RestController de JAVA
		 * @param controller - Controller de JAVA
		 * @param action - Accion del controller JAVA que se ejecutara
		 * @params - Parametros extra para realizar la busueda (opcional)
		 * @idInput - Id del input al que se le aplicara la funcionalidad del autocompletado
		 * @minLength - Numero minimo de caracteres en el que se activara el autocompletado
		 * @successSearchFunction - Funcion que lenara los parametros que necesitamos de la respuesta del Rest
		 *
		 * @errorSearchFunction - Funcion que se ejecutara en caso de que el objeto BaseResponseDTO<>
			 						regrese el codigo de error ERROR (opcional - si no se ocupa una funcion
			 						mandar null)
		 *
		 * @requestErrorFunction - Funcion que se ejecutara en caso de que no se haya podido conectar al servicio REST
		 * @selectedFunction - Funcion que se ejecutar al seleccionar un elemento del listado del autocompletado
		 * @changeFuntion - Funcion que se ejecutara si se cambia el registro y ya no es el registro
		 * 					seleccionado del listado del autocompletado
		 */
		this.autocomplete = function (modulo, controller, action, params, idInput,
										minLength, successSearchFunction,
										errorSearchFunction, requestErrorFunction,
										selectedFunction, changeFuntion)
		{
			/* OBTENEMOS LA URL DE ACUERDO A LOS PARAMETROS*/
			var url = services[modulo][controller][action];

			$(idInput).autocomplete(
			{
				source : function(request, response)
				{
					var paramAjax = {
						criterio : request.term
					};

					if (params !== null) {
						angular.forEach(params, function(i, item)
						{
							paramAjax[item] = i;
						});
					}

					$http.post(url, paramAjax)
						.success(function(data)
						{
							if (data.codigoError.codigo == 1)
							{
								response($.map(data.response, function(item)
								{
									return successSearchFunction(item);
								}));
							}
							else
							{
								if (errorSearchFunction !== null)
								{
									errorSearchFunction(data.response);
								}
							}
						})
						.error(function(data)
						{
							$(this).removeClass("ui-autocomplete-loading");
							if (requestErrorFunction !== null)
							{
								requestErrorFunction(data);
							}
						});
				},
				minLength : minLength,
				select : function(event, ui)
				{
					if (selectedFunction !== null)
					{
						selectedFunction(event, ui);
					}
				},
				search : function()
				{
					$(this).addClass('ui-autocomplete-loading');
				},
				open : function()
				{
					$(this).removeClass('ui-autocomplete-loading');
				},
				change : function()
				{
					if (changeFuntion !== null)
					{
						changeFuntion();
					}
				}
			});
		};
	}]);
});
