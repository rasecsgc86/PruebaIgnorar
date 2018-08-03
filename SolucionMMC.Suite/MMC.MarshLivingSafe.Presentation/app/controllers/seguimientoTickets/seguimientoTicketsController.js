define(['modules/app', 'services/requestService', 'services/mappingService', 'services/uploadFilesService'],
    function(app) {
        /**
         * DEFINIMOS Y REGISTRAMOS EL CONTROLLER CalendarioController.
         * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
         * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
         * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
         * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
         *
         * @var tittle - TITULO DE LA PAGINA  
         */
        app.controller('SeguimientoTicketsController',
        [
            '$scope', '$rootScope', '$location', 'requestService', '$localStorage', '$base64', 'mappingService',
            '$sessionStorage', '$ngBootbox', '$stateParams', '$state', 'uploadFilesService',
            function($scope,
                $rootScope,
                $location,
                requestService,
                $localStorage,
                $base64,
                mappingService,
                $sessionStorage,
                $ngBootbox,
                $stateParams,
                $state,
                uploadFilesService) {
                $rootScope.tittle = "Seguimiento Tickets";

                $scope.date = new Date();
                $scope.listaComentarios = [];
                $scope.listaArchivos = [];
                $scope.estatusTickets = [];
                $scope.comentariosTicketsModel = {};
                $scope.seguimientoTicketModel =
                {
                    TicketId: 0,
                    PersonaIdTipoUsuarioTicket: 0
                };
                $scope.selectAsegTicket = {
                    AseguradoraId: 0,
                    Nombre: 'NA'
                };
                $scope.asegTicket = [
                    {
                        AseguradoraId: 0,
                        Nombre: 'NA'
                    }
                ];
                $scope.NoOt = "";
                var TicketsEstatusModel = {};
                init2();
                loadEstatusTickets();
                //obtenerComentarios();
                $scope.seguimientoticket = {
                    TicketId: 0,
                    IdEstatusTicket: 0
                };

                $scope.archivoTicketsModel = {
                    idArchivoTicket: 0
                };
                $scope.check = false;
                $scope.siFloti = true;
                $scope.siTipoUsuario = false;
                //Para mostrar o ocultar el control que permitira cargar el archivo de cierre

                $scope.Archivos = [];
                $scope.ArchivosCierre = [];
                //Para bloquear la seleccion de la aseguradora 
                $scope.bndAseg = true;


                //440797
                //440798
                //440799

                /*
                * ################################################################
                * ######          Comienza Cargar Archivo Seguimiento       ######
                * ################################################################
               */
                var urlUpload = {
                    modulo: 'tickets',
                    controller: 'seguimiento',
                    action: 'cargarArchivoSeguimiento'
                }
                var uploader = $scope.uploader = uploadFilesService.__getInstance(urlUpload);

                //Filtros Podemos validar el tamano del archivo, entre otras validaciones
                uploader.filters.push({
                    name: 'sizeFilter',
                    fn: function(item) {
                        if ((item.size / 1024 / 1024) > 10) {
                            $scope.showModalValidacion = true;
                            $scope.mensaje = "El tama\u00F1o del archivo no debe exceder los 10 MB.";
                            return false;
                        }
                        return true;
                    }
                });
                uploader.filters.push({
                    name: 'quantityFilter',
                    fn: function() {
                        if (uploader.queue.length > 0) {
                            uploader.clearQueue();
                        }
                        return true;
                    }
                });

                uploader.filters.push({
                    name: 'extensionfilter',
                    fn: function(item) {
                        var extensionesPermitidas = new Array("xls",
                            "xlsx",
                            "doc",
                            "docx",
                            "zip",
                            "gif",
                            "jpg",
                            "png",
                            "xml",
                            "msg",
                            "pdf");
                        var extension = item.name.split(".").pop();
                        var permitida = false;
                        for (var i = 0; i < extensionesPermitidas.length; i++) {
                            if (extensionesPermitidas[i] === extension) {
                                permitida = true;
                                break;
                            }
                        }
                        if (!permitida) {
                            $scope.showModalValidacion = true;
                            $scope.mensaje = "La extensi\u00F3n del archivo no es permitida.";
                            return false;
                        } else {
                            return true;
                        }

                    }
                });

                uploader.onSuccessItem = function(fileItem, response) {
                    angular.element("input[type='file']").val(null);
                    $scope.uploader.clearQueue();
                    if (response.IsOk) {
                        $scope.ArchivosCierre.push({
                            IdArchivoTicket: response.Response.IdArchivoTicket,
                            NombreArchivo: response.Response.NombreArchivo,
                            RutaArchivo: response.Response.RutaArchivo
                        });
                    } else {
                        if (response.Message) {
                            $scope.showModalValidacion = true;
                            $scope.mensaje = response.Message;
                        }
                    }

                };

                /*
                 *######          Finaliza       ######
                 */
                /*
               * ################################################################
               * ######          Comienza Cargar Archivo Cierre Ticket     ######
               * ################################################################
              */
                var urlUploadCierre = {
                    modulo: 'tickets',
                    controller: 'seguimiento',
                    action: 'guardarArchivoSeguimientoCierre'
                }
                var uploadercierre = $scope.uploadercierre = uploadFilesService.__getInstance(urlUploadCierre);

                //Filtros Podemos validar el tamano del archivo, entre otras validaciones
                uploadercierre.filters.push({
                    name: 'sizeFilter',
                    fn: function(item) {
                        if ((item.size / 1024 / 1024) > 10) {
                            $scope.showModalValidacion = true;
                            $scope.mensaje = "El tama\u00F1o del archivo no debe exceder los 10 MB.";
                            return false;
                        }
                        return true;
                    }
                });
                uploadercierre.filters.push({
                    name: 'quantityFilter',
                    fn: function() {
                        if (uploadercierre.queue.length > 0) {
                            uploadercierre.clearQueue();
                        }
                        return true;
                    }
                });

                uploadercierre.filters.push({
                    name: 'extensionfilter',
                    fn: function(item) {
                        var extensionesPermitidas = new Array("xls",
                            "xlsx",
                            "doc",
                            "docx",
                            "zip",
                            "gif",
                            "jpg",
                            "png",
                            "xml",
                            "msg",
                            "pdf");
                        var extension = item.name.split(".").pop();
                        var permitida = false;
                        for (var i = 0; i < extensionesPermitidas.length; i++) {
                            if (extensionesPermitidas[i] === extension) {
                                permitida = true;
                                break;
                            }
                        }
                        if (!permitida) {
                            $scope.showModalValidacion = true;
                            $scope.mensaje = "La extensi\u00F3n del archivo no es permitida.";
                            return false;
                        } else {
                            return true;
                        }

                    }
                });

                uploadercierre.onSuccessItem = function(fileItem, response) {
                    angular.element("input[type='file']").val(null);
                    $scope.uploadercierre.clearQueue();
                    if (response.IsOk) {
                        $scope.comentariosTicketsModel = {
                            PersonaId: $scope.seguimientoticket.PersonaId,
                            Comentario: $scope.Comentario,
                            TicketId: $scope.seguimientoticket.TicketId,
                            IdEstatusTicket: 0,
                            IdEstatusTicketActual: $scope.seguimientoticket.IdEstatusTicket,
                            CveEstatus: $scope.estatusTicket.CveEstatus,
                            cerrado: true,
                            TicketsEstatusUpdate: {
                                IdTicketEstatus: $scope.seguimientoticket.IdTicketEstatus
                            },
                            RegistroTicketsUpdate: {
                                NumeroOT: $scope.NoOt,
                                NumeroOTSICS: $scope.OtRegistro
                            },
                            ArchivoTickets: {
                                NombreArchivo: response.Response.NombreArchivoTicketCerrado,
                                RutaArchivo: response.Response.RutaArchivoTicketCerrado
                            },
                            AseguradoraId: $scope.selectAsegTicket.AseguradoraId

                        }
                        //Guarda el estatus 
                        requestService.post(
                            'tickets',
                            'seguimiento',
                            'guardarComentariosTicket',
                            $scope
                            .comentariosTicketsModel,
                            true,
                            true,
                            true,
                            function successFunction() {
                                //$scope.showModalConfirm = true;
                                $location.path('/registros');
                            },
                            function errorFunction() {},
                            function badRequestFunction() {});

                    } else {
                        if (response.Message) {
                            $scope.showModalValidacion = true;
                            $scope.mensaje = response.Message;
                        }
                    }
                    $scope.uploadercierre.clearQueue();
                };

                /*
                *######          Finaliza carga archivo cierre      ######
                */

                function init2() {
                    $scope.isCarga = $stateParams.isCarga;
                    obtenerInformacionTicket();
                }

                function obtenerInformacionTicket() {
                    var ticketId = $stateParams.TicketId;
                    var isCArga = false;
                    if ($scope.isCarga === '1') {
                        isCArga = true;
                    }
                    // Duenio 177080
                    // Responsable 440797
                    $scope.seguimientoTicketModel =
                    {
                        TicketId: parseInt(ticketId),
                        IsCarga: isCArga
                    }
                    requestService.post(
                        'tickets',
                        'seguimiento',
                        'consultarInformacionTicket',
                        $scope.seguimientoTicketModel,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            if (response != null) {
                                $scope.seguimientoticket = response;
                                $scope.NoOt = $scope.seguimientoticket.NumeroOt;
                                $scope.OtRegistro = $scope.seguimientoticket.NumeroOtsics;
                                $scope.uploader.url = uploader.url +
                                    "?ticketId=" +
                                    $scope.seguimientoticket.TicketId +
                                    "&idEstatusTicket=" +
                                    ($scope.seguimientoticket.IdEstatusTicket);
                                $scope.uploadercierre.url = uploadercierre.url +
                                    "?ticketId=" +
                                    $scope.seguimientoticket.TicketId +
                                    "&idEstatusTicket=" +
                                    ($scope.seguimientoticket.IdEstatusTicket) +
                                    "&idTicketEstatus=" +
                                    ($scope.seguimientoticket.IdTicketEstatus) +
                                    "&personaId=" +
                                    ($scope.seguimientoticket.PersonaId) +
                                    "&Tkn=" +
                                    JSON.parse($base64.decode($sessionStorage.tkn));
                                $scope.selectAsegTicket
                                    .AseguradoraId = ($scope.seguimientoticket.AseguradoraId == null &&
                                        $scope.seguimientoticket.AseguradoraId == undefined &&
                                        $scope.seguimientoticket.AseguradoraId == 0)
                                    ? 0
                                    : $scope.seguimientoticket.AseguradoraId;
                                $scope.selectAsegTicket
                                    .Nombre = ($scope.seguimientoticket.Nombre == null &&
                                        $scope.seguimientoticket.Nombre == undefined &&
                                        $scope.seguimientoticket.Nombre == '')
                                    ? 0
                                    : $scope.seguimientoticket.Nombre;
                                if ($scope.seguimientoticket.CveEstatus !== 6 &&
                                    $scope.seguimientoticket.CveEstatus !== 7) {
                                    $scope.bndAseg = false;
                                    consultaAseguradora($scope.seguimientoticket);
                                }
                                TicketsEstatusModel = {
                                    IdTicketEstatus: $scope.seguimientoticket.IdTicketEstatus,
                                    IdEstatusTicket: $scope.seguimientoticket.IdEstatusTicket,
                                    TicketId: $scope.seguimientoticket.TicketId,
                                    PersonaId: $scope.seguimientoticket.PersonaId,
                                    FechaRegistro: null,
                                    NombreArchivoTicketCerrado: null,
                                    RutaArchivoTicketCerrado: null,
                                    Activo: false
                                }
                                //$scope.gestionControles();
                                /*Si el ticket ya se encuentra en el estatus “En Trámite” y el usuario que 
                                ingresa al seguimiento es el responsable de atención, el sistema deberá recuperar 
                                la información de estos campos y mostrarla en caso de contar con ella, 
                                además de dejar habilitados los campos para poder capturar.
                                */
                                if ($scope.seguimientoticket.CveEstatus === 3 &&
                                    $scope.seguimientoticket.TipoUsuario === "Responsable") {
                                    $scope.siTipoUsuario = false; //Los campos OT y No. OT Estaran habilitados
                                    $scope.siFloti = false; // Se muestra el div que contiene los campos OT y No. OT
                                } else {
                                    $scope.siTipoUsuario = true;
                                    $scope.siFloti = false;
                                }
                                /*  
                                Si el que ingresa es el usuario que levanto el ticket, 
                                podrá visualizar esta información pero los campos deberán estar inhabilitados.
                                */
                                /* else if ($scope.seguimientoticket.CveEstatus === 3 &&
                                     $scope.seguimientoticket.TipoUsuario === "Duenio") {
                                     $scope.siTipoUsuario = true; //Los campos OT y No. OT Estaran deshabilitados
                                     $scope.siFloti = false; // Se muestra el div que contiene los campos OT y No. OT
                                 }*/


                                obtenerComentarios($scope.seguimientoticket.TicketId);
                                obtnerArchivos();
                                $scope.siGuarda = false;
                                $scope.siReasigna = false;
                                if ($scope.seguimientoticket.TipoUsuario === "Duenio") {
                                    $scope.showRasigna = true;
                                } else {
                                    $scope.showRasigna = false;
                                }


                            } else {
                                $scope.siGuarda = true;
                                $scope.siReasigna = true;
                                $scope.showModalValidacion = true;
                                $scope.mensaje = "No se encontro informaci\u00F3n para el ticket seleccionado";
                            }

                        },
                        function errorFunction() {},
                        function badRequestFunction() {});
                }

                function consultaAseguradora(seguim) {
                    requestService.post('tickets',
                        'gestionTickets',
                        'consultarAseg',
                        {
                            ClienteId: seguim.IdCliente
                        },
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.asegTicket = response;
                            $scope.selectAsegTicket
                                .AseguradoraId = (seguim.AseguradoraId == null ||
                                    seguim.AseguradoraId == undefined ||
                                    seguim.AseguradoraId == 0)
                                ? 0
                                : seguim.AseguradoraId;
                            $scope.selectAsegTicket.Nombre = (seguim.Nombre == null ||
                                    seguim.Nombre == undefined ||
                                    seguim.Nombre == '')
                                ? 'NA'
                                : seguim.Nombre;
                        },
                        function errorFunction() {},
                        function badRequestFunction() {});
                }

                $scope.gestionControles = function() {
                    /*
                        Si el usuario firmado es el de Escalamiento de un Responsable, 
                        la lista se deberá llenar con los estatus: Incompleto, En Trámite y Cancelado, no importando el estatus actual del ticket.
                        Si el estatus cambia a “En Trámite” y el ticket corresponde a un cliente de Flotillas, 
                        el sistema deberá mostrar 2 campos adicionales para poder 
                        capturar el número de OT generado y el Numero de OT registrado para SICS, en caso de aplicar
    
                    */
                    $scope.check = $scope.estatusTicket.CveEstatus === 6 ? true : false;
                    if ($scope.seguimientoticket.SiFlotilla === true && $scope.estatusTicket.CveEstatus === 3) {
                        $scope.siFloti = false;
                        $scope.siTipoUsuario = false;
                    } else {
                        $scope.siFloti = true;
                    }
                }

                function loadEstatusTickets() {
                    var ticketId = $stateParams.TicketId;
                    $scope.seguimientoTicketModel =
                    {
                        TicketId: parseInt(ticketId)
                        //PersonaIdTipoUsuarioTicket: parseInt(personaIdTipoU)
                    }
                    requestService.post('tickets',
                        'seguimiento',
                        'obetnerEstatusByUsuario',
                        $scope.seguimientoTicketModel, //en caso de que no lleve parametros puse null
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.estatusTickets = response;
                            init();

                        },
                        function errorFunction() {},
                        function badRequestFunction() {});
                };

                //Set dafault value 'Todos'
                function init() {
                    var target = {
                        "IdEstatusTicket": "0",
                        "Descripcion": "Seleccionar",
                        "CveEstatus": "0"
                    };
                    for (var i = 0; i < $scope.estatusTickets.length; ++i) {
                        if ($scope.estatusTickets[i].Descripcion === target.Descripcion) {
                            $scope.estatusTickets[i] = target;
                            $scope.estatusTicket = $scope.estatusTickets[i];
                        }
                    }
                }

                $scope.obtener = function() {
                    console.log($scope.estatusTicket.IdEstatusTicket);

                };
                $scope.showModalValidacion = false;
                $scope.toggleModalConfirm = function() {
                    $scope.showModalValidacion = !$scope.showModalValidacion;
                }

                $scope.showReasignar = function() {
                    $("#myModal").modal("show");
                }
                $scope.hideReasignar = function() {
                    $("#myModal").modal("hide");
                    $scope.targetModelCero = {
                        ResponsableId: 0,
                        TicketId: 0
                    };
                    $scope.PersonaResponsableModel.Nombre = "";
                }

                $scope.showModalConfirm = false;
                $scope.toggleModalConfirmSuccess = function() {
                    $scope.showModalConfirm = !$scope.showModalConfirm;

                };
                /*
                * #########################################################################
                * ####### Garudar: Comentarios, archivos y actulizar estatus tickets ######
                * #########################################################################
               */
                $scope.mensaje = "";
                $scope.guardar = function() {
                    $scope.isCarga = 0;
                    var ban = true;
                    //if ($scope.estatusTickets.CveEstatus != undefined) {
                    if ($scope.estatusTicket.CveEstatus === 4 || $scope.estatusTicket.CveEstatus === "4") {
                        if ($scope.Comentario === "" || $scope.Comentario == null || $scope.Comentario.length === 0) {
                            $scope.showModalValidacion = true;
                            $scope.mensaje = "Para cambiar el estatus a incompleto debe capturar un comentario";
                            ban = false;
                        }
                    }
                    if ($scope.seguimientoticket.SiFlotilla === true &&
                    (($scope.estatusTicket.CveEstatus === "3" ||
                            $scope.seguimientoticket.CveEstatus === 3) &&
                        $scope.seguimientoticket.TipoUsuario === "Responsable")) {
                        if ($scope.NoOt === "" || $scope.NoOt == null || $scope.NoOt.length === 0) {
                            $scope.showModalValidacion = true;
                            $scope.mensaje = "Debe capturar el No. de OT";
                            ban = false;
                        }
                    }
                    //}
                    if ($scope.check === true) {
                        if ($scope.Comentario === "" || $scope.Comentario == null || $scope.Comentario.length === 0) {
                            $scope.showModalValidacion = true;
                            $scope.mensaje = "Para cerrar el ticket, debe capturar un comentario";
                            ban = false;
                        }
                        if ($scope.seguimientoticket.SiFlotilla === true &&
                            uploadercierre.getNotUploadedItems().length === 0) {
                            $scope.showModalValidacion = true;
                            $scope.mensaje = "Debe anexar un archivo para poder cerrar el ticket.";
                            ban = false;
                        }
                    }
                    if (ban) {
                        if ($scope.check === true) {
                            var options2 = {
                                message: '¿Est\u00E1 seguro de cerrar este ticket? ',
                                title: 'Confirmar',
                                className: 'text-info',
                                buttons: {
                                    warning: {
                                        label: "Cancelar",
                                        className: "btn-danger",
                                        callback: function() {

                                        }
                                    },
                                    success: {
                                        label: "Aceptar",
                                        className: "btn-info",
                                        callback: function() {
                                            if (uploadercierre.getNotUploadedItems().length === 0)
                                                cierreTicket();
                                            else
                                                uploadercierre.uploadAll();
                                        }
                                    }
                                }

                            }
                            $ngBootbox.customDialog(options2);
                        } else {
                            $scope.comentariosTicketsModel = {
                                PersonaId: $scope.seguimientoticket.PersonaId,
                                Comentario: $scope.Comentario,
                                TicketId: $scope.seguimientoticket.TicketId,
                                IdEstatusTicket: $scope.estatusTicket.IdEstatusTicket,
                                IdEstatusTicketActual: $scope.seguimientoticket.IdEstatusTicket,
                                CveEstatus: ($scope.estatusTicket.CveEstatus == "0")
                                    ? $scope.seguimientoticket.CveEstatus
                                    : $scope.estatusTicket.CveEstatus,
                                TicketsEstatusUpdate: {
                                    IdTicketEstatus: $scope.seguimientoticket.IdTicketEstatus,
                                    TicketId: $scope.seguimientoticket.TicketId
                                },
                                RegistroTicketsUpdate: {
                                    NumeroOT: $scope.NoOt,
                                    NumeroOTSICS: $scope.OtRegistro
                                },
                                AseguradoraId: $scope.selectAsegTicket.AseguradoraId

                            }
                            requestService.post(
                                'tickets',
                                'seguimiento',
                                'guardarComentariosTicket',
                                $scope.comentariosTicketsModel,
                                true,
                                true,
                                true,
                                function successFunction() {
                                    if ($scope.estatusTicket.CveEstatus === 7) {
                                        $location.path('/registros');
                                    } else {
                                        $scope.showModalConfirm = true;
                                        $scope.uploader.url = uploader.url;
                                        $scope.Comentario = "";
                                        obtenerInformacionTicket();

                                    }

                                },
                                function errorFunction() {},
                                function badRequestFunction() {});
                        }

                    }

                }

                /*
                * ################################################################
                * ######               Obtener Comentarios                  ######
                * ################################################################
               */
                function obtenerComentarios(ticketId) {
                    //var ticketId = $stateParams.TicketId;
                    $scope.seguimientoTicketModel =
                    {
                        TicketId: parseInt(ticketId)
                    }
                    requestService.post(
                        'tickets',
                        'seguimiento',
                        'listarComentariosTicket',
                        $scope.seguimientoTicketModel, //en caso de que no lleve parametros puse null
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.listaComentarios = response;
                        },
                        function errorFunction() {},
                        function badRequestFunction() {});
                }

                /*
                * ################################################################
                * ######                   Buscar Archivos                  ######
                * ################################################################
               */
                function obtnerArchivos() {
                    var ticketId = $stateParams.TicketId;
                    $scope.seguimientoTicketModel =
                    {
                        TicketId: parseInt(ticketId)
                    }
                    requestService.post(
                        'tickets',
                        'seguimiento',
                        'listaArchivosTickets',
                        $scope.seguimientoTicketModel, //en caso de que no lleve parametros puse null
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.ArchivosCierre = response;
                        },
                        function errorFunction() {},
                        function badRequestFunction() {});
                }

                /*
                 * ################################################################
                 * ######                   Descargar Archivo                ######
                 * ################################################################
                */
                $scope.descargarArchivo = function(downloadPath, fileName) {
                    window.open(encodeURI(mappingService.services['tickets']['Archivo']['descargarArchivo'] +
                        "?rutaArchivo=" +
                        downloadPath +
                        "&nombreArchivo=" +
                        fileName,
                        '_blank',
                        ''));
                }
                /*
                 * ################################################################
                 * ######                    Eliminar Archivo                ######
                 * ################################################################
                */
                $scope.eliminarArchivo = function(idArchivoTicket) {
                    var nombreArchivo = "";
                    for (var i = 0; i < $scope.ArchivosCierre.length; i++) {
                        if ($scope.ArchivosCierre[i].IdArchivoTicket === idArchivoTicket) {
                            nombreArchivo = $scope.ArchivosCierre[i].NombreArchivo;
                        }
                    }

                    var options = {
                        message: 'Est\u00E1 seguro de que desea eliminar el archivo ' + nombreArchivo,
                        title: 'Confirmar',
                        className: 'text-info',
                        buttons: {
                            warning: {
                                label: "Cancelar",
                                className: "btn-danger",
                                callback: function() {

                                }
                            },
                            success: {
                                label: "Aceptar",
                                className: "btn-info",
                                callback: function() {

                                    requestService.post('tickets',
                                        'seguimiento',
                                        'eliminarArchivos',
                                        $scope.archivoTicketsModel = {
                                            IdArchivoTicket: idArchivoTicket,
                                            TicketId: $scope.seguimientoticket.TicketId
                                        },
                                        true,
                                        true,
                                        true,
                                        function successFunction() {
                                            for (var j = 0; j < $scope.ArchivosCierre.length; j++) {
                                                if ($scope.ArchivosCierre[j].IdArchivoTicket === idArchivoTicket) {
                                                    $scope.ArchivosCierre.splice(j, 1);
                                                }
                                            }
                                        },
                                        function errorFunction() {
                                        },
                                        function badRequestFunction() {
                                        }
                                    );
                                }
                            }

                        }
                    };
                    $ngBootbox.customDialog(options);
                }

                /*
                 * ################################################################
                 * ###### Autocompletar para el campo de Responsable - Modal ######
                 * ################################################################
                */
                $scope.PersonaResponsableModel =
                {
                    Nombre: ""
                };
                $scope.targetModelCero = {
                    ResponsableId: 0,
                    TicketId: 0
                };

                $("#autocompleteResponsable").autocomplete({
                    source: function(request, response) {
                        $.ajax({
                            method: "POST",
                            url: mappingService
                                .services['tickets']['seguimiento']['buscarUsuarioResponsableSeguimiento'],
                            data: $.param({ Nombre: $scope.PersonaResponsableModel.Nombre }),
                            success: function(data) {
                                response($.map(data.Response,
                                    function(item) {
                                        item.label = item.Nombre;
                                        item.value = item.Nombre;
                                        return item;
                                    }));
                            },
                            headers: {
                                "Authorization": "Bearer " + $base64.decode($sessionStorage.tkn),
                                "Content-Type": 'application/x-www-form-urlencoded'
                            }
                        });
                    },
                    minLength: 5,
                    select: function(event, ui) {
                        $scope.targetModelCero = {
                            ResponsableId: ui.item.PersonaId,
                            TicketId: $stateParams.TicketId
                        }
                    },
                    focus: function(event, ui) {
                        $scope.PersonaResponsableModel.Nombre = ui.item.Nombre;
                    },
                    change: function(event, ui) {
                        if (ui.item == null) {
                            $scope.targetModelCero = {
                                ResponsableId: 0,
                                TicketId: 0
                            }
                        }
                    }
                });

                /*
                 * ################################################################
                 * ######                   Reasignar                        ######
                 * ################################################################
                */
                $scope.showModalConfirmReasignar = false;
                $scope.toggleModalConfirmReasignar = function() {
                    $scope.showModalConfirmReasignar = !$scope.showModalConfirmReasignar;
                    $location.path('/registros');
                }
                $scope.reasignar = function() {
                    var ban = true;
                    if ($scope.PersonaResponsableModel.Nombre === "" || $scope.PersonaResponsableModel.Nombre == null) {
                        $scope.showModalValidacion = true;
                        $scope.mensaje = "Debe reasignar un responsable";
                        ban = false;
                    }
                    if ($scope.targetModelCero.ResponsableId === null ||
                        $scope.targetModelCero.ResponsableId === "" ||
                        $scope.targetModelCero.ResponsableId === 0) {
                        $scope.showModalValidacion = true;
                        $scope.mensaje = "Debe reasignar un responsable";
                        ban = false;
                    }
                    if (ban) {
                        requestService.post(
                            'tickets',
                            'seguimiento',
                            'reasignarResposnable',
                            $scope.targetModelCero, //en caso de que no lleve parametros puse null
                            true,
                            true,
                            true,
                            function successFunction() {
                                $("#myModal").modal("hide");
                                $scope.showModalConfirmReasignar = true;
                            },
                            function errorFunction() {
                                $("#myModal").modal("hide");
                            },
                            function badRequestFunction() {});
                    }

                }

                /*Cierre de ticket sin arcivo*/
                function cierreTicket() {
                    $scope.comentariosTicketsModel = {
                        PersonaId: $scope.seguimientoticket.PersonaId,
                        Comentario: $scope.Comentario,
                        TicketId: $scope.seguimientoticket.TicketId,
                        IdEstatusTicket: 0,
                        IdEstatusTicketActual: $scope.seguimientoticket.IdEstatusTicket,
                        CveEstatus: $scope.estatusTicket.CveEstatus,
                        cerrado: true,
                        TicketsEstatusUpdate: {
                            IdTicketEstatus: $scope.seguimientoticket.IdTicketEstatus
                        },
                        RegistroTicketsUpdate: {
                            NumeroOT: $scope.NoOt,
                            NumeroOTSICS: $scope.OtRegistro
                        },
                        ArchivoTickets: {
                            NombreArchivo: null,
                            RutaArchivo: null
                        },
                        AseguradoraId: $scope.selectAsegTicket.AseguradoraId

                    }
                    requestService.post(
                        'tickets',
                        'seguimiento',
                        'guardarSeguimientoCierreSinArchivo',
                        TicketsEstatusModel,
                        true,
                        true,
                        true,
                        function successFunction() {
                            requestService.post(
                                'tickets',
                                'seguimiento',
                                'guardarComentariosTicket',
                                $scope
                                .comentariosTicketsModel,
                                true,
                                true,
                                true,
                                function successFunction() {
                                    //$scope.showModalConfirm = true;
                                    $location.path('/registros');
                                },
                                function errorFunction() {},
                                function badRequestFunction() {});
                        },
                        function errorFunction() {},
                        function badRequestFunction() {});
                }
            }
        ]);

    });