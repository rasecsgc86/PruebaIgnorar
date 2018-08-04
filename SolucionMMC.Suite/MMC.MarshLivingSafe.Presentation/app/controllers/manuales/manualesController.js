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
        app.controller('ManualesController',
        [
            '$scope', '$rootScope', '$location', 'requestService', '$localStorage', 'Notification', 'mappingService', '$sessionStorage', '$base64', '$http', 'uploadFilesService',
            function ($scope, $rootScope, $location, requestService, $localStorage, Notification, mappingService, $sessionStorage, $base64, $http, uploadFilesService) {
                $rootScope.tittle = "Cotizador-Gen";
                $rootScope.bMenu = true;



                //Variables
                $scope.manualModel = {};
                $scope.textoFiltro = "";
                $scope.auxiliarFiltroProd = "0";
                $scope.UsuarioAdmin = false;
                $scope.isEdithMode = false;
                $scope.IdEditar = 0;
                var solicitud;




                $scope.ManualesResponse = [];

                $scope.ProductosFiltro = [];
                $scope.ClientesFiltro = [];
                $scope.CstegoriasFiltro = [];

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

                function buscaIndexProducto(valor) {
                    for (var i = 0, len = $scope.productos.length; i < len; i++) {
                        if ($scope.productos[i].ProductoId === valor) {
                            return i;
                        }
                    }
                }


                function buscaIndexcategoria(valor) {
                    for (var i = 0, len = $scope.Categorias.length; i < len; i++) {
                        if ($scope.Categorias[i].Id === valor) {
                            return i;
                        }
                    }
                }

                $scope.editarArchivo = function (tre) {
                    //document.getElementById("nuevoManualesForm").reset();

                    var indexClientes = buscaIndexCliente(tre.ClienteId);
                    var indexProductos = buscaIndexProducto(tre.ProductoID);
                    var indexCategoria = buscaIndexcategoria(tre.IdCategoria);

                    $scope.isEdithMode = true;
                    $scope.NombreDocumento = tre.Nombre;
                    $scope.descripcionTextArea = tre.Descripcion;
                    $scope.SelClientes = $scope.clientes[indexClientes];
                    $scope.SelProductos = $scope.productos[indexProductos];
                    $scope.selectCategoria = $scope.Categorias[indexCategoria];
                    document.getElementById('txtFile').innerHTML = 'Seleccione si desea cambiar el Archivo';
                    $scope.IdEditar = tre.Id;
                    $scope.mostrarNuevoManualModal();

                }
                //cargarManuales();
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
                    if ($scope.selectProductosFiltro != undefined) {
                        $scope.auxiliarFiltroProd = $scope.selectProductosFiltro.Clave;

                    }

                    if ($scope.selectCategoriaFiltro == undefined) {
                        $scope.mostrarErrorModal("Seleccione Una categoria");
                        return;
                    }



                    manualModel = {
                        Cliente: $scope.selectClientesFiltro.Clave,
                        Producto: $scope.auxiliarFiltroProd,
                        Categoria: $scope.selectCategoriaFiltro.Id,
                        Texto: $scope.textoFiltro,
                        Todo: 0

                    }
                    cargarManuales();
                }
                function cargarManuales() {



                    var url = mappingService.services['manuales']['manuales']['consultaManuales'];
                    $http.post(url, manualModel)
                        .then(function success(dataee) {

                            $scope.ManualesResponse = dataee.data.Response;

                            if (dataee.data.Response.length == 0) {
                                $scope.mostrarErrorModal("Sin Resultados.");
                            }

                        },
                            function error(data) {

                            });






                    $scope.today = new Date();
                    var dd = $scope.today.getDate();
                    var mm = $scope.today.getMonth() + 1; //January is 0!
                    var yyyy = $scope.today.getFullYear();
                    var hr = $scope.today.getHours();
                    var min = $scope.today.getMinutes();
                    dd = numMenorDiez(dd);
                    mm = numMenorDiez(mm);
                    hr = numMenorDiez(hr);
                    min = numMenorDiez(min);
                    $scope.today = dd + "/" + mm + "/" + yyyy + " " + hr + ":" + min;
                    //};




                }

                function cargarFiltrosClienteProducto() {



                    var request = {
                        Tkn: JSON.parse($base64.decode($sessionStorage.tkn))

                    }



                    var url = mappingService.services['manuales']['manuales']['filtrosDocumentos'];
                    $http.post(url, request)
                        .then(function success(dataee) {

                            $scope.ClientesFiltro = dataee.data.Response.ClientesList;
                            $scope.ProductosFiltro = dataee.data.Response.ProductosList;

                        },
                            function error(data) {

                            });

                }






                $scope.descargarArchivo = function (downloadPath) {
                    window.open(mappingService.services['manuales']['manuales']['descargarArchivo'] +
                        "?rutaArchivo=" +
                        downloadPath,
                        '_blank',
                        '');
                }



                $scope.eliminarArchivo = function (IdDocumento) {

                    var r = confirm("¿Desea elimiar este registro?");
                    if (r != true) {
                        return;
                    }


                    var requesModel =
                        {
                            Id: IdDocumento
                        };
                    var url = mappingService.services['manuales']['manuales']['elimarDocumento'];
                    $http.post(url, requesModel)
                        .then(function success(dataee) {



                            alert("Registro Eliminado.")
                            cargarManuales();
                        },
                            function error(data) {

                            });



                }



                $scope.nuevoManualesModal = function () {

                    $scope.NombreDocumento = "";
                    $scope.descripcionTextArea = "";

                    document.getElementById('txtFile').innerHTML = 'Seleccione un Archivo';




                    $scope.CurrentDate = new Date();
                    $scope.clienteProductoModel = {};

                    $scope.descripcionTextArea = null;



                    $scope.datosContactoClienteModel = {
                        NombreCliente: ''
                    };


                    $scope.Archivos = {};
                    $scope.mostrarNuevoManualModal();



                    $scope.isValidaDescripcion = true;



                    $scope.isDescripcionTextAreaRequired = true;

                    $scope.isArchivoRequired = true;


                    $scope.selectClientes = {
                        IdCliente: 0,
                        NombreCliente: ""
                    }




                };







                $scope.Produtos = {

                    ProductoID: '',
                    NombreProducto: '',
                    Fexible: '',
                    Cp: ''

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



                    if (document.getElementById("cliente").value == "") {
                        $scope.mostrarErrorModal("Seleccione un cliente");
                        return;
                    }

                    if ($scope.selectCategoria == undefined) {
                        $scope.mostrarErrorModal("Seleccione una Categoria");
                        return;
                    }



                    if (document.getElementById('NombreDocumento').value == "") {
                        $scope.mostrarErrorModal("Agrege un  nombre al documento/Archivo ");
                        return;
                    }

                    if (document.getElementById('NombreDocumento').value.length > 49) {
                        $scope.mostrarErrorModal("Solo se permiten 50 caracteres");
                        return;
                    }
                    if (document.getElementById('descripcionTextArea').value == "") {
                        $scope.mostrarErrorModal("Agrege una descripcion ");
                        return;
                    }

                    if ($scope.isEdithMode == false) {
                        if (document.getElementById('file').value == "") {
                            $scope.mostrarErrorModal("Seleccione un Archivo");
                            return;
                        }
                    }


                    if ($scope.isEdithMode == false) {
                        $scope.uploader.uploadAll();
                    }

                    if ($scope.isEdithMode == true && document.getElementById('file').value != "") {
                        $scope.uploader.uploadAll();
                    }

                    if ($scope.isEdithMode == true && document.getElementById('file').value == "") {
                        GuardarDatosFormulario();
                    }

                }


                var urlUpload = {
                    modulo: 'manuales',
                    controller: 'manuales',
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
                        Array("xls",
                            "xlsx",
                            "doc",
                            "docx",
                            "zip",
                            "gif",
                            "jpg",
                            "jpeg",
                            "png",
                            "xml",
                            "pdf",
                            "msg",
                            "XLS",
                            "XLSX",
                            "DOC",
                            "DOCX",
                            "ZIP",
                            "GIF",
                            "JPG",
                            "JPEG",
                            "PNG",
                            "XML",
                            "MSG",
                            "PDF",
                            "mp4");
                        var extension = item.name.split(".").pop();
                        var permitida = false;
                        for (var i = 0; i < extensionesPermitidas.length; i++) {
                            if (extensionesPermitidas[i] === extension) {
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
                    var idCate = $scope.selectCategoria.Id;
                    var idCliente = $("#cliente option:selected").val();
                    var IsUpdate = 0;
                    var rutaFile = "";
                    if ($scope.Archivos != undefined) {
                        rutaFile = $scope.Archivos.RutaArchivo;
                    }
                    if ($scope.isEdithMode == true) {
                        IsUpdate = 1;
                    }
                    var as = document.getElementById("producto").value;
                    if (document.getElementById("producto").value != "") {

                        idPro = $("#producto option:selected").val();
                    }

                    requestModel = {
                        Id: $scope.IdEditar,
                        Nombre: $scope.NombreDocumento,
                        Url: rutaFile,
                        Descripcion: $scope.descripcionTextArea,

                        Tkn: JSON.parse($base64.decode($sessionStorage.tkn)),

                        IdCategoria: idCate,

                        IdCliente: idCliente,

                        IdProducto: idPro,
                        IsUpdate: IsUpdate
                    }




                    var url = mappingService.services['manuales']['manuales']['guardarDatosDocumento'];

                    requestService.post('manuales',
                                        'manuales',
                                        'guardarDatosDocumento',
                                        requestModel,
                                        true,
                                        true,
                                        true,
                                        function successFunction(response) {






                                            $("#nuevoManualesModal").modal('hide');
                                            $scope.mostrarExitoModal("Registro Correcto.");
                                        },
                                        function errorFunction() { },
                                        function badRequestFunction() { }

                                        );


                    cargarClientes();
                    cargarProductos();
                    cargarCategoria();
                    idPro = '0';
                    $scope.Archivos = undefined;

                    $scope.isEdithMode = false;
                    $scope.IdEditar = 0;
                }

                cargarClientes();
                cargarProductos();
                cargarCategoria();
                $scope.Categorias = {

                    Id: 0,
                    NombreCategoria: ''

                };
                $scope.CstegoriasFiltro = {

                    Id: 0,
                    NombreCategoria: ''

                };
                $scope.Categorias = [];
                function cargarCategoria() {







                    solicitud = {

                    }

                    requestService.post('manuales',
                                        'manuales',
                                        'consultarCategoria',
                                        solicitud,
                                        true,
                                        true,
                                        true,
                                        function successFunction(response) {



                                            if (response != null && response.length === 1) {

                                                $scope.Categorias = $scope.Categorias[0];
                                            }
                                            else {
                                                $scope.Categorias = response;
                                                $scope.CstegoriasFiltro = response;
                                            }



                                        },
                                        function errorFunction() { },
                                        function badRequestFunction() { }

                                        );


                }


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

                function cargarProductos() {
                    $scope.productos = [];
                    solicitud = {

                    }

                    requestService.post('configurador',
                            'configurador',
                            'consultaProductosFlexibles',
                            solicitud,
                            true,
                            true,
                            true,
                            function successFunction(response) {

                                $scope.productos = response;


                            },
                            function errorFunction() { },
                            function badRequestFunction() { }

                            );


                }

                $scope.categoriaSel = function (combo) {

                    //$scope.cliente = [];
                    //cargarClientes();

                }
                $scope.clientesSel = function (combo) {

                    //$scope.cliente = [];
                    //cargarClientes();

                }

                $scope.productosSel = function (combo) {


                }

                $scope.validarDescripcion = function () {
                    var descripcion = $scope.descripcionTextArea;
                    if (descripcion !== null && descripcion !== "") {
                        if (validarAlfanumerico(descripcion) && descripcion.length <= 500) {
                            $scope.resultadoDescripcion = null;
                            $scope.isValidaDescripcion = true;
                        } else {
                            $scope.resultadoDescripcion = "Inserte letras y n\u00FAmeros, m\u00E1ximo 500 caracteres";
                            $scope.isValidaDescripcion = false;
                        }
                    }
                    return false;
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