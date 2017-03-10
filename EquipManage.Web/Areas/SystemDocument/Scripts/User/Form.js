var keyValue = $.request("keyValue");
$(function () {
    initControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/User/GetFormJson",
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
                $("#FUserPassword").val("******").attr('disabled', 'disabled');
            }
        });
    }
});
function initControl() {
    $("#FGender").bindSelect();
    $("#FIsAdministrator").bindSelect();
    $("#FEnabledMark").bindSelect();
    $("#FSource").bindSelect();
    $("#FOrganizeId").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson"
    });
    $("#FDepartmentId").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson",
    });
    $("#FRoleId").bindSelect({
        url: "/SystemDocument/Role/GetGridJson",
        id: "FId",
        text: "FFullName"
    });
    $("#FDutyId").bindSelect({
        url: "/SystemDocument/Duty/GetGridJson",
        id: "FId",
        text: "FFullName"
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemDocument/User/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}