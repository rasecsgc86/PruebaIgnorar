// const urlAM = "http://qa.automarshprueba.com.mx/AM45RESTService/api/"
const urlAM = "Inicio.aspx/IniciaSesion";

$(document).ready(function(e){
	$("#loginButton").addClass("disabled").prop("disabled",true);	
	$("#form1Inicio").validate({
		rules:{
			emailAddress:{
				required:true			
			},
			log_password:{
				required:true
			},
			captcha: {
				captchaCheck: true
			}
		},		
		submitHandler: function(element){			
			var capOk = Boolean($("#captchaCheck").val());
			if(capOk){
				if($("#form1Inicio").valid()){					
					IniciaSesion();
				}
			}			
		},
		messages:{
			emailAddress: $("#usernameRequired").data("message") || $.validator.messages.required,
			log_password: $("#passwordRequired").data("message") || $.validator.messages.required,
			captcha: $("#captchaAlert").data("message") || jQuery.validator.messages.required
		},
		errorClass: "error-flag"
	});

});


function IniciaSesion(event) {
	// event.preventDefault();
	const user = $("#emailAddress").val();
	const pass = $("#logPassword").val();

	$("#loginButton").addClass("disabled").prop("disabled",true);

	var variables = {
		"Username": user,
		"Password": pass
	}
	
	$.ajax({
			type: "POST",
			url: urlAM,
			data: JSON.stringify(variables),
			contentType: "application/json; charset=utf-8",
			crossDomain: true,
			dataType: "json",
			success: function(data, status, jqXHR) {				
				var continuar = FnUsuarioLogeado(data);
				FnPintaDatosApi(data,"resultado");
				if (continuar) {window.location = "Pages/Benchmark.aspx"}	//	<-------------------------- 	PARA PRUEBAS ESTÁ COMENTADO
				else{ $("#loginButton").removeClass("disabled").prop("disabled",false);  }
			},
			error: function(xhr, texto, error) {
				// var err = JSON.parse(xhr.responseText);
				alert("Error en la invocación del servicio: ", error)
				console.log("hubo error: ", error, xhr, texto)
			},
			complete: function() {				
			}
		});
	return false;
}

function FnUsuarioLogeado(datos) {
	if(datos.d.Existe){
		sessionStorage.setItem("user", datos.d.IdPersona);
		sessionStorage.setItem("fraileTuk", datos.d.IdPerfil);	
		
		return true;
	}
	return false;
}

function FnPintaDatosApi(datos, idDiv){	
	datos = datos.d
	console.log(datos)
	$("#mensajes").css("display","none");
	$("#mensajes").removeClass(["confirm","alert"])

	const tipo = datos.Existe ? "confirm" : "alert";
	const mensaje = datos.Existe ? datos.Nombre : "datos.Error";

	// const clases = "message " + tipo + " blue";

	$("#mensajes").addClass(tipo);
	$("#mensaje").text(mensaje);
	$("#mensajes").css("display", "block");
}



function FnSalir(){	
	sessionStorage.clear();
	window.location = "homepage.html";
}