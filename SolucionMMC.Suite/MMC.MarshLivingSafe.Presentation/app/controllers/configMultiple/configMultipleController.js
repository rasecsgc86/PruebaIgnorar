define(['modules/app', 'services/requestService', 'services/mappingService'],
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
        app.controller('ConfigMultipleController',
        [
            '$scope', '$rootScope', '$location', 'requestService', '$localStorage', 'Notification', 'mappingService', '$sessionStorage', '$base64', '$http', 
            function ($scope, $rootScope, $location, requestService, $localStorage, Notification, mappingService, $sessionStorage, $base64, $http) {
                $rootScope.tittle = "Configuración Emisión Múltiple y Captura de Contrato";
                $rootScope.bMenu = true;
                $scope.rama = 'cot';
                $scope.bramaCot = true;
                $scope.bramaComp = false;
                $scope.bramaEmit = false;
                $scope.bramaPag = false;
                $scope.bramaImp = false;
                $scope.styleModalInfo = '';
                $scope.hayDatosPanel = false;
                $scope.paquete = '';
                $scope.idSolicitudR = '';
                $scope.PAQUETEFLEX = "Personalizado";
                $scope.listaAdaptaciones = [];
                $scope.bndFactura = false;
                $scope.valorp = "";
                $scope.GuadaDocumento = false;
                $scope.manualModel = {};
                $scope.textoFiltro = "";
                $scope.auxiliarAseguradora = "0";
                $scope.auxiliarProductos = "0";
                $scope.auxiliarPerfil = "0";
                $scope.auxiliarUsuario = "0";
                $scope.UsuarioAdmin = false;
                $scope.isEdithMode = false;
                $scope.IdEditar = 0;
                var solicitud;

                $scope.ConfigMultipleResponse = [];

                $scope.ProductosFiltro = [];
                $scope.AseguradorasFiltro = [];
                $scope.perfil = [];
                $scope.usuario = [];

                $scope.UsuariosPorPerfil = {

                    PersonaID: '',
                    PerfilUsuarioID: '',
                    Nombre: ''

                }

                $scope.usuariosPorPerfil = [];

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

                function buscaIndexAseguradora(valor) {
                    for (var i = 0, len = $scope.aseguradoras.length; i < len; i++) {
                        if (parseInt($scope.aseguradoras[i].Clave) === valor) {
                            return i;
                        }
                    }
                }

                function buscaIndexProducto(valor) {
                    for (var i = 0, len = $scope.productos.length; i < len; i++) {
                        if (parseInt($scope.productos[i].Clave) === valor) {
                            return i;
                        }
                    }
                }

                function buscaIndexPerfiles(valor) {
                    for (var i = 0, len = $scope.perfiles.length; i < len; i++) {
                        if ($scope.perfiles[i].PerfilUsuarioID === valor) {
                            return i;
                        }
                    }
                }

                function buscaIndexUsuarios(valor) {
                    for (var i = 0, len = $scope.usuarios.length; i < len; i++) {
                        if ($scope.usuarios[i].PersonaID === valor) {
                            return i;
                        }
                    }
                }

             
                $scope.editarArchivo = function (tre) {

                    $scope.isSelectAseguradorasRequired = false;
                    $scope.isSelectProductosRequired = false;
                    $scope.isSelectPerfilRequired = false;
                    $scope.isSelectUsuarioRequired = false;
                    $scope.isSelectEmisionMultipleRequired = true;
                    $scope.isSelectContratosRequired = true;


                  //  document.getElementById("nuevoManualesForm").reset();

                    var indexAseguradoras = buscaIndexAseguradora(tre.idAseguradora);
                    var indexProductos = buscaIndexProducto(tre.idProducto);
                    var indexPerfiles = buscaIndexPerfiles(tre.idPerfil);

                    //cargarUsuarios(tre.idPerfil);

                    //var indexUsuarios = buscaIndexUsuarios(tre.idUsuario);

                    $scope.isEdithMode = true;
                    $scope.Nuevo = false;
                    $scope.Edita = true;

                    $scope.selectAseguradorasFiltroMod = $scope.aseguradoras[indexAseguradoras];
                    $scope.selectProductosFiltroMod = $scope.productos[indexProductos];
                    $scope.PerfilesUsuario = $scope.perfiles[indexPerfiles];
                    $scope.AseguradoraSelect = $scope.aseguradoras[indexAseguradoras].Nombre;
                    $scope.ProductoSelect = $scope.productos[indexProductos].Nombre;
                    $scope.PerfilSelect = $scope.perfiles[indexPerfiles].Nombre;
                    $scope.UsuarioSelect = tre.Usuario;
                    $scope.UsuariosPorPerfil.PersonaID = tre.idUsuario;
                    $scope.IdEditar = 1;
                    if (tre.iPermiteEmisionMultiple == 1) {
                        $scope.confirmedMultiple = true;
                        tre.iPermiteEmisionMultiple = 1;
                        $scope.changeConf();
                    } else {
                        $scope.confirmedMultiple = false;
                        tre.iPermiteEmisionMultiple = 0;
                        $scope.changeConf();
                    }
                    if (tre.iPermiteContrato == 1) {
                        $scope.confirmedContratos = true;
                        tre.iPermiteContrato = 1;
                        $scope.changeConf();
                    } else {
                        $scope.confirmedContratos = false;
                        tre.iPermiteContrato = 0;
                        $scope.changeConf();
                    }

                    $scope.mostrarNuevoManualModal();

                }

                cargarFiltrosAseguradoraProducto();
                CargaDatosPerfil();
                cargarUsuariosFlexibles();
                esUsuarioAdmin();

                function esUsuarioAdmin() {


                    var request = {
                        Tkn: JSON.parse($base64.decode($sessionStorage.tkn))

                    }
                    var url = mappingService.services['emitir']['emitir']['usuarioAdministador'];
                    $http.post(url, request)
                         .then(function success(dataee) {
                             $scope.UsuarioAdmin = dataee.data.Response;
                         },
                             function error(data) {
                             });



                }

                $scope.changeConf = function () {
                    if ($scope.confirmedContratos) {
                        if ($scope.confirmedMultiple == true) {
                            iPermiteEmisionMultiple = 1;
                        }
                        else {
                            iPermiteEmisionMultiple = 0;
                        }

                        if ($scope.confirmedContratos == true) {
                            iPermiteCapContratos = 1;
                        }
                        else {
                            iPermiteCapContratos = 0;
                        }
                    }
                    else {
                        if ($scope.confirmedMultiple == true) {
                            iPermiteEmisionMultiple = 1;
                        }
                        else {
                            iPermiteEmisionMultiple = 0;
                        }

                        if ($scope.confirmedContratos == true) {
                            iPermiteCapContratos = 1;
                        }
                        else {
                            iPermiteCapContratos = 0;
                        }
                    }

                    if ($scope.confirmedMultiple) {
                        if ($scope.confirmedMultiple == true) {
                            iPermiteEmisionMultiple = 1;
                        }
                        else {
                            iPermiteEmisionMultiple = 0;
                        }

                        if ($scope.confirmedContratos == true) {
                            iPermiteCapContratos = 1;
                        }
                        else {
                            iPermiteCapContratos = 0;
                        }
                    }
                    else {
                        if ($scope.confirmedMultiple == true) {
                            iPermiteEmisionMultiple = 1;
                        }
                        else {
                            iPermiteEmisionMultiple = 0;
                        }

                        if ($scope.confirmedContratos == true) {
                            iPermiteCapContratos = 1;
                        }
                        else {
                            iPermiteCapContratos = 0;
                        }
                    }
                    
                };


                $scope.BusquedaArchivos = function () {

                    if($scope.UsuarioAdmin) { 

                        if ($scope.selectAseguradorasFiltro == undefined) {
                            $scope.mostrarErrorModal("Seleccione una aseguradora");
                            return;
                        }

                        if ($scope.selectAseguradorasFiltro != undefined || $scope.selectAseguradorasFiltro != null) {
                            $scope.auxiliarAseguradora = $scope.selectAseguradorasFiltro.Clave;
                        }

                        try {
                            if ($scope.selectProductosFiltro != undefined || $scope.selectProductosFiltro != Null) {
                                $scope.auxiliarProductos = $scope.selectProductosFiltro.Clave;
                            } else {
                            }
                        }
                        catch (err) {
                            $scope.auxiliarProductos = 0;
                        }

                        
                        try {
                            if ($scope.PerfilesUsuario.PerfilUsuarioID != undefined || $scope.PerfilesUsuario.PerfilUsuarioID != Null) {
                                if ($scope.PerfilesUsuario.PerfilUsuarioID == "") {
                                    $scope.auxiliarPerfil = 0;
                                } else {
                                    $scope.auxiliarPerfil = $scope.PerfilesUsuario.PerfilUsuarioID;
                                }

                            } else { }
                        }
                        catch (err) {
                            $scope.auxiliarPerfil = 0;
                        }

                        try {
                            if ($scope.UsuariosPorPerfil.PersonaID != undefined || $scope.UsuariosPorPerfil.PersonaID != Null) {
                                if ($scope.UsuariosPorPerfil.PersonaID == "") {
                                    $scope.auxiliarUsuario = 0;
                                } else {
                                    $scope.auxiliarUsuario = $scope.UsuariosPorPerfil.PersonaID;
                                }

                            } else {

                            }
                        }
                        catch (err) {
                            $scope.auxiliarUsuario = 0;
                        }

                        

                    
                    } else { 
                    
                        if ($scope.selectAseguradorasFiltro == undefined) {
                            $scope.mostrarErrorModal("Seleccione una aseguradora");
                            return;
                        }

                        try {
                            if ($scope.selectAseguradorasFiltro != undefined) {
                                $scope.auxiliarAseguradora = $scope.selectAseguradorasFiltro.Clave;

                            }
                        }
                        catch (err) {
                            $scope.auxiliarAseguradora = 0;
                        }

                        
                        if ($scope.selectProductosFiltro == undefined) {
                            $scope.mostrarErrorModal("Seleccione un Producto");
                            return;
                        }

                        try {
                            if ($scope.selectProductosFiltro != undefined) {
                                $scope.auxiliarProductos = $scope.selectProductosFiltro.Clave;
                            }
                        }
                        catch (err) {
                            $scope.auxiliarProductos = 0;
                        }

                        if ($scope.PerfilesUsuario.PerfilUsuarioID == undefined || $scope.PerfilesUsuario.PerfilUsuarioID == "") {
                            $scope.mostrarErrorModal("Seleccione un Perfil");
                            return;
                        }

                        try {
                            if ($scope.PerfilesUsuario.PerfilUsuarioID != undefined) {
                                if ($scope.PerfilesUsuario.PerfilUsuarioID == "") {
                                    $scope.auxiliarPerfil = 0;
                                } else {
                                    $scope.auxiliarPerfil = $scope.PerfilesUsuario.PerfilUsuarioID;
                                }
                            }
                        }
                        catch (err) {
                            $scope.auxiliarPerfil = 0;
                        }
                        

                        if ($scope.UsuariosPorPerfil.PersonaID == undefined) {
                            $scope.mostrarErrorModal("Seleccione un Usuario");
                            return;
                        }

                        try {
                            if ($scope.UsuariosPorPerfil.PersonaID != undefined) {
                                if ($scope.UsuariosPorPerfil.PersonaID == "") {
                                    $scope.auxiliarUsuario = 0;
                                } else {
                                    $scope.auxiliarUsuario = $scope.UsuariosPorPerfil.PersonaID;
                                }
                            }
                        }
                        catch (err) {
                            $scope.auxiliarUsuario = 0;
                        }
                    }
                                       
                  configRequestModel = {
                        Aseguradora: $scope.selectAseguradorasFiltro.Clave,
                        Producto: $scope.auxiliarProductos,
                        Perfil: $scope.auxiliarPerfil,
                        Usuario: $scope.auxiliarUsuario,
                    }

                    cargarConfigMultiple();
                }

                function cargarConfigMultiple() {
                    var url = mappingService.services['emitir']['emitir']['consultaConfigMultiple'];
                    $http.post(url, configRequestModel)
                        .then(function success(dataee) {
                            $scope.ConfigMultipleResponse = dataee.data.Response;
                            if (dataee.data.Response.length == 0) {
                                $scope.mostrarErrorModal("No se encontraron datos con el criterio de búsqueda seleccionado.");
                            }
                        },
                            function error(data) {
                            });
                }

                function cargarFiltrosAseguradoraProducto() {
                    var request = {
                        Tkn: JSON.parse($base64.decode($sessionStorage.tkn))
                    }
                    var url = mappingService.services['emitir']['emitir']['filtrosConfigMultiple'];
                    $http.post(url, request)
                        .then(function success(dataee) {
                            $scope.AseguradorasFiltro = dataee.data.Response.ClientesList;
                            $scope.ProductosFiltro = dataee.data.Response.ProductosList;
                        },
                            function error(data) {
                            });
                }

                $scope.eliminarArchivo = function (tre) {

                    var r = confirm("¿Desea elimiar este registro?");
                    if (r != true) {
                        return;
                    }

                    $scope.isSelectAseguradorasRequired = false;
                    $scope.isSelectProductosRequired = false;
                    $scope.isSelectPerfilRequired = false;
                    $scope.isSelectUsuarioRequired = false;
                    $scope.isSelectEmisionMultipleRequired = true;
                    $scope.isSelectContratosRequired = true;


                    //  document.getElementById("nuevoManualesForm").reset();

                    var indexAseguradoras = buscaIndexAseguradora(tre.idAseguradora);
                    var indexProductos = buscaIndexProducto(tre.idProducto);
                    var indexPerfiles = buscaIndexPerfiles(tre.idPerfil);

                    //cargarUsuarios(tre.idPerfil);

                    //var indexUsuarios = buscaIndexUsuarios(tre.idUsuario);

                    $scope.isEdithMode = true;
                    $scope.Nuevo = false;
                    $scope.Edita = true;

                    $scope.selectAseguradorasFiltroMod = $scope.aseguradoras[indexAseguradoras];
                    $scope.selectProductosFiltroMod = $scope.productos[indexProductos];
                    $scope.PerfilesUsuario = $scope.perfiles[indexPerfiles];
                    $scope.AseguradoraSelect = $scope.aseguradoras[indexAseguradoras].Nombre;
                    $scope.ProductoSelect = $scope.productos[indexProductos].Nombre;
                    $scope.PerfilSelect = $scope.perfiles[indexPerfiles].Nombre;
                    $scope.UsuarioSelect = tre.Usuario;
                    $scope.UsuariosPorPerfil.PersonaID = tre.idUsuario;
                    $scope.IdEditar = 1;
                    if (tre.iPermiteEmisionMultiple == 1) {
                        $scope.confirmedMultiple = true;
                        tre.iPermiteEmisionMultiple = 1;
                        $scope.changeConf();
                    } else {
                        $scope.confirmedMultiple = false;
                        tre.iPermiteEmisionMultiple = 0;
                        $scope.changeConf();
                    }
                    if (tre.iPermiteContrato == 1) {
                        $scope.confirmedContratos = true;
                        tre.iPermiteContrato = 1;
                        $scope.changeConf();
                    } else {
                        $scope.confirmedContratos = false;
                        tre.iPermiteContrato = 0;
                        $scope.changeConf();
                    }


                    configRequestModel = {
                        Aseguradora: tre.idAseguradora,
                        Producto: tre.idProducto,
                        Perfil: tre.idPerfil,
                        Usuario: tre.idUsuario,
                        Tkn: JSON.parse($base64.decode($sessionStorage.tkn))
                    }

                    var url = mappingService.services['emitir']['emitir']['elimarDatosConfigMultiple'];
                    $http.post(url, configRequestModel)
                        .then(function success(dataee) {
                            alert("El registro a sido Eliminado.")
                            $scope.auxiliarUsuario = 0;
                            cargarFiltrosAseguradoraProducto();
                            CargaDatosPerfil();
                            cargarUsuariosFlexibles();
                            esUsuarioAdmin();
                            cargarConfigMultiple();
                        },
                            function error(data) {

                            });
                }



                $scope.NuevoConfigMultipleModal = function () {


                    $scope.esAseguradoraFlotillas = [];
                    $scope.responsable = [];
                    $scope.caratulas = [];
                    $scope.ticketModel = {};
                    $scope.CurrentDate = new Date();
                    $scope.aseguradoraProductoModel = {};

                    $scope.datosContactoAseguradora = {
                        NombreAseguradora: ''
                    };
                    $scope.datosContactoAseguradoraModel = {
                        Nombreaseguradora: ''
                    };
                    $scope.datosContactoModel = [];
                    $scope.mostrarNuevoManualModal();

                    $scope.isResponsable = false;
                    $scope.isValidaDescripcion = true;
                    $scope.isValidoEmail = true;
                    $scope.isSelectAseguradorasRequired = true;
                    $scope.isSelectProductosRequired = true;
                    $scope.isSelectPerfilRequired = true;
                    $scope.isSelectUsuarioRequired = true;
                    $scope.isSelectEmisionMultipleRequired = true;
                    $scope.isSelectContratosRequired = true;

                    $scope.Nuevo = true;
                    $scope.Edita = false;

                    $scope.confirmedMultiple = false;
                    $scope.confirmedContratos = false;

                    $scope.selectAseguradoras = {
                        IdAseguradora: 0,
                        NombreAseguradora: ""
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
                    $("#NuevoConfigMultipleModal").modal('show');
                }

                $scope.cerrarNuevoConfigMultipleModal = function () {
                    $("#NuevoConfigMultipleModal").modal('hide');
                }


                $scope.guardarInformacion = function () {



                    if ($scope.selectAseguradorasFiltroMod == undefined) {
                        $scope.mostrarErrorModal("Seleccione una aseguradora");
                        return;
                    }

                    try {
                        if ($scope.selectAseguradorasFiltroMod != undefined) {
                            $scope.auxiliarAseguradora = $scope.selectAseguradorasFiltroMod.Clave;
                        }
                    }
                    catch (err) {
                        $scope.mostrarErrorModal("Seleccione una aseguradora");
                        return;
                    }

                    

                    if ($scope.selectProductosFiltroMod == undefined) {
                        $scope.mostrarErrorModal("Seleccione un Producto");
                        return;
                    }

                    try {
                        if ($scope.selectProductosFiltroMod != undefined) {
                            $scope.auxiliarProductos = $scope.selectProductosFiltroMod.Clave;
                        }
                    }
                    catch (err) {
                        $scope.mostrarErrorModal("Seleccione un Producto");
                        return;
                    }
                    


                    if ($scope.PerfilesUsuario.PerfilUsuarioID == undefined || $scope.PerfilesUsuario.PerfilUsuarioID == "") {
                        $scope.mostrarErrorModal("Seleccione un Perfil");
                        return;
                    }

                    try {
                        if ($scope.PerfilesUsuario.PerfilUsuarioID != undefined) {
                            $scope.auxiliarPerfil = $scope.PerfilesUsuario.PerfilUsuarioID;
                        }
                    }
                    catch (err) {
                        $scope.mostrarErrorModal("Seleccione un Perfil");
                        return;
                    }

                    

                    if ($scope.UsuariosPorPerfil.PersonaID == undefined) {
                        $scope.mostrarErrorModal("Seleccione un Usuario");
                        return;
                    }

                    try {
                        if ($scope.UsuariosPorPerfil != undefined) {
                            if ($scope.UsuariosPorPerfil.PersonaID == "") {
                                $scope.mostrarErrorModal("Seleccione un Usuario");
                                return;
                            } else {
                                $scope.auxiliarUsuario = $scope.UsuariosPorPerfil.PersonaID;
                            }

                        } else
                        {
                        }
                    }
                    catch (err) {
                        $scope.mostrarErrorModal("Seleccione un Usuario");
                        return;
                    }
                    
                                        
                        var idAseguradora = $scope.auxiliarAseguradora;
                        var idProducto = $scope.auxiliarProductos;
                        var idPerfil = $scope.auxiliarPerfil;
                        var idUsuario = $scope.auxiliarUsuario;
                        var iPermiteEmisionMultiple = 0;
                        var iPermiteCapContratos = 0;

                        if ($scope.confirmedMultiple == true) {
                            iPermiteEmisionMultiple = 1;
                        }
                        else {
                            iPermiteEmisionMultiple = 0;
                        }

                        if ($scope.confirmedContratos == true) {
                            iPermiteCapContratos = 1;
                        }
                        else {
                            iPermiteCapContratos = 0;
                        }

                    if ($scope.isEdithMode == true) {
                        GuardarDatosFormulario(idAseguradora, idProducto, idPerfil, idUsuario, iPermiteEmisionMultiple, iPermiteCapContratos, $scope.isEdithMode);

                    } else
                    {
                        GuardarDatosFormulario(idAseguradora, idProducto, idPerfil, idUsuario, iPermiteEmisionMultiple, iPermiteCapContratos, $scope.isEdithMode);
                    }
                }


                idPro = '0';

                function GuardarDatosFormulario(idAseguradora, idProducto, idPerfil, idUsuario, iPermiteEmisionMultiple, iPermiteCapContratos, Modo) {
                    
                    var IsUpdate = 0;
                                        
                    if ($scope.isEdithMode == true) {
                        IsUpdate = 1;
                    } else {
                        IsUpdate = 0;
                    }

                    requestModel = {
                        Aseguradora: idAseguradora,
                        Producto: idProducto,
                        Perfil: idPerfil,
                        Usuario: idUsuario,
                        PermiteEmisionMultiple: iPermiteEmisionMultiple,
                        PermiteCapContratos: iPermiteCapContratos,
                        IsUpdate : IsUpdate,
                        Tkn: JSON.parse($base64.decode($sessionStorage.tkn)),
                    }


                   //* var url = mappingService.services['emitir']['emitir']['guardarDatosConfigMultiple'];

                    requestService.post('emitir',
                                        'emitir',
                                        'guardarDatosConfigMultiple',
                                        requestModel,
                                        true,
                                        true,
                                        true,
                                        function successFunction(response) {
                                            $("#NuevoConfigMultipleModal").modal('hide');
                                            $scope.mostrarExitoModal("Se almaceno la configuación correctamente.");
                                        },
                                        function errorFunction() { },
                                        function badRequestFunction() { }
                                        );



                    cargarFiltrosAseguradoraProducto();
                    CargaDatosPerfil();
                    cargarUsuariosFlexibles();
                    esUsuarioAdmin();
                    idPro = '0';

                   

                    $scope.isEdithMode = false;
                    $scope.IdEditar = 0;

                    cargarAseguradoras();
                    CargaPerfil();

                    configRequestModel = {
                        Aseguradora: $scope.selectAseguradorasFiltro.Clave,
                        Producto: $scope.auxiliarProductos,
                        Perfil: $scope.auxiliarPerfil,
                        Usuario: $scope.auxiliarUsuario,
                    }

                    cargarConfigMultiple();

                    $scope.UsuariosPorPerfil = {

                        PersonaID: '',
                        PerfilUsuarioID: '',
                        Nombre: ''

                    }
                }

                cargarAseguradoras();
                CargaPerfil();

                function cargarAseguradoras() {

                    $scope.Aseguradoras = {
                        PersonaID: '',
                        Nombre: ''
                    };

                    $scope.aseguradoras = [];
                    $scope.productos = [];

                    var request = {
                        Tkn: JSON.parse($base64.decode($sessionStorage.tkn))
                    }
                    var url = mappingService.services['emitir']['emitir']['filtrosConfigMultiple'];
                    $http.post(url, request)
                        .then(function success(dataee) {
                            if (dataee.data.Response.ClientesList != null ) {
                                $scope.aseguradoras = dataee.data.Response.ClientesList;
                                $scope.productos = dataee.data.Response.ProductosList;
                            }
                            else {
                                $scope.aseguradoras = dataee.data.Response.ClientesList;
                                $scope.productos = dataee.data.Response.ProductosList;
                            }
                        },
                            function error(data) {
                            });
                }

                $scope.aseguradorasSel = function (combo) {
                }

                $scope.productosSel = function (combo) {
                }

                function CargaDatosPerfil() {

                    $scope.PerfilesUsuario = {

                        PerfilUsuarioID: '',
                        Nombre: '',
                        PerfilPadreID: '',
                        Activo: '',
                        OpcionAcceso: '',
                        OpcionAccesoB: ''

                    }
                    $scope.perfilesUsuario = [];
                    solicitud = {}

                    requestService.post('configurador',
                        'configurador',
                        'consultaPerfilesSistema',
                        solicitud,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            if (response != null && response.length === 1) {
                                $scope.perfilesUsuario = response;
                                $scope.PerfilesUsuario = $scope.perfilesUsuario[0];
                            }
                            else {
                                $scope.perfilesUsuario = response;
                            }

                        },
                          function errorFunction() { },
                          function badRequestFunction() { }
                        );
                }

                function CargaPerfil() {

                    $scope.perfiles = [];
                    solicitud = {}

                    requestService.post('configurador',
                        'configurador',
                        'consultaPerfilesSistema',
                        solicitud,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            if (response != null && response.length === 1) {
                                $scope.perfiles = response;
                                $scope.Perfiles = response;
                            }
                            else {
                                $scope.perfiles = response;
                            }
                        },
                          function errorFunction() { },
                          function badRequestFunction() { }
                        );
                }


                $scope.selPerfil = function (combo) {

                    var selProducto = $("#perfilUsuario option:selected").val();
                    cargarUsuariosPorPerfil(selProducto);
                }

                $scope.selPerfilMod = function (combo) {

                    var selPerfil = $("#perfilUsuarioMod option:selected").val();
                    cargarUsuariosPorPerfil(selPerfil);
                }


                $scope.selUsuario = function (combo) { }

                $scope.selUsuarioMod = function (combo) { }

                function cargarUsuarios(val) {

                    $scope.usuarios = [];
                    solicitud = {
                        usuariosPerfilModel: {
                            PerfilUsuarioID: val
                        }
                    }

                    requestService.post('configurador',
                        'configurador',
                        'consultaUsuariosPorPerfil',
                        solicitud,
                        true,
                        true,
                        true,
                        function successFunction(response) {
        
                            if (response != null && response.length === 1) {
                                $scope.usuarios = response;
                                 }
                            else {
                                $scope.usuarios = response;
                            }
                        },
                        function errorFunction() { },
                        function badRequestFunction() { }
        
                        );
                }

                function cargarUsuariosPorPerfil(val) {

                    $scope.UsuariosPorPerfil = {

                        PersonaID: '',
                        PerfilUsuarioID: '',
                        Nombre: ''

                    }

                    $scope.usuariosPorPerfil = [];
                    solicitud = {
                        usuariosPerfilModel: {
                            PerfilUsuarioID: val
                        }
                    }

                    requestService.post('configurador',
                        'configurador',
                        'consultaUsuariosPorPerfil',
                        solicitud,
                        true,
                        true,
                        true,
                        function successFunction(response) {

                            if (response != null && response.length === 1) {
                                $scope.usuariosPorPerfil = response;
                                $scope.UsuariosPorPerfil = $scope.usuariosPorPerfil[0];
                            }
                            else {
                                $scope.usuariosPorPerfil = response;
                            }



                        },
                        function errorFunction() { },
                        function badRequestFunction() { }

                        );
                }

                function cargarUsuariosFlexibles() {
                    $scope.UsuariosFlexibles = {
                        PerfilFlexibleId: '',
                        PerfilId: '',
                        PersonaId: '',
                        Comentario: '',
                        maneja_udi: ''

                    }

                    $scope.usuariosFlexibles = [];
                    solicitud = {};
                    requestService.post('configurador',
                                        'configurador',
                                        'consultarUsuariosFlexibles',
                                        solicitud,
                                        true,
                                        true,
                                        true,
                                        function successFunction(response) {
                                            $scope.usuariosFlexibles = response;
                                        },
                                           function errorFunction() { },
                                           function badRequestFunction() { }

                                          );

                }
            }
        ]);

    });