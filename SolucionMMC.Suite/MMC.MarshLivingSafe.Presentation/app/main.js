/**
 * @author      : <Josue Ramirez Davila>
 * @mail        : <josueramirezdavila@gmail.com>
 *
 * @description : ARCHIVO DE CONFIGURACION PRINCIPAL
 *                PARA LA APLICACION EN ANGULAR
 *
 */

/* CONFUIGRAMOS LAS DEPENDENCIAS DE ANGULAR */
require.config({
    baseUrl: "./app",
    paths: {
        'angular': '../bower_components/angular/angular.min',
        'angular-route': '../bower_components/angular-route/angular-route.min',
        'angularAMD': '../bower_components/angularAMD/angularAMD.min',
        'angular-resource': '../bower_components/angular-resource/angular-resource.min',
        'jquery': '../bower_components/jquery/dist/jquery.min',
        'jquery-ui': '../bower_components/jquery-ui/jquery-ui.min',
        'ngStorage': '../bower_components/ngstorage/ngStorage.min',
        'ngAnimate': '../bower_components/angular-animate/angular-animate.min',
        'angularUtils-dirPagination': '../bower_components/angularUtils-pagination/dirPagination',
        'angularUtils-uiBreadcrumbs': '../bower_components/angular-utils-ui-breadcrumbs/uiBreadcrumbs',
        'bootstrap': '../bower_components/bootstrap/dist/js/bootstrap.min',
        'angular-bootstrap': '../bower_components/angular-bootstrap/ui-bootstrap-tpls',
        'angular-ui-routes': '../bower_components/angular-ui-router/release/angular-ui-router.min',
        'angular-block-ui': '../bower_components/angular-block-ui/dist/angular-block-ui.min',
        'angular-ui-notification': '../bower_components/angular-ui-notification/dist/angular-ui-notification.min',
        'angular-recaptcha': '../bower_components/angular-recaptcha/release/angular-recaptcha.min',
        'angular-locale_es-es': '../bower_components/angular-i18n/angular-locale_es-es',
        'angular-base64': '../bower_components/angular-base64/angular-base64.min',
        'imgAseguradoras': '../assets/resource/aseguradorasImg',
        'listaMenus': '../assets/resource/listaMenus',
        'angular-file-upload': '../bower_components/angular-file-upload/dist/angular-file-upload.min',
        'bootbox': '../bower_components/bootbox/bootbox',
        'ngBootbox': '../bower_components/ngBootbox/dist/ngBootbox.min',
        'maskMoney': '../bower_components/jquery-maskMoney/jquery-maskMoney.min'
    },
    shim: {
        'angularAMD': ['angular'],
        'angular-locale_es-es': {
            'exports': 'angular-locale_es-es',
            'deps': ['angular']
        },
        'angular-route': ['angular'],
        'angular': {
            'exports': 'angular'
        },
        'angular-resource': {
            'deps': ['angular']
        },
        'jquery': {
            'exports': 'jquery'
        },
        'jquery-ui': {
            'exports': 'jquery-ui',
            'deps': ['jquery']
        },
        'ngStorage': {
            'deps': ['angular']
        },
        'ngAnimate': {
            'exports': 'ngAnimate',
            'deps': ['angular']
        },
        'angular-ui-routes': {
            'deps': ['angular']
        },
        'angularUtils-dirPagination': {
            'deps': ['angular']
        },
        'angularUtils-uiBreadcrumbs': {
            'deps': ['angular-ui-routes']
        },
        'bootstrap': {
            'exports': 'bootstrap',
            'deps': ['jquery']
        },
        'angular-bootstrap': {
            'exports': 'angular-bootstrap',
            'deps': ['angular', 'bootstrap', 'angular-locale_es-es']
        },
        'angular-block-ui': {
            'exports': 'angular-block-ui',
            'deps': ['angular']
        },
        'angular-ui-notification': {
            'exports': 'angular-ui-notification',
            'deps': ['angular']
        },
        'angular-recaptcha': {
            'exports': 'angular-recaptcha',
            'deps': ['angular', 'jquery']
        },
        'angular-base64': {
            'exports': 'angular-base64',
            'deps': ['angular']
        },
        'angular-file-upload': {
            'exports': 'angular-file-upload',
            'deps': ['angular']
        },
        'bootbox': {
            'exports': 'bootbox',
            'deps': ['jquery-ui']
        },
        'ngBootbox': {
            'exports': 'ngBootbox',
            'deps': ['angular', 'bootbox']
        },
        'maskMoney': {
            'exports': 'maskMoney',
            'deps': ['jquery']
        }
    },
    deps: [
      'modules/app',
      'factories/utilsFactories',
      'routes/routes',
      'config/appConfig',
      'config/appRun',
      'jquery',
      'jquery-ui',
      'angular-bootstrap',
      'services/requestService',
      'directives/utilsDirectives',
      'ngStorage',
      'ngAnimate',
      'angularUtils-dirPagination',
      'angularUtils-uiBreadcrumbs',
      'routes/breadCrumbs',
      'angular-block-ui',
      'angular-ui-notification',
      'angular-recaptcha',
      'controllers/index/IndexController',
      'angular-locale_es-es',
      'angular-base64',
      'imgAseguradoras',
      'listaMenus',
      'angular-file-upload',
      'bootbox',
      'ngBootbox',
      'maskMoney'
    ]
});
