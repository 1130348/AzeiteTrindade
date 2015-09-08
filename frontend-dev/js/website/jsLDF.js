
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