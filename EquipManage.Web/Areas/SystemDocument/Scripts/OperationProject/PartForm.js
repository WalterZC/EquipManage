var keyValue = $.request("keyValue");
var itemId = $.request("itemId");
var FItemType = $.request("FItemType");
$(function () {
    initControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/OperationItem/GetPartFormJson",
            data: { keyValue: keyValue },
            dataType: "json",
            success: function (data) {
                $("#form1").formSerialize(data);
                $("#imgcode").attr("src", "/Files/PartsImg/" + data.FFileName + ".jpg");
            }
        });
    }
});

function initControl() {
    $("#FSystemId").bindSelect({
        url: "/SystemDocument/ItemsData/GetSelectJson",
        param: { enCode: "EquipmentSystem" }
    });

    $("#FItemId").val(itemId);
}

function submitForm() {

    var postData = $("#form1").formSerialize();
    postData["FImage"] = $('.fileinput-preview img').attr('src');
    postData["keyValue"] = keyValue;
    postData["FItemType"] = FItemType;
    $.submitForm({
        url: "/SystemDocument/OperationItem/SubmitPartForm?keyValue=" + keyValue,
        param: postData,
        sync: false,
        success: function () {
            if (typeof (top.frames["ProjectForm"]) != "undefined") {
                top.frames["ProjectForm"].$('#PartItemTable').DataTable().ajax.reload();
                //top["Parts"].$("#gridList").trigger("reloadGrid");
                top["ProjectForm"].$(".operate").animate({ "left": '-100.1%' }, 200);
            }
            if (typeof(top.frames["Parts"]) != "undefined") {
                //top.frames["ProjectForm"].$('#PartItemTable').DataTable().ajax.reload();
                top["Parts"].$("#gridList").trigger("reloadGrid");
                top["Parts"].$(".operate").animate({ "left": '-100.1%' }, 200);
            }
        }
    });
}