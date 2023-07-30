function formatCurrency(amount) {
    var neg = false;
    if(amount < 0) { neg = true; amount = Math.abs(amount);  }
    return (neg ? "-" : '') + parseFloat(amount, 10).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString();
}
function formatCoinCount(number) {
    var neg = false;
    if (number < 0) { neg = true; number = Math.abs(number); }
    return (neg ? "-" : '') + parseFloat(number, 10).toFixed(0).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString();
}
function formatAmount(amount) { return formatCurrency(amount); }
function formatNumeric(number) { return formatCurrency(number); }
function formatTime(time, timeFormat){
	if(timeFormat == "H:mm:ss"){ 
		var hour = time.getHours(); var min = time.getMinutes(); var sec=time.getSeconds();
		var strHour = hour; var strMin = ""; var strSec = "";
		if (min < 10) strMin = "0" + min; else strMin = "" + min;
		if (sec < 10) strSec = "0" + sec; else strSec = "" + sec;
		return strHour + ":" + strMin + ":" + strSec;
	}
	else if (timeFormat == "yyyy-MM-dd HH:mm:ss") {
	    var year = time.getFullYear(); var month = time.getMonth() + 1; var day = time.getDate();
	    var hour = time.getHours(); var min = time.getMinutes(); var sec = time.getSeconds();
	    var strYear = "" + year; var strMonth = ""; var strDay = "";
	    var strHour = "" + hour; var strMin = ""; var strSec = "";
	    if (month < 10) strMonth = "0" + month; else strMonth = "" + month;
	    if (day < 10) strDay = "0" + day; else strDay = "" + day;
	    if (hour < 10) strHour = "0" + hour; else strHour = "" + hour;
	    if (min < 10) strMin = "0" + min; else strMin = "" + min;
	    if (sec < 10) strSec = "0" + sec; else strSec = "" + sec;
	    return strYear + "-" + strMonth + "-" + strDay + " " + strHour + ":" + strMin + ":" + strSec;
	}
	return time;
}
function ConvertJsonDateToJSDate(obj) {
    return new Date(parseInt(obj.substr(6)));
}
function formatJsonTime(time, timeFormat) {
    var time = ConvertJsonDateToJSDate(time);
    return formatTime(time, timeFormat);
}
function formatJsonTimeSpan(timeSpan, timeFormat){
    if (timeFormat == "H:mm:ss" || timeFormat == "D:H:mm:ss") {
        var days = timeSpan.Days;
        var hour = timeSpan.Hours; var min = timeSpan.Minutes; var sec = timeSpan.Seconds;
        var strDays = ""; var strHour = hour; var strMin = ""; var strSec = "";
        if (days > 0) strDays = "" + days + "天";
		if(min < 10) strMin = "0" + min; else strMin = "" + min;
		if(sec < 10) strSec = "0" + sec; else strSec = "" + sec;
		return strDays + strHour + ":" + strMin + ":" + strSec;
	}
	return timeSpan;
}
function formatTotalSecconds(totalSeconds, timeFormat) {
    if (timeFormat == "H:mm:ss" || timeFormat == "D:H:mm:ss") {
        var days = Math.floor(totalSeconds / 86400);
        var hours = Math.floor((totalSeconds % 86400) / 3600);
        var min = Math.floor((totalSeconds % 3600) / 60); 
        var sec = totalSeconds % 60;
        var strDays = ""; var strHour = hours; var strMin = ""; var strSec = "";
        if (days > 0) strDays = "" + days + "天";
		if(min < 10) strMin = "0" + min; else strMin = "" + min;
		if(sec < 10) strSec = "0" + sec; else strSec = "" + sec;
		return strDays + strHour + ":" + strMin + ":" + strSec;
	}
    return totalSeconds;
}
function checkIsInt(strVal) {
    var regExprStr = "^(0|([1-9](\\d+)?))$";
    var regExpr = new RegExp(regExprStr);
    if (regExpr.test(strVal)) {
        try {
            var val = parseInt(strVal);
            return true;
        }
        catch (ex) {
            return false;
        }
    }
    return false;
}
function checkIsDecimal(strVal, intLength, precisionLength) {
    var regExprStr = "";
    if (intLength !== null && intLength !== undefined
        && precisionLength !== null && precisionLength !== undefined) {
        regExprStr = "^(0|([1-9]\\d{0," + (intLength - 1) + "}))(\\.\\d{1," + precisionLength + "})?$";
    }
    else {
        regExprStr = "^(0|([1-9]\\d{0,14}))(\\.\\d{1,2})?$";
    }
    var regExpr = new RegExp(regExprStr);
    return regExpr.test(strVal);
}
function addFavorite() {
    try {
        var url = window.location;
        var title = document.title;
        var ua = navigator.userAgent.toLowerCase();
        if (window.sidebar) { // For Mozilla Firefox Bookmark
            window.sidebar.addPanel(title, url, "");
        }
        else if (document.all || window.external) { // For IE Favorite
            window.external.addFavorite(url, title);
        }
        else { // for other browsers which does not support
            alertMessage("浏览器不支持,请按 Ctrl+D 手动收藏!");
        }
    }
    catch (ex) {
        alertMessage("浏览器不支持,请按 Ctrl+D 手动收藏!");
    }
}

function formatDatePicker(id) {
    $("#" + id).datepicker();
}
function FormatDatePicker(id) {
    formatDatePicker(id);
}
function activeTab(tab) {
    $('.nav-tabs button[data-bs-toggle="#' + tab + '"]').click();
}
function replaceAll(str, a, b) {
    return str.replace(new RegExp('\\' + a, 'g'), b);
}
String.prototype.replaceAll = function (a, b) {
    return replaceAll(this, a, b);
}
function resetConditionCheckErrorMsg() {
    $('.conditionCheckErrorMsg').each(function () {
        $(this).html("");
        $(this).css("display", "none");
    });
}
function checkErrorMessageRequired(checkObjId, displayErrorMsgId, displayErrorMessage) {
    var checkObj = $("#" + checkObjId);
    if (checkObj.length > 0 && checkObj.val() == "") {
        var errorMsgObj = $("#" + displayErrorMsgId);
        errorMsgObj.css("display", "block");
        errorMsgObj.html(displayErrorMessage);
        return false;
    }
    return true;
}
function checkRangeErrorMessage(checkObjId, displayErrorMsgId, displayErrorMessage, minValue, maxValue) {
    var checkObj = $("#" + checkObjId);
    if (checkObj.length > 0 && checkObj.val() != "") {
        var isValid = true;
        var val = checkObj.val();
        if (parseFloat(val) < minValue || parseFloat(val) > maxValue) {
            isValid = false;
        }
        if (!isValid) {
            var errorMsgObj = $("#" + displayErrorMsgId);
            errorMsgObj.css("display", "block");
            errorMsgObj.html(displayErrorMessage);
            return false;
        }
    }
    return true;
}
function checkErrorMessageRegularExpression(checkObjId, regExpString, displayErrorMsgId, displayErrorMessage) {
    var checkObj = $("#" + checkObjId);
    if (checkObj.length > 0) {
        var val = checkObj.val();
        if (val != "") {
            var regExp = new RegExp(regExpString);
            if (!regExp.test(val)) {
                var errorMsgObj = $("#" + displayErrorMsgId);
                errorMsgObj.css("display", "block");
                errorMsgObj.html(displayErrorMessage);
                return false;
            }
        }
    }
    return true;
}
function showPict(pictObj) {
    var id = "ShowPict_" + (new Date()).getTime();
    var currentUIWidth = $(window).width();
    var left = ($(window).width() - 400) / 2; 
    var top = ($(window).height() - 400) / 2;//+ $(document).scrollTop();
    var htmlContent = "<div name='ShowPict_Content' style='width: 400px; margin-left:" + left + "px; margin-top:" + top + "px; text-align:left; background-color:white; padding: 10px; filter:alpha(Opacity=1);-moz-opacity:1;opacity: 1;'><div style='margin:0px; padding: 5px; text-align:right;'><a style='color:blue;' href='Javascript:;' onclick=\"closeShowPict('" + id + "');\">关闭</a></div>"
        + "<div class='height5'></div>"
        + "<div style='text-align:center; margin: 0px;'><img src='" + $(pictObj).attr("src") + "' style='width: 300px; height: 300px;' /></div>"
        + "<div class='height20'></div>"
        + "</div>";
    var divShowPict = document.createElement("div")
    $(divShowPict).attr("class", "showPict");
    $(divShowPict).attr("id", id);
    $(divShowPict).html(htmlContent);

    $("body").append(divShowPict);
    return false;
}
function closeShowPict(id) {
    $("#" + id).html("");
    $("#" + id).css("display", "none");
    return false;
}
