define(['modules/app', 'services/requestService', 'services/mappingService'], function (app) {
    /**
     * DEFINIMOS Y REGISTRAMOS EL CONTROLLER CalendarioController.
     * @param $scope - PROVEEDOR DE ANGULAR REQUERIDO PARA MAPEAR INFORMACION DE LOS MODELOS AL TEMPLATE
     * @param $location - PROVEEDOR DE ANGULAR REQUERIDO PARA REDIRECCIONAR HACIA OTROS TEMPLATES
     * @param requestService - SERVICIO NECESARIO PARA PODER LLAMAR LOS SERVICIOS REST-CONTROLLER
     * @param $localStorage - PROVEEDOR DE ANGULAR PARA OBTENER INFORMACION DE LA MEMORIA LOCAL
     *
     * @var tittle - TITULO DE LA PAGINA  
     */
    app.controller('ConfiguradorParametrosTicketsController', [
        '$scope', '$rootScope', '$location', 'requestService', '$localStorage', '$base64', 'mappingService', '$sessionStorage', '$ngBootbox',
        function ($scope, $rootScope, $location, requestService, $localStorage, $base64, mappingServices, $sessionStorage, $ngBootbox) {
            $rootScope.tittle = "Configurador Parametros Tickets";
            $scope.isEditar = false;

            var tiposTicketsClientesModel = {

            };
            $scope.targetModel = {
                IdCliente: 0,
                NombreCliente: ""
            }
            $scope.targetModelCero = {
                PersonaId: 0,
                MailResponsable: ""
            };
            $scope.targetModelUno = {
                PersonaId: 0,
                MailEscalemiento1: ""
            };
            $scope.targetModelDos = {
                PersonaId: 0,
                MailEscalamiento2: ""
            };

            $scope.listaConfiguraciones = [];
            $scope.clienteProductoModel = {
                NombreCliente: ""
            };
            $scope.PersonaResponsableModel = {
                Nombre: ""
            };
            $scope.PersonaEscalamiento1 = {
                Nombre: ""
            };
            $scope.PersonaEscalamiento2 = {
                Nombre: ""
            };

            //Autocompletar para el campo de cliente
            $("#clienteAutocomplete").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        method: "POST",
                        url: mappingServices.services['tickets']['configuracion']['consultarClientesConfigurarParametros'],
                        data: $.param({
                            NombreCliente: $scope.clienteProductoModel.NombreCliente,
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
                minLength: 5,
                select: function (event, ui) {
                    $scope.targetModel = {
                        IdCliente: ui.item.IdCliente,
                        NombreCliente: ui.item.NombreCliente
                    }
                },
                focus: function (event, ui) {

                },
                change: function (event, ui) {
                    if (ui.item === null) {
                        $scope.targetModel = {
                            IdCliente: 0,
                            NombreCliente: ""
                        }

                    }
                }
            });

            // Autocompletar para el campo de Responsable - Modal
            $("#autocompleteResponsable").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        method: "POST",
                        url: mappingServices.services['tickets']['configuracion']['buscarUsuarioResponsable'],
                        data: $.param({
                            Nombre: $scope.PersonaResponsableModel.Nombre
                        }),
                        success: function (data) {
                            response($.map(data.Response, function (item) {
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
                select: function (event, ui) {
                    $("#autocompleteResponsable").val(ui.item.Nombre);
                    $scope.targetModelCero = {
                        PersonaId: ui.item.PersonaId,
                        MailResponsable: ui.item.Mail
                    }

                    $scope.targetModelCero.MailResponsable = ui.item.Mail;
                    $("#responsableUno").val(ui.item.Mail);


                },
                focus: function (event, ui) {
                    $("#autocompleteResponsable").val(ui.item.Nombre);
                    $scope.PersonaResponsableModel.Nombre = ui.item.Nombre;
                    $scope.targetModelCero.MailResponsable = ui.item.Mail;
                    $("#responsableUno").val(ui.item.Mail);
                },
                change: function (event, ui) {
                    if (ui.item == null) {
                        $scope.targetModelCero = {
                            PersonaId: 0,
                            MailResponsable: ""
                        };
                    }
                }
            });

            //Autocompletar para el campo de Escalamineto uno - Modal
            $("#autocompleteEscalamientoUno").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        method: "POST",
                        url: mappingServices.services['tickets']['configuracion']['buscarUsuarioResponsable'],
                        data: $.param({
                            Nombre: $scope.PersonaEscalamiento1.Nombre
                        }),
                        success: function (data) {
                            response($.map(data.Response, function (item) {
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
                select: function (event, ui) {
                    $scope.targetModelUno = {
                        PersonaId: ui.item.PersonaId,
                        MailEscalemiento1: ui.item.Mail
                    }
                    $("#mailEscalamientoUno").val(ui.item.Mail);
                },
                focus: function (event, ui) {
                    $scope.PersonaEscalamiento1.Nombre = ui.item.Nombre;
                    $scope.targetModelUno.MailResponsable = ui.item.Mail;
                    $("#mailEscalamientoUno").val(ui.item.Mail);
                },
                change: function (event, ui) {
                    if (ui.item == null) {
                        $scope.targetModelUno = {
                            PersonaId: 0,
                            MailEscalemiento1: ""
                        };
                    }
                }
            });

            //Autocompletar para el campo de Escalamineto dos - Modal
            $("#autocompleteEscalamientoDos").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        method: "POST",
                        url: mappingServices.services['tickets']['configuracion']['buscarUsuarioResponsable'],
                        data: $.param({
                            Nombre: $scope.PersonaEscalamiento2.Nombre
                        }),
                        success: function (data) {
                            response($.map(data.Response, function (item) {
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
                select: function (event, ui) {
                    $scope.targetModelDos = {
                        PersonaId: ui.item.PersonaId,
                        MailEscalamiento2: ui.item.Mail
                    }
                    $("#mailEscalamientoDos").val(ui.item.Mail);
                },
                focus: function (event, ui) {
                    $scope.PersonaEscalamiento2.Nombre = ui.item.Nombre;
                    $scope.targetModelDos.MailEscalamiento2 = ui.item.Mail;
                    $("#mailEscalamientoDos").val(ui.item.Mail);
                },
                change: function (event, ui) {
                    if (ui.item == null) {
                        $scope.targetModelDos = {
                            PersonaId: 0,
                            MailEscalamiento2: ""
                        };
                    }
                }

            });
            $scope.toggleModalOpen = function () {
                $scope.isEditar = false;
                limpiarFormulario();
                $('#myModal').modal('show');
            };
            $scope.toggleModal = function () {
                limpiarFormulario();
                $('#myModal').modal('hide');
            };
            $scope.showModalConfirm = false;
            $scope.toggleModalConfirm = function () {
                $scope.showModalConfirm = !$scope.showModalConfirm;
                $('#myModal').modal('hide');
            };
            $scope.mensaje = "";
            $scope.showModal2 = false;
            $scope.toggleModal2 = function () {
                $scope.showModal2 = !$scope.showModal2;
                $scope.mensaje = "No existe información para el cliente seleccionado.";
            };


            //funcion que realiza la busqueda de configuraciones de parametros de ticktes
            $scope.buscarconfiguraciones = function () {
                //$scope.targetModel = {};
                //$scope.clienteProductoModel.NombreCliente = "";
                cargarConfiguracionesParam();
            }


            function cargarConfiguracionesParam() {
                $scope.listaConfiguraciones = [];
                var configurarParametros = {
                    IdCliente: $scope.targetModel.IdCliente
                }
                requestService.post('tickets',
                    'configuracion',
                    'consultarConfigurarParametros',
                    configurarParametros,
                    true,
                    true,
                    true,
                    function successFunction(response) {
                        $scope.listaConfiguraciones = response;
                        if ($scope.listaConfiguraciones.length === 0) {
                            $scope.showModal2 = !$scope.showModal2;
                            $scope.mensaje = "No existe información para el cliente seleccionado.";
                        }



                    },
                    function errorFunction(response) {},
                    function badRequestFunction(response) {});
            };



            //Guardar un nuevo tipo de tiket
            $scope.guardar = function (descripcion, horasSegundoEscalamiento, tiempoAtencion, tipoId) {
                var accion = '';
                console.log($scope.targetModel);
                if ($scope.targetModel.IdCliente == 0) {
                    $scope.showModal2 = true;
                    $scope.mensaje = "El cliente selecciondado no es correcto.";
                    //alert("El cliente selecciondado no es correcto");
                    return false;
                }
                if (tipoId == null || tipoId === 0) {
                    tiposTicketsClientesModel = {
                        IdCliente: $scope.targetModel.IdCliente,
                        IdPersonaResponsable: $scope.targetModelCero.PersonaId,
                        IdPersonaEscalamiento1: $scope.targetModelUno.PersonaId,
                        IdPersonaEscalamiento2: $scope.targetModelDos.PersonaId,
                        HorasAtencion: tiempoAtencion,
                        HorasSegundoEscalamiento: horasSegundoEscalamiento,
                        TiposTicket: {
                            Descripcion: descripcion,
                            TiempoAtencion: tiempoAtencion
                        }
                    };
                    accion = 'guardarTiposTicketsClientes';
                } else {
                    tiposTicketsClientesModel = {
                        IdCliente: $scope.targetModel.IdCliente,
                        IdPersonaResponsable: $scope.targetModelCero.PersonaId,
                        IdPersonaEscalamiento1: $scope.targetModelUno.PersonaId,
                        IdPersonaEscalamiento2: $scope.targetModelDos.PersonaId,
                        HorasAtencion: tiempoAtencion,
                        HorasSegundoEscalamiento: horasSegundoEscalamiento,
                        TiposTicket: {
                            Descripcion: descripcion,
                            TiempoAtencion: tiempoAtencion
                        },
                        TipoId: tipoId
                    };
                    accion = 'actulizarTiposTicketsClientes';
                }
                requestService.post('tickets',
                    'configuracion',
                    accion,
                    tiposTicketsClientesModel,
                    true,
                    true,
                    true,
                    function successFunction(response) {

                        $scope.showModalConfirm = true;
                        $scope.listaConfiguraciones = response;
                        cargarConfiguracionesParam();
                        limpiarFormulario();

                    },
                    function errorFunction(response) {

                    },
                    function badRequestFunction(response) {
                        limpiarFormulario();
                    });
            }

            function limpiarFormulario() {
                $scope.TipoId = 0;
                $("#responsableUno").val("");
                $("#mailEscalamientoDos").val("");
                $("#mailEscalamientoUno").val("");
                $("#autocomplete2").val("");
                $("#autocomplete3").val("");
                $("#autocomplete4").val("");
                $("#horassegundoescalamiento").val("");
                $("#descrip").val("");
                $("#tiempoAten").val("");
                $("#responsableUno").val("");
                $("#autocompleteResponsable").val("");
                $scope.Descripcion = "";
                $scope.TiempoAtencion = "";
                $scope.PersonaResponsableModel.Nombre = "";
                $scope.HorasSegundoEscalamiento = "";
                $scope.targetModelCero = {
                    PersonaId: 0,
                    MailResponsable: ""
                };
                $scope.targetModelUno = {
                    PersonaId: 0,
                    MailEscalemiento1: ""
                };
                $scope.targetModelDos = {
                    PersonaId: 0,
                    MailEscalamiento2: ""
                };
                $scope.PersonaResponsableModel = {
                    Nombre: ""
                };
                $scope.PersonaEscalamiento1 = {
                    Nombre: ""
                };
                $scope.PersonaEscalamiento2 = {
                    Nombre: ""
                };

            }

            $scope.eliminarConfiguracion = function (tipoId) {
                var options = {
                    message: 'Está seguro de que desea eliminar la información seleccionada?',
                    title: 'Confirmar',
                    className: 'text-info',
                    buttons: {
                        warning: {
                            label: "No",
                            className: "btn-danger",
                            callback: function () {

                            }
                        },
                        success: {
                            label: "Si",
                            className: "btn-success",
                            callback: function () {
                                $scope.listaConfiguraciones = [];
                                tiposTicketsClientesModel = {
                                    TipoId: tipoId
                                };
                                requestService.post('tickets',
                                    'configuracion',
                                    'eliminarTipoTicketsCliente',
                                    tiposTicketsClientesModel,
                                    true,
                                    true,
                                    true,
                                    function successFunction(response) {
                                        //$scope.calendario = response;
                                        cargarConfiguracionesParam();
                                    },
                                    function errorFunction() {},
                                    function badRequestFunction(response) {});
                            }
                        }
                    }
                };
                $ngBootbox.customDialog(options);

            }

            $scope.editarCongifuracion = function (configuracion) {
                $scope.isEditar = true;
                $("#descrip").val(configuracion.Descripcion);

                $scope.Descripcion = configuracion.Descripcion;
                $scope.TiempoAtencion = configuracion.HorasAtencion;
                $scope.TipoId = configuracion.TipoId;
                $scope.targetModelCero = {
                    PersonaId: configuracion.IdPersonaResponsable,
                    MailResponsable: configuracion.Mail
                };
                $scope.PersonaResponsableModel = {
                    Nombre: configuracion.PersonaResponsable
                };

                $scope.targetModelUno = {
                    PersonaId: configuracion.IdPersonaEscalamiento1,
                    MailEscalamiento1: configuracion.MailEscalamiento1
                };
                $scope.PersonaEscalamiento1.Nombre = configuracion.PersonaEscalamiento1;

                $scope.targetModelDos = {
                    PersonaId: configuracion.IdPersonaEscalamiento2,
                    MailEscalamiento2: configuracion.MailEscalamiento2
                };
                $scope.PersonaEscalamiento2.Nombre = configuracion.PersonaEscalamiento2;
                $("#horassegundoescalamiento").val(configuracion.HorasSegundoEscalamiento);
                $scope.HorasSegundoEscalamiento = configuracion.HorasSegundoEscalamiento;
                $('#myModal').modal('show');

                $scope.enabletext = false;
            }

            $scope.enabletext = false;
            $scope.enabletext2 = false;
            $scope.enabletext3 = false;


        }
    ]);

});