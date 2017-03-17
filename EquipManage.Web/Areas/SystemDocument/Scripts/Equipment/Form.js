var keyValue = $.request("keyValue");
var itemId = $.request("itemId");
$(function () {
    initControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/Equipment/GetFormJson",
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
    $("#FBelongOrgID").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson"
    });
    $("#FUseOrgID").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson"
    });
    $("#FManuOrgID").bindSelect({
        url: "/SystemDocument/Organize/GetTreeSelectJson"
    });
    $("#FUnit").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "Unit" }
    });
    $("#FCategory").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "Category" }
    });
    $("#FStatus").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "EquipmentStatus" }
    });
    $("#FABC").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "ABCType" }
    });
    $("#FAccuracy").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "AccuracyStatus" }
    });
    $("#FUseOrgID").on("change", function () {
        var UseOrgID = $(this).val();
        if (!!UseOrgID) {
            $("#FPositionID").bindSelect({
                url: "/SystemDocument/Position/GetSelectJson",
                id: "FId",
                text: "FShortName",
                param: { keyValue: UseOrgID }
            });
            $("#FOperatorID").bindSelect({
                url: "/SystemDocument/User/GetGridJsonByOrg",
                id: "FId",
                text: "FRealName",
                param: { keyword: UseOrgID }
            });
        }
    });
    $("#FManuOrgID").on("change", function () {
        var ManuOrgID = $(this).val();
        if (!!ManuOrgID) {
            $("#FManuClassID").bindSelect({
                url: "/SystemDocument/OperationClass/GetSelectJson",
                id: "FId",
                text: "FShortName",
                param: { keyValue: ManuOrgID }
            });
        }
    });
    $("#FManuClassID").on("change", function () {
        var ManuClassID = $(this).val();
        if (!!ManuClassID) {
            $("#FManuPrincipalID").bindSelect({
                url: "/SystemDocument/OperationClassMember/GetSelectJson",
                id: "FId",
                text: "FRealName",
                param: { keyValue: ManuClassID }
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