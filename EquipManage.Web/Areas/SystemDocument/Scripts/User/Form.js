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
                var FUserRoleIdlist = new Array();
                FUserRoleIdlist = data.FRoleId.split(',');
                $('#FRoleIdList').select2().val(FUserRoleIdlist).trigger("change");

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
    $("#FRoleIdList").bindSelect({
        url: "/SystemDocument/Role/GetGridJson",
        id: "FId",
        text: "FFullName"
    });
    $("#FDutyId").bindSelect({
        url: "/SystemDocument/Duty/GetGridJson",
        id: "FId",
        text: "FFullName"
    });
    $('#FRoleIdList').select2({
        tags: true,
        maximumSelectionLength: 2,
        tokenSeparators: [',', ' ']
    }).on("select2-selecting", function (e) { log("selecting val=" + e.val + " choice=" + JSON.stringify(e.choice)); })  // 选中事件;

}
function submitForm() {
    var FUserRoleId = new Array();
    $($('#FRoleIdList').select2("data")).each(function (index, element) {
        FUserRoleId.push(element.id);
    });
    var FRoleId = FUserRoleId.join(',');
    $('#FRoleId').val(FRoleId);
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