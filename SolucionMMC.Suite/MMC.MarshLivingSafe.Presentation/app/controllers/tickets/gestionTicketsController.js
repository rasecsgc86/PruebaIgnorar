define(['modules/app', 'services/requestService', 'services/mappingService', 'services/uploadFilesService'],
    function (app) {
        app.controller('GestionTicketsController', [
            '$scope', '$rootScope', '$location', 'requestService', '$base64', 'uploadFilesService', '$ngBootbox',
            'mappingService', '$sessionStorage', '$filter',
            function ($scope,
                $rootScope,
                $location,
                requestService,
                $base64,
                uploadFilesService,
                $ngBootbox,
                mappingService,
                $sessionStorage,
                $filter) {
                $rootScope.tittle = "Registro";
                $('body').removeClass('modal-open');
                $('.modal-backdrop').remove();
                $scope.ESTATUS_REGISTRADO = 1;
                $scope.ESTATUS_PROCESO = 2;
                $scope.ESTATUS_TRAMITE = 3;
                $scope.ESTATUS_INCOMPLETO = 4;
                $scope.ESTATUS_DOCUMENTACION = 5;
                $scope.gestionTickets = [];

                cargarGestionTickets();

                $scope.nuevoTicketsModal = function () {
                    document.getElementById("nuevoTicketsForm").reset();
                    $scope.clientes = [];
                    $scope.esClienteFlotillas = [];
                    $scope.responsable = [];
                    $scope.caratulas = [];
                    $scope.ticketModel = {};
                    $scope.CurrentDate = new Date();
                    $scope.clienteProductoModel = {};
                    $scope.disabledCaratula = true;
                    $scope.disabledTiposTicket = true;
                    $scope.descripcionTextArea = null;
                    $scope.emailCopiarA = null;
                    $scope.tiposTicket = [];
                    $scope.reportaA = [];
                    $scope.estatusTicket = [];
                    $scope.asegTicket = [{
                        AseguradoraId: 0,
                        Nombre: 'NA'
                    }];
                    $scope.datosContactoCliente = {
                        NombreCliente: ''
                    };
                    $scope.datosContactoClienteModel = {
                        NombreCliente: ''
                    };
                    $scope.datosContactoModel = [];
                    $scope.nuevoTicketModel = {};
                    $scope.Archivos = [];
                    $scope.mostrarNuevoTicketModal();
                    $scope.datosContactoNombre = null;
                    $scope.datosContactoApellidos = null;
                    $scope.datosContactoTelefonos = null;
                    $scope.datosContactoEmail = null;

                    $scope.isResponsable = false;
                    $scope.isValidaDescripcion = true;
                    $scope.isValidoEmail = true;
                    $scope.isSelectClientesRequired = true;
                    $scope.isSelectCaratulasRequired = true;
                    $scope.isSelectTiposTicketRequired = true;
                    $scope.isDescripcionTextAreaRequired = true;
                    $scope.isSelectReportaARequired = true;
                    $scope.isSelectEstatusRequired = true;
                    $scope.isArchivoRequired = true;
                    $scope.isDatosContactoClienteRequired = true;
                    $scope.isDatosContactoNombreRequired = true;
                    $scope.isDatosContactoApellidosRequired = true;
                    $scope.isDatosContactoTelefonosRequired = true;
                    $scope.isDatosContactoEmailRequired = true;
                    $scope.isValidoDatosContactoCliente = true;
                    $scope.isValidoDatosContactoNombre = true;
                    $scope.isValidoDatosContactoApellidos = true;
                    $scope.isValidoDatosContactoTelefonos = true;
                    $scope.isValidoDatosContactoEmail = true;

                    $scope.selectClientes = {
                        IdCliente: 0,
                        NombreCliente: ""
                    }
                    $scope.selectTiposTicket = {
                        IdTipoTicket: 0,
                        DescripcionTipoTicket: ""
                    }
                    $scope.selectEstatusTickets = {
                        IdEstatusTicket: 0,
                        ClaveEstatus: 0,
                        Descripcion: ""
                    }
                    $scope.selectCaratulas = {
                        PolizaCaratula: 0,
                        FormaPago: "",
                        TipoString: "",
                        TipoCobranzaString: ""
                    }
                    $scope.selectReportaA = {
                        IdOrigenTicket: 0,
                        OrigenTicket: ""
                    }
                    $scope.selectAsegTicket = {
                        AseguradoraId: 0,
                        Nombre: "NA"
                    }
                };
                $scope.registarDatosContactoModal = function () {
                    document.getElementById("datosContactoModal").style.zIndex = "1100";
                    document.getElementById("nuevoTicketsModal").style.overflowY = "hidden";
                    document.getElementById("nuevoTicketsModal").style.display = "none";
                    $("#datosContactoModal").fadeIn();
                }

                $scope.cerrrarDatosContactoModal = function () {
                    document.getElementById("nuevoTicketsModal").style.overflowY = "scroll";
                    document.getElementById("nuevoTicketsModal").style.display = "block";
                    $("#datosContactoModal").fadeOut();
                    $scope.isSelectReportaARequired = true;
                    $scope.mostrarNuevoTicketModal();
                }

                
                $scope.cerrrarDatosContactoModalIncompletos = function () {                    
                    $scope.limpiaDatosContacto();
                    document.getElementById("nuevoTicketsModal").style.overflowY = "scroll";
                    document.getElementById("nuevoTicketsModal").style.display = "block";
                    $("#datosContactoModal").fadeOut();
                    $scope.selectReportaA = "";
                    $scope.isSelectReportaARequired = false;
                    $scope.mostrarNuevoTicketModal();
                }

                $scope.limpiaDatosContacto = function (){
                    $scope.datosContactoCliente = {
                        NombreCliente: ''
                    };
                    $scope.datosContactoClienteModel = {
                        NombreCliente: ''
                    };
                    $scope.datosContactoModel = [];
                    $scope.datosContactoNombre = null;
                    $scope.datosContactoApellidos = null;
                    $scope.datosContactoTelefonos = null;
                    $scope.datosContactoEmail = null;
                }
                $scope.mostrarNuevoTicketModal = function () {
                    setTimeout(function () {
                        document.getElementById("datosContactoModal").style.zIndex = "1";
                    }, 1);
                    $("#nuevoTicketsModal").modal('show');
                }

                $scope.cerrarNuevoTicketModal = function () {
                    $("#nuevoTicketsModal").modal('hide');
                }

                $scope.mostrarErrorModal = function (mensajeError) {
                    $scope.mensajeError = mensajeError;
                    $("#errorModal").fadeIn();
                }

                $scope.cerrarErrorModal = function () {
                    $("#errorModal").fadeOut();
                    $scope.mostrarNuevoTicketModal();
                }

                $scope.mostrarExitoModal = function (mensajeExito) {
                    $scope.mensajeExito = mensajeExito;
                    $("#exitoModal").fadeIn();
                }

                $scope.cerrarExitoModal = function () {
                    $("#exitoModal").fadeOut();
                    $scope.cerrarNuevoTicketModal();
                }
                
                //Autocompletar para el campo de cliente
                $("#clienteAutocomplete").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            method: "POST",
                            url: mappingService.services['tickets']['gestionTickets']['consultarClientes'],
                            data: $.param({   
                                NombreCliente: $scope.clienteAutocomplete,
                                Tkn: JSON.parse($base64.decode($sessionStorage.tkn))
                            }),
                            success: function (data) {
                                response($.map(data.Response, function (item) {
                                    item.label = item.NombreCliente;
                                    item.value = item.NombreCliente;
                                    return item;
                                }));
                            },
                            headers: {
                                "Authorization": "Bearer " + $base64.decode($sessionStorage.tkn),
                                "Content-Type": 'application/x-www-form-urlencoded'
                            }
                        });
                    },
                    minLength: 4,
                    select: function (event, ui) {
                        $scope.selectClientes = ui.item;
                        $scope.consultarSiEsClienteFlotillas();
                    },
                    focus: function (event, ui) {

                    },
                    change: function (event, ui) {
                        if (ui.item === null) {
                            $scope.selectClientes = [];
                        }
                    }
                });

                $scope.consultarSiEsClienteFlotillas = function () {
                    $scope.responsable.NombreCompletoResponsable = '';
                    $scope.isResponsable = false;
                    $scope.selectTiposTicket = [];
                    $scope.selectCaratulas = [];
                    requestService.post('tickets',
                        'gestionTickets',
                        'consultarSiEsClienteFlotillas',
                        $scope.clienteProductoModel = {
                            idCliente: $scope.selectClientes.IdCliente
                        },
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.esClienteFlotillas = response;
                            if ($scope.esClienteFlotillas) {
                                requestService.post('tickets',
                                    'gestionTickets',
                                    'consultarCaratula',
                                    $scope.clienteProductoModel = {
                                        idCliente: $scope.selectClientes.IdCliente
                                    },
                                    true,
                                    true,
                                    true,
                                    function successFunction(response) {
                                        $scope.caratulas = response;
                                        if (response.length === 0) {
                                            $scope
                                                .mostrarErrorModal("No existen Caratulas asociadas al cliente seleccionado");
                                        } else {
                                            $scope.disabledCaratula = false;
                                        }
                                    },
                                    function errorFunction() {},
                                    function badRequestFunction() {}
                                );
                            } else {
                                $scope.cerrrarDatosContactoModalIncompletos();
                                requestService.post('tickets',
                                    'gestionTickets',
                                    'consultarReportaA',
                                    null,
                                    true,
                                    true,
                                    true,
                                    function successFunction(response) {
                                        $scope.reportaA = response;
                                    },
                                    function errorFunction() {},
                                    function badRequestFunction() {}
                                );
                            }
                        },
                        function errorFunction() {},
                        function badRequestFunction() {}
                    );

                    requestService.post('tickets',
                        'gestionTickets',
                        'consultarTiposTickets',
                        $scope.clienteProductoModel = {
                            idCliente: $scope.selectClientes.IdCliente
                        }, //en caso de que no lleve parametros puse null
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.tiposTicket = response;
                            if (response[0].DescripcionTipoTicket === null) {
                                $scope
                                    .mostrarErrorModal("No existen Tipos de Tickets asociados al cliente seleccionado");
                                $scope.disabledTiposTicket = true;
                            } else {
                                $scope.disabledTiposTicket = false;

                            }

                        },
                        function errorFunction() {},
                        function badRequestFunction() {});


                    requestService.post('tickets',
                        'gestionTickets',
                        'consultaEstatusTickets',
                        null,
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.estatusTicket = response;
                        },
                        function errorFunction() {},
                        function badRequestFunction() {});

                    requestService.post('tickets',
                        'gestionTickets',
                        'consultarAseg', {
                            ClienteId: $scope.selectClientes.IdCliente
                        },
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.asegTicket = response;
                            $scope.selectAsegTicket = {
                                AseguradoraId: 0,
                                Nombre: "NA"
                            }
                        },
                        function errorFunction() {},
                        function badRequestFunction() {});
                };
                $scope.consultarResponsable = function () {
                    if (($scope.selectTiposTicket.IdTipoTicket != undefined &&
                            $scope.selectTiposTicket.IdTipoTicket !== '' &&
                            $scope.selectTiposTicket.IdTipoTicket != null) &&
                        $scope.selectClientes.IdCliente != undefined &&
                        $scope.selectClientes.IdCliente !== '' &&
                        $scope.selectClientes.IdCliente != null) {
                        requestService.post('tickets',
                            'gestionTickets',
                            'consultarResponsable',
                            $scope.clienteProductoModel = {
                                IdTipoTicket: $scope.selectTiposTicket.IdTipoTicket,
                                IdCliente: $scope.selectClientes.IdCliente
                            },
                            true,
                            true,
                            true,
                            function successFunction(response) {
                                $scope.responsable = response;
                                $scope.isResponsable = response.NombreCompletoResponsable !== "  " ? true : false;
                            },
                            function errorFunction() {},
                            function badRequestFunction() {}
                        );
                    }
                };
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

                $scope.validarCorreo = function () {
                    var email = $scope.emailCopiarA;
                    if (email !== null && email !== "") {
                        if (validarCorreoCopiarA(email)) {
                            $scope.resultadoValidaEmail = null;
                            $scope.isValidoEmail = true;
                        } else {
                            $scope.resultadoValidaEmail = email + " no es valido";
                            $scope.isValidoEmail = false;
                        }
                        return false;
                    } else {
                        $scope.isValidoEmail = true;
                        return false;
                    }
                }

                $("#datosContactoCliente").autocomplete({
                    source: function (request, response) {
                        $.ajax({
                            method: "POST",
                            url: mappingService
                                .services['tickets']['gestionTickets']['consultarAgencias'],
                            data: $.param({
                                Agencias: {
                                    Agencia: $scope.datosContactoCliente.NombreCliente
                                },
                                Clientes: {
                                    ClienteId: $scope.selectClientes.IdCliente
                                },
                                Tkn: JSON.parse($base64.decode($sessionStorage.tkn))
                            }),
                            success: function (data) {
                                response($.map(data.Response,
                                    function (item) {
                                        item.label = item.Agencia;
                                        item.value = item.Agencia;
                                        return item;
                                    }));
                            },
                            headers: {
                                "Authorization": "Bearer " +
                                    $base64.decode($sessionStorage.tkn),
                                "Content-Type": 'application/x-www-form-urlencoded'
                            }
                        });
                    },
                    minLength: 2,
                    select: function (event, ui) {
                        $scope.datosContactoClienteModel = {
                            NombreCliente: ui.item.NombreCliente,
                            IdCliente: ui.item.IdCliente
                        }
                    },
                    focus: function (event, ui) {
                        $scope.datosContactoClienteModel.NombreCliente = ui.item.NombreCliente;
                    },
                    change: function (event, ui) {
                        if (ui.item == null) {
                            $scope.datosContactoClienteModel = {
                                NombreCliente: 0,
                                IdCliente: 0
                            }
                        }
                    }

                });

                $scope.subirArchivos = function(){
                    $scope.uploader.uploadAll();
                }


                $scope.guardarInformacion = function () {
                    $scope.isSelectClientesRequired = $scope.selectClientes.IdCliente !== 0 ? true : false;
                    $scope.isSelectTiposTicketRequired = $scope.selectTiposTicket.IdTipoTicket !== 0 ? true : false;
                    $scope.isDescripcionTextAreaRequired = $scope.gestionTicketsForm.descripcionTextArea.$valid;
                    $scope.isSelectReportaARequired = $scope.gestionTicketsForm.selectReportaA.$valid;
                    $scope.isSelectEstatusRequired = $scope.selectEstatusTickets.IdEstatusTicket !== 0 ? true : false;
                    $scope.isArchivoRequired = $scope.Archivos.length > 0;
                    if ($scope.esClienteFlotillas) {
                        $scope.isSelectCaratulasRequired = $scope.selectCaratulas.PolizaCaratula !== 0 ? true : false;
                        if ($scope.isSelectClientesRequired &&
                            $scope.isSelectTiposTicketRequired &&
                            $scope.isDescripcionTextAreaRequired &&
                            $scope.isSelectCaratulasRequired &&
                            $scope.isSelectEstatusRequired &&
                            $scope.isValidoEmail &&
                            $scope.isValidaDescripcion &&
                            $scope.isArchivoRequired) {
                            $scope.guardarTicket();
                        } else {
                            $scope
                                .mostrarErrorModal("Para poder guardar el ticket deben estar los datos completos y llenados correctamente");
                        }
                    } else {
                        $scope.isSelectReportaARequired = $scope.gestionTicketsForm.selectReportaA.$valid;
                        if ($scope.isSelectClientesRequired &&
                            $scope.isSelectTiposTicketRequired &&
                            $scope.isDescripcionTextAreaRequired &&
                            $scope.isSelectEstatusRequired &&
                            $scope.isSelectReportaARequired &&
                            $scope.isValidoEmail &&
                            $scope.isArchivoRequired &&
                            $scope.isDatosContactoNombreRequired &&
                            $scope.isDatosContactoApellidosRequired &&
                            $scope.isDatosContactoTelefonosRequired &&
                            $scope.isDatosContactoEmailRequired) {
                            if ($scope.selectReportaA.OrigenTicket === 'Agencia') {
                                if ($scope.isDatosContactoClienteRequired) {
                                    $scope.guardarTicket();
                                }
                            } else {
                                $scope.guardarTicket();
                            }
                        } else {
                            $scope.mostrarErrorModal("Para poder guardar el ticket deben estar los datos completos");
                        }
                    }
                }

                $scope.guardarDatosContacto = function () {
                    var banAgencia;

                    if ($scope.datosContactoClienteModel.IdCliente !== 0) {
                        banAgencia = true;
                        $scope.isValidoDatosContactoCliente = true;
                    } else {
                        banAgencia = false;
                        $scope.isValidoDatosContactoCliente = false;
                    }

                    var nombre = $("#datosContactoNombre").val();
                    var banNombre = false;
                    if (nombre !== "null" &&
                        nombre !== "" &&
                        nombre !== null &&
                        nombre !== " ") {
                        if (!validarLetras(nombre)) {
                            banNombre = false;
                            $scope.resultadoDatosContactoNombre = "Inserte caracteres correctos";
                            $scope.isValidoDatosContactoNombre = false;
                        } else {
                            banNombre = true;
                        }
                    } else {
                        $scope.isDatosContactoNombreRequired = false;
                    }
                    if (banNombre) {
                        $scope.resultadoDatosContactoNombre = null;
                        $scope.isValidoDatosContactoNombre = true;
                        $scope.isDatosContactoNombreRequired = true;
                    }


                    var apellidos = $("#datosContactoApellidos").val();
                    var banApellidos = false;
                    if (apellidos !== "null" &&
                        apellidos !== "" &&
                        apellidos !== null &&
                        apellidos !== " ") {
                        if (!validarLetras(apellidos)) {
                            banApellidos = false;
                            $scope.resultadoDatosContactoApellidos = "Inserte caracteres correctos";
                            $scope.isValidoDatosContactoApellidos = false;
                        } else {
                            banApellidos = true;
                        }
                    } else {
                        $scope.isDatosContactoApellidosRequired = false;
                    }
                    if (banApellidos) {
                        $scope.resultadoDatosContactoApellidos = null;
                        $scope.isValidoDatosContactoApellidos = true;
                        $scope.isDatosContactoApellidosRequired = true;
                    }


                    var telefonos = $("#datosContactoTelefonos").val();
                    var banTelefonos = false;
                    if (telefonos !== "null" &&
                        telefonos !== "" &&
                        telefonos !== null) {
                        if (!validarNumeros(telefonos)) {
                            banTelefonos = false;
                            $scope
                                .resultadoDatosContactoTelefonos =
                                "Inserte caracteres correctos. 10 caracteres obligatorios.";
                            $scope.isValidoDatosContactoTelefonos = false;
                        } else {
                            banTelefonos = true;
                        }
                    } else {
                        $scope.isDatosContactoTelefonosRequired = false;
                    }
                    if (banTelefonos) {
                        $scope.resultadoDatosContactoTelefonos = null;
                        $scope.isValidoDatosContactoTelefonos = true;
                        $scope.isDatosContactoTelefonosRequired = true;
                    }

                    var email = $("#datosContactoEmail").val();
                    var banEmail = false;
                    if (email !== "null" &&
                        email !== "" &&
                        email !== null) {
                        if (!validarCorreo(email)) {
                            banEmail = false;
                            $scope.resultadoDatosContactoEmail = "Inserte caracteres correctos";
                            $scope.isValidoDatosContactoEmail = false;
                        } else {
                            banEmail = true;
                        }
                    } else {
                        $scope.isDatosContactoEmailRequired = false;
                    }
                    if (banEmail) {
                        $scope.resultadoDatosContactoEmail = null;
                        $scope.isValidoDatosContactoEmail = true;
                        $scope.isDatosContactoEmailRequired = true;
                    }

                    if ($scope.selectReportaA.OrigenTicket === 'Agencia') {
                        $scope.isDatosContactoClienteRequired = $scope.datosContactoForm.datosContactoCliente.$valid;
                        if ($scope.isDatosContactoClienteRequired &&
                            banAgencia &&
                            banNombre &&
                            banApellidos &&
                            banTelefonos &&
                            banEmail) {
                            $scope.cerrrarDatosContactoModal();
                        } else {
                            $scope.resultadoDatosContacto = "Capturar toda la informaci\u00F3n.";
                        }
                    } else {
                        if (banNombre &&
                            banApellidos &&
                            banTelefonos &&
                            banEmail) {
                            $scope.cerrrarDatosContactoModal();
                        } else {
                            $scope.resultadoDatosContacto = "Capturar toda la informaci\u00F3n.";
                        }
                    }
                }

                $scope.guardarTicket = function () {
                    $scope.nuevoTicketModel = {
                        EsClienteFlotillas: $scope.esClienteFlotillas,
                        PersonaId: $scope.selectClientes.IdCliente,
                        TipoId: $scope.selectTiposTicket.IdTipoTicket,
                        Tipo: $scope.selectTiposTicket.DescripcionTipoTicket,
                        ResponsableId: $scope.responsable.IdResponsable,
                        NombreCompletoResponsable: $scope.responsable.NombreCompletoResponsable,
                        MailResponsable: $scope.responsable.MailResponsable,
                        DescripcionTicket: $scope.descripcionTextArea,
                        CopiarA: $scope.emailCopiarA,
                        IdEstatusTicket: $scope.selectEstatusTickets.IdEstatusTicket,
                        ClaveEstatus: $scope.selectEstatusTickets.CveEstatus,
                        Archivos: $scope.Archivos,
                        AseguradoraId: $scope.selectAsegTicket.AseguradoraId
                    }
                    if (!$scope.esClienteFlotillas) {
                        $scope.nuevoTicketModel.CatalogoOrigenId = $scope.selectReportaA.IdOrigenTicket;
                        if ($scope.selectReportaA.OrigenTicket === 'Agencia') {
                            $scope.nuevoTicketModel.DatosContactoAgenciaId = $scope.datosContactoClienteModel.IdCliente;
                        }
                        $scope.nuevoTicketModel.DatosContactoNombre = $scope.datosContactoNombre;
                        $scope.nuevoTicketModel.DatosContactoApellidos = $scope.datosContactoApellidos;
                        $scope.nuevoTicketModel.DatosContactoTelefonos = $scope.datosContactoTelefonos;
                        $scope.nuevoTicketModel.DatosContactoEmail = $scope.datosContactoEmail;
                    } else {
                        $scope.nuevoTicketModel.CaratulaId = $scope.selectCaratulas.PolizaCaratula;
                    }
                    requestService.post('tickets',
                        'gestionTickets',
                        'guardarTicket',
                        $scope.nuevoTicketModel,
                        true,
                        true,
                        true,
                        function successFunction() {
                            $scope.mostrarExitoModal("Ticket Registrado Correctamente");
                            cargarGestionTickets();
                        },
                        function errorFunction() {
                            $scope.cerrarNuevoTicketModal();
                        },
                        function badRequestFunction() {}
                    );
                };

                var urlUpload = {
                    modulo: 'tickets',
                    controller: 'gestionTickets',
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
                            "PDF");
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
                        if (nombreArchivo.length > 25) {
                            nombreArchivo = $filter('limitTo')(nombreArchivo, 25, 0) +
                                "..." +
                                nombreArchivo.split(".")[1];
                        }
                        $scope.Archivos.push({
                            IdArchivoTicket: response.Response.IdArchivoTicket,
                            NombreArchivo: response.Response.NombreArchivo,
                            RutaArchivo: response.Response.RutaArchivo,
                            NombreCorto: nombreArchivo
                        });
                    } else {
                        if (response.Message) {
                            $scope.mostrarErrorModal(response.Message);
                        }
                    }
                    $scope.isArchivoRequired = true;
                };

                $scope.eliminarArchivo = function (idArchivoTicket) {
                    var nombreArchivo = "";
                    for (var i = 0; i < $scope.Archivos.length; i++) {
                        if ($scope.Archivos[i].IdArchivoTicket === idArchivoTicket) {
                            nombreArchivo = $scope.Archivos[i].NombreCorto;
                        }
                    }

                    var options = {
                        message: 'Est\u00E1 seguro de que desea eliminar el archivo ' + nombreArchivo,
                        title: 'Confirmar',
                        className: 'text-info',
                        buttons: {
                            warning: {
                                label: "Cancelar",
                                className: "btn-warning",
                                callback: function () {
                                    document.getElementById("nuevoTicketsModal").style.overflowY = "scroll";
                                }
                            },
                            success: {
                                label: "Aceptar",
                                className: "btn-info",
                                callback: function () {

                                    requestService.post('tickets',
                                        'gestionTickets',
                                        'eliminarArchivo',
                                        $scope.archivoTicketsModel = {
                                            idArchivoTicket: idArchivoTicket
                                        },
                                        true,
                                        true,
                                        true,
                                        function successFunction() {
                                            for (var i = 0; i < $scope.Archivos.length; i++) {
                                                if ($scope.Archivos[i].IdArchivoTicket === idArchivoTicket) {
                                                    $scope.Archivos.splice(i, 1);
                                                }
                                            }
                                            document.getElementById("nuevoTicketsModal").style.overflowY = "scroll";
                                        },
                                        function errorFunction() {},
                                        function badRequestFunction() {}
                                    );
                                }
                            }
                        }
                    };
                    $ngBootbox.customDialog(options);
                }


                $scope.uploader.url = uploader.url;

                $scope.descargarArchivo = function (downloadPath, fileName) {
                    window.open(mappingService.services['tickets']['Archivo']['descargarArchivo'] +
                        "?rutaArchivo=" +
                        downloadPath +
                        "&nombreArchivo=" +
                        fileName,
                        '_blank',
                        '');
                }

                function validarCorreo(email) {
                    var re =
                        /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                    return re.test(email);
                }


                function validarCorreoCopiarA(email) {
                    var re =
                        /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))((;)(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,})))*$/;
                    return re.test(email);
                }

                function validarAlfanumerico(campo) {
                    var re =
                        /^[a-zA-Z0-9\u00f1\u00d1\u00e1\u00e9\u00ed\u00f3\u00fa\u00c1\u00c9\u00cd\u00d3\u00da,. ]+$/;
                    return re.test(campo);
                }

                function validarLetras(campo) {
                    var re = /^[a-zA-Z\u00f1\u00d1\u00e1\u00e9\u00ed\u00f3\u00fa\u00c1\u00c9\u00cd\u00d3\u00da ]+$/;
                    return re.test(campo);
                }

                function validarNumeros(cadena) {
                    var re = /[0-9]{10}/;
                    return re.test(cadena);
                }

                function numMenorDiez(num) {
                    if (num < 10) {
                        num = '0' + num;
                    }
                    return num;
                }


                function cargarGestionTickets() {
                    requestService.post('tickets',
                        'gestionTickets',
                        'consultarTickest', {
                            'ticketModel': $scope.ticketModel
                        },
                        true,
                        true,
                        true,
                        function successFunction(response) {
                            $scope.gestionTickets = response;
                        },
                        function errorFunction() {},
                        function badRequestFunction() {});


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
                };
            }
        ]);

    });