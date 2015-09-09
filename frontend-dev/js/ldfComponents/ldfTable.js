if (typeof(ldfTableRowsArray) == 'undefined') {  var ldfTableRowsArray = new Array(); }
if (typeof(ldfTableOrderArray) == 'undefined') {  var ldfTableOrderArray = new Array(); }
if (typeof(ldfTableHeaderArray) == 'undefined') {  var ldfTableHeaderArray = new Array(); } 
if (typeof(ldfTableLoadCallbackArray) == 'undefined') {  var ldfTableLoadCallbackArray = new Array(); } 
if (typeof(ldfTableAddCallbackArray) == 'undefined') {  var ldfTableAddCallbackArray = new Array(); } 
if (typeof(ldfTableSearchCallbackArray) == 'undefined') {  var ldfTableSearchCallbackArray = new Array(); } 
if (typeof(ldfTableRemoveCallBackArray) == 'undefined') {  var ldfTableRemoveCallBackArray = new Array(); } 
if (typeof(ldfTableEditCallBackArray) == 'undefined') {  var ldfTableEditCallBackArray = new Array(); } 
if (typeof(ldfTableTBodyArray) == 'undefined') {  var ldfTableTBodyArray = new Array(); } 
if (typeof(ldfTableDivFooterArray) == 'undefined') {  var ldfTableDivFooterArray = new Array(); } 
if (typeof(ldfTableTHeaderArray) == 'undefined') { var ldfTableTHeaderArray = new Array(); } 
if (typeof(ldfTableTPageArray) == 'undefined') {  var ldfTableTPageArray = new Array(); } 
if (typeof(ldfTableFilterDataArray) == 'undefined') {  var ldfTableFilterDataArray = new Array(); } 
if (typeof(ldfTableAttributes) == 'undefined') {  var ldfTableAttributes = new Array(); } 
if (typeof(ldfTableCurPageAttribute) == 'undefined') {  var ldfTableCurPageAttribute = new Array(); } 
if (typeof(ldfTableRefreshTime) == 'undefined') {  var ldfTableRefreshTime = new Array(); } 
if (typeof(ldfTableRefreshTimeObject) == 'undefined') {  var ldfTableRefreshTimeObject = new Array(); } 


//==============================================
//
//Main Methods
//
//==============================================
//

function LDFTableSort(obj, attrid)
{
    var curObject = $(obj);
    var isSortable = curObject.attr("data-sortable");


    if (isSortable == "True") 
    {
        var fieldName = curObject.attr("data-field");
        var orderValue = "1";

        if (curObject.attr("data-order") != undefined) {
            orderValue = curObject.attr("data-order");
        }

        var orderData = [];
        orderData[0] = fieldName;
        orderData[1] = orderValue;
        var filterData = ldfTableFilterDataArray[attrid];
        ldfTableOrderArray[attrid] = orderData;

        $.ajax({
            url: ldfTableLoadCallbackArray[attrid],
            dataType: 'json',
            data: { 'pageNumber': 1, 'orderData': orderData, 'filterData' : filterData },
            type:     'GET',
            traditional:true,
            success : function (res) {
                if (res.header.status == false)
                {
                   alert(res.header.message);
                } else {
                    var value = parseInt(orderValue) * -1;
                    curObject.attr("data-order", value);

                    curObject.siblings().removeClass("sortAsc");
                    curObject.siblings().removeClass("sortDesc");
                    curObject.siblings().removeClass("active");

                    curObject.removeClass("sortAsc");
                    curObject.removeClass("sortDesc");

                    if (value == 1)
                    {
                        curObject.addClass("sortAsc");
                    } else {
                        curObject.addClass("sortDesc");
                    }
                    curObject.addClass("active");
                    
                    ldfTableCurPageAttribute[attrid] = "1";
                    LDFTableRenderBody(res, attrid);
                }

            }, 
            error : function (e, jqxhr, settings, exception) {
                LDFTableShowError(e, jqxhr, settings, exception);
            }

        });

    }
}

function LDFTableRenderBody(data, attrid) {
    if (data.header.status == false)
    {
        LDFTableShowError(data.header.message);
        return;
    }

    var pageCount    = data.body.pageCount;
    var rowCount     = data.body.rowCount;
    var filterData   = data.body.filters;
    data             = data.body.list;
    ldfTableTBodyArray[attrid].empty();

    if (typeof(filterData) != "undefined") {
        ldfTableFilterDataArray[attrid] = filterData;
    }
    ldfTableRowsArray[attrid]       = data;

    if ($(data).length == 0)
    {
        row = "<tr><td colspan='" + $(ldfTableHeaderArray[attrid]).length + "' style='text-align:center;'>Sem registos</td></tr>";
        ldfTableTBodyArray[attrid].append(row);
    }


    $.each(data, function (datakey, dataValue) {
        var row = "<tr data-rowid='" + dataValue.rowID + "' style='" + dataValue.style + "'>";

        $.each(dataValue.rowItems, function (rowKey, rowValue) {

            $.each(ldfTableHeaderArray[attrid], function (headerKey, headerValue) {
                if (rowValue.itemColumnName == headerValue["headerID"] && headerValue["headerVisible"] != false) {
                    var value = rowValue.itemValue;
                    if (value == null)
                    {
                        value = "";
                    }
                    if (headerValue["headerEncode"] == true)
                    {

                    }


                    if (rowValue.itemLink != null && rowValue.itemLink !== "") {
                        value = "<a href=\"" + rowValue.itemLink + "\">" + value + "</a>";
                    } 
                    var attrMobile = "False";
                    if (headerValue["headerMobile"] == false)
                    {
                        attrMobile = "False";
                    } else {
                        attrMobile = "True";
                    }

                    var style = "";
                    for (var c in headerValue["headerStyle"])
                    {
                        if (headerValue["headerStyle"].hasOwnProperty(c)) 
                        {
                            style += c + ":" + headerValue["headerStyle"][c] + ";";
                        }
                    }


                    row += "<td data-showMobile='" + attrMobile + "' data-column='" + rowValue.itemColumnName + "' data-editable='" + headerValue["headerEditable"] + "' style='" + style + "'>" + value + "</td>";
                }

            });

                  

        });
     
        
        if (ldfTableAttributes[attrid].canEditRow == true) {
            row += "<td><button type='button' class='btn btn-success ldfTableEditRow_" + attrid + "' value='Editar'><li class='fa fa-edit'></li> Editar</button></td>";            
            //row += "<td><input type='button' class='ldfTableEditRow_" + attrid + "' value='Editar'/></td>";
        }

        if (ldfTableAttributes[attrid].canDeleteRow == true) {
            row += "<td><button type='button' class='btn btn-danger ldfTableRemoveRow_" + attrid + "' value='Remover'><li class='fa fa-remove'></li> Remover</button></td>";            

            //row += "<td><input type='button' class='ldfTableRemoveRow_" + attrid + "' value='Remover'/></td>";
        }

        row += "</tr>";

        ldfTableTBodyArray[attrid].append(row);
    });

    InitRowInlineButtons(attrid);
    LDFTableRenderPages(pageCount, rowCount, attrid);
}

function LDFTableFilterTable(fields, attrid)
{

    var fieldObject = JsonFields(fields);

    $.ajax({
        url: ldfTableSearchCallbackArray[attrid],
        dataType: 'JSON',
        data: { 'pageNumber': 1, 'orderData': ldfTableOrderArray[attrid], 'filterData' : fieldObject },
        type: 'GET',
        traditional: true,
        success : function (res) {
            if (res.header.status == false)
            {
               alert(res.header.message);
            } else {
                ldfTableCurPageAttribute[attrid] = 1;
                LDFTableRenderBody(res, attrid); 
            }
        }, 
        error : function (e, jqxhr, settings, exception) {
            LDFTableShowError(e, jqxhr, settings, exception);
        }
    });

}


function LDFTableAddItem(fields, attrid)
{
    var insertObject = JsonFields(fields);

    $.ajax({
        url: ldfTableAddCallbackArray[attrid],
        dataType: 'JSON',
        data: { 'pageNumber': ldfTableCurPageAttribute[attrid], 'orderData': ldfTableOrderArray[attrid], 'filterData' : ldfTableFilterDataArray[attrid], 'insertData' : insertObject },
        type: 'POST',
        traditional: true,
        success : function (res) {
            if (res.header.status == false)
            {
               alert(res.header.message);
            } else {
                LDFTableRenderBody(res, attrid); 
            }            
        }, 
        error : function (e, jqxhr, settings, exception) {
            LDFTableShowError(e, jqxhr, settings, exception);
        }
    });    
}

//==============================================
//
//End Main Methods
//
//==============================================
//



//==============================================
//
//Page Methods
//
//==============================================
//

function LDFTableRenderPages(pageCount, rowCount, attrid) {
    var i = 0;
    var activePage = "";
    var nHidePages = 0;
    ldfTableDivFooterArray[attrid].empty();

    for (i = 0; i <= pageCount; i++) {

        if ((i+1) == ldfTableCurPageAttribute[attrid])
            activePage = "active";
        else
            activePage = "";

        hidePage = "";

        if ((i+1)>5)
        {
            if (i <= pageCount) {
                hidePage = "hidden";
                nHidePages += 1;
            }
            
            if ((i+1) <= ldfTableCurPageAttribute[attrid]) {
                if (hidePage != "")
                    nHidePages -= 1;

                hidePage = "";
            }
        }

        itemPage = "<li class='" + activePage + " " + hidePage + "'><a href='#' data-page='" + (i + 1) + "' data-order='1'>" + (i + 1) + "</a></li>";
        ldfTableDivFooterArray[attrid].append(itemPage);

    }

    if (nHidePages > 0)
    {
        itemPage = "<li><a href='#' data-page='0'>...</a></li>";
        ldfTableDivFooterArray[attrid].append(itemPage);    
    }

    ldfTableDivFooterArray[attrid].parent().find(".row").remove();
    itemPage = "<div class='row' style='position: relative;width: 100%;margin-top: -10px;'><span class='rowCount'>Total: " + rowCount + " Registos</span></div>";
    ldfTableDivFooterArray[attrid].parent().append(itemPage);


}


function LDFTableChangePage(page, attrid) {
    var orderData = [];

    if (page == 0) {

        var objID   = $(ldfTableDivFooterArray[attrid])[0].id;
        var idx     = 0;
        $("#" + objID + " li.hidden").each(function() {
            if (idx <= 10) {
                $(this).removeClass("hidden");
            } else {
                return;
            }
            
            idx ++;
        });

        if ($("#" + objID + " li.hidden").length == 0)
        {
            $("#" + objID + " li").find("[data-page='0']").remove();
        }


    } else {
        var elem = ldfTableTHeaderArray[attrid].find("th.active");
        orderData[0] = elem.attr("data-field");
        var curitem = elem.attr("data-order");
        if (typeof(elem.attr("data-order")) != "undefined")
        {
            curitem  = curitem *-1;
        }
        orderData[1] = curitem;

        var filterData = ldfTableFilterDataArray[attrid];

        $.ajax({
            url: ldfTableLoadCallbackArray[attrid],
            dataType: 'json',
            data: { 'pageNumber': page, 'orderData': ldfTableOrderArray[attrid], 'filterData' : filterData },
            type: 'GET',
            traditional:true,
            success : function (res) {
                if (res.header.status == false)
                {
                   alert(res.header.message);

                } else {
                    ldfTableCurPageAttribute[attrid] = page;
                    LDFTableRenderBody(res, attrid);
                }                
            }, 
            error : function (e, jqxhr, settings, exception) {
                LDFTableShowError(e, jqxhr, settings, exception);
            }
        });
    }
}



$(".internalFooter").on("click", "li > a", function (e) {
    e.preventDefault();
});


//==============================================
//
//End Page Methods
//
//==============================================
//


//==============================================
//
//Auto Refresh
//
//==============================================
//

function LDFTableResetCounter(attrid)
{
    clearInterval(ldfTableRefreshTimeObject[attrid]);
}

function LDFTableRestartCounter(attrid)
{
    LDFTableResetCounter(attrid);
    var timeoutCount = ldfTableRefreshTime[attrid];
    ldfTableRefreshTimeObject[attrid] = setInterval(function () { 
        UpdateRefreshHeader(attrid, timeoutCount); 
        
        LDFAutoRefreshTable(attrid);
    }, timeoutCount);
}


function UpdateRefreshHeader(attrid, time)
{
    var dtTime = new Date();
    dtTime.setSeconds(dtTime.getSeconds() + (time/1000));
    var hr = (dtTime.getHours() <= 9 ? "0" : "") + dtTime.getHours();
    var mn = (dtTime.getMinutes() <= 9 ? "0" : "") + dtTime.getMinutes();
    var sc = (dtTime.getSeconds() <= 9 ? "0" : "") + dtTime.getSeconds();


    $("#autoRefresh" + attrid).children('span').html("Próxima atualização " + hr + ":" + mn + ":" + sc);
}

function LDFAutoRefreshTable(attrid)
{
    LDFTableRestartCounter(attrid);    

    $.ajax({
        url: ldfTableLoadCallbackArray[attrid],
        dataType: 'json',
        data: { 'pageNumber': ldfTableCurPageAttribute[attrid], 'orderData': ldfTableOrderArray[attrid], 'filterData' : ldfTableFilterDataArray[attrid] },            
        type: 'GET',
        traditional:true,
        success : function (res) {
            if (res.header.status == false)
            {
               alert(res.header.message);

            } else {
                LDFTableRenderBody(res, attrid); 
            }            
        }, 
        error : function (e, jqxhr, settings, exception) {
            LDFTableShowError(e, jqxhr, settings, exception);
        }
    });
}


//==============================================
//
//End Auto Refresh
//
//==============================================
//







//==============================================
//
//Edit and Remove methods
//
//==============================================
//


function InitRowInlineButtons(attrid)
{
    $(".ldfTableEditRow_" + attrid).on("click", function() {
        var curObject   = $(this);
        var parentTR    = $(this).closest("tr");

        if ($(parentTR).attr("data-editing") == "true")
        {
            SaveRow(attrid, curObject, parentTR);
        } else {
            EditRow(attrid, curObject, parentTR);
        }
    });

    $(".ldfTableRemoveRow_" + attrid).on("click", function() {
        var parentTR    = $(this).closest("tr");

        $.ajax({
            url: ldfTableRemoveCallBackArray[attrid],
            dataType: 'json',
            data: { 'rowID': parentTR.attr("data-rowid") },
            type: 'POST',
            traditional: true,
            success : function (res) {
                if (res == true)
                {
                    $.ajax({ 
                        url: ldfTableLoadCallbackArray[attrid], 
                        dataType: 'JSON', 
                        type: 'GET',
                        data: { 'pageNumber': ldfTableCurPageAttribute[attrid], 'orderData': ldfTableOrderArray[attrid], 'filterData' : ldfTableFilterDataArray[attrid]},                        
                        traditional:true,
                        success : function (res) { 
                            if (res.header.status == false)
                            {
                               alert(res.header.message);

                            } else {
                                LDFTableRenderBody(res, attrid); 
                            }                            
                        },
                        error: function(e, jqxhr, settings, exception) {
                         LDFTableShowError(e, jqxhr, settings, exception);
                        }
                    });
                } else {
                    alert("Infelizmente não foi possível alterar o registo pretendido");                    
                }
            }, 
            error : function (e, jqxhr, settings, exception) {
                LDFTableShowError(e, jqxhr, settings, exception);
            }                
        });    
    });    
}


function EditRow(attrid, curObject, parentTR)
{
    $(parentTR).attr("data-editing", "true");

    
    $(curObject).html("<li class='fa fa-save'></li> Gravar");

    //$(curObject).parent().append("<input type='button' class='ldfTableCancelEditRow_" + attrid + "' value='Cancelar'/>");
    $(curObject).parent().append("<button type='button' class='btn btn-warning ldfTableCancelEditRow_" + attrid + "' value='Cancelar'><li class='fa fa-retweet'></li> Cancelar</button>");
    InitRowInlineCancelButton(attrid);
    var rowID       = parentTR.attr("data-rowid");

    $.each(ldfTableHeaderArray[attrid], function (datakey, dataValue) {
    
        if (dataValue.headerEditable == true)
        {
            $.each($(parentTR).children("td"), function (tdkey, tdValue) {
                if ($(tdValue).attr("data-column") == dataValue.headerID )
                { 
                    $(tdValue).attr("data-curValue", $(tdValue).html());
                    //if (headerValue["headerType"] != null && headerValue["headerType"].indexOf("System.DateTime") != -1) {

                    $(this).html(RenderEditorField(dataValue, $(tdValue).html()));
                    $('.ldfeditable.minimal').iCheck({
                        checkboxClass: 'icheckbox_flat-green'
                    });
                }
            });

            //['data-column'='" + dataValue.headerID + "']")
        }
    });
}

function SaveRow(attrid, curObject, parentTR)
{
    var jsonObject      = new Object();
    var jsonObjectText  = new Object();

    $.each($(parentTR).children("td[data-editable='true']"), function (tdkey, tdValue) {
        $.each(ldfTableHeaderArray[attrid], function (datakey, dataValue) {
            if (dataValue.headerID == $(tdValue).attr("data-column"))
            {
                var changeData = GetEditorFieldValue(dataValue, tdValue);
                if (typeof(changeData.id) !== 'undefined')
                {
                    jsonObject[dataValue.headerID] = changeData.id;
                    jsonObjectText[dataValue.headerID] = changeData.optionValue;
                } else {
                    jsonObject[dataValue.headerID] = jsonObjectText[dataValue.headerID] = changeData;
                }

            }
        });
    });
    
    $.ajax({
        url: ldfTableEditCallBackArray[attrid],
        dataType: 'json',
        data: { 'rowID': parentTR.attr("data-rowid"), 'fields': JSON.stringify(jsonObject) },
        type: 'POST',
        traditional: true,
        success : function (res) {
            if (res.header.status == false)
            {
               alert(res.header.message);
            } else {
                parentTR.attr("data-editing", "false");
                $(parentTR).find("button.ldfTableCancelEditRow_" + attrid).remove();    

                $(curObject).html("<li class='fa fa-edit'></li> Editar");

                $.each($(parentTR).children("td[data-editable='true']"), function (tdkey, tdValue) {
                    $.each(ldfTableHeaderArray[attrid], function (datakey, dataValue) {
                        if (dataValue.headerID == $(tdValue).attr("data-column"))
                        {
                            $(tdValue).html(jsonObjectText[dataValue.headerID]);
                        }
                    });
                });   
            }            
        },       
        error : function (e, jqxhr, settings, exception) {
            LDFTableShowError(e, jqxhr, settings, exception);
        }                   
    });     
}

function InitRowInlineCancelButton(attrid)
{
    $(".ldfTableCancelEditRow_" + attrid).on("click", function() {
        var curObject   = $(this);
        var parentTR    = $(this).closest("tr");
        $.each($(parentTR).children("td[data-editable='true']"), function (tdkey, tdValue) {
            $(tdValue).html($(tdValue).attr("data-curValue"));
            $(tdValue).attr("data-curValue", "");
        });

        curObject.remove();
        var editObj = $(parentTR).find("button.ldfTableEditRow_" + attrid);

        $(editObj).html("<li class='fa fa-edit'></li> Editar");

        parentTR.attr("data-editing", "false");
    });        
}



function RenderEditorField(colObject, value)
{
    if (colObject.headerType.indexOf("String") != -1 || colObject.headerType.indexOf("Int") != -1) {
        return "<input type='text' class='form-control' value='" + value + "' />";
    } else if (colObject.headerType.indexOf("DateTime") != -1) {
        return "<input type='text' class='form-control' value='" + value + "' />";
    } else if (colObject.headerType.indexOf("SelectList") != -1 || colObject.headerType.indexOf("Boolean") != -1) {
        var item = "";

        for (var c in colObject.headerEditContent)
        {
            if (colObject.headerEditContent.hasOwnProperty(c)) 
            {
                var selected = "";
                if (value == colObject.headerEditContent[c])
                {
                    selected = "selected";
                }
                item += "<option value='" + c + "' " +  selected + ">" + colObject.headerEditContent[c] + "</option>";

            }
        }
        return "<select class='form-control'>" + item + "</select>";
    } 
}

function GetEditorFieldValue(colObject, editObject)
{
    if (colObject.headerType.indexOf("String") != -1 || colObject.headerType.indexOf("Int") != -1 ) {
        return $(editObject).children().val();
    } else if (colObject.headerType.indexOf("DateTime") != -1) {
        return $(editObject).children().val();
    } else if (colObject.headerType.indexOf("SelectList") != -1 || colObject.headerType.indexOf("Boolean") != -1) {
        var jsonArr = {
                        "id": $(editObject).children().val(),
                        "optionValue" : $("option:selected", $(editObject).children()).text()
                        };

        return jsonArr;
    }

}


//==============================================
//
//End Edit and Remove methods
//
//==============================================
//





//==============================================
//
//Aux Methods
//
//==============================================
//

function htmlEscape(str) {
    return String(str)
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            .replace(/'/g, '&#39;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');
}


function ToJavaScriptDate(value) {
    var pattern;
    var results;
    var dt;
    var res;
    try {
        pattern = /Date\(([^)]+)\)/;
        results = pattern.exec(value);
        dt = new Date(parseFloat(results[1]));
        res = (dt.getMonth() + 1) + "/" + dt.getDate() + "/" + dt.getFullYear();
    }
    catch(err) {
        console.log("ldfTable | ToJavaScriptDate" + err.message);
        res = "";
    }
    return res;
}

function LDFTableShowError(e, jqxhr, settings, exception)
{
    alert('De momento não é possível satisfazer o seu pedido');
    
}

function JsonFields(fields)
{

    var fieldArray  = fields.split("&");
    var fieldObject = "";

    $.each(fieldArray, function(idx, elem) {
        var arr = fieldArray[idx].split("=");
        
        if (fieldObject != "")
            fieldObject += ",";
        fieldObject += "{'fieldName':'" + arr[0] + "','fieldValue':'" + arr[1] + "'}";
    });
    if (fieldObject != "")
    {
        fieldObject = "[" + fieldObject + "]";
    }

    return fieldObject;
}


function LoadLDFTable(attrid)
{
    $.ajax({ 
        url: ldfTableLoadCallbackArray[attrid], 
        dataType: 'JSON', 
        type: 'GET',
        success : function (res) { 
            if (res.header.status == false)
            {
               alert(res.header.message);

            } else {
                LDFTableRenderBody(res, attrid); 
            }
        },
        error: function(e, jqxhr, settings, exception) {
            LDFTableShowError(e, jqxhr, settings, exception);
        }
    });
} 

//==============================================
//
//End Aux Methods
//
//==============================================
//