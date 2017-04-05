var keyValue = $.request("keyValue");
$(function () {
    initControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/Role/GetFormJson",
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
            }
        });
    }
})
function initControl() {
    $("#FOrganizeId").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson",
    });
    $("#FType").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "RoleType" }
    });
    $('#wizard').wizard().on('change', function (e, data) {
        var $finish = $("#btn_finish");
        var $next = $("#btn_next");
        if (data.direction == "next") {
            switch (data.step) {
                case 1:
                    if (!$('#form1').formValid()) {
                        return false;
                    }
                    $finish.show();
                    $next.hide();
                    break;
                default:
                    break;
            }
        } else {
            $finish.hide();
            $next.show();
        }
    });
    $("#permissionTree").treeview({
        height: 444,
        showcheck: true,
        url: "/SystemDocument/RoleAuthorize/GetPermissionTree",
        param: { roleId: keyValue }
    });
}
function submitForm() {
    var postData = $("#form1").formSerialize();
    postData["permissionIds"] = String($("#permissionTree").getCheckedNodes());
    $.submitForm({
        url: "/SystemDocument/Role/SubmitForm?keyValue=" + keyValue,
        param: postData,
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    });
}