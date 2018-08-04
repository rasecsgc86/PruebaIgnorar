/**
 * @author <Josue Ramirez Davila>
 * @mail <josueramirezdavila@gmail.com>
 *
 * SE DEFINE EL SERVICIO QUE EJECUTA
 * LAS PETICIONES AL API-REST
 *
 */

define(['modules/app', 'services/mappingService'],
    function (app) {
        app.service('uploadFilesService',
        [
            'mappingService',
            '$base64',
            'FileUploader',
            '$sessionStorage',
            function (mappingService, $base64, FileUploader, $sessionStorage) {
                var url = '';
                var param = {}
                function instance() {
                    return new FileUploader({
                        url: url,
                        headers : {
                            "Authorization" : "Bearer " + JSON.parse($base64.decode($sessionStorage.tkn))
                        }
                    });
                }

                return {
                    __getInstance: function (urlParam) {
                        url = mappingService.services[urlParam.modulo][urlParam.controller][urlParam.action];
                        return instance();
                    }
                }
            }
        ]);
    });