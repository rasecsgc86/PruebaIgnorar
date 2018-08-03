// Implied Consent Cookie JS 
function cookieAlert() {
        
    var $cookieBtn = $(".cookie-consent .btn"),
        $container = $(".cookie-consent").show();
    
    $cookieBtn.on('click', function(event){        
        event.preventDefault();    
        $container.hide();
    });    
    
}
cookieAlert();
