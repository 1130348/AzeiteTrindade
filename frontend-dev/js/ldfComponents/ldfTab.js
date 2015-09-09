

function InitLDFTab(attrid)
{
    var elem = $("#" + attrid);
    var selElem = elem.children("li.active");

    var childElem = $(selElem).children("a");
    ShowElementData($(childElem), attrid);
 
}


function ShowElementData(obj, attrid) {

    var itemLink    = obj.attr("data-link");
    var itemID      = obj.attr("data-ID");
    var itemChild   = $("#" + itemID);


    if (ldfTabCallRefreshArray[attrid] == true || (itemChild.text().length == 0 && itemLink !== "")) {
        $.ajax({  
            url: itemLink,
            async : false
        }).done(function (res) {
            itemChild.html(res);
        });
    }


    var fn = window[ldfTabCallBackArray[attrid]];

    if (typeof fn !== 'undefined' && $.isFunction(fn)) {
        setTimeout(function () { fn(); }, 800);
    }

}