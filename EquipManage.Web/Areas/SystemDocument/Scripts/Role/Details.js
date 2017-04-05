var keyValue = $.request("keyValue");
$(function () {
    initControl();
    $.ajax({
        url: "/SystemDocument/Role/GetFormJson",
        data: { keyValue: keyValue },
        dataType: "json",
        async: false,
        success: function (data) {
            $("#form1").formSerialize(data);
            $("#form1").find('.form-control,select,input').attr('readonly', 'readonly');
            $("#form1").find('div.ckbox label').attr('for', '');
        }
    });
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
        var $next = $("#btn_next");
        if (data.direction == "next") {
            switch (data.step) {
                case 1:
                    $next.attr('disabled', 'disabled');
                    break;
                default:
                    break;
            }
        } else {
            $next.removeAttr('disabled');
        }
    });
    $("#permissionTree").treeview({
        height: 493,
        showcheck: true,
        url: "/SystemDocument/RoleAuthorize/GetPermissionTree",
        param: { roleId: keyValue }
    });
}