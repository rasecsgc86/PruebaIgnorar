var SW = new BwGateway({
	// Quitar o establecer a false cuando pase a produccion
	sandbox: true,
	// Nombre de usuario de Banwire
	user: 'pruebasbw',
	// Titulo de la entana
	title: "Segurísimo",
	// Referencia
	reference: 'testref01',
	// Concepto
	concept: 'Pago de Póliza',
	// Opcional: Moneda
	currency: 'MXN',
	// Opcional: Tipo de cambio definido por el comercio (En caso de seleccionar una moneda que requiera mostrar el tipo de cambio a MXN. Solo informativo). Ejemplo: 15.00
	exchangeRate: '',
	// Total de la compra
	total: "1750",
	// Opcional: Meses sin intereses
	months: [3,6,9,12],
	// Arreglo con los items de compra
	items: [
		{
			name: "Pago de Prime",
			qty: 1,
			desc: "Esta es la cooperacion correspondiente",
			unitPrice: 25
		},
		{
			name: "Pago de Leo",
			qty: 1,
			desc: "Esta es la cooperacion correspondiente",
			unitPrice: 25
		}
	],
	cust: {
		fname: "Francisco", //Nombre del comprador
		mname: "Valencia", //Apellido paterno del comprador
		lname: "Gil", //Apeliido materno del comprador
		email: "francisco.valencia@marsh.com", //Email del comprador
		phone: "59994400", //Número telefónico del comprador
		addr: "Paseo de la Reforma 505", //Dirección del comprador (calle y número)
		city: "Mexico", //Ciudad del comprador
		state: "DF", //Estado del comprador (2 dígitos de acuerdo al formato ISO)
		country: "MEX", //País del comprador (3 dígitos de acuerdo al formato ISO)
		zip: "14145" //Código de postal del comprador
	},
	ship: {
		addr: "Paseo de la Reforma 505", //Dirección de envío
		city: "Mexico", //Ciudad de envío
		state: "DF", //Estado de envío (2 dígitos de acuerdo al formato ISO)
		country: "MEX", //País de envío (3 dígitos de acuerdo al formato ISO)
		zip: "14145" //Código de postal del envío
	},
	// Opciones de pago, por defecto es "all". Puede incluir varias opciones separadas por comas
	paymentOptions: 'visa,mastercard', // visa,mastercard,amex,oxxo,speifast,all
	// Mostrar o no pagina de resumen de compra
	reviewOrder: true,
	// Mostrar o no mostrar los campos de envio
	showShipping: true,
	// Solamente para pagos recurrentes o suscripciones
	recurring: {
		// Cada cuanto se ejecutará el pago "month","year" o un entero representando numero de días
		interval: "month",
		// Opcional: Limitar el número de pagos (si no se pone entonces no tendrá limite)
		limit: 10, 
		// Opcional: Fecha del primer cargo (en caso de no especificar se ejecutará de inmediato)
		start: "2014-01-01", // Formaro YYYY-MM-DD
		// Opcional: En caso de que los pagos subsecuentes (después del primero)
		// tengan un monto distinto al inicial
		total: "50.00"
	},
	// URL donde se van a enviar todas las notificaciones por HTTP POST de manera asoncrónica
	notifyUrl: 'https://qa.automarshprueba.com.mx/MMCSuite/Pages/RecibeBanwire.aspx',
	// Handler en caso de exito en el pago
	successPage: 'https://qa.automarshprueba.com.mx/MMCSuite/Pages/Contrata.aspx',
	onSuccess: function(data){
	    alert("¡Gracias por tu pago! - " + reference);
	},
	// Pago pendiente OXXO
	pendingPage: 'https://qa.automarshprueba.com.mx/MMCSuite/archivos/CERTIFICADO2.pdf',
	onPending: function(data){
		alert("El pago está pendiente por ser efectuado");
	},
	// Pago challenge
	// challengePage: 'http://challenge.com',
	challengePage: 'https://qa.automarshprueba.com.mx/MMCSuite/archivos/CERTIFICADO2.pdf',
	onChallenge: function(data){
		console.log("Pago enviado a valida ciones de seguridad");
	},
	// Handler en caso de error en el pago
	errorPage: 'https://qa.automarshprueba.com.mx/MMCSuite/#',
	onError: function(data){
		console.log("Error en el pago");
	},
	// Cuando cierra el popup sin completar el proceso
	onCancel: function(data){
	    console.log("Se cancelo el proceso");
	    alert("Se cancelo el proceso");
	}
});

function pagar(urlSuite, ref) {
	const prefix = "ContentPlaceHolder1_";
	const nombre = $("#txtNombre").val();
	const paterno = $("#txtPaterno").val();
	const materno = $("#txtMaterno").val();
	const correo = $("#txtMail").val();
	const telefono = $("#txtTelefono").val();
	const calle = $("#txtCalle").val();
	const numero = $("#txtNumero").val();
	const cp = $("#txtCP").val();
	const colonia = $("#txtColonia").val();
	const municipio = $("#txtDelegacion").val();
	const precio = $("#txtPrecioPlan").val();
	const referencia = ref;
	const urlSuccessPage = urlSuite + '/Pages/Contrata.aspx?CertificadoID=' + referencia;
	const urlNotify = urlSuite + '/Pages/RecibeBanwire.aspx?CertificadoID=' + referencia;

    // REM LCEH I
	const intervalo = $("#txtIntervalo").val();
	const limite = $("#txtLimite").val();
	const empezar = $("#txtStart").val();
	const totalAPagar = $("#txtTotal").val();
    // REM LCEH F
	const variables = {	    
		cust: {
		    fname: nombre, //Nombre del comprador
		    mname: paterno, //Apellido paterno del comprador
		    lname: materno, //Apeliido materno del comprador
		    email: correo, //Email del comprador
		    phone: telefono, //Número telefónico del comprador
		    addr: calle + ' ' + numero, //Dirección del comprador (calle y número)
		    city: colonia, //Ciudad del comprador
		    state: municipio, //Estado del comprador (2 dígitos de acuerdo al formato ISO)
		    country: "MEX", //País del comprador (3 dígitos de acuerdo al formato ISO)
		    zip: cp //Código de postal del comprador
		},
		recurring: {
		    // Cada cuanto se ejecutará el pago "month","year" o un entero representando numero de días
		    interval: intervalo,
		    // Opcional: Limitar el número de pagos (si no se pone entonces no tendrá limite)
		    limit: limite,
		    // Opcional: Fecha del primer cargo (en caso de no especificar se ejecutará de inmediato)
		    start: empezar, //"2014-01-01", // Formaro YYYY-MM-DD
		    // Opcional: En caso de que los pagos subsecuentes (después del primero)
		    // tengan un monto distinto al inicial
		    total: totalAPagar //"50.00"
		},
		items: [
			{
				name: "Asistencia Integral Iké",
				qty: 1,
				desc: "Pago de su Póliza de seguro",
				unitPrice: precio
			}
		],
		total: precio,
		reference: referencia,
		notifyUrl: urlNotify,
		successPage: urlSuccessPage,
		pendingPage: urlSuccessPage,
		challengePage: urlSuccessPage,
		errorPage: urlSuccessPage
	};

	SW.pay(variables);
}

function abrirCertificado(urlSuite, cert) {
    var urlCert = urlSuite + 'Certificados/Ike/WefmCertificadoIke.aspx?CertificadoID=' + cert;
    window.open(urlCert);
    window.location.href = urlSuite + 'Pages/Benchmark.aspx';
}
