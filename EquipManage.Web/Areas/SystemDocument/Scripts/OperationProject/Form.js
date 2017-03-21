var keyValue = $.request("keyValue");//设备方案ID
var itemId = $.request("itemId");//设备类型ID
$(function () {
    initControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/OperationProject/GetFormJson",
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
            }
        });
    } else {
        $("#FEquipmentTypeId").val(itemId);
    }
})
function initControl() {
    var FParentNo = "OperationType";
    
    $("#FOperationTypeId").bindSelect({
        url: "/SystemDocument/ItemsType/GetGridSelectJson",
        id: "FId",
        text: "FFullName",
        param: { itemId: FParentNo, keyword: "" }
    });
    $("#FOperationTypeId").on("change", function () {
        var ItemId = $(this).val();
        if (!!ItemId) {
            $("#FOperationLevelId").bindSelect({
                url: "/SystemDocument/ItemsData/GetGridJson",
                id: "FId",
                text: "FItemName",
                param: { itemId: ItemId, keyword:""  }
            });
        }
    });

    $('#wizard').wizard().on('change', function (e, data) {
        var $finish = $("#btn_finish");
        var $next = $("#btn_next");
        if (data.direction == "next") {
            switch (data.step) {
                case 1:
                    if (!$('#step-1').formValid()) {
                        return false;
                    }
                    submitFirstStepForm();
                    break;
                case 2:
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
    //$("#permissionTree").treeview({
    //    height: 444,
    //    showcheck: true,
    //    url: "/SystemDocument/RoleAuthorize/GetPermissionTree",
    //    param: { roleId: keyValue }
    //});
}

function submitFirstStepForm() {
    var postData = $("#step-1").formSerialize();
    var ItemId = $("#FID").val();
    if (!!itemId) {
            $.ajax({
                url: "/SystemDocument/OperationProject/SubmitForm?keyValue=" + keyValue,
                data: postData,
                dataType: "json",
                success: function (data) {
                    //$("#form1").formSerialize(data);
                    $("#FID").val(data.FID);
                }
            });
        }
}

//function submitForm() {
//    var postData = $("#form1").formSerialize();
//    postData["permissionIds"] = String($("#permissionTree").getCheckedNodes());
//    $.submitForm({
//        url: "/SystemDocument/Role/SubmitForm?keyValue=" + keyValue,
//        param: postData,
//        success: function () {
//            $.currentWindow().$("#gridList").trigger("reloadGrid");
//        }
//    });
//}