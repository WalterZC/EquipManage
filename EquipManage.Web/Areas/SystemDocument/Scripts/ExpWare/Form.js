var keyValue = $.request("keyValue");
var itemId = $.request("itemId");
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
            }
        });
    }
});

function initControl() {
    //$("#FOperationTypeID").bindSelect({
    //    url: "/SystemDocument/Organize/GetTreeSelectJson"
    //});
    //$("#FMalfunctionType").bindSelect({
    //    url: "/SystemDocument/Organize/GetTreeSelectJson"
    //});
    //$("#FMalfunctionReasonId").bindSelect({
    //    url: "/SystemDocument/Organize/GetTreeSelectJson"
    //});

    var FParentNo = "OperationType";

    $("#FOperationTypeId").bindSelect({
        url: "/SystemDocument/ItemsType/GetGridSelectJson",
        id: "FId",
        text: "FFullName",
        param: { itemId: FParentNo, keyword: "" }
    });

    $("#FMalfunctionType").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "FMalfunctionType" }
    });

    $("#FMalfunctionReasonId").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "FMalfunctionReasonId" }
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
    postData["FEquipmentTypeId"] = itemId;
    $.submitForm({
        url: "/SystemDocument/Equipment/SubmitForm?keyValue=" + keyValue,
        param: postData,
        success: function () {
            $.currentWindow().$("#gridList").trigger("reloadGrid");
        }
    })
}