var keyValue = $.request("keyValue");
$(function () {
    initControl();
    $.ajax({
        url: "/SystemDocument/User/GetFormJson",
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            $("#form1").find('.form-control,select,input').attr('readonly', 'readonly');
            $("#form1").find('div.ckbox label').attr('for', '');
            $("#FUserPassword").val("******");
        }
    });
});
function initControl() {
    $("#FGender").bindSelect()
    $("#FIsAdministrator").bindSelect()
    $("#FEnabledMark").bindSelect()
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
    $("#FLastModifyUserId").bindSelect({
        url: "/SystemDocument/User/GetGridJsonByOrg",
        id: "FId",
        text: "FRealName"
    });
}