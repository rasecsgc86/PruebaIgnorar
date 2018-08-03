// Foundation JavaScript
// Documentation can be found at: http://foundation.zurb.com/docs
//Set all custom form error messages by extending the messages object
//TODO: Verify that this can be removed. Most of this is getting handled in the validation initializer
/*$.extend(jQuery.validator.messages, {
	required: $("#requiredAlert").data("message") || jQuery.validator.messages.required,
	email: $("#emailAlert").data("message") || jQuery.validator.messages.email,
	equalTo: $("#equalToAlert").data("message") || jQuery.validator.messages.equalTo
});*/
$(document).ready(function(e) {
	// $(window).trigger('setup');
	// datepicker on search results template
	$('#datepicker1').datetimepicker({
		yearOffset:0,
		//startDate:false,
		lang:$("html").attr("lang"),
		todayButton: false,	
		closeOnDateSelect:true,
		timepicker:false,
		format:'m/d/Y',
		formatDate:'m/d/Y'        
	});
	$('#datepicker2').datetimepicker({
		yearOffset:0,
		//startDate:false,
		lang:$("html").attr("lang"),
		todayButton: false,	
		closeOnDateSelect:true,
		timepicker:false,
		format:'m/d/Y',
		formatDate:'m/d/Y'
	});
	//TODO: check about registering slick.js for 'reflow'
	//$(document).foundation('interchange', 'reflow');
	$(document).foundation();
	$(document).foundation('interchange', 'reflow');
	var $aside = $('aside.realign');
	var $sFilter = $(".search-filter");
//	var masonFlag = false; 
	globalDrop.init();
	//searchHide($sFilter);
	dotInits();
	//infoAccordion();
	$(".office-locator").officeLocator();
	$(".attendees-list").popper();
	$(".global-location-list").popper({
		modalType: "lazyimages"
	});
	$(".video-player").popper({
		modalType: "iframe"
	});    
	$("#credentials").popper({
		customClone: ".login-box-modal",
        altClass: "alt-modal",
        openCallback: function(){
        	$("#modalWindow #logInModal").validate({
        		showErrors: function( error, element ) {
        			return true;
        		},
        		rules: {
        			username: {
        				required: true,
        				validateDynUser: true
        			},
        			log_password: {
        				required: true
        			}	
        		},
        		onfocusout: function(element) {
        			var $submit = $("#loginButton");
        			if($("#logInModal").valid()){
        				$submit.removeClass("disabled");
        			} else {
        				$submit.addClass("disabled");
        			}
        		},
        		onkeyup: function() {
        			_toggleSubmit(validUser, "#modalWindow #loginButton", "#logPassword");
        		}
        	});
        }
    });
	$(".login-box").popper({
         customClone: ".login-box-modal",
         altClass: "alt-modal right-rail-login",
         openCallback: function(){
         $("#modalWindow #logInModal").validate({
                showErrors: function( error, element ) {
                       return true;
                },
                rules: {
                       username: {
                             required: true,
                             validateDynUser: true
                       },
                       log_password: {
                             required: true
                       }     
                },
                onfocusout: function(element) {
        			var $submit = $("#loginButton");
        			if($("#logInModal").valid()){
        				$submit.removeClass("disabled");
        			} else {
        				$submit.addClass("disabled");
        			}
        		},
        		onkeyup: function() {
        			_toggleSubmit(validUser, "#modalWindow #loginButton", "#logPassword");
        		}
            });
        }
    });
        
//	if($(".tagboard-embed").length) {
//		masonFlag = 1;
//	}
//	if(window.innerWidth <= 1024 && window.innerWidth >= 641 && masonFlag === false) {
//		$aside.masonry({
//			itemSelector: "aside > div"
//		});
//		masonFlag = true;
//	}
	var $userValue;
	var validUser;
	jQuery.validator.addMethod("validateDynUser", function(value, element) {
		var currentUser = $("#emailAddress").val();
		if ($userValue !== currentUser) {
			var $submit = $("#loginButton");	
			$submit.addClass("disabled").prop("disabled", true);
			validUser = null;
			$.ajax({
				url: "/bin/UserIdValidation",
				//type: "GET",
                type: "POST",
				dataType: "json",
				//data: $(element).serialize()
                data: {"a": currentUser, "path": $("#contentPath").val()}
			})
			.done(function(data) {
				if (data.isVerified === false && data.isVerified !== null) {
					if(data.url) {
						window.location.replace(data.url);
					}
					validUser = false;
					return false;
				}
			})
			.fail(function (data) {
		        console.log(data);
			})
			.always(function(){
				validUser = true;
				$submit.prop("disabled", false);
				_toggleSubmit(validUser, "#modalWindow #loginButton", "#logPassword");
			});
			$userValue = currentUser;
			
		} 
		return validUser;
	});
	jQuery.validator.addMethod("validateUser", function(value, element) {
		var currentUser = $("#username").val();
		if ($userValue !== currentUser) {
			var $submit = $("#credentials .main-submit");
			$submit.addClass("disabled").prop("disabled", true);
			validUser = null;
			$.ajax({
				url: "/bin/UserIdValidation",
				//type: "GET",
                type: "POST",
				dataType: "json",
				//data: $(element).serialize()
                data: {"a": currentUser, "path": $("#contentPath").val()}                
			})
			.done(function(data) {
				if (data.isVerified === false && data.isVerified !== null) {
					if(data.url) {
						window.location.replace(data.url);
					}
					validUser = false;
					return false;
				}
			})
			.fail(function (data) {
		        console.log(data);
			})
			.always(function(){
				validUser = true;
				$submit.prop("disabled", false);
				_toggleSubmit(validUser, "#credentials .main-submit", "#password");
			});
			$userValue = currentUser;
			
		} 
		return validUser;
	});

	var email = null;
	var validFlag = true;
	jQuery.validator.addMethod("validateJSON", function(value, element) {
		if (email !== $("#regEmailAddress").val() || email === null) {
				validFlag = true;
				$('#registerButton').attr('disabled', 'disabled');
				email = $("#regEmailAddress").val();
				$.ajax({
					url: "/bin/UserIdValidation",
					type: "POST",
					dataType: "json",
					data: $(element).serialize() + "&" + "path=" + $("#contentPath").val(),
				})
				.done(function(data) {
					if( data.black_listed ){
						jQuery.validator.messages.validateJSON = $("#blacklistAlert").data("message");
						$("#register").validate().showErrors({
							email_address: jQuery.validator.messages.validateJSON
						});			
						validFlag = false;
						return false;
					}
					if( data.unavailable ){
						jQuery.validator.messages.validateJSON = $("#emailUsedAlert").data("message");
						$("#register").validate().showErrors({
							email_address: jQuery.validator.messages.validateJSON
						});
						validFlag = false;
						return false;
					}
					$('#registerButton').prop('disabled', false);
					
				})
				.fail(function (data) {
			        console.log(data);
				});
		} 
		return validFlag;
	});

	jQuery.validator.addMethod("validateMicrositeJSON", function(value, element) {
		if (email !== $("#regEmailAddress").val() || email === null) {
				validFlag = true;
				$('#registerButton').attr('disabled', 'disabled');
				email = $("#regEmailAddress").val();
				$.ajax({
					url: "/bin/UserIdValidation",
					type: "POST",
					dataType: "json",
					data: $(element).serialize() + "&" + "path=" + $("#contentPath").val(),
				})
				.done(function(data) {
					if( data.black_listed ){
						jQuery.validator.messages.validateMicrositeJSON = $("#blacklistAlert").data("message");
						$("#micrositeRegister").validate().showErrors({
							email_address: jQuery.validator.messages.validateMicrositeJSON
						});			
						validFlag = false;
						return false;
					}
					if( data.unavailable ){
						jQuery.validator.messages.validateMicrositeJSON = $("#emailUsedAlert").data("message");
						$("#micrositeRegister").validate().showErrors({
							email_address: jQuery.validator.messages.validateMicrositeJSON
						});
						validFlag = false;
						return false;
					}
					$('#registerButton').prop('disabled', false);
					
				})
				.fail(function (data) {
			        console.log(data);
				});
		} 
		return validFlag;
	});
    jQuery.validator.addMethod("pwdRestrictions", function(value, element) {
		return /^(?=(?:[^A-Za-z]*[A-Za-z]){4})(?=\S*$)(?=.*\d)(?!.*([A-Za-z0-9!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?])\1{2})(?=(?=.*[-+_!@#$%^&*.,?])|(?=.*[A-Z])(?=.*[a-z])).{8,15}$/.test(value);
	});    
    
	jQuery.validator.addMethod("captchaCheck", function(value, element) {
		return Boolean($("#captchaCheck").val());
	});
	$("#register").validate({
		//debug: true,
		rules: {
			first_name: {
				required: true,
			},
			last_name: {
				required: true
			},
			username: {
				required: true
			},
			email_address: {
				required: true,
				email: true,
				validateJSON: true
			},
			confirm_email: {
				required: true,
				email: true,
				equalTo: "#regEmailAddress"
			},
			password_entry: {
				required: true,
				pwdRestrictions: true
			},
			confirm_password: {
				required: true,
				equalTo: "#entryPassword"
			},
			postal_code: {
				required: true,
				number: true
			},
			help_description: {
				required: true
			},
			temp_password: {
				required: true
			},
            relationship: {
				required: true
			},
			/*region_select: {
				required: true
			},
			canada_region_select: {
				required: true
			},*/
			captcha: {
				captchaCheck: true
			}
		},
		messages: {
			required: $("#requiredAlert").data("message") || jQuery.validator.messages.required,
			first_name: $("#firstNameAlert").data("message") || jQuery.validator.messages.required,
			last_name: $("#lastNameAlert").data("message") || jQuery.validator.messages.required,
			job_title: $("#jobTitleAlert").data("message") || jQuery.validator.messages.required,
			username: $("#usernameRequired").data("message") || jQuery.validator.messages.required,
			email_address: {
				required: $("#emailRequired").data("message") || jQuery.validator.messages.required,
				email: $("#emailCheckAlert").data("message") || jQuery.validator.messages.email,
			},
			confirm_email: {
				required: $("#confirmEmailRequired").data("message") || jQuery.validator.messages.required,
				email: $("#confirmEmailAlert").data("message") || jQuery.validator.messages.email,
				equalTo: $("#confirmEmailEqual").data("message") || jQuery.validator.messages.equalTo
			},
			password_entry: {
				required: $("#passwordRequired").data("message") || jQuery.validator.messages.required,
				pwdRestrictions: $("#passwordRestrictions").data("message") || jQuery.validator.messages.required
			},
			confirm_password: {
				required: $("#confirmPassRequired").data("message") || jQuery.validator.messages.required,
				equalTo: $("#confirmPassAlert").data("message") || jQuery.validator.messages.equalTo
			},
			company_name: $("#companyRequired").data("message") || jQuery.validator.messages.required,
			country: $("#countryRequired").data("message") || jQuery.validator.messages.required,
			region_select: $("#regionRequired").data("message") || jQuery.validator.messages.required,
			postal_code: $("#zipRequired").data("message") || jQuery.validator.messages.required,
			help_description: $("#helpAlert").data("message") || jQuery.validator.messages.required,
			temp_password: $("#tempPassRequired").data("message") || jQuery.validator.messages.required,
			captcha: $("#captchaAlert").data("message") || jQuery.validator.messages.required,
			relationship: $("#relationshipRequired").data("message") || jQuery.validator.messages.required
		},
		errorClass: "error-flag",
		ignore: "",
		onkeyup: false,
		onfocusout: function(element) {
			$(element).valid();
		}
	});
	
    // CONTACT US VALIDATION
    
    $("#contactUs").validate({
		rules: {
			FirstName: {
				required: true,
			},
            LastName: {
				required: true,
			},
            Email: {
				required: true,
			},
            Relationship: {
				required: true,
			},
            CompanyName: {
				required: true,
			},
            Country: {
				required: true,
			},
            Request: {
				required: true,
			},
            captcha: {
				captchaCheck: true
			}
		},
		messages: {
			FirstName: $("#firstNameAlert").data("message") || jQuery.validator.messages.required,
			LastName: $("#lastNameAlert").data("message") || jQuery.validator.messages.required,
            Email: {
				required: $("#emailRequired").data("message") || jQuery.validator.messages.required,
				email: $("#emailCheckAlert").data("message") || jQuery.validator.messages.email,
			},
            Relationship: $("#relationshipRequired").data("message") || jQuery.validator.messages.required,
            CompanyName: $("#companynameRequired").data("message") || jQuery.validator.messages.required,
            Country: $("#lastNameAlert").data("message") || jQuery.validator.messages.required,
            Request: $("#helpAlert").data("message") || jQuery.validator.messages.required,
            captcha: $("#captchaAlert").data("message") || jQuery.validator.messages.required
		},
		errorClass: "error-flag",
		//ignore: "",
		onkeyup: false,
		onfocusout: function(element) {
			$(element).valid();
		}
	});
    //END
    
    // MICROSITE REGISTRATION VALIDATION
    $("#micrositeRegister").validate({
		//debug: true,
		rules: {
			first_name: {
				required: true
			},
			last_name: {
				required: true
			},
			email_address: {
				required: true,
				email: true,
				validateMicrositeJSON: true
			},
			confirm_email: {
				required: true,
				email: true,
				equalTo: "#regEmailAddress"
			},
			password_entry: {
				required: true,
				pwdRestrictions: true
			},
			confirm_password: {
				required: true,
				equalTo: "#entryPassword"
			},
            industry: {
                required: true
            },
			captcha: {
				captchaCheck: true
			}
		},
		messages: {
			required: $("#requiredAlert").data("message") || jQuery.validator.messages.required,
			first_name: $("#firstNameAlert").data("message") || jQuery.validator.messages.required,
            industry: $("#industryRequired").data("message") || jQuery.validator.messages.required,
			last_name: $("#lastNameAlert").data("message") || jQuery.validator.messages.required,			
			email_address: {
				required: $("#emailRequired").data("message") || jQuery.validator.messages.required,
				email: $("#emailCheckAlert").data("message") || jQuery.validator.messages.email,
			},
			confirm_email: {
				required: $("#confirmEmailRequired").data("message") || jQuery.validator.messages.required,
				email: $("#confirmEmailAlert").data("message") || jQuery.validator.messages.email,
				equalTo: $("#confirmEmailEqual").data("message") || jQuery.validator.messages.equalTo
			},
			password_entry: {
				required: $("#passwordRequired").data("message") || jQuery.validator.messages.required,
				pwdRestrictions: $("#passwordRestrictions").data("message") || jQuery.validator.messages.required
			},
			confirm_password: {
				required: $("#confirmPassRequired").data("message") || jQuery.validator.messages.required,
				equalTo: $("#confirmPassAlert").data("message") || jQuery.validator.messages.equalTo
			},			
			captcha: $("#captchaAlert").data("message") || jQuery.validator.messages.required
		},
		errorClass: "error-flag",
		ignore: "",
		onkeyup: false,
		onfocusout: function(element) {
			$(element).valid();
		}
	});
    //END MICROSITE REGISTRATION VALIDATION
    
    $("#credentials").validate({
		showErrors: function( error, element ) {
			return true;
		},
		rules: {
			username: {
				required: true,
				validateUser: true
			},
			log_password: {
				required: true
			}	
		},
		onfocusout: function(element) {
			var $submit = $("#credentials .main-submit");
			if($("#credentials").valid()){
				$submit.removeClass("disabled");
			} else {
				$submit.addClass("disabled");
			}
		},
		onkeyup: function() {
			_toggleSubmit(validUser, "#credentials .main-submit", "#password");
		}
	});

	
	$("#logIn, #resetPswd, #resetTempPswd").validate({
		rules: {
			username: {
				required: true
			},
			log_password: {
				required: true
			},
            temp_password: {
                required: true   
            },
            password_entry: {
                required: true   
            },
            confirm_password: {
                required: true   
            }
		},
        messages: {
			username: $("#usernameRequired").data("message") || jQuery.validator.messages.required,
			log_password: $("#passwordRequired").data("message") || jQuery.validator.messages.required,
			temp_password: $("#tempPassRequired").data("message") || jQuery.validator.messages.required,
            password_entry: $("#passwordRequired").data("message") || jQuery.validator.messages.required,
            confirm_password: $("#confirmPassAlert").data("message") || jQuery.validator.messages.required
        },
		errorClass: "error-flag"
    });
	$(".blog-wrap").infinitescroll();
	$("#tweetList").tickertweet();
	$("#countrySelection").formswitch();
	$("#facetSearch").searchlister();
	$(".article-resources").contentlister();
	$(".region-selector-wrap").cookiejar();
	$(".blog-wrap .tags").socialplugs();
	//Repaint element to trigger css font fixes when device orientation changes.
	var $repaintElements = $(".list-nav li");
	$(window).resize(function() {
		$repaintElements.css("z-index", 1);
		dotUpdates();
//		if (window.innerWidth <= 1024 && window.innerWidth >= 641 && masonFlag === false) {
//			$aside.masonry({
//				itemSelector: "aside.realign > div"
//			});
//			masonFlag = true;
//		} else if (masonFlag === true) { 
//			if(window.innerWidth >= 1025 || window.innerWidth <= 640) {	
//				$aside.masonry("destroy");
//				masonFlag = false;
//			}
//		}
		if ($(window).width() > 625) {
			$sFilter.show();	
		}
		if (window.innerWidth >= 1024 && $(".faceted-container").hasClass("open-animate")) {
			$(".faceted-container").removeClass("open-animate").addClass("close-animate");
		}
	});
	$(window).on('orientationchange', function(){
		console.log("orientation change here");
		dotUpdates();
	});
});
$(window).load(function() {
	flyoutAnimation();
});
function _toggleSubmit(userflag, submit_id, pswd_id) {
	var $submit = $(submit_id);
	if($(pswd_id).valid()){
		//Check whether user id has been verified
		if(userflag) {
			$submit.removeClass("disabled");
		}
	} else {
		$submit.addClass("disabled");
	}
}
function dotInits() {
	$(".carousel-panel p").dotdotdot();
	$(".carousel-panel .text-wrap div").dotdotdot();
	$(".author-box p").dotdotdot();
	$(".full-width-carousel .text-box div").dotdotdot();
}

function dotUpdates() {
	$(".carousel-panel p").trigger("update.dot");
	$(".carousel-panel .text-wrap div").trigger("update.dot");
	$(".author-box p").trigger("update.dot");
	$(".full-width-carousel .text-box div").trigger("update.dot");
}	

//

// VIMEO Tracking
;(function($){ 
    var vimTrack = function(_el) {        
        $(_el).each(function(){            
            Froogaloop(this).addEvent('ready', ready);            
        });
        function ready(playerID){
            console.log(playerID + ' is ready');
            Froogaloop(playerID).addEvent('play', onPlay );
            Froogaloop(playerID).addEvent('playProgress', playProg );
            Froogaloop(playerID).addEvent('finish', vidDone );
            Froogaloop(playerID).addEvent('pause', vidPause );
        }
        function onPlay(PlayerID) {
            console.log(PlayerID + " has started");
        }
        function playProg(data, playerID){
            console.log(playerID + " is at " + Math.round(data.percent * 100) + "%");
        }
        function vidDone(playerID){
            console.log("The video" + playerID + " has finished");
        }
        function vidPause(playerID){
            console.log("The video" + playerID + " has paused");
        }
    }    
    vimTrack('iframe');
})(jQuery);
// END Vimeo Tracking