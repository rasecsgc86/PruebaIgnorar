define(['modules/app'], function(app) {
    
    app.factory('requestInterceptor', function ($sessionStorage, $base64,$rootScope) {
        var requestInterceptor = {
            request: function (config) {
                if ($sessionStorage.tkn) {
                    config.headers["Authorization"] = "Bearer " + JSON.parse($base64.decode($sessionStorage.tkn));
                    config.headers["Content-Type"] = 'application/json; charset=iso-8859-1';
                } else {
                    config.headers["Content-Type"] = 'application/x-www-form-urlencoded; charset=iso-8859-1';
                }
                return config;
            }
        }

        return requestInterceptor;
    }).factory('Excel',function($window){
        var uri = 'data:application/vnd.ms-excel;base64,'
                  , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><head><meta charset="utf-8"><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body><table>{table}</table></body></html>'
                  , base64 = function(s) { return window.btoa(unescape(encodeURIComponent(s))) }
                  , format = function(s, c) { return s.replace(/{(\w+)}/g, function(m, p) { return c[p]; }) }

        return {
            tableToExcel:function(tableId,name){
                var table = $(tableId);
                var ctx = {worksheet: name, table: table.html()};
                var url = uri + base64(format(template, ctx));
                var a = document.createElement('a');
                a.href = url;
                a.download = name+'.xls';
                a.click();  

                return a;
            }
        };
    })
});