var keyValue = $.request("keyValue");
$(function () {
    initContent();

});
function initContent() {
    $.ajax({
        type: "get",
        url: "/SystemDocument/ChangeContent/GetGridJson",
        contentType: "application/json",
        dataType: "json", //表示返回值类型，不必须
        async:false,
        success: function (resultData) {
            if (resultData) {
                resultData.forEach(function (value, index, array) {
                    $(".mt-checkbox-list").append('<label class="mt-checkbox mt-checkbox-outline">' + value.FFullName + '<input type="checkbox" value="0" name="ChangeType' + value.FId + '"  id="ChangeType' + value.FId + '" /><span></span></label>');
                });
            }
        }
    });
    if (!!keyValue) {
        initForm(keyValue)
    }
}

function initForm(keyValue) {
    $.ajax({
        url: "/SystemDocument/ChangeType/GetFormJson",
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            var ChangeTypeArray = new Array();
            ChangeTypeArray = data.FContent.split(',');
            ChangeTypeArray.forEach(function (val, index, array) {
                $("#ChangeType" + val).prop("checked", true);
            });
        }
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemDocument/ChangeType/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}