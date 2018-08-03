	
	function getUserData(url) {
		$.ajax({
			type : 'GET',
			url : url,
			success : function(data) {
				var userData = data;
				if(userData != undefined && userData != "" && userData.userName != undefined && userData.userName != "") {
					createCookie("userCookie", JSON.stringify(userData),"",true);				
				} 
				updateComponents();
			},
			error: function (xhr, ajaxOptions, thrownError) {
				updateComponents();
			}
		});	
	}
	
	function submitLoginModal(form, url) {
		var rightRailClasses = $(form).parent().parent().parent().parent().attr("class");
		if(form != undefined && rightRailClasses != undefined && rightRailClasses.indexOf('right-rail-login') > -1) {
			createCookie('saml_request_path', url, 1,true);
		} 
		
	}
	
	function updateComponents() {
		updateHeader();
		if(typeof(updateRightRailLogin) == "function") {
			updateRightRailLogin();
		}
		updateArticle();
		updateCHBanner();
		updateBreadcrumbs();
	}
	
	function updateBreadcrumbs() {
		if(!isLoggedIn()) {				
            $("#home").removeClass('hide');
		} else {
			$("#clientHubHome").removeClass('hide');
		}
	}
	
	function updateHeader() {
		if(isLoggedIn()) {

			var str = $("#user-welcome").text();
			str = str.replace("##name##", getUserName());
			$("#user-welcome").text(str);
			$("#loggedIn").removeClass('hide');
		} else {
			$("#loggedOutText").removeClass('hide');
		}
	}
	
	function updateArticle() {
		if(!isLoggedIn()) {
            $("#articleLoggedOutText").removeClass('hide');
		} else {
			$("#articleLoggedInText").removeClass('hide');
		}
	}
	
	function updateCHBanner() {
		if(isLoggedIn()) {
					$("#welcome_user h1").append(
							" " + getUserName());
					$("#companyName")
							.text(getCompany());
                    var href = $("#emailID").attr('href');
                    $("#emailID").attr('href',href + "?"+ "emailID="+getEmail());
					}
			}
	function isLoggedIn() {
		var userCookie = JSON.parse(readCookie("userCookie"));
		if( userCookie == undefined || userCookie == null || userCookie == "") {
			return false;
		} else {
			return true;
		}
	}
	
	function getUserName() {
		if(!isLoggedIn()) {
			return "";
		} else {
			var userCookie = JSON.parse(readCookie("userCookie"));
			return userCookie.userName;
		}
	}
	
	function getCompany() {
		if(!isLoggedIn()) {
			return "";
		} else {
			var userCookie = JSON.parse(readCookie("userCookie"));
			return userCookie.company;
		}
	}
	
	function getEmail() {
		if(!isLoggedIn()) {
			return "";
		} else {
			var userCookie = JSON.parse(readCookie("userCookie"));
			return userCookie.email;
		}
	}
	
	function createCookie(name,value,days,isSecure) {
		isSecure = (typeof isSecure === 'undefined') ? false : isSecure;
		if (days) {
			var date = new Date();
			date.setTime(date.getTime()+(days*24*60*60*1000));
			var expires = "; expires="+date.toGMTString();
		}
		else var expires = "";
		if (isSecure){
			document.cookie = name+"="+value+expires+"; path=/;secure";
		} else {
			document.cookie = name+"="+value+expires+"; path=/";
		}
	}

	function readCookie(name) {
		var nameEQ = name + "=";
		var ca = document.cookie.split(';');
		for(var i=0;i < ca.length;i++) {
			var c = ca[i];
			while (c.charAt(0)==' ') c = c.substring(1,c.length);
			if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length,c.length);
		}
		return null;
	}

	function eraseCookie(name) {
		createCookie(name,"",-1);
	}