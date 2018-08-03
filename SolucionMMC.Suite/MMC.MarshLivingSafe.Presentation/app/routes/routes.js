/**
 * @author <Josue Ramirez Davila>
 * @mail <josueramirezdavila@gmail.com>
 * @description ARCHIVO DONDE SE DEBENDE DEFINIR LOS CONTROLLERS Y TEMPLATES
 * QUE RESOLVERA ANGULAR DE ACUERDO A UNA URL.
 * 
 * Modificacion INDRA FJQP -- Emisión Multiple
 *
 */
define(['modules/app', 'angularAMD'], function (app, angularAMD) {
    return app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
        /* PATHS DONDE ESTAN LOS CONTROLLERS Y LAS VISTAS */
        var PATHS = {
            CONTROLLERS: './controllers/',
            VIEWS: './views/'
        }

        /* SUFIJOS PARA LOS CONTROLLERS Y LAS VISTAS */
        var SUFIJOS = {
            CONTROLLER: "Controller",
            HTML: ".html"
        }

        /* FUNCION QUE ARMA LAS PROPIEDADES CONTROLLER Y TEMPLATE
         * PARA EL $routeProvider
         */
        function routeResolver(jurl, route, cntrl) {
            return {
                url: jurl,
                templateUrl: PATHS.VIEWS + route + '/' + route + SUFIJOS.HTML,
                controller: cntrl + SUFIJOS.CONTROLLER,
                controllerUrl: PATHS.CONTROLLERS + route + '/' + cntrl + SUFIJOS.CONTROLLER
            }
        }
        /* RUTAS DE LA APLICACION */
        $stateProvider
            .state('/', angularAMD.route(routeResolver('/', 'login', 'Login')))
            .state('/blank', angularAMD.route(routeResolver('/blank', 'blank', 'Blank')))
            .state('home', angularAMD.route(routeResolver('/home', 'home', 'Home')))
            .state("configurador", angularAMD.route(routeResolver('/configurador', 'configurador', 'Configurador')))
            .state('cotizadorgen', angularAMD.route(routeResolver('/cotizadorgen', 'cotizadorgen', 'CotizadorGen')))
            .state('cotizadorgen.comparador', angularAMD.route(routeResolver('/comparador/{idSolicitud}', 'benchmark', 'Benchmark')))
            .state('cotizadorgen.cotizador', angularAMD.route(routeResolver('/cotizador/{idSolicitud}', 'cotizador', 'Cotizador')))
            .state('cotizadorgen.emitir', angularAMD.route(routeResolver('/emitir/{idSolicitud},{idCotizacion},{numero}', 'emitir', 'Emitir')))
            .state('registros', angularAMD.route(routeResolver('/registros', 'tickets', 'GestionTickets')))
            .state('reportes', angularAMD.route(routeResolver('/reportes', 'reportes', 'Reportes')))
            .state('calendario', angularAMD.route(routeResolver('/calendario', 'calendario', 'Calendario')))
            .state('configuradorParametroTickets', angularAMD.route(routeResolver('/configuradorParametroTickets', 'configuradorParametroTickets', 'ConfiguradorParametrosTickets')))
            .state('seguimientoTickets', angularAMD.route(routeResolver('/seguimientoTickets/{TicketId},{isCarga}', 'seguimientoTickets', 'SeguimientoTickets')))
            .state('reporteCotizador', angularAMD.route(routeResolver('/reporteCotizador', 'reporteCotizador', 'ReporteCotizador')))
            .state('cotizadorgen.imprimir', angularAMD.route(routeResolver('/imprimir/{policy},{subsection},{endorsement},{idSolicitud},{numero},{esNormal}', 'print', 'Print'))) /* * Modificacion INDRA FJQP -- Emisión Multiple */
            .state("manuales", angularAMD.route(routeResolver('/manuales', 'manuales', 'Manuales')))
            .state("configMultiple", angularAMD.route(routeResolver('/configMultiple', 'configMultiple', 'ConfigMultiple')))
            .state("loginExterno", angularAMD.route(routeResolver('/loginExterno/{tkn}', 'loginExterno', 'LoginExterno')))
            .state("imagenCliente", angularAMD.route(routeResolver('/imagenCliente', 'imagenCliente', 'ImagenCliente')))
        $urlRouterProvider.otherwise('/');
    }]);
});