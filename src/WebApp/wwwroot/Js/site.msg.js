//Modal
function getModalAlertHtml(id, title, msg) {
    if (msg && (msg.trim() == "")) {
        msg = "&nbsp;";
    }
    var modalHtml = ""
     + "<div class=\"modal fade\" id=\"" + id + "\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"exampleModalCenterTitle\" aria-hidden=\"true\">"
       + "<div class=\"modal-dialog modal-dialog-centered\" role=\"document\">"
         + "<div class=\"modal-content\">"
          + " <div class=\"modal-header bg-light\">"
           + "  <h5 class=\"modal-title\" id=\"" + id + "_title\">" + title + "</h5>"
            + " <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"Close\">"
            + " </button>"
           + "</div>"
          + " <div class=\"modal-body\" id=\"" + id + "_msg\">"
          + msg
          + " </div>"
          + " <div class=\"modal-footer\">"
          + "   <button type=\"button\" class=\"btn btn-primary\" data-bs-dismiss=\"modal\">关闭</button>"
           + "</div>"
         + "</div>"
      + " </div>"
     + "</div>";
    return modalHtml;
}

function getModalConfirmHtml(id, title, msg) {
    if (msg && (msg.trim() == "")) {
        msg = "&nbsp;";
    }
    var modalHtml = ""
     + "<div class=\"modal fade\" id=\"" + id + "\" tabindex=\"-1\" role=\"dialog\" aria-labelledby=\"exampleModalCenterTitle\" aria-hidden=\"true\">"
       + "<div class=\"modal-dialog modal-dialog-centered\" role=\"document\">"
         + "<div class=\"modal-content\">"
          + " <div class=\"modal-header bg-light\">"
           + "  <h5 class=\"modal-title\" id=\"" + id + "_title\">" + title + "</h5>"
            + " <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"modal\" aria-label=\"Close\">"
            + " </button>"
           + "</div>"
          + " <div class=\"modal-body\" id=\"" + id + "_msg\">"
          + msg
          + " </div>"
          + " <div class=\"modal-footer\">"
            + " <button id=\"" + id + "_btnConfirm\" type=\"button\" class=\"btn btn-primary\">确认</button>"
          + "   <button type=\"button\" class=\"btn btn-secondary\" data-bs-dismiss=\"modal\">取消</button>"
           + "</div>"
         + "</div>"
      + " </div>"
     + "</div>";
    return modalHtml;
}
var g_modal_alertMsg_id = "g_modal_alertMsg_id_888";
function alertMessage(msg) {
    if (msg) {
        msg = msg.replace("\"", "&quote;");
    }
    var modalId = g_modal_alertMsg_id;
    var msgObj = $("#" + modalId + "_msg");
    if (msgObj.length > 0) {
        msgObj.text(msg);
        $('#' + modalId).modal('show');
        return;
    }
    else {
        var modalHtml = getModalAlertHtml(modalId, "消息", msg);
        //var divModal = $("<div></div>");
        //divModal.attr("style", "width:50%;");
        //divModal.html(modalHtml);
        //$("body").append(divModal);
        $("body").append(modalHtml);
        $('#' + modalId).modal('show');
    }
}
//confirm message
var g_modal_confirmMsg_id = "g_modal_confirmMsg_id";
function confirmMessage(msg, callBackFunc, diableConfirmButtonWhenSubmit) {
    if (msg) {
        msg = msg.replace("\"", "&quote;");
    }
    var modalId = g_modal_confirmMsg_id;
    var msgObj = $("#" + modalId + "_msg");
    if (msgObj.length > 0) {
        msgObj.text(msg);
    }
    else {
        var modalHtml = getModalConfirmHtml(modalId, "消息确认", msg);
        //var divModal = $("<div></div>");
        //divModal.attr("style", "width:100%;");
        //divModal.html(modalHtml);
        //$("body").append(divModal);
        $("body").append(modalHtml);
        $('#' + modalId).modal('show');
    }
    $('#' + modalId + "_btnConfirm").prop("disabled", false);
    $('#' + modalId + "_btnConfirm").on("click", function () {
        if (callBackFunc != null && callBackFunc != undefined) {
            if (diableConfirmButtonWhenSubmit == true) {
                $('#' + modalId + "_btnConfirm").prop("disabled", true);
            }
            callBackFunc();
        }
        $('#' + modalId).modal('hide');
    });
    $('#' + modalId).modal('show');
}