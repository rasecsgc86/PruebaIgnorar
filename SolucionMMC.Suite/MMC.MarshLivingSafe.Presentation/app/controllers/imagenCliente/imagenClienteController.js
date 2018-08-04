define(['modules/app', 'services/requestService', 'services/mappingService', 'services/uploadFilesService'],
    function (app) {
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
        app.controller('ImagenClienteController',
        [
            '$scope', '$rootScope', '$location', 'requestService', '$localStorage', 'Notification', 'mappingService', '$sessionStorage', '$base64', '$http', 'uploadFilesService',
            function ($scope, $rootScope, $location, requestService, $localStorage, Notification, mappingService, $sessionStorage, $base64, $http, uploadFilesService) {
                $rootScope.tittle = "Cotizador-Gen";
                $rootScope.bMenu = true;



                //Variables
                $scope.requestModel = {};
                $scope.textoFiltro = "";
                $scope.auxiliarFiltroProd = "0";
                $scope.UsuarioAdmin = false;
                $scope.isEdithMode = false;
                $scope.tieneImagen = true;
                $scope.IdEditar = 0;
                var solicitud;
                var permitida = false;



                $scope.ImagenResponse = [];

                $scope.ProductosFiltro = [];
           

                function numMenorDiez(num) {
                    if (num < 10) {
                        num = '0' + num;
                    }
                    return num;
                }

                $scope.mostrarErrorModal = function (mensajeError) {
                    $scope.mensajeError = mensajeError;
                    $("#errorModal").fadeIn();
                }

                $scope.cerrarErrorModal = function () {
                    $("#errorModal").fadeOut();

                }

                $scope.mostrarExitoModal = function (mensajeExito) {
                    $scope.mensajeExito = mensajeExito;
                    $("#exitoModal").fadeIn();
                }

                $scope.cerrarExitoModal = function () {
                    $("#exitoModal").fadeOut();

                }


                function buscaIndexCliente(valor) {
                    for (var i = 0, len = $scope.clientes.length; i < len; i++) {
                        if ($scope.clientes[i].PersonaID === valor) {
                            return i;
                        }
                    }
                }

              



                $scope.editarArchivo = function (idEdita) {
             
                    document.getElementById("file").value = "";
              
                    $scope.isEdithMode = true;
                
                        
                   
                    document.getElementById('txtFile').innerHTML = 'Seleccione nueva imagen del Cliente';
            
                    $scope.IdEditar = idEdita;
                    $scope.mostrarNuevoManualModal();

                }
                //cargarDatosImagen();
                cargarFiltrosClienteProducto();
                esUsuarioAdmin();

                function esUsuarioAdmin() {


                    var request = {
                        Tkn: JSON.parse($base64.decode($sessionStorage.tkn))

                    }
                    var url = mappingService.services['manuales']['manuales']['usuarioAdministador'];
                    $http.post(url, request)
                         .then(function success(dataee) {


                             $scope.UsuarioAdmin = dataee.data.Response;


                         },
                             function error(data) {

                             });



                }


                $scope.BusquedaArchivos = function () {



                    if ($scope.selectClientesFiltro == undefined) {
                        $scope.mostrarErrorModal("Seleccione un producto");
                        return;
                    }
                    

                   



                    requestModel = {
                        IdCliente: $scope.selectClientesFiltro.Clave,
                        IdSolictud:0

                    }
                    cargarDatosImagen();
                }
                function cargarDatosImagen() {



                    var url = mappingService.services['imagenCliente']['imagenCliente']['obtenerImagenCliente'];
                    $http.post(url, requestModel)
                        .then(function success(dataee) {

                            $scope.ImagenResponse = dataee.data.Response;

                            if (dataee.data.Response == null) {
                                $scope.tieneImagen = false;
                                $scope.mostrarErrorModal("Sin Resultados.");

                            }
                            else
                            {
                                $scope.tieneImagen = true;
                            }


                           
                        },
                            function error(data) {

                            });


                }

                function cargarFiltrosClienteProducto() {



                    var request = {
                        Tkn: JSON.parse($base64.decode($sessionStorage.tkn))

                    }



                    var url = mappingService.services['manuales']['manuales']['filtrosDocumentos'];
                    $http.post(url, request)
                        .then(function success(dataee) {

                            $scope.ClientesFiltro = dataee.data.Response.ClientesList;
                          

                        },
                            function error(data) {

                            });

                }



                $scope.eliminarArchivo = function (IdDocumento) {

                    var r = confirm("¿Desea elimiar la imagen del cliente seleccionado?");
                    if (r != true) {
                        return;
                    }


                    var requesModel =
                        {
                            Id: IdDocumento
                        };
                    var url = mappingService.services['imagenCliente']['imagenCliente']['elimarDocumento'];
                    $http.post(url, requesModel)
                        .then(function success(dataee) {



                            alert("Registro Eliminado.")
                            cargarDatosImagen();
                        },
                            function error(data) {

                            });



                }



                $scope.nuevoManualesModal = function () {
                    document.getElementById("file").value = "";

                    $scope.isEdithMode = false;

                    document.getElementById('txtFile').innerHTML = 'Seleccione un Archivo';




                 
                    $scope.clienteProductoModel = {};

                 

                    $scope.Archivos = {};
                    $scope.mostrarNuevoManualModal();



                    $scope.isArchivoRequired = true;


                    $scope.selectClientes = {
                        IdCliente: 0,
                        NombreCliente: ""
                    }




                };

              

                $scope.mostrarNuevoManualModal = function () {
                
                    setTimeout(function () {
                        //document.getElementById("datosContactoModal").style.zIndex = "1";
                    }, 1);
                    $("#nuevoManualesModal").modal('show');
                }

                $scope.cerrarNuevoManualesModal = function () {
                    $("#nuevoManualesModal").modal('hide');
                }



                $scope.guardarInformacion = function () {



                 
                        if (document.getElementById('file').value == "") {
                            $scope.mostrarErrorModal("Seleccione un Archivo");
                            return;
                        }
                    


                    if (permitida==true) {
                        $scope.uploader.uploadAll();
                    }
                    else {
                        $scope.mostrarErrorModal("Seleccione un Archivo con extencion permitida");
                    }

                

                }


                var urlUpload = {
                    modulo: 'imagenCliente',
                    controller: 'imagenCliente',
                    action: 'cargarArchivo'
                }
                var uploader = $scope.uploader = uploadFilesService.__getInstance(urlUpload);

                //Filtros Podemos validar el tamano del archivo, entre otras validaciones
                uploader.filters.push({
                    name: 'sizeFilter',
                    fn: function (item) {
                        if ((item.size / 1024 / 1024) > 10) {
                            $scope.mostrarErrorModal("El tama\u00F1o del archivo no debe exceder los 10 MB.");
                            return false;
                        }
                        return true;
                    }
                });
                uploader.filters.push({
                    name: 'quantityFilter',
                    fn: function () {
                        if (uploader.queue.length > 0) {
                            uploader.clearQueue();
                        }
                        return true;
                    }
                });

                uploader.filters.push({
                    name: 'extensionfilter',
                    fn: function (item) {
                        var extensionesPermitidas = new
                        Array("JPG",
                            "JPEG",
                            "PNG");

                        var extension = item.name.split(".").pop();
                        
                        permitida = false;
                        for (var i = 0; i < extensionesPermitidas.length; i++) {
                            if (extensionesPermitidas[i] == extension.toUpperCase()) {
                                permitida = true;
                                break;
                            }
                        }
                        if (!permitida) {
                            $scope.mostrarErrorModal("La extensi\u00F3n del archivo no es permitida");
                            return false;
                        } else {
                            return true;
                        }

                    }
                });

                uploader.onSuccessItem = function (fileItem, response) {

                    var input = $("input[name=file]");
                    var fileName = input.val();
                    if (fileName) { // returns true if the string is not empty
                        input.val('');
                    }
                    $scope.uploader.clearQueue();
                    if (response.IsOk) {
                        var nombreArchivo = response.Response.NombreArchivo;

                        $scope.Archivos = {

                            NombreArchivo: response.Response.NombreArchivo,
                            RutaArchivo: response.Response.Ruta,

                        };

                        GuardarDatosFormulario();
                    }
                    else {
                        if (response.Message) {
                            $scope.mostrarErrorModal(response.Message);
                        }
                    }
                    $scope.isArchivoRequired = true;
                };

                idPro = '0';
                function GuardarDatosFormulario() {
         
                    var idCliente =0;
                    if ($scope.isEdithMode == false) {
                        var idCliente = $scope.selectClientesFiltro.Clave;
                    }

                    var IsUpdate = 0;
                    var rutaFile = "";
                    if ($scope.Archivos != undefined) {
                        rutaFile = $scope.Archivos.RutaArchivo;
                    }
                    if ($scope.isEdithMode == true) {
                        IsUpdate = 1;
                    }
               

                    requestModel = {
                        Id: $scope.IdEditar,
                       
                        Url: rutaFile,

                        IdCliente: idCliente,

                 
                        IsUpdate: IsUpdate
                    }




           
                    requestService.post('imagenCliente',
                                        'imagenCliente',
                                        'guardarDatosImagenCliente',
                                        requestModel,
                                        true,
                                        true,
                                        true,
                                        function successFunction(response) {

                                            $("#nuevoManualesModal").modal('hide');
                                            $scope.mostrarExitoModal("Cambio de imagen Correcto.");
                                        },
                                        function errorFunction() { },
                                        function badRequestFunction() { }

                                        );


                    cargarClientes();
              
                    idPro = '0';
                    $scope.Archivos = undefined;

                    $scope.isEdithMode = false;
                    $scope.IdEditar = 0;


               
                }

                cargarClientes();
             
            
          


                function cargarClientes() {

                    $scope.Clientes = {

                        PersonaID: '',
                        Nombre: ''

                    };

                    $scope.clientes = [];

                    solicitud = {

                    }
                    nuevoManualesModal
                    requestService.post('configurador',
                                        'configurador',
                                        'consultaClientesConfigurador',
                                        solicitud,
                                        true,
                                        true,
                                        true,
                                        function successFunction(response) {



                                            if (response != null && response.length === 1) {

                                                $scope.Clientes = $scope.clientes[0];
                                            }
                                            else {
                                                $scope.clientes = response;
                                            }



                                        },
                                        function errorFunction() { },
                                        function badRequestFunction() { }

                                        );


                }

        

                $scope.clientesSel = function (combo) {

                    //$scope.cliente = [];
                    //cargarClientes();

                }

             

          



                function validarAlfanumerico(campo) {
                    var re =
                        /^[a-zA-Z0-9\u00f1\u00d1\u00e1\u00e9\u00ed\u00f3\u00fa\u00c1\u00c9\u00cd\u00d3\u00da,. ]+$/;
                    return re.test(campo);
                }
                //eND cONTRLLER

            }
        ]);

    });