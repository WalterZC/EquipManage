﻿var keyValue = $.request("keyValue");
var itemId = $.request("itemId");
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
    $.submitForm({
        url: "/SystemDocument/OperationItem/SubmitPartForm?keyValue=" + keyValue,
        param: postData,
        sync: false,
        success: function () {
            top["Parts"].$("#gridList").trigger("reloadGrid");
            top["Parts"].$(".operate").animate({ "left": '-100.1%' }, 200);
        }
    });
}