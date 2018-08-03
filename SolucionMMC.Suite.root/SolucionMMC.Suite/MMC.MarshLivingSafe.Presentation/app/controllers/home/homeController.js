define(['modules/app', 'services/requestService', 'services/uploadFilesService', 'services/mappingService'], function (app) {
    /**
	 * DEFINIMOS Y REGISTRAMOS EL CONTROLLER homeController.
	 * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
	 * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
	 * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
	 * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
	 *
	 * @var tittle - TITULO DE LA PAGINA  
	 * @bMenu - BANDERA PARA MOSTRAR EL MENU
	 */
    app.controller('HomeController', ['$scope', '$rootScope', '$location', 'requestService', '$base64', 'uploadFilesService', '$sessionStorage', 'mappingService', '$ngBootbox',
		function ($scope, $rootScope, $location, requestService, $base64, uploadFilesService, $sessionStorage, mappingService, $ngBootbox) {
		    $rootScope.bMenu = true;
		    $rootScope.tittle = "Inicio";
		    $scope.productos = [];
		    var cotizadorModel = {
		        ClienteModel: {
		            ClienteId: 0
		        }
		    }
		    $scope.cotizadorModel = cotizadorModel;


		    $scope.TipofiltroFecha = {

		        filtro1: { ValorFiltro: "Todo", Value: "1" },
		        filreo2: { ValorFiltro: "Ultimos 7 dias", Value: "2" },
		        filtro3: { ValorFiltro: "Ultimos 30 dias", Value: "3" },
		        filtro4: { ValorFiltro: "Mes Actual", Value: "4" },
		        diltro5: { ValorFiltro: "Mes Anterior", Value: "5" }
		    }



		    loadProducts();

		    $scope.listaFrutas = [
				{ nombre: 'Manzanas' },
				{ nombre: 'Peras' },
				{ nombre: 'Zanahorias' },
				{ nombre: 'Manzanas' },
				{ nombre: 'Peras' },
				{ nombre: 'Zanahorias' },
				{ nombre: 'Manzanas' },
				{ nombre: 'Peras' },
				{ nombre: 'Zanahorias' },
				{ nombre: 'Manzanas' },
				{ nombre: 'Peras' },
				{ nombre: 'Zanahorias' },
				{ nombre: 'Manzanas' },
				{ nombre: 'Peras' },
				{ nombre: 'Zanahorias' },
				{ nombre: 'Manzanas' },
				{ nombre: 'Peras' },
				{ nombre: 'Zanahorias' },
				{ nombre: 'Manzanas' },
				{ nombre: 'Peras' }
		    ];

		    $scope.agregarFruta = function () {
		        if ($scope.fruta.trim() != '') {
		            $scope.listaFrutas.push({
		                nombre: $scope.fruta
		            });
		            $scope.fruta = '';
		        }
		    }

		    $scope.eliminarFruta = function () {
		        var options = {
		            message: 'Esta seguro de eliminar la fruta?',
		            title: 'Confirmar',
		            className: 'text-info',
		            buttons: {
		                warning: {
		                    label: "No",
		                    className: "btn-warning",
		                    callback: function () {
		                        console.log('La fruta no se elimino!');
		                    }
		                },
		                success: {
		                    label: "Si",
		                    className: "btn-success",
		                    callback: function () {
		                        $scope.$apply(function () {
		                            $scope.listaFrutas.pop();
		                        });
		                        console.log('Fruta eliminada correctamente!');
		                    }
		                }
		            }
		        };
		        $ngBootbox.customDialog(options);
		    }

		    $scope.showModal = false;
		    $scope.toggleModal = function () {
		        $scope.showModal = !$scope.showModal;
		    };

		    /*$scope.showNotifications = function () {
    		    Notification.error({ message: 'Error Bottom Right', positionY: 'bottom', positionX: 'right' });
    		    Notification.info({ message: 'Info notification<br>Some other <b>content</b><br><a href="https://github.com/alexcrack/angular-ui-notification">This is a link</a><br><img src="https://angularjs.org/img/AngularJS-small.png">', title: 'Html content' });
    		    Notification({ message: 'Primary notification', title: 'Primary notification' });
    		    Notification.success({ message: 'Success Bottom Right', positionY: 'bottom', positionX: 'right' });
    		    Notification.warning({ message: 'Warning Bottom Right', positionY: 'bottom', positionX: 'right' });
    		}*/


		    $scope.exportarExcelMail = function () {


		        window.open(mappingService.services['comparador']['comparador']['descargaReporteCotEmail'] +
                    "?opcionFiltro=" +
                    $scope.filtroFechas.Value,
                    '_blank',
                    '');
		    }

		    function loadProducts() {
		        requestService.post('cotizador',
		            'cotizacion',
		            'consultarProductosCliente',
		            cotizadorModel,
		            true,
		            true,
		            true,
		            function successFunction(response) {
		                $scope.productos = response;
		            },
		            function errorFunction() { },
		            function badRequestFunction(response) { });
		    };

		    $scope.urlautocomplete = $base64.encode(JSON.stringify([
	            'cotizador',
	            'cotizacion',
	            'consultarProductosCliente'
		    ]));
		    $scope.minlength = 5;
		    $scope.itemValueAutocomplete = "NombreProducto";
		    $scope.itemLabelAutocomplete = "NombreProducto";

		    var urlUpload = {
		        modulo: 'test',
		        controller: 'test',
		        action: 'upload'
		    }
		    var uploader = $scope.uploader = uploadFilesService.__getInstance(urlUpload);

		    //Filtros Podemos validar el tamano del archivo, entre otras validaciones
		    uploader.filters.push({
		        name: 'sizeFilter',
		        fn: function (item, options) {
		            if ((item.size / 1024 / 1024) > 5) {
		                $rootScope.toggleModalErrors("El tama&ntilde;o del archivo no debe exceder los 5 MB.");
		                return false;
		            }
		            return true;
		        }
		    });
		    uploader.filters.push({
		        name: 'quantityFilter',
		        fn: function (item, options) {
		            if (uploader.queue.length > 0) {
		                uploader.clearQueue();
		            }
		            return true;
		        }
		    });

		    uploader.onSuccessItem = function (fileItem, response, status, headers) {
		        $rootScope.toggleModalErrors(JSON.stringify(response));
		    };

		    $scope.uploader.url = $scope.uploader.url + '?parametroA=valorA&parametroB=valorB';

		    $(document)
	            .ready(function () {
	                $('#autocompletsse')
                             .autocomplete({
                                 source: function (request, response) {
                                     angular.element.ajax({
                                         url: mappingService
                                             .services['cotizador']['cotizacion']['consultarProductosCliente'],
                                         method: "POST",
                                         data: $scope.cotizadorModel,
                                         headers: {
                                             "Authorization":
                                                 "Bearer " + JSON.parse($base64.decode($sessionStorage.tkn)),
                                             "Content-Type": 'application/json'
                                         },
                                         type: "jsonp"
                                         //})
                                         ,
                                         success: function (data) {
                                             if (data.IsOk) {
                                                 response($.map(data.Response,
                                                     function (item) {
                                                         item.label = item.NombreProducto;
                                                         item.value = item.NombreProducto;
                                                         return item;
                                                     }));
                                             }
                                         }
                                     })
                                 },
                                 minLength: 5,
                                 select: function (event, ui) {
                                     $scope.targetModel = ui.item;
                                 },
                                 search: function (ul, item) {
                                     $('#autocomplete').addClass('ui-autocomplete-loading');
                                 },
                                 open: function () {
                                     $('#autocomplete').removeClass('ui-autocomplete-loading');
                                 },
                                 change: function () {
                                 }
                             });
	            });
		}
    ]);

});
