var keyValue = $.request("keyValue");
var itemId = $.request("itemId");

$(function () {
    $("#FMemberID").bindSelect({
        url: "/SystemDocument/User/GetGridJsonByOrg",
        id: "FId",
        text: "FRealName"
    });
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/OperationClassMember/GetFormJson",
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
            }
        });
    }

    $("#FOperationClassID").val(itemId);
});

function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    var postData = $("#form1").formSerialize();
    postData["FItemId"] = itemId;
    $.submitForm({
        url: "/SystemDocument/OperationClassMember/SubmitForm?keyValue=" + keyValue,
        param: postData,
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}



