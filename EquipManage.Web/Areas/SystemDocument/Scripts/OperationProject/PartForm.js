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

    var TypeVal = $("#FValType").val();
    var ItemVal = $("#FCheckItems").val();
    var MaxVal = $("#FMaxVal").val();
    var MinVal = $("#FMinVal").val();

    if (isNaN(MaxVal)) {
        $.modalAlert("最大值必须为数字！", "warning");
        return;
    }
    if (isNaN(MinVal)) {
        $.modalAlert("最小值必须为数字！", "warning");
        return;
    }
    if (parseFloat(MinVal) > parseFloat(MaxVal)) {
        $.modalAlert("最小值不允许大于最大值！", "warning");
        return;
    }

    if (TypeVal == "FCheck") {
        if (ItemVal.length == 0) {
            $.modalAlert("请填写一个选项！", "warning");
            return;
        }
        if (ItemVal.indexOf("/") >= 0) {
            $.modalAlert("单选选项格式不正确！", "warning");
            return;
        }
    } else if (TypeVal == "FDecimal") {
        if (isNaN(ItemVal)) {
            $.modalAlert("选项必须为数字！", "warning");
            return;
        }
    } else if (TypeVal == "FString") {

    } else if (TypeVal == "FMultiCheck") {
        var EmptyItem = 0;
        if (ItemVal.length == 0) {
            $.modalAlert("请填写一组选项！", "warning");
            return;
        }
        if (ItemVal.indexOf("/") < 0) {
            $.modalAlert("多选选项格式不正确！", "warning");
            return;
        }
        var items = new Array();
        items = ItemVal.split('/');
        items.forEach(function (value, index, array) {
            if (value == '') {
                $.modalAlert("多选时，选项不为空！", "warning");
                EmptyItem++;
            }
        });
        if (EmptyItem > 0) {
            return;
        }
    } else {

    }

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