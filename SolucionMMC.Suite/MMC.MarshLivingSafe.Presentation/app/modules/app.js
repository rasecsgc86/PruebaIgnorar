/**
 * @author <Josue Ramirez Davila>
 * @mail <josueramirezdavila@gmail.com>
 * @description CONFIGURAMOS EL MODULO APP DE NUESTRA APLICACION EN ANGULAR
 *
 */

/* CARGAMOS LA DEPENDENCIA DE LIBRERIAS */
define(
  [
    'angularAMD',
    'angular-route',
    'angular-resource',
    'ngStorage',
    'jquery-ui',
    'ngAnimate',
    'angularUtils-dirPagination',
    'angularUtils-uiBreadcrumbs',
    'angular-block-ui',
    'angular-ui-notification',
    'angular-recaptcha',
    'angular-bootstrap',
    'angular-locale_es-es',
    'angular-base64',
    'angular-file-upload',
    'ngBootbox',
    'maskMoney'
  ],
  function (angularAMD) {
      var app = angular.module("app",
        [
          'ui.router',
          'ngResource',
          'ngStorage',
          'ngAnimate',
          'angularUtils.directives.dirPagination',
          'angularUtils.directives.uiBreadcrumbs',
          'blockUI',
          'ui-notification',
          'vcRecaptcha',
          'ui.bootstrap',
          'ngLocale',
          'base64',
          'angularFileUpload',
          'ngBootbox'
        ]
      );
      return angularAMD.bootstrap(app);
  }
);
