var keyValue = $.request("keyValue");//经验ID
var itemId = $.request("itemId");    //设备ID
var FEquipTypeId = $.request("FEquipTypeId");    //设备类型ID
$(function () {
    initControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/ExpWare/GetFormJson",
            data: { keyValue: keyValue },
            dataType: "json",
            async: false,
            success: function (data) {
                $("#form1").formSerialize(data);
                $("#FOperationLevelId").bindSelect({
                    url: "/SystemDocument/ItemsData/GetGridJson",
                    id: "FId",
                    text: "FItemName",
                    param: { itemId: data.FOperationLevelId, keyword: "" }
                });
            }
        });
    } else {
        $("#FItemId").val(itemId);
        $("#FEquipTypeId").val(FEquipTypeId);
        $("#FId").val(keyValue);
    }
});

function initControl() {

    var FParentNo = "OperationType";

    $("#FOperationTypeId").bindSelect({
        url: "/SystemDocument/ItemsType/GetGridSelectJson",
        id: "FId",
        text: "FFullName",
        param: { itemId: FParentNo, keyword: "" }
    });
    $("#FOrganizeId").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson"
    });
    $("#FMalfunctionType").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "MalfunctionType" }
    });

    $("#FMalfunctionReasonId").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "MalfunctionReasonId" }
    });
    $("#FOperationTypeId").on("change", function () {
        var ItemId = $(this).val();
        if (!!ItemId) {
            $("#FOperationLevelId").bindSelect({
                url: "/SystemDocument/ItemsData/GetGridJson",
                id: "FId",
                text: "FItemName",
                param: { itemId: ItemId, keyword: "" }
            });
        }
    });
}

function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    var postData = $("#form1").formSerialize();
    //postData["itemId"] = itemId;
    $.submitForm({
        url: "/SystemDocument/ExpWare/SubmitForm?keyValue=" + keyValue,
        param: postData,
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}