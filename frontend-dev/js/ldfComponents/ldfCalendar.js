
if (typeof(ldfCalendarArray) == 'undefined') {  var ldfCalendarArray = new Array(); } 
if (typeof(ldfCalendarPageCallback) == 'undefined') {  var ldfCalendarPageCallback = new Array(); } 


function ldfCalendarChangeDay(year, month, inst, attrid) {
    $.ajax({
        url: ldfCalendarPageCallback[attrid],
        dataType: 'json',
        data: { "year": year, "month":month },
        type: 'POST',
        async: false
    }).success(function (res) {
        ldfCalendarArray[attrid] = res;
    });
}

function ldfcalendarSelDay(dateText, attrid) {

    $(ldfCalendarArray[attrid]).each(function () {

        if (this.itemCallback != null)
        {
            var itemDt = new Date(this.itemDate);
            var dateDt = new Date(dateText);

            var itemDatestr = itemDt.getDate() + "-" + (itemDt.getMonth() + 1) + "-" + itemDt.getFullYear();
            var dateDaystr = dateDt.getDate() + "-" + (dateDt.getMonth() + 1) + "-" + dateDt.getFullYear();

            if (itemDatestr == dateDaystr)
            {
                window[this.itemCallback](dateText);
                return false;
            }
        }
    });
}

function ldfCalendarRenderDay(dateDay, attrid) {
    var currentData = [true, "", ""];

    $(ldfCalendarArray[attrid]).each(function () {
        var itemDt = new Date(this.itemDate);

        var itemDatestr = itemDt.getDate() + "-" + (itemDt.getMonth()+1) + "-" + itemDt.getFullYear();
        var dateDaystr = dateDay.getDate() + "-" + (dateDay.getMonth()+1) + "-" + dateDay.getFullYear();
        var dateClass = "ui-Calendar-highlightDate";

        if (itemDatestr == dateDaystr)
        {
            if (this.itemClass != "")
            {
                dateClass = this.itemClass;
            }
            currentData = [true, dateClass, ""];
            return false;
        }
    });

    return currentData;
}