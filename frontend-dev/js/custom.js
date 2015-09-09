/**************************************************

HTML5 TEMPLATE
JS Functions

Site: 

Author: @Diamond by Bold

Date: 15 / 01 / 2014

**************************************************/

//==============================================
//
//Global Variables
//
//==============================================
//
	//All global Variables go here
	$body = $("body");

//==============================================
//
//Global Functions
//
//==============================================
//
	//************************
	//
	//Smooth scroll to content
	//
	//************************/
	//
		function smoothScroll(whereTo,specialOffset){
		    //calculate destination place
		    var dest = 0;
		    if(whereTo.offset().top > jQuery(document).height()-jQuery(window).height()){
		        dest = jQuery(document).height()-jQuery(window).height();
		    }else{
		        dest = whereTo.offset().top - specialOffset;
		    }
		    //go to destination
		    jQuery('html,body').animate({scrollTop:dest}, 1000,'swing');
		}

	//************************
	//
	//Scroll messages down
	//
	//************************/
	//
		function updateScroll( el ){
			el.scrollTop(el[0].scrollHeight);
		}

	//************************
	//
	//Check if element is visible/partially visible inside div
	//
	//************************/
	//
		function visibleCheck( el ){

			var distTop = el.offset().top;
			var parentDistTop = el.parent().offset().top;
			var parentHeight = el.parent().height();
			var totalParentTopDist = parentDistTop + parentHeight;

			el.removeClass('isBellow');
			el.removeClass('isVisible');
			el.removeClass('isAbove');

			if( distTop >= totalParentTopDist ){
				el.addClass('isBellow');
			}

			if( distTop >= parentDistTop && distTop < totalParentTopDist ){
				el.addClass('isVisible');
			}

			if( distTop < parentDistTop ){
				el.addClass('isAbove');
			}
		}

	//************************
	//
	//Send Message / Conversation View Switcher
	//
	//************************/
	//
		function msgToggle(){
			var switcherBtn = $('.switchMsgView');

			if( switcherBtn.hasClass('new') ){
				switcherBtn.removeClass('new');
				switcherBtn.addClass('cancel');
				switcherBtn.html('CANCELAR');

			}else{
				switcherBtn.addClass('new');
				switcherBtn.removeClass('cancel');
				switcherBtn.html('NOVA MENSAGEM');
			}

			if( !switcherBtn.is(':visible')  ){
				switcherBtn.fadeIn();
			}

			$('.msgList').toggleClass('active');
			$('.newMsg').toggleClass('active');
		}

		function showSendMsg(){
			if( !$('.newMsg').hasClass('active') ){
				msgToggle();
			}
		}

		function showConversation(){
			if( !$('.msgList').hasClass('active') ){
				msgToggle();
			}
		}

		function hideSwitcher(){
			var switcherBtn = $('.switchMsgView');
			switcherBtn.hide();
		}

	//==============================================
	//
	//RESPONSIVE ADAPTATIONS
	//
	//==============================================
	//
		//==============================================
		//
		//CALENDAR
		//
		//==============================================
		//
			function fillMobileTable(){
				$('.mobileSchedule').empty().promise().done(function(){
					$('.fullSchedule .dateHeader').each( function(){
						$('.mobileSchedule').append('<div class="mobileDate"><div class="dateHolder"></div><div class="itemsTotals"></div></div>');
						$(this).children().clone().prependTo('.mobileDate:last .dateHolder');
					});

					calculateItemTotals();
				});
			}

			function calculateItemTotals(){

				for( i = 0; i < 7; i++ ){
					if( $('.fullSchedule tbody').find('td:nth-of-type('+ (i+2) +')').find('.typeConsult').length > 0 ){
						$('.mobileSchedule').find('.mobileDate:nth-of-type('+ (i+1) +')').find('.itemsTotals').append( '<div class="singleTotal consultTotal">'+ $('.fullSchedule tbody').find('td:nth-of-type('+ (i+2) +')').find('.typeConsult').length +'</div>' );
					}

					if( $('.fullSchedule tbody').find('td:nth-of-type('+ (i+2) +')').find('.typeExam').length > 0 ){
						$('.mobileSchedule').find('.mobileDate:nth-of-type('+ (i+1) +')').find('.itemsTotals').append( '<div class="singleTotal examTotal">'+ $('.fullSchedule tbody').find('td:nth-of-type('+ (i+2) +')').find('.typeExam').length +'</div>' );
					}

					if( $('.fullSchedule tbody').find('td:nth-of-type('+ (i+2) +')').find('.typeSurgery').length > 0 ){
						$('.mobileSchedule').find('.mobileDate:nth-of-type('+ (i+1) +')').find('.itemsTotals').append( '<div class="singleTotal surgeryTotal">'+ $('.fullSchedule tbody').find('td:nth-of-type('+ (i+2) +')').find('.typeSurgery').length +'</div>' );
					}
				}

				buildMobileDays();
			};

			function buildMobileDays(){

				for( i = 0; i < 7; i++ ){
					if( $('.fullSchedule tbody').find('td:nth-of-type('+ (i+2) +')').find('.itemcalendar').length > 0 ){

						var dayDateNumber = $('.mobileDate').eq(i).find('.dateNumber').html();
						var dayDateDay = $('.mobileDate').eq(i).find('.dateDay').html();

						$('.mobileDays').append('<div data-day="'+ i +'" class="singleDay day'+ i +'"><div class="dayOverview active"><div class="dayHeader"><span class="dayNumber">'+ dayDateNumber +'</span><span class="dayDate">'+ dayDateDay +'</span><a href="#" class="icon-cross"></a></div><div class="dayAppts"></div></div><div class="dayPopups"></div></div>');

						$('.fullSchedule tbody').find('td:nth-of-type('+ (i+2) +')').children('.itemcalendar').clone().appendTo('.day'+i+' .dayAppts');

					}
				}

				$('.dayAppts').find('.itemcalendar').removeClass('inExcess').removeAttr( 'onclick' );

				buildMobilePopups();
			}

			function buildMobilePopups(){

				$('.singleDay').each(function(){

					var popupAmount = $(this).find('.dayAppts .itemcalendar').length;

					for( i = 0; i < popupAmount; i++ ){
						var popupContents = $(this).find('.dayAppts .itemcalendar').eq(i).find('.popupContents').children().clone();

						$(this).find('.dayPopups').append('<div data-popup="'+ i +'" class="singlePopup"><div class="popupHeader"><a href="#" class="icon-arrow-prev">Voltar</a></div></div>');

						var targetPopupHolder = $(this).find('.singlePopup[data-popup="' + i + '"]');

						popupContents.appendTo( targetPopupHolder );
					}
				});
			}

		//==============================================
		//
		//MESSAGES
		//
		//==============================================
		//
			function showConversationMobile(){
				$('#messages .messages').toggleClass('active');
				$('#messages .conversation').toggleClass('active');
			}

			function showConversationList(){
				$('#messages .conversation').removeClass('active');
				$('#messages .messages').addClass('active');
			}

			function hideConversationList(){
				$('#messages .conversation').addClass('active');
				$('#messages .messages').removeClass('active');
			}

		//==============================================
		//
		//BILLING
		//
		//==============================================
		//
			function fillBillingDetails(){

				if( $('.mobileBilling').find('.singleBill').length > 0 ){
					$('.mobileBilling').empty();
				}

				var totalBillingLines = $('.billingTable tbody').find('tr').length;

				for( i = 0; i < totalBillingLines; i++ ){

					var eachBill = $('.billingTable tbody').find('tr').eq(i);


					var billDate = eachBill.find('.billDateTD').html();
					var billPatient = eachBill.find('.billPatientTD').html();
					var billName = eachBill.find('.billNameTD span').html();
					var billService = eachBill.find('.billServiceTD').html();
					var billEntity = eachBill.find('.billEntityTD span').html();
					var billAct = eachBill.find('.billActTD span').html();
					var billQtd = eachBill.find('.billQtdTD').html();
					var billValueTD = eachBill.find('.billValueTD').html();


					$('.mobileBilling').append('<div data-bill="'+ i +'" class="singleBill bill'+ i +'">'
							+'<div class="billHeader">'
								+'<a href="#" class="icon-arrow-prev">Voltar</a>'
							+'</div>'
							+'<div class="billMobileContents">'
								+'<div class="billDateMobile">'
									+'<span class="billTitle" >Data</span>'
									+'<div class="billData">'+ billDate + '</div>'
								+'</div>'
								+'<div class="billPatientMobile">'
									+'<span class="billTitle" >Doente</span>'
									+'<div class="billData">'+ billPatient + '</div>'
								+'</div>'
								+'<div class="billNameMobile">'
									+'<span class="billTitle" >Nome</span>'
									+'<div class="billData">'+ billName + '</div>'
								+'</div>'
								+'<div class="billServiceMobile">'
									+'<span class="billTitle" >Serviço</span>'
									+'<div class="billData">'+ billService + '</div>'
								+'</div>'
								+'<div class="billEntityMobile">'
									+'<span class="billTitle" >Entidade</span>'
									+'<div class="billData">'+ billEntity + '</div>'
								+'</div>'
								+'<div class="billActMobile">'
									+'<span class="billTitle" >Acto</span>'
									+'<div class="billData">'+ billAct + '</div>'
								+'</div>'
								+'<div class="billQtdMobile">'
									+'<span class="billTitle" >Qtd</span>'
									+'<div class="billData">'+ billQtd + '</div>'
								+'</div>'
								+'<div class="billValueMobile">'
									+'<div class="mValue">'+ billValueTD +'</div>'
									+'<span class="billTitle" >Valor Pago</span>'
								+'</div>'
							+'</div>'
						+'</div>');
				}
			}

			function showBillDetails(){
				$('.desktopBilling').toggleClass('active');
				$('.mobileBilling').toggleClass('active');
			}

//==============================================
//
//Window Functions
//
//==============================================
//
	//==============================================
	//
	//Run functions after all is loaded
	//
	//==============================================
	//
		$( window ).load(function(){
			if( $('#messages .conversation').css('display') == 'block' ){
				$('#messages .messages').addClass('active');

				msgToggle();
			}

			
		}); 

	//==============================================
	//
	//Run functions on scroll
	//
	//==============================================
	//
		$(window).scroll(function(){
			//Show header on scroll
		    //$(".site").show();
		    if( $('.site').css('position') == 'fixed' ){

			    if ($('.site').is(':visible') && $(window).scrollTop() > 0) {
			    	$('.site').addClass('hollow');
			    }

			    if ( $(window).scrollTop() <= 50 ) {
			    	$('.site').removeClass('hollow');
			    }
			}else{
				if( $('.site').hasClass('hollow') ){
					$('.site').removeClass('hollow');
				}
			}

		});

	//==============================================
	//
	//Run functions on resize
	//
	//==============================================
	//
		$(window).resize(function(){
			if( $('.site').css('position') == 'static' ){
				if( $('.fullSchedule').length > 0 ){
					RemovePopup();
				}
			}
		});

	//==============================================
	//
	//Run functions on orientation change
	//
	//==============================================
	//

		$(window).on( 'orientationchange', function() {
  			if( $('.fullSchedule').length > 0 ){
				RemovePopup();
			}

			if( $('#messages').length > 0 ){
				showConversationList();
				showSendMsg();
			}
		});

//==============================================
//
//jQuery Doc. Ready
//
//==============================================
//
	$(function(){

		//==============================================
		//
		//Add preloader to body on ajax requests
		//
		//==============================================
		//
			$(document).ajaxStart(function () {
	            $('body').addClass("loading");
	        });
	        $(document).ajaxStop(function () {
	            $('body').removeClass("loading");
	        });
	        $(document).ajaxError(function (e, jqxhr, settings, exception) {
	           $('body').removeClass("loading");

	           e.stopPropagation();
	           if (jqxhr != null) {
	               alert("De momento n\xE3o \xE9 poss\xEDvel satisfazer o pedido. Pedimos desculpa pelo incomodo");
	               window.location = '/';
	           }
	           //alert(jqxhr.responseText);

	        });

	    //==============================================
		//
		//Hide header after some time after scroll
		//
		//==============================================
		//
			/*setInterval(function () {
	            if ($(".site").show() && $(window).scrollTop() > 0) {
	                $(".site").fadeOut("slow");
	            }
	        }, 5000);*/

		//==============================================
		//
		//Toggle between send message and conversation list
		//
		//==============================================
		//
			$('.switchMsgView').on( 'click', function(e){
				e.preventDefault();

				if( $(this).hasClass('new') ){

					hideConversationList();

					if( !$('.newMsg').hasClass('active') ){
						msgToggle();
					}

				}else if( $(this).hasClass('cancel') ){
					showConversationList();

					if( $('.site').css('position') == 'static' ){
						$('#ulLastMsg').find('li.active').removeClass('active');
					}

					msgToggle();
				}

			});

	    //==============================================
		//
		//Scroll to Top
		//
		//==============================================
		//
			$('.scrollToTop').on('click', function(e){
				e.preventDefault();

				smoothScroll( $('html'), 0);
			});

		//==============================================
		//
		//Close Calendar legend
		//
		//==============================================
		//
			$(document).on('click', '.closeLegend', function(e){
				e.preventDefault();

				$('aside.legend').slideToggle(300);
			});

		//==============================================
		//
		//Check which messages are visible
		//
		//==============================================
		//
			$('#ulLastMsg').find('li').each(function(){
				visibleCheck( $(this) );
			});

		//==============================================
		//
		//Scroll to next/previous visible messages
		//
		//==============================================
		//
			$(document).on('click', '#messages .more', function(e){
				e.preventDefault();

				var el = $('#ulLastMsg').find('.isBellow:first');

				if( el.length > 0 ) {
					var distTop = el.offset().top;
					var parentDistTop = el.parent().offset().top;
					var parentScrollTopDist = el.parent().scrollTop();

					var distanceFromTop = distTop - parentDistTop + parentScrollTopDist - 20;

					$('#ulLastMsg').animate({scrollTop:distanceFromTop}, 1000,'swing').promise().done(function(){
						$('#ulLastMsg').find('li').each(function(){
							visibleCheck( $(this) );
						});		
					});
				}
			});

			$(document).on('click', '#messages .less', function(e){
				e.preventDefault();

				var el = $('#ulLastMsg').find('.isAbove:last');

				if( el.length > 0  ) {
					var elHeight = el.height();
					var distTop = el.offset().top;
					var parentHeight = el.parent().height();
					var parentDistTop = el.parent().offset().top;
					var parentScrollTopDist = el.parent().scrollTop();

					var distanceFromTop = distTop - parentDistTop + parentScrollTopDist - parentHeight + elHeight + 10;

					$('#ulLastMsg').animate({scrollTop:distanceFromTop}, 1000,'swing').promise().done(function(){

						$('#ulLastMsg').find('li').each(function(){
							visibleCheck( $(this) );
						});
						
					});
				}
			});

		//==============================================
		//
		//Setup TinyMCE
		//
		//==============================================
		//
			var editorActive = false;

			function initTinyMCE() {
				tinymce.init({
					skin: 'light',
				    selector: '#txt_newmsg,#txt_msg',
				    menubar: false,
				    statusbar: false,
				    plugins: "textcolor",
				    toolbar: "undo redo | styleselect | bold italic underline | forecolor backcolor"
				});
			}

			function removeTinyMCE() {
			    tinymce.remove("#txt_newmsg");
			    tinymce.remove("#txt_msg");
			}

			tinymce.on('RemoveEditor', function(e) {
			    //console.log("Removed!");
			    editorActive = false;
			});

			tinymce.on('AddEditor', function(e) {
			    //console.log("Added!");
			    editorActive = true;
			});

			$('.msgList, .newMsg').on('click', '.code',
			    function(e) {
			    	e.preventDefault();

			        if ( editorActive ) {
			            removeTinyMCE();
			        }
			        else {
			            initTinyMCE();
			        }
			    }
			);


		//==============================================
		//
		//Enable click outside to close Dropdown Selects in Billing
		//
		//==============================================
		//
			$(document).on('click', function () {

				if( $('#sel_billDate').find('.selectPseudoCalendar').hasClass('active') ){
					$('#sel_billDate').find('.selectPseudoCalendar').trigger('click');
				}

				if( $('#sel_billDetails').find('.selectDropSelected').hasClass('active') ){
					$('#sel_billDetails').find('.selectDropSelected').trigger('click');
				}

			});

			$(document).on('click', '#sel_billDate, #sel_billDetails', function(e){
				e.stopPropagation();
			});

		//==============================================
		//
		//Simulate Select Box with Dropdown
		//
		//==============================================
		//
			$(document).on('click', '.selectDropSelected', function (e) {
				e.preventDefault();

				if( $('#sel_billDate').find('.selectPseudoCalendar').hasClass('active') ){
					$('#sel_billDate').find('.selectPseudoCalendar').trigger('click');
				}

			    if ($(this).hasClass('active')) {
			        $(this).removeClass('active');
			        $(this).siblings('.selectDrop').stop().slideUp(700);
			    } else {
			        $(this).addClass('active');
			        $(this).siblings('.selectDrop').stop().slideDown(700);
			    }
			});

			$(document).on('click', '.selectDrop li', function (e) {
				e.preventDefault();

			    //Get current select menu
			    var curNav = $(this).parent().siblings('.selectDropSelected');

			    //Get option value and text
			    var selectedItemValue = $(this).attr('data-value');
			    var selectedItemTxt = $(this).html();

			    //Add selected state to list item
			    $(this).addClass('active');
			    
			    //Remove selected state from other list options
			    $(this).siblings('li').removeClass('active');
			    
			    //Pass value to selected item
			    curNav.attr('data-value', selectedItemValue);
			    //curNav.html(selectedItemTxt);
			    
			    //Close select after selection
			    curNav.trigger('click');

			    updateBilling(selectedItemValue, selectedItemTxt);
			});

		//==============================================
		//
		//Show pseudo calendar for year/month selection
		//
		//==============================================
		//

			$('.filterDate').on('click', '.selectPseudoCalendar', function (e) {

				if( $('#sel_billDetails').find('.selectDropSelected').hasClass('active') ){
					$('#sel_billDetails').find('.selectDropSelected').trigger('click');
				}

			    if ($(this).hasClass('active')) {
			        $(this).removeClass('active');
			        $(this).siblings('.pseudoCalendar').stop().slideUp(700);
			    } else {
			        $(this).addClass('active');
			        $(this).siblings('.pseudoCalendar').stop().slideDown(700);
			    }
			});

			$('.filterDate').on('click', '.monthPicker', function (e) {
				e.preventDefault();

				//Get current select menu
			    var curNav = $('.selectPseudoCalendar');

				var theMonth = $(this).attr('data-month');
				var theYear = $('.inputYear').val();

				curNav.trigger('click');

				RefreshBillingData(theYear, theMonth);
			});

			$('.filterDate').on('click', '.yearControls', function (e) {
				e.preventDefault();

				var theYear = parseInt( $('.inputYear').val() );

				if( $(this).hasClass('prevYear') ){
					theYear = theYear -1;
					$('.inputYear').val( theYear );
				}

				if( $(this).hasClass('nextYear') ){
					theYear = theYear +1;
					$('.inputYear').val( theYear );
				}
			});

		//==============================================
		//
		//Show more detail in Billing
		//
		//==============================================
		//

			$('#billing').on('click', '.billDetail', function(){

				if( $('.billPatient').css('display') == 'none' ){

					var selectedBill = $('.singleBill[data-bill="' + $(this).parent('tr').index() + '"]');

					$('.singleBill').hide();
					selectedBill.show();

					showBillDetails();

				}else{
					if( !$(this).parent('tr').hasClass('active') ){
						$('.billingTable').find('tr').removeClass('active');
						$(this).parent('tr').addClass('active');
					}else{
						$(this).parent('tr').removeClass('active');
					}
				}
			});

			$(document).on('click', '.billHeader .icon-arrow-prev', function(e){
				e.preventDefault();

				showBillDetails();
			});

		//==============================================
		//
		//Clear Filters
		//
		//==============================================
		//
			$('.clearFilters').on('click', function(e){
			   e.preventDefault();
			   ClearData();
			});

		//==============================================
		//
		//RESPONSIVE ADAPTATIONS
		//
		//==============================================
		//
			//==============================================
			//
			//CALENDAR
			//
			//==============================================
			//
				//==============================================
				//
				//FILTERS
				//
				//==============================================
				//
					$( '.calendar_menu .curMonth' ).clone().appendTo( '.mobileCalendarFooter' );
					$( '.calendar_menu .weekControls' ).clone().appendTo( '.mobileCalendarFooter' );

				//==============================================
				//
				//TABLE
				//
				//==============================================
				//
					if( $('.fullSchedule').length > 0 ){
						fillMobileTable();						
					}

				//==============================================
				//
				//DAY SELECTOR
				//
				//==============================================
				//
					$(document).on('click', '.mobileDate', function(){

						if( $(this).find('.singleTotal').length > 0 ){

							$('.mobileSchedule').toggleClass('active');
							$('.mobileDays').toggleClass('active');

							var selectedDay = $('.singleDay[data-day="' + $(this).index() + '"]');
							
							$('.singleDay').hide();
							selectedDay.show();

						}
					});

				//==============================================
				//
				//CLOSE SELECTED DAY
				//
				//==============================================
				//
					$(document).on('click', '.dayHeader .icon-cross', function(e){
						e.preventDefault();

						$('.mobileSchedule').toggleClass('active');
						$('.mobileDays').toggleClass('active');
					});

				//==============================================
				//
				//POPUP SELECTOR
				//
				//==============================================
				//
					$(document).on('click', '.dayAppts .itemcalendar', function(){

						$(this).closest('.dayOverview').toggleClass('active');
						$(this).closest('.dayOverview').siblings('.dayPopups').toggleClass('active');

						var selectedPopup = $('.singlePopup[data-popup="' + $(this).index() + '"]');

						$('.legend').hide();
						$('.mobileCalendarFooter').hide();
						
						$('.singlePopup').hide();
						selectedPopup.show();

					});

				//==============================================
				//
				//CLOSE SELECTED POPUP
				//
				//==============================================
				//
					$(document).on('click', '.popupHeader .icon-arrow-prev', function(e){
						e.preventDefault();

						$(this).closest('.dayPopups').toggleClass('active');
						$(this).closest('.dayPopups').siblings('.dayOverview').toggleClass('active');

						$('.legend').show();
						$('.mobileCalendarFooter').show();
					});

			//==============================================
			//
			//MESSAGES
			//
			//==============================================
			//
				$('.received').on('click', function(){
					if( $('.conversation').css('display') == 'block' ){
						showConversationList();

						if( $('.switchMsgView').hasClass('cancel') ){
							msgToggle();
						}

						$('#ulLastMsg').find('li.active').removeClass('active');


					}
				});

			//==============================================
			//
			//BILLING
			//
			//==============================================
			//
				/*fillBillingDetails();*/

	});
//== End Jquery d.ready