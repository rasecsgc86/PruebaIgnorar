/**
 * @author      : <Josue Ramirez Davila>
 * @mail        : <josueramirezdavila@gmail.com>
 *
 * @description : Directivas HTML de utilerias.
 *
 */

define(['modules/app'],
    function (app) {

        /* INICIAN CONSTANTES QUE DEFINEN LA ESTRUCTURA COMUN DE LOS ALERTS */
        var HEADER_ALERTS = '<div ng-show=¬¬SHOW_ATTRIBUTE¬¬ class="alert alert-¬¬ALERT_TYPE¬¬ alert-dismissable">';
        var BUTTON_DISMISS_ALERT =
            '<button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">' +
                '&times;</span>' +
                '</button>';

        var BODY_ALERTS = '<div ng-transclude></div>';
        var END_ALERT = '</div>';
        /* FIN CONSTANTES COMUNES DE LOS ALERTS */

        /**
         * @author              : <Josue Ramirez Davila>
         * @mail                : <josueramirezdavila@gmail.com>
         *
         * @description         : CONSTRUYE UN ALERT BOOTSTRAP
         * @param showAttribute : Nombre del modelo con el cual se estara mostrando u ocultando el alert
         * @param alertType     : Tipo de alerta que se quiere construir (danger, warning, info, success)
         * @param isDismiss     : Bandera que define si el alert sera dismissable o no ('true', 'false')
         *
         */
        function buildAlert(showAttribute, alertType, isDismiss) {
            var template = HEADER_ALERTS.replace("¬¬SHOW_ATTRIBUTE¬¬", showAttribute);
            template = template.replace("¬¬ALERT_TYPE¬¬", alertType);
            if (isDismiss == 'true') {
                template += BUTTON_DISMISS_ALERT;
            }
            template += BODY_ALERTS + END_ALERT;
            return template;
        }

        /**
         * @author            : <Josue Ramirez Davila>
         * @mail              : <josueramirezdavila@gmail.com>
         *
         * @description       : CONSTRUYE UN ALERT BOOTSTRAP
         * @attr show         : Nombre del modelo con el cual se estara mostrando u ocultando el alert
         * @attr type         : Tipo de alerta que se quiere construir (danger, warning, info, success)
         * @attr dismissable  : Bandera que define si el alert sera dismissable o no ('true', 'false')
         *
         */
        app.directive("alert",
            function () {
                return {
                    restrict: "E",
                    transclude: true,
                    template: function (element, attr) {
                        return buildAlert(attr.show, attr.type, attr.dismissable);
                    }
                };
            });

        /**
         * @author            : <Josue Ramirez Davila>
         * @mail              : <josueramirezdavila@gmail.com>
         *
         * @description       : CONSTRUYE UN ALERT BOOTSTRAP TIPO DANGER
         * @attr show         : Nombre del modelo con el cual se estara mostrando u ocultando el alert
         * @attr dismissable  : Bandera que define si el alert sera dismissable o no ('true', 'false')
         *
         */
        app.directive("alertDanger",
            function () {
                return {
                    restrict: "E",
                    transclude: true,
                    template: function (element, attr) {
                        return buildAlert(attr.show, "danger", attr.dismissable);
                    }
                };
            });

        /**
         * @author            : <Josue Ramirez Davila>
         * @mail              : <josueramirezdavila@gmail.com>
         *
         * @description       : CONSTRUYE UN ALERT BOOTSTRAP TIPO WARNING
         * @attr show         : Nombre del modelo con el cual se estara mostrando u ocultando el alert
         * @attr dismissable  : Bandera que define si el alert sera dismissable o no ('true', 'false')
         *
         */
        app.directive("alertWarning",
            function () {
                return {
                    restrict: "E",
                    transclude: true,
                    template: function (element, attr) {
                        return buildAlert(attr.show, "warning", attr.dismissable);
                    }
                };
            });

        /**
         * @author            : <Josue Ramirez Davila>
         * @mail              : <josueramirezdavila@gmail.com>
         *
         * @description       : CONSTRUYE UN ALERT BOOTSTRAP TIPO INFO
         * @attr show         : Nombre del modelo con el cual se estara mostrando u ocultando el alert
         * @attr dismissable  : Bandera que define si el alert sera dismissable o no ('true', 'false')
         *
         */
        app.directive("alertInfo",
            function () {
                return {
                    restrict: "E",
                    transclude: true,
                    template: function (element, attr) {
                        return buildAlert(attr.show, "info", attr.dismissable);
                    }
                };
            });

        /**
         * @author            : <Josue Ramirez Davila>
         * @mail              : <josueramirezdavila@gmail.com>
         *
         * @description       : CONSTRUYE UN ALERT BOOTSTRAP TIPO SUCCESS
         * @attr show         : Nombre del modelo con el cual se estara mostrando u ocultando el alert
         * @attr dismissable  : Bandera que define si el alert sera dismissable o no ('true', 'false')
         *
         */
        app.directive("alertSuccess",
            function () {
                return {
                    restrict: "E",
                    transclude: true,
                    template: function (element, attr) {
                        return buildAlert(attr.show, "success", attr.dismissable);
                    }
                };
            });

        /**
         *
         * @description   : CONSTRUYE UN MODAL BOOTSTRAP
         * @attr title    : Titulo del modal
         * @attr visible  : Bandera que define si el modal se muestra o no ('true', 'false')
         * @ettr widthModal : Tamaño de la ventana modal 80% (Info)
         * @attr modalHeadType : Tipo de encabezado que se quiere construir (danger, warning, info, success, primary, default, infoMarsh) 
         */
        app.directive('modal',
            function () {
                return {
                    template: '<div class="modal fade" data-backdrop="static" data-keyboard="false">' +
                        '<div class="modal-dialog widthModal-{{ widthmodalsize }}">' +
                        '<div class="modal-content">' +
                        '<div class="modal-header modalHead-{{ modalheadtype }}">' +
                        '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
                        '<h4 class="modal-title">{{ title }}</h4>' +
                        '</div>' +
                        '<div class="modal-body" ng-transclude></div>' +
                        '</div>' +
                        '</div>' +
                        '</div>',
                    restrict: 'E',
                    transclude: true,
                    replace: true,
                    scope: true,
                    link: function postLink(scope, element, attrs) {
                        scope.title = attrs.title;
                        scope.widthmodalsize = attrs.widthmodalsize;
                        scope.modalheadtype = attrs.modalheadtype;


                        scope.$watch(attrs.visible,
                            function (value) {
                                if (value == true)
                                    $(element).modal('show');
                                else
                                    $(element).modal('hide');
                            });

                        $(element).on('shown.bs.modal',
                            function () {
                                scope.$apply(function () {
                                    scope.$parent[attrs.visible] = true;
                                });
                            });

                        $(element).on('hidden.bs.modal',
                            function () {
                                scope.$apply(function () {
                                    scope.$parent[attrs.visible] = false;
                                });
                            });
                    }
                };
            });

        /**
         * @author          : <Josue Ramirez Davila>
         * @mail            : <josueramirezdavila@gmail.com>
         *
         * @description     : CONSTRUYE UN PANEL BOOTSTRAP
         * @attr title      : Titulo del panel
         * @attr type       : Tipo de panel que se quiere construir (danger, warning, info, success, primary, default)
         * @attr footer     : Titulo del pie del panel
         *
         */
        app.directive('panel',
            function () {
                return {
                    restrict: 'E',
                    transclude: true,
                    template: function (element, attr) {
                        var template = '<div class="panel panel-' +
                            attr.type +
                            '">' +
                            '<div class="panel-heading"> <h3 class="panel-title">' +
                            attr.title +
                            '</h3> </div>' +
                            '<div class="panel-body" ng-transclude></div>';
                        if (attr.footer != undefined && attr.footer != '') {
                            template += '<div class="panel-footer">' + attr.footer + '</div>'
                        }
                        template += '</div>';
                        return template;
                    }
                }
            });

        /**
         * @author          : <Josue Ramirez Davila>
         * @mail            : <josueramirezdavila@gmail.com>
         *
         * @description     : CONSTRUYE UN BS-CALLOUT BOOTSTRAP
         * @attr type       : Tipo de callout que se quiere construir (danger, warning, info, success, primary, default)
         *
         */
        app.directive('callout',
            function () {
                return {
                    restrict: 'E',
                    transclude: true,
                    template: function (element, attr) {
                        return '<div class="bs-callout bs-callout-' + attr.type + '"><div ng-transclude></div></div>';
                    }
                }
            });

        /************************************************************************************************
         * @author            : <Irving Nava Aguilera>                                                  *
         * @mail              : <IrvingVisor@Hotmail.com>                                               *
         * @description       : Valida los caracteres, Limita longitud y solo muestra los numericos     *
         * @attr max          : Nombre del modelo con el cual limitara longitud  maxima                 *
         * @attr $observe     : Función que valida caracteres y solo deja ingresar numeros              *
         ************************************************************************************************/
        app.directive('numbersOnly',
            function () {
                return {
                    restrict: 'A',
                    require: 'ngModel',
                    link: function (scope, element, attrs, ctrl) {
                        var validateNumber = function (inputValue) {

                            if (attrs.max) {
                                maxLength = attrs.max;
                            }
                            if (inputValue === undefined) {
                                return '';
                            }
                            var transformedInput = inputValue.replace(/[^0-9]/g, '');
                            if (transformedInput !== inputValue) {
                                ctrl.$setViewValue(transformedInput);
                                ctrl.$render();
                            }
                            if (transformedInput.length > maxLength) {
                                transformedInput = transformedInput.substring(0, maxLength);
                                ctrl.$setViewValue(transformedInput);
                                ctrl.$render();
                            }
                            var isNotEmpty = (transformedInput.length === 0) ? true : false;
                            ctrl.$setValidity('notEmpty', isNotEmpty);
                            return transformedInput;
                        };

                        ctrl.$parsers.unshift(validateNumber);
                        ctrl.$parsers.push(validateNumber);
                        attrs.$observe('notEmpty',
                            function () {
                                validateNumber(ctrl.$viewValue);
                            });
                    }
                };
            });


        /**
         * @author          : <Israel Borja Ruiz>
         * @mail            : <israeelbr14@gmail.com>
         * @description     : Valida Email a traves de expresion regular
         */
        app.directive('validateEmail',
            function () {
                var EMAIL_REGEXP = /^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$/;
                return {
                    link: function (scope, elm) {
                        elm.on("keyup",
                            function () {
                                var isMatchRegex = EMAIL_REGEXP.test(elm.val());
                                if (isMatchRegex && elm.hasClass('warning') || elm.val() == '') {
                                    elm.removeClass('warning');
                                } else if (isMatchRegex == false && !elm.hasClass('warning')) {
                                    elm.addClass('warning');
                                }
                            });
                    }
                }
            });


        /**
         * @author          : <Israel Borja Ruiz>
         * @mail            : <israeelbr14@gmail.com>
         * @description     : Valida RFC a traves de expresion regular
         */
        app.directive('validateRfc',
            function () {
                var RFC_REGEXP = /^[a-zA-Z]{3,4}(\d{6})((\D|\d){3})?$/;
                return {
                    link: function (scope, elm) {
                        elm.on("keyup",
                            function () {
                                var isMatchRegex = RFC_REGEXP.test(elm.val());
                                if (isMatchRegex && elm.hasClass('warning') || elm.val() == '') {
                                    elm.removeClass('warning');
                                } else if (isMatchRegex == false && !elm.hasClass('warning')) {
                                    elm.addClass('warning');
                                }
                            });
                    }
                }
            });


        /**
         * @author          : <Israel Borja Ruiz>
         * @mail            : <israeelbr14@gmail.com>
         * @description     : Valida el CURP a traves de expresion regular
         */
        app.directive('validateCurp',
            function () {
                var ESP_CHAR_REGEXP = /^[a-zA-Z]{4}\d{6}[a-zA-Z]{6}\d{2}$/;

                function updateTooltip() {
                    $rootScope.tooltip = true;
                }

                function updateTooltipF() {
                    $rootScope.tooltip = false;
                }

                return {
                    link: function (scope, elm) {
                        elm.on("keyup",
                            function () {
                                var isMatchRegex = ESP_CHAR_REGEXP.test(elm.val());
                                if (isMatchRegex && elm.hasClass('warning') || elm.val() == '') {
                                    elm.removeClass('warning');
                                } else if (isMatchRegex == false && !elm.hasClass('warning')) {
                                    elm.addClass('warning');
                                }
                            });
                    }
                }
            });

        /**************************************************************************
         * ********************************************************************** *
         * * @author        : <Irving Nava Aguilera>                            * *
         * * @mail          : <IrvingVisor@Hotmail.com>                         * *
         * * @Descripción   : Funcion que permite validar maximo de caracteres  * *
         * *                  permitidos en un input, ya que la propiedad del   * *
         * *                  HTML5 no la soporta FireFox                       * *
         * ********************************************************************** *
         **************************************************************************/
        app.directive('limiteMaximo',
            function () {
                var txts = document.getElementsByTagName('Length');

                for (var i = 0, l = txts.length; i < l; i++) {
                    if (/^[0-9]+$/.test(txts[i].getAttribute("maxLength"))) {
                        var func = function () {
                            var len = parseInt(this.getAttribute("maxLength"), 10);

                            if (this.value.length > len) {
                                alert('Maximum length exceeded: ' + len);
                                this.value = this.value.substr(0, len);
                                return false;
                            }
                        }

                        txts[i].onkeyup = func;
                        txts[i].onblur = func;
                    }
                };
            });


        /**
         * @author          : <Israel Borja Ruiz>
         * @mail            : <israeelbr14@gmail.com>
         * @description     : Permite formatear el input en tipo moneda
         */
        app.directive('maskMoney', function ($timeout, $locale) {
            return {
                restrict: 'A',
                require: 'ngModel',
                scope: {
                    model: '=ngModel',
                    mmOptions: '=?',
                    prefix: '@',
                    suffix: '@',
                    affixesStay: '=',
                    thousands: '@',
                    decimal: '@',
                    precision: '=',
                    allowZero: '=',
                    allowNegative: '='
                },
                link: function (scope, el, attr, ctrl) {

                    scope.$watch(checkOptions, init, true);

                    scope.$watch(attr.ngModel, eventHandler, true);
                    //el.on('keyup', eventHandler); //change to $watch or $observe

                    function checkOptions() {
                        return scope.mmOptions;
                    }

                    function checkModel() {
                        return scope.model;
                    }

                    //this parser will unformat the string for the model behid the scenes
                    function parser() {
                        return $(el).maskMoney('unmasked')[0];
                    }

                    ctrl.$parsers.push(parser);

                    ctrl.$formatters.push(function (value) {
                        $timeout(function () {
                            init();
                        });
                        return parseFloat(value).toFixed(2);
                    });

                    function eventHandler() {
                        $timeout(function () {
                            scope.$apply(function () {
                                ctrl.$setViewValue($(el).val());
                            });
                        });
                    }

                    function init(options) {
                        $timeout(function () {
                            elOptions = {
                                prefix: scope.prefix || '$',
                                suffix: scope.suffix || '',
                                affixesStay: scope.affixesStay,
                                //thousands: scope.thousands || $locale.NUMBER_FORMATS.GROUP_SEP,
                                //decimal: scope.decimal || $locale.NUMBER_FORMATS.DECIMAL_SEP,
                                thousands: scope.thousands || ',',
                                decimal: scope.decimal || '.',
                                precision: scope.precision,
                                allowZero: scope.allowZero,
                                allowNegative: scope.allowNegative
                            };

                            if (!scope.mmOptions) {
                                scope.mmOptions = {};
                            }

                            for (var elOption in elOptions) {
                                if (elOptions[elOption]) {
                                    scope.mmOptions[elOption] = elOptions[elOption];
                                }
                            }

                            $(el).maskMoney(scope.mmOptions);
                            $(el).maskMoney('mask');
                            eventHandler();

                        }, 0);

                        $timeout(function () {
                            scope.$apply(function () {
                                ctrl.$setViewValue($(el).val());
                            });
                        });
                    }
                }
            }
        });

        /**
         * @author          : <Josue Ramirez Davila>
         * @mail            : <josueramirezdavila@gmail.com>
         * @description     : Inicializa la funcion autocomplete de jquery en un input text
         */
        app.directive('autocomplete',
        [
            '$base64', '$sessionStorage', 'mappingService', function ($base64, $sessionStorage, mappingService) {
                return {
                    restrict: 'A',
                    require: 'ngModel',
                    link: function postLink(scope, element, attrs) {
                        var url = JSON.parse($base64.decode(attrs.urlautocomplete));

                        function setToken(params) {
                            params = JSON.parse(params);
                            params['Tkn'] = JSON.parse($base64.decode($sessionStorage.tkn));
                            params = JSON.stringify(params);
                            return params;
                        }

                        $('#' + attrs.id)
                            .autocomplete({
                                source: function (request, response) {
                                    $.ajax({
                                        url: mappingService.services[url[0]][url[1]][url[2]],
                                        method: "POST",
                                        data: setToken(JSON.parse(JSON.stringify(attrs.params))),
                                        headers: {
                                            "Authorization": "Bearer " + $base64.decode($sessionStorage.tkn),
                                            "Content-Type": 'application/json'
                                        },
                                        type: "jsonp"
                                    })
                                        .done(function (data) {
                                            /* VERIFICAMOS SI LA RESPUESTA ES DE TIPO SingleResponse */
                                            if (attrs.issingleresponse) {
                                                /* VERIFICAMOS EL CODIGO DE ERROR REGRASDO POR EL REST-CONTROLLER
                                                 * EN EL OBJETO SingleResponse<T> */
                                                if (data.IsOk) {
                                                    /* SI EL CODIGO DE ERROR DEL REST-CONTROLLER
                                                     * ES EXITOSO, SI ES ASI EXECUTAMOS LA FUNCION SUCCES RECIBIDA. */
                                                    response($.map(data.Response,
                                                        function (item) {
                                                            item.label = item[attrs.itemlabel];
                                                            item.value = item[attrs.itemvalue];
                                                            return item;
                                                        }));
                                                }
                                            } else {
                                                response($.map(data.Response,
                                                    function (item) {
                                                        item.label = item[attrs.itemlabel];
                                                        item.value = item[attrs.itemvalue];
                                                        return item;
                                                    }));
                                            }
                                        });
                                },
                                minLength: attrs.minlength,
                                select: function (event, ui) {
                                    if (attrs.targetmodel) {
                                        scope.$apply(function () {
                                            scope[attrs.targetmodel] = ui.item;
                                        });
                                    }
                                    if (attrs.selectedfunction) {
                                        scope[attrs.selectedfunction]();
                                    }
                                },
                                search: function (ul, item) {
                                    $('#' + attrs.id).addClass('ui-autocomplete-loading');
                                },
                                open: function () {
                                    $('#' + attrs.id).removeClass('ui-autocomplete-loading');
                                },
                                change: function () {

                                }
                            });
                    }
                };
            }
        ]);

        app.directive('ngFiles',
        [
            '$parse',
            function ($parse) {

                function fnLink(scope, element, attrs) {
                    var onChange = $parse(attrs.ngFiles);
                    element.on('change',
                        function (event) {
                            onChange(scope,
                            {
                                $files: event.target.files
                            });
                        });
                };

                return {
                    link: fnLink
                }
            }
        ]);
    });