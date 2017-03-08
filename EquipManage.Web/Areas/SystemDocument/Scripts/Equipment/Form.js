var keyValue = $.request("keyValue");
var itemId = $.request("itemId");
$(function () {
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/Equipment/GetFormJson",
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
            }
        });
    }
});
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    var postData = $("#form1").formSerialize();
    postData["FItemId"] = itemId;
    $.submitForm({
        url: "/SystemDocument/Equipment/SubmitForm?keyValue=" + keyValue,
        param: postData,
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}