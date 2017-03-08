var keyValue = $.request("keyValue");
$(function () {
    initControl();
    if (!!keyValue) {
        $.ajax({
            url: "/SystemDocument/EquipmentType/GetFormJson",
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
    $("#FParentId").bindSelect({
        url: "/SystemDocument/EquipmentType/GetTreeSelectJson"
    });
}
function submitForm() {
    if (!$('#form1').formValid()) {
        return false;
    }
    $.submitForm({
        url: "/SystemDocument/EquipmentType/SubmitForm?keyValue=" + keyValue,
        param: $("#form1").formSerialize(),
        success: function () {
            top.EquipmentType.$("#gridList").resetSelection();
            top.EquipmentType.$("#gridList").trigger("reloadGrid");
        }
    })
}