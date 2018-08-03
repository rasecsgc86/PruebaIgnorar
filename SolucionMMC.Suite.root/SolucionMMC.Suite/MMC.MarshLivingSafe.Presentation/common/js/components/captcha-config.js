if ($("#g-recaptcha").length) {
	var onloadCallback = function() {
		
		grecaptcha.render("g-recaptcha", {
			"sitekey" : $("#siteKey").val(),
			"callback" : captchaSuccess,
			'expired-callback': expCallback
		});
	};
	var captchaSuccess = function(response) {
		$("#captchaCheck").val(Boolean(response)).attr("disabled", true).valid();
		$("#loginButton").removeClass("disabled").prop("disabled",false);
	};
	var expCallback = function(){
		$("#captchaCheck").val(false).attr("disabled", false);
		$("#loginButton").addClass("disabled").prop("disabled",true);
		grecaptcha.reset();
	}
}